
#include "TukanBusApi.h"
#include "TukanBus_Protocol.c"
#include "TukanBus_Common.h"

typedef enum _TukanBusApiStatus
{
    TukanBusApiStatus_Offline = 0,
    TukanBusApiStatus_Online,
    TukanBusApiStatus_InEvaluate,
    TukanBusApiStatusNo
} TukanBusApiStatus;

static NuSocket_t   *_Socket = NULL;

struct _TukanBusApi_t
{
    int                 Status;
    NuSocketNode_t      *SocketNode;
    char                ID[TukanBusProtocolLen_ID];
    int                 IDLen;          
    TukanBusApiCB_t     CB;
};

static void _TukanBusDefaultCB(char *ID, size_t IDLen, char *Msg, size_t MsgLen, void *Argu)
{
    return;
}

static void _TukanBusDefaultLogCB(void *Argu, char *Msg, ...)
{
    return;
}

static void _TukanBusApiDisconnect(TukanBusApi_t *pApi, TukanBusHdr_t *pHdr, char *FarewellMsg, int Reconnect)
{
    NuSocketNode_t  *Node = pApi->SocketNode;
    size_t          SendLen = TukanBusMsg(pHdr, TukanBusProtocolMsgType_LogOut, pApi->ID, pApi->IDLen, FarewellMsg, strlen(FarewellMsg));

    if(pApi->Status == TukanBusApiStatus_Offline)
    {
        return;
    }

    pApi->CB.OnLog(pApi->CB.CBArgu, "(%s:%d) < LogOut[%.*s]\n", NuSocketGetAddr(Node), NuSocketGetPort(Node), SendLen, pHdr);
    NuSocketSend(Node, pHdr, SendLen);

    if(Reconnect)
    {
        NuSocketReconnect(Node);
    }
    else
    {
        NuSocketDisconnect(Node);
    }

    return;
}

static void _TukanBusApiSend(TukanBusHdr_t *pHdr, char MsgType, char *ID, int IDLen, char *Msg, int MsgLen, char *LogMsgHdr, TukanBusApi_t *pApi)
{
    int             TryCnt = TukanBusSendRetryTime;
    size_t          SendLen = TukanBusMsg(pHdr, MsgType, ID, IDLen, Msg, MsgLen);
    NuSocketNode_t  *Node = pApi->SocketNode;

    pApi->CB.OnLog(pApi->CB.CBArgu, "(%s:%d) < %s[%.*s]\n", NuSocketGetAddr(Node), NuSocketGetPort(Node), LogMsgHdr, SendLen, pHdr);

    while(NuSocketSend(Node, pHdr, SendLen) < 0)
    {
        if(pApi->Status == TukanBusApiStatus_InEvaluate)
        {
            _TukanBusApiDisconnect(pApi, pHdr, "Send failed and InEvaluate, disconnect.", 1);
            break;
        }
        else if(!(-- TryCnt))
        {
            pApi->Status = TukanBusApiStatus_InEvaluate;
            pApi->CB.OnLog(pApi->CB.CBArgu, "(%s:%d) < Sending failed in %d times, go evaluating.\n", NuSocketGetAddr(Node), NuSocketGetPort(Node), TukanBusSendRetryTime);
            break;
        }
    }

    return;
}

static void OnConnectCB(NuSocketNode_t *Node, void *Argu) 
{
    TukanBusApi_t       *pApi = (TukanBusApi_t *)Argu;
    char                Msg[TukanBusHdrSize + TukanBusProtocolLen_Msg] = "\0", MsgType = '\0';
    TukanBusHdr_t       *pHdr = (TukanBusHdr_t *)Msg;
    int                 IDLen = 0, MsgLen = 0;
    char                *Addr = NuSocketGetAddr(Node);
    int                 Port = NuSocketGetPort(Node);

    _TukanBusApiSend(pHdr, TukanBusProtocolMsgType_LogOn, pApi->ID, pApi->IDLen, "", 0, "LogOn", pApi);

    if(NuSocketRecvInTime(Node, Msg, TukanBusHdrSize, TukanBusProtocolTime_WaitForLogOn) < TukanBusHdrSize)
    {
        pApi->CB.OnLog(pApi->CB.CBArgu, "(%s:%d) > No LogOn confirm in %d seconds.\n", Addr, Port, TukanBusProtocolTime_WaitForLogOn);
        _TukanBusApiDisconnect(pApi, pHdr, "No LogOn confirm", 1);
    }
    else
    {
        MsgType = TukanBusHdrCheck(pHdr, &IDLen, &MsgLen);

        if(NuSocketRecvInTime(Node, pHdr->ID, IDLen + MsgLen, TukanBusProtocolTime_WaitForLogOn) < IDLen + MsgLen)
        {
            pApi->CB.OnLog(pApi->CB.CBArgu, "(%s:%d) > (%.*s) Incomplete message.\n", Addr, Port, TukanBusHdrSize, pHdr);
            _TukanBusApiDisconnect(pApi, pHdr, "Incomplete message.", 1);
        }
        else if(MsgType != TukanBusProtocolMsgType_LogOn)
        {
            pApi->CB.OnLog(pApi->CB.CBArgu, "(%s:%d) > (%.*s) but not LogOn confirm.\n", Addr, Port, TukanBusHdrSize + IDLen + MsgLen, pHdr);
            _TukanBusApiDisconnect(pApi, pHdr, "Not LogOn confirm.", 1);
        }
        else
        {
            pApi->CB.OnLog(pApi->CB.CBArgu, "(%s:%d) > Logon confirm[%.*s]\n", Addr, Port, TukanBusHdrSize + IDLen + MsgLen, pHdr);
            pApi->Status = TukanBusApiStatus_Online;
            pApi->CB.OnLogon("\0", 0, "\0", 0, pApi->CB.CBArgu);
    
            NuSocketSetTimeout(Node, TukanBusProtocolTime_RemoteHbt, TukanBusProtocolTime_LocalHbt);
        }
    }

    return;
}

static void OnRemoteTimeoutCB(NuSocketNode_t *Node, void *Argu)
{
    TukanBusApi_t   *pApi = (TukanBusApi_t *)Argu;
    char            Msg[TukanBusHdrSize + TukanBusProtocolLen_ID + 1] = "\0";
    TukanBusHdr_t   *pHdr = (TukanBusHdr_t *)Msg;

    if(pApi->Status == TukanBusApiStatus_InEvaluate)
    {
        _TukanBusApiDisconnect(pApi, pHdr, "Disconnected since timeout & InEvaluate.", 1);
    }
    else
    {
        pApi->Status = TukanBusApiStatus_InEvaluate;
        _TukanBusApiSend(pHdr, TukanBusProtocolMsgType_TestRequest, pApi->ID, pApi->IDLen, "", 0, "TestReq", pApi);
    }

    return;
}

static void OnLocalTimeoutCB(NuSocketNode_t *Node, void *Argu)
{
    TukanBusApi_t   *pApi = (TukanBusApi_t *)Argu;
    char            Msg[TukanBusHdrSize + TukanBusProtocolLen_ID + 1] = "\0";
    TukanBusHdr_t   *pHdr = (TukanBusHdr_t *)Msg;

    _TukanBusApiSend(pHdr, TukanBusProtocolMsgType_HeartBeat, pApi->ID, pApi->IDLen, "", 0, "HBT", pApi);

    return;
}

static void OnDataArriveCB(NuSocketNode_t *Node, void *Argu)
{
    TukanBusApi_t       *pApi = (TukanBusApi_t *)Argu;
    char                Msg[TukanBusHdrSize + TukanBusProtocolLen_Msg] = "\0";
    char                MsgType = '\0';
    TukanBusHdr_t       *pHdr = (TukanBusHdr_t *)Msg;
    size_t              RecvLen = 0;
    int                 IDLen = 0, MsgLen = 0;
    char                *Addr = NuSocketGetAddr(Node);
    int                 Port = NuSocketGetPort(Node);

    if((RecvLen = NuSocketRecvInTime(Node, Msg, TukanBusHdrSize, TukanBusProtocolTime_WaitForLogOn)) < TukanBusHdrSize)
    {
        pApi->CB.OnLog(pApi->CB.CBArgu, "(%s:%d) > Incomplete header[%.*s] in %d seconds.\n", Addr, Port, RecvLen, pHdr, TukanBusProtocolTime_WaitForLogOn);
        _TukanBusApiDisconnect(pApi, pHdr, "Incomplete header.", 1);
    }
    else if((MsgType = TukanBusHdrCheck(pHdr, &IDLen, &MsgLen)) == '\0')
    {
        pApi->CB.OnLog(pApi->CB.CBArgu, "(%s:%d) > Invalid header[%.*s]\n", Addr, Port, TukanBusHdrSize, pHdr);
        _TukanBusApiDisconnect(pApi, pHdr, "Invalid header.", 1);
    }
    else
    {
        RecvLen = IDLen + MsgLen;
        if(NuSocketRecvInLen(Node, pHdr->ID, RecvLen, RecvLen) < RecvLen)
        {
            pApi->CB.OnLog(pApi->CB.CBArgu, "(%s:%d) > Incomplete message[%.*s]\n", Addr, Port, RecvLen, pHdr);
            _TukanBusApiDisconnect(pApi, pHdr, "Incomplete message.", 1);
        }
        else
        {
            pApi->CB.OnLog(pApi->CB.CBArgu, "(%s:%d) > [%.*s]\n", Addr, Port, TukanBusHdrSize + RecvLen, pHdr);
            pApi->Status = TukanBusApiStatus_Online;

            switch(MsgType)
            {
            case TukanBusProtocolMsgType_Message:
                pApi->CB.OnDataArrive(pHdr->ID, IDLen, pHdr->ID + IDLen, MsgLen, pApi->CB.CBArgu);
                break;
            case TukanBusProtocolMsgType_TestRequest:
                _TukanBusApiSend(pHdr, TukanBusProtocolMsgType_HeartBeat, pHdr->ID, IDLen, "", 0, "TestReq", pApi);
                break;
            case TukanBusProtocolMsgType_LogOut:
                _TukanBusApiDisconnect(pApi, pHdr, "", 1);
                break;
            default:
                break;
            }
        }
    }

    return;
}

static void OnDisconnectCB(NuSocketNode_t *Node, void *Argu)
{
    TukanBusApi_t   *pApi = (TukanBusApi_t *)Argu;
    
    pApi->CB.OnLog(pApi->CB.CBArgu, "(%s:%d)   Disconnected.\n", NuSocketGetAddr(Node), NuSocketGetPort(Node));
    pApi->Status = TukanBusApiStatus_Offline;
    pApi->CB.OnLogout("\0", 0, "\0", 0, pApi->CB.CBArgu);

    return;
}

static NuSocketProtocol_t  Proto = {&OnConnectCB, &OnDataArriveCB, &OnRemoteTimeoutCB, &OnLocalTimeoutCB, &OnDisconnectCB};

static void OnLog(char *Format, va_list ArguList, void *Argu)
{
    TukanBusApi_t   *pApi = (TukanBusApi_t *)Argu;
    char            Buf[2048] = "\0";

    vsnprintf(Buf, 2048, Format, ArguList);
    pApi->CB.OnLog(pApi->CB.CBArgu, Buf);

    return;
}

TukanBusApi_t *TukanBusApiNew(NuSocket_t *Socket, int SocketType, char *BusAddr, int BusPort, char *Local, char *LoginID, TukanBusApiCB_t *CBs)
{
    NuSocket_t      *pSocket = Socket;
    TukanBusApi_t   *pApi = NULL;

    if(!Socket)
    {
        pSocket = _Socket;
    }

    if(!pSocket)
    {
        if(NuSocketNew(&pSocket, 1, &OnLog) < 0)
        {
            return NULL;
        }
    }

    if((pApi = (TukanBusApi_t *)malloc(sizeof(TukanBusApi_t))) != NULL)
    {
        if(!(pApi->CB.OnLogon = CBs->OnLogon))
        {
            pApi->CB.OnLogon = &_TukanBusDefaultCB;
        }
 
        if(!(pApi->CB.OnLogout = CBs->OnLogout))
        {
            pApi->CB.OnLogout = &_TukanBusDefaultCB;
        }
 
        if(!(pApi->CB.OnLog = CBs->OnLog))
        {
            pApi->CB.OnLog = &_TukanBusDefaultLogCB;
        }
 
        if(!(pApi->CB.OnDataArrive = CBs->OnDataArrive))
        {
            pApi->CB.OnDataArrive = &_TukanBusDefaultCB;
        }
 
        pApi->CB.CBArgu = CBs->CBArgu;
 
        strncpy(pApi->ID, LoginID, TukanBusProtocolLen_ID);
        pApi->IDLen = strlen(LoginID);
 
        pApi->Status = TukanBusApiStatus_Offline;
 
        if(SocketType == TukanBusSocketType_Domain)
        {
            pApi->SocketNode = NuSocketAdd(pSocket, &UnixStrmClnt, &Proto, BusAddr, BusPort, Local, 0, pApi);
        }
        else if(SocketType == TukanBusSocketType_Internet)
        {
            pApi->SocketNode = NuSocketAdd(pSocket, &InetStrmClnt, &Proto, BusAddr, BusPort, "", strtol(Local, (char**)NULL, 10), pApi);
        }

        NuSocketSetAutoReconnect(pApi->SocketNode, 1);
    }

    return pApi;    
}

void TukanBusApiSubscribe(TukanBusApi_t *pApi, char *TopicName)
{
    char                Topic[TukanBusProtocolLen_ID + 1] = "\0";
    char                Msg[TukanBusHdrSize + TukanBusProtocolLen_Msg] = "\0";
    TukanBusHdr_t       *pHdr = (TukanBusHdr_t *)Msg;

    if(pApi->Status == TukanBusApiStatus_Offline)
    {
        return;
    }
 
        
    if(*TopicName != TukanBusProtocolID_TopicPrefix)
    {
        snprintf(Topic, TukanBusProtocolLen_ID, "%c%s", TukanBusProtocolID_TopicPrefix, TopicName);
    }
    else
    {
        strncpy(Topic, TopicName, TukanBusProtocolLen_ID);
    }

    _TukanBusApiSend(pHdr, TukanBusProtocolMsgType_SubReq, Topic, strlen(Topic), "", 0, "SubReq", pApi);

    return;
}

void TukanBusApiSubscribeOnlineNotify(TukanBusApi_t *pApi)
{
    TukanBusApiSubscribe(pApi, TukanBusBuiltInTopic_Online);
    return;
}

void TukanBusApiSubscribeOfflineNotify(TukanBusApi_t *pApi)
{
    TukanBusApiSubscribe(pApi, TukanBusBuiltInTopic_Offline);
    return;
}

void TukanBusApiSend(TukanBusApi_t *pApi, char *ID, char *SendMsg)
{
    char                Msg[TukanBusHdrSize + TukanBusProtocolLen_Msg] = "\0";
    TukanBusHdr_t       *pHdr = (TukanBusHdr_t *)Msg;

    if(pApi->Status == TukanBusApiStatus_Offline)
    {
        return;
    }

    _TukanBusApiSend(pHdr, TukanBusProtocolMsgType_Message, ID, strlen(ID), SendMsg, strlen(SendMsg), "", pApi);

    return;
}

void TukanBusApiBroadcast(TukanBusApi_t *pApi, char *ID, char *SendMsg)
{
    char                Msg[TukanBusHdrSize + TukanBusProtocolLen_Msg] = "\0";
    char                BroadcastID[TukanBusProtocolLen_ID] = "@";
    TukanBusHdr_t       *pHdr = (TukanBusHdr_t *)Msg;

    if(pApi->Status == TukanBusApiStatus_Offline)
    {
        return;
    }

    strncpy(BroadcastID + 1, ID, TukanBusProtocolLen_ID - 1);
    BroadcastID[TukanBusProtocolLen_ID - 1] = '\0';

    _TukanBusApiSend(pHdr, TukanBusProtocolMsgType_Message, BroadcastID, strlen(BroadcastID), SendMsg, strlen(SendMsg), "", pApi);

    return;
}

void TukanBusApiDisconnect(TukanBusApi_t *pApi, char *FarewellMsg)
{
    char                Msg[TukanBusHdrSize + TukanBusProtocolLen_Msg] = "\0";
    TukanBusHdr_t       *pHdr = (TukanBusHdr_t *)Msg;

    _TukanBusApiDisconnect(pApi, pHdr, FarewellMsg, 0);

    return;
}

void TukanBusApiFree(TukanBusApi_t *pApi)
{
    if(pApi->Status != TukanBusApiStatus_Offline)
    {
        TukanBusApiDisconnect(pApi, "Api down.");
    }

    if(_Socket != NULL)
    {
        NuSocketFree(_Socket);
    }

    free(pApi);

    return;
}

