
#include "TukanBus.h"
#include "TukanBus_Protocol.c"
#include "TukanBus_Common.h"

static TukanBus_t Bus;

static TukanBusNode_t *_ExpandNodes()
{
    int             Cnt = 0;
    TukanBusNode_t  *pNodes = (TukanBusNode_t *)malloc(sizeof(TukanBusNode_t) * 10);
    TukanBusNode_t  *pFirst = pNodes;

    for(Cnt = 0; Cnt < 10; ++ Cnt)
    {
        pNodes->ID[0] = '\0';
        pNodes->IDLen = 0;
        pNodes->SocketNode = NULL;
        pNodes->Status = TukanBusStatus_Offline;

        base_vector_push(Bus.AllNodes, pNodes);

        ++ pNodes;
    }

    return pFirst;
}

static TukanBusNode_t *_GetNode(NuSocketNode_t *pSocketNode, char *ID, int IDLen, int NewOne)
{
    base_vector_t   *pVec = Bus.AllNodes;
    base_vector_it  VecIt;
    TukanBusNode_t  *pNode = NULL;
    unsigned int    Idx = 0;
    NuHashItem_t    *pItem = NULL;

    NuLockLock(&(Bus.NodeLock));

    if((pItem = NuHashSearch(Bus.Nodes, ID, IDLen, &Idx)) != NULL)
    {
        pNode = (TukanBusNode_t *)NuHashGetValue(pItem);
    }
    else if(NewOne)
    {
        base_vector_it_set(VecIt, pVec);
        while(VecIt != base_vector_it_end(pVec))
        {
            pNode = (TukanBusNode_t *)(*VecIt);

            if(pNode->ID[0] == '\0')
            {
                break;
            }

            ++ VecIt;
        }

        if(VecIt == base_vector_it_end(pVec))
        {
            pNode = _ExpandNodes();
        }

        memcpy(pNode->ID, ID, IDLen);
        pNode->ID[IDLen] = '\0';
        pNode->IDLen = IDLen;
        pNode->SocketNode = NULL;
        pNode->Version = 0;
        NuHashAdd(Bus.Nodes, pNode->ID, IDLen, pNode, sizeof(TukanBusNode_t), Idx);
    }

    NuLockUnLock(&(Bus.NodeLock));

    if(pNode && NewOne)
    {
        ++ (pNode->Version);
    }

    return pNode;
}

static TukanBusTopic_t *_GetTopic(char *TopicID, int IDLen, int NewOne)
{
    unsigned int    Idx = 0;
    NuHashItem_t    *pItem = NULL;
    TukanBusTopic_t *pTopic = NULL;

    NuLockLock(&(Bus.TopicLock));

    if((pItem = NuHashSearch(Bus.Topics, TopicID, IDLen, &Idx)) != NULL)
    {
        pTopic = (TukanBusTopic_t *)NuHashGetValue(pItem);
    }
    else if(NewOne)
    {
        pTopic = (TukanBusTopic_t *)malloc(sizeof(TukanBusTopic_t));

        memcpy(pTopic->ID, TopicID, IDLen);
        pTopic->IDLen = IDLen;
        base_vector_new(&(pTopic->Subscribers), 10);
        NuLockInit(&(pTopic->Lock), &NuLockType_Mutex);

        NuHashAdd(Bus.Topics, pTopic->ID, IDLen, pTopic, sizeof(TukanBusTopic_t), Idx);
    }

    NuLockUnLock(&(Bus.TopicLock));

    return pTopic;
}

static void TukanBusDisconnect(TukanBusNode_t *pNode, TukanBusHdr_t *pHdr, char *Farewell);

static int _CheckID(char *IDStr, char *Addr, int Port, int SocketType)
{
    char    *Buf = NULL;

    if(*IDStr == TukanBusProtocolID_TopicPrefix)
    {
        NuLog(Bus.Log, "(%s:%d) ID[%s] start by topic prefix[%c].\n", Addr, Port, IDStr, TukanBusProtocolID_TopicPrefix);
        return -1;
    }

    /* Check SocketType */
    if(!(Buf = NuIniFind(Bus.ClntIni, IDStr, TukanBusIniPropertyType)))
    {
        NuLog(Bus.Log, "(%s:%d) %s::%s not in Ini.\n", Addr, Port, IDStr, TukanBusIniPropertyType);
        return -1;
    }
    else
    {
        if(strstr(Buf, TukanBusIniPropertyType_Internet))
        {
            if(SocketType != TukanBusSocketType_Internet)
            {
                NuLog(Bus.Log, "(%s:%d) SocketType(%d) is not %s.\n", Addr, Port, SocketType, TukanBusIniPropertyType_Internet);
                return -1;
            }
        }
        else if(strstr(Buf, TukanBusIniPropertyType_Domain))
        {
            if(SocketType != TukanBusSocketType_Domain)
            {
                NuLog(Bus.Log, "(%s:%d) SocketType(%d) is not %s.\n", Addr, Port, SocketType, TukanBusIniPropertyType_Domain);
                return -1;
            }   
        }
        else
        {
            NuLog(Bus.Log, "(%s:%d) SocketType(%d) unknown.\n", Addr, Port, SocketType);
            return -1;
        }
    }

    /* Check Addr */
    if(!(Buf = NuIniFind(Bus.ClntIni, IDStr, TukanBusIniPropertyAddr)))
    {
        NuLog(Bus.Log, "(%s:%d) %s::%s not in Ini.\n", Addr, Port, IDStr, TukanBusIniPropertyAddr);
        return -1;
    }
    else if(strcmp(Buf, TukanBusIniPropertyWildcard))
    {
        if(strcmp(Addr, Buf))
        {
            NuLog(Bus.Log, "(%s:%d) Addr != Ini::Addr[%s].\n", Addr, Port, Buf);
            return -1;
        }
    }

    /* Check Port */
    if(!(Buf = NuIniFind(Bus.ClntIni, IDStr, TukanBusIniPropertyPort)))
    {
        NuLog(Bus.Log, "(%s:%d) %s::%s not in Ini.\n", Addr, Port, IDStr, TukanBusIniPropertyPort);
        return -1;
    }
    else if(strcmp(Buf, TukanBusIniPropertyWildcard))
    {
        if(Port != strtol(Buf, (char **)NULL, 10))
        {
            NuLog(Bus.Log, "(%s:%d) Port != Ini::Port[%s].\n", Addr, Port, Buf);
            return -1;
        }
    }

    return 0;
}

static void TukanBusSubscribe(TukanBusTopic_t *pTopic, TukanBusNode_t *pNode)
{
    int                 Registered = 0;
    base_vector_t       *pVec = pTopic->Subscribers;
    base_vector_it      VecIt;
    TukanBusTopicNode_t *pTopicNode = NULL;

    NuLockLock(&(pTopic->Lock));

    base_vector_it_set(VecIt, pVec);
    while(VecIt != base_vector_it_end(pVec))
    {
        pTopicNode = (TukanBusTopicNode_t *)(*VecIt);

        if(pNode == pTopicNode->Node)   
        {
            pTopicNode->SubVersion = pNode->Version;
            Registered = 1;
            break;
        }

        ++ VecIt;
    }

    if(!Registered)
    {
        NuLockLock(&(Bus.TopicLock));
        pTopicNode = NuBufferGet(Bus.TopicNodeBuffer);
        NuLockUnLock(&(Bus.TopicLock));

        pTopicNode->Node = pNode;
        pTopicNode->SubVersion = pNode->Version;
        base_vector_push(pVec, pTopicNode);
    }

    NuLockUnLock(&(pTopic->Lock));

    return;
}

static void TukanBusBrdCstMsg(TukanBusHdr_t *pHdr, TukanBusTopic_t *pTopic, size_t MsgLen)
{
    base_vector_t       *pVec = pTopic->Subscribers;
    base_vector_it      VecIt;
    TukanBusNode_t      *pNode = NULL;
    TukanBusTopicNode_t *pTopicNode = NULL;

    NuLockLock(&(pTopic->Lock));

    base_vector_it_set(VecIt, pVec);
    while(VecIt != base_vector_it_end(pVec))
    {
        pTopicNode = (TukanBusTopicNode_t *)(*VecIt);
        pNode = pTopicNode->Node;

        if(pNode->Status != TukanBusStatus_Offline && pNode->Version == pTopicNode->SubVersion)
        {
            NuLog(Bus.Log, "(%s) < +[%.*s]\n", pNode->ID, MsgLen, pHdr);
            NuSocketSend(pNode->SocketNode, pHdr, MsgLen);
        }

        ++ VecIt;
    }

    NuLockUnLock(&(pTopic->Lock));

    return;
}

static void TukanBusBrdCst(TukanBusHdr_t *pHdr, TukanBusTopic_t *pTopic, char *Msg, int MsgLen)
{
    size_t          SendLen = TukanBusMsg(pHdr, TukanBusProtocolMsgType_Message, pTopic->ID, pTopic->IDLen, Msg, MsgLen);

    TukanBusBrdCstMsg(pHdr, pTopic, SendLen);

    return;
}

static void TukanBus_MsgError(TukanBusHdr_t *pHdr, char *ID, char *Msg, NuSocketNode_t *Node)
{
    size_t  SendLen = TukanBusMsg(pHdr, TukanBusProtocolMsgType_LogOut, ID, strlen(ID), Msg, strlen(Msg));

    NuLog(Bus.Log, "(%s:%d) < KickAway[%.*s]\n", NuSocketGetAddr(Node), NuSocketGetPort(Node), SendLen, pHdr);
    NuSocketSend(Node, pHdr, SendLen);

    NuSocketDisconnect(Node);
    return;
}

static void TukanBusSend(TukanBusHdr_t *pHdr, char MsgType, char *ID, int IDLen, char *Msg, int MsgLen, char *LogMsgHdr, TukanBusNode_t *BusNode)
{
    int     TryCnt = TukanBusSendRetryTime;
    size_t  SendLen = TukanBusMsg(pHdr, MsgType, ID, IDLen, Msg, MsgLen);

    if(BusNode->Status != TukanBusStatus_Offline)
    {
        NuLog(Bus.Log, "(%s) < %s[%.*s]\n", BusNode->ID, LogMsgHdr, SendLen, pHdr);
        while(NuSocketSend(BusNode->SocketNode, pHdr, SendLen) < 0)
        {
            if(BusNode->Status == TukanBusStatus_InEvaluate)
            {
                TukanBusDisconnect(BusNode, pHdr, "Send failed & Evaluate");
                break;
            }
            else if(!(-- TryCnt))
            {
                BusNode->Status = TukanBusStatus_InEvaluate;
                NuLog(Bus.Log, "(%s) < Sending failed in %d times, go evaluating.\n", BusNode->ID, TukanBusSendRetryTime);
                break;
            }
        }
    }

    return;
}

static void TukanBusDisconnect(TukanBusNode_t *pNode, TukanBusHdr_t *pHdr, char *Farewell)
{
    if(pNode->Status != TukanBusStatus_Offline)
    {
        pNode->Status = TukanBusStatus_Offline;

        if(Farewell)
        {
            TukanBusSend(pHdr, TukanBusProtocolMsgType_LogOut, pNode->ID, pNode->IDLen, Farewell, strlen(Farewell), "LogOut", pNode);
        }

        NuSocketDisconnect(pNode->SocketNode);
    }

    return;
}

static void OnConnectCB(NuSocketNode_t *Node, void *Argu) 
{
    int             IDLen = 0, MsgLen = 0;
    size_t          RecvLen = 0;
    char            Msg[TukanBusHdrSize + TukanBusProtocolLen_Msg] = "\0";
    char            IDStr[TukanBusProtocolLen_ID + 1] = "\0";
    TukanBusHdr_t   *pHdr = (TukanBusHdr_t *)Msg;
    TukanBusNode_t  *pNode = NULL;
    char            *Addr = NuSocketGetAddr(Node);
    int             Port = NuSocketGetPort(Node);

    NuLog(Bus.Log, "(%s:%d)   Connected.\n", Addr, Port);

    if(NuSocketRecvInTime(Node, Msg, TukanBusHdrSize, TukanBusProtocolTime_WaitForLogOn) < TukanBusHdrSize)
    {
        TukanBus_MsgError(pHdr, "NoName", "Recv no LogOn", Node);
    }
    else if(TukanBusHdrCheck(pHdr, &IDLen, &MsgLen) != TukanBusProtocolMsgType_LogOn)
    {
        NuLog(Bus.Log, "(%s:%d) > (%.*s) but not LogOn.\n", Addr, Port, TukanBusHdrSize, pHdr);
        TukanBus_MsgError(pHdr, "NoName", "Recv not LogOn", Node);
    }
    else if((RecvLen = NuSocketRecvInTime(Node, pHdr->ID, IDLen + MsgLen, TukanBusProtocolTime_WaitForLogOn)) < IDLen + MsgLen)
    {
        NuLog(Bus.Log, "(%s:%d) > Incomplete message[%.*s] in %d seconds.\n", Addr, Port, TukanBusHdrSize + RecvLen, pHdr, TukanBusProtocolTime_WaitForLogOn);
        TukanBus_MsgError(pHdr, "NoName", "Incomplete LogOn", Node);
    }
    else
    {
        NuLog(Bus.Log, "(%s:%d) > Logon[%.*s]\n", Addr, Port, TukanBusHdrSize + RecvLen, pHdr);

        memcpy(IDStr, pHdr->ID, IDLen);
        IDStr[IDLen] = '\0';
        if(_CheckID(IDStr, Addr, Port, ((TukanBusNode_t *)Argu)->Version) < 0)
        {
            NuLog(Bus.Log, "(%s:%d) > LogonID[%.*s] permission denied.\n", Addr, Port, IDLen, pHdr->ID);
            TukanBus_MsgError(pHdr, IDStr, "Permission denied", Node);
        }
        else
        {
            if((pNode = _GetNode(Node, pHdr->ID, IDLen, 1))->Status != TukanBusStatus_Offline)
            {
                NuLog(Bus.Log, "(%s) Disconnect former connection(%s:%d)\n", IDStr, NuSocketGetAddr(pNode->SocketNode), NuSocketGetPort(pNode->SocketNode));
                TukanBusDisconnect(pNode, pHdr, "Disconnected by new connecting.");
            }

            pNode->Status = TukanBusStatus_Online;
            pNode->SocketNode = Node;
            NuSocketSetArgu(Node, pNode);
 
            TukanBusSend(pHdr, TukanBusProtocolMsgType_LogOn, pNode->ID, pNode->IDLen, "Login OK!", strlen("Login OK!"), "LogonOK", pNode);

            TukanBusBrdCst(pHdr, Bus.BuiltInTopic_Online, pNode->ID, pNode->IDLen);

            NuSocketSetTimeout(Node, TukanBusProtocolTime_RemoteHbt, TukanBusProtocolTime_LocalHbt);
        }
    }

    return;
}

static void OnRemoteTimeoutCB(NuSocketNode_t *Node, void *Argu)
{
    char                Msg[TukanBusHdrSize + TukanBusProtocolLen_Msg] = "\0";
    TukanBusHdr_t       *pHdr = (TukanBusHdr_t *)Msg;
    TukanBusNode_t      *pSenderNode = (TukanBusNode_t *)Argu;

    if(pSenderNode->Status == TukanBusStatus_InEvaluate)
    {
        TukanBusDisconnect(pSenderNode, pHdr, "HBT Timeout & Evaluate");
    }
    else
    {
        pSenderNode->Status = TukanBusStatus_InEvaluate;

        TukanBusSend(pHdr, TukanBusProtocolMsgType_TestRequest, pSenderNode->ID, pSenderNode->IDLen, "", 0, "TestReq", pSenderNode);
    }

    return;
}

static void OnLocalTimeoutCB(NuSocketNode_t *Node, void *Argu)
{
    char                Msg[TukanBusHdrSize + TukanBusProtocolLen_Msg] = "\0";
    TukanBusHdr_t       *pHdr = (TukanBusHdr_t *)Msg;
    TukanBusNode_t      *pSenderNode = (TukanBusNode_t *)Argu;

    TukanBusSend(pHdr, TukanBusProtocolMsgType_HeartBeat, pSenderNode->ID, pSenderNode->IDLen, "", 0, "HBT", pSenderNode);

    return;
}

static void OnDataArriveCB(NuSocketNode_t *Node, void *Argu)
{
    int                 Len = 0, IDLen = 0, MsgLen = 0, RecvLen = 0;
    char                Msg[TukanBusHdrSize + TukanBusProtocolLen_Msg] = "\0", MsgType = '\0';
    char                *pRecv = Msg, *pMsgEnd = Msg + TukanBusHdrSize + TukanBusProtocolLen_Msg;
    size_t              MsgSize = 0;
    TukanBusHdr_t       *pHdr = (TukanBusHdr_t *)Msg;
    char                SendMsg[TukanBusHdrSize + TukanBusProtocolLen_Msg] = "\0";
    TukanBusHdr_t       *pSendHdr = (TukanBusHdr_t *)SendMsg;
    char                *pID = pHdr->ID, *pIDComma = pHdr->ID, *pIDEnd = NULL;
    TukanBusNode_t      *pNode = NULL, *pSenderNode = (TukanBusNode_t *)Argu;
    TukanBusTopic_t     *pTopic = NULL;

    do
    {
        if(pRecv - Msg < TukanBusHdrSize)
        {
            if((RecvLen = NuSocketRecvInLen(Node, pRecv, 1, pMsgEnd - pRecv)) > 0)
            {
                pRecv += RecvLen;
                continue;
            }
            else
            {
                NuLog(Bus.Log, "(%s) > Recv no Hdr.\n");
                TukanBus_MsgError(pSendHdr, pSenderNode->ID, "Recv no Hdr.", Node);
                break;
            }
        }
        else if((MsgType = TukanBusHdrCheck(pHdr, &IDLen, &MsgLen)) == '\0')
        {
            NuLog(Bus.Log, "(%s) > Invalid header[%.*s]\n", pSenderNode->ID, TukanBusHdrSize, pHdr);
            TukanBus_MsgError(pSendHdr, pSenderNode->ID, "Invalid header.", Node);
        
            break;
        }
        else
        {
            MsgSize = IDLen + MsgLen + TukanBusHdrSize;

            if(pRecv - Msg < MsgSize)
            {
                if((RecvLen = NuSocketRecvInLen(Node, pRecv, 1, pMsgEnd - pRecv)) > 0)
                {
                    pRecv += RecvLen;
                    continue;
                }
                else
                {
                    NuLog(Bus.Log, "(%s) > Recv no Msg.\n");
                    TukanBus_MsgError(pSendHdr, pSenderNode->ID, "Recv no Msg.", Node);
                    break;
                }
            }

            NuLog(Bus.Log, "(%s) > [%.*s]\n", pSenderNode->ID, MsgSize, pHdr);

            pID = pIDComma = pHdr->ID;
            pIDEnd = pID + IDLen;

            switch(MsgType)
            {
            case TukanBusProtocolMsgType_Message:
                while(++ pIDComma)
                {
                    if(*pIDComma == TukanBusProtocolID_Sep || pIDComma == pIDEnd)
                    {
                        if(*pID == TukanBusProtocolID_TopicPrefix)
                        { /* Topic broadcast */
                            if((pTopic = _GetTopic(pID, pIDComma - pID, 0)) != NULL)
                            {
                                TukanBusBrdCstMsg(pHdr, pTopic, MsgSize);
                            }
                        }
                        else
                        { /* One-to-One message. */
                            if((pNode = _GetNode(Node, pID, Len = pIDComma - pID, 0)) != NULL)
                            {
                                if(pNode->Status != TukanBusStatus_Offline)
                                {
                                    TukanBusSend(pSendHdr, TukanBusProtocolMsgType_Message, pSenderNode->ID, pSenderNode->IDLen, pHdr->ID + IDLen, MsgLen, "", pNode);
                                }
                                else
                                {
                                    TukanBusSend(pSendHdr, TukanBusProtocolMsgType_Reject, pID, Len, "Target Offline.", sizeof("Target Offline."), "Rej", pSenderNode);
                                }
                            }
                            else
                            {
                                TukanBusSend(pSendHdr, TukanBusProtocolMsgType_Reject, pID, Len, "Target Not exists.", sizeof("Target Not exists."), "Rej", pSenderNode);
                            }
                        }
            
                        if(pIDComma == pIDEnd)
                        {
                            break;
                        }

                        pID = pIDComma + 1;
                    }
                }

                break;
            case TukanBusProtocolMsgType_HeartBeat:
                pSenderNode->Status = TukanBusStatus_Online;
                break;
            case TukanBusProtocolMsgType_SubReq:
                if(pSenderNode->Status != TukanBusStatus_Offline)
                {
                    while(++ pIDComma)
                    {
                        if(*pIDComma == TukanBusProtocolID_Sep || pIDComma == pIDEnd)
                        {
                            if(*pID == TukanBusProtocolID_TopicPrefix)
                            {
                                pTopic = _GetTopic(pID, pIDComma - pID, 1);
                                TukanBusSubscribe(pTopic, pSenderNode);
                            }

                            if(pIDComma == pIDEnd)
                            {
                                break;
                            }

                            pID = pIDComma + 1;
                        }
                    }

                    TukanBusSend(pSendHdr, TukanBusProtocolMsgType_SubConfirm, pHdr->ID, IDLen, "", 0, "SubConfirm", pSenderNode);
                }

                break;
            case TukanBusProtocolMsgType_TestRequest:
                TukanBusSend(pSendHdr, TukanBusProtocolMsgType_HeartBeat, pHdr->ID, IDLen, "", 0, "HBT", pSenderNode);
                break;
            case TukanBusProtocolMsgType_LogOut:
            default:
                TukanBusDisconnect(pSenderNode, pSendHdr, "Goodbye.");
                break;
            }

            memmove(Msg, Msg + MsgSize, pRecv - Msg - MsgSize);
            pRecv -= MsgSize;
        }
    }
    while(pRecv != Msg);

    return;
}

static void OnDisconnectCB(NuSocketNode_t *Node, void *Argu)
{
    TukanBusNode_t      *pNode = (TukanBusNode_t *)Argu;
    char                Msg[TukanBusHdrSize + TukanBusProtocolLen_Msg] = "\0";
    TukanBusHdr_t       *pHdr = (TukanBusHdr_t *)Msg;

    pNode->Status = TukanBusStatus_Offline;

    NuLog(Bus.Log, "(%s:%d)   Disconnected.\n", NuSocketGetAddr(Node), NuSocketGetPort(Node));

    if(pNode->ID[0] != '\0')
    {
        TukanBusBrdCst(pHdr, Bus.BuiltInTopic_Offline, pNode->ID, pNode->IDLen);
    }

    return;
}

static void OnLog(char *Format, va_list ArguList, void *Argu)
{
    NuLogV(Bus.Log, Format, ArguList);
    return;
}

static void OnSignal(int Sig, void *Argu)
{
    base_vector_t   *pVec = Bus.AllNodes;
    base_vector_it  VecIt;
    TukanBusNode_t  *pNode = NULL;
    char            Msg[TukanBusHdrSize + TukanBusProtocolLen_Msg] = "\0";
    TukanBusHdr_t   *pHdr = (TukanBusHdr_t *)Msg;

    NuLog(Bus.Log, "Recved Signal[%d]\n", Sig);

    if(Bus.SocketTypeNode[TukanBusSocketType_Domain] != NULL)
    {
        NuSocketSetAutoReconnect(Bus.SocketTypeNode[TukanBusSocketType_Domain], 0);
        NuSocketDisconnect(Bus.SocketTypeNode[TukanBusSocketType_Domain]);
        NuLog(Bus.Log, "Shutdown domain socket entry.\n");
        Bus.SocketTypeNode[TukanBusSocketType_Domain] = NULL;
    }

    if(Bus.SocketTypeNode[TukanBusSocketType_Internet] != NULL)
    {
        NuSocketSetAutoReconnect(Bus.SocketTypeNode[TukanBusSocketType_Internet], 0);
        NuSocketDisconnect(Bus.SocketTypeNode[TukanBusSocketType_Internet]);
        NuLog(Bus.Log, "Shutdown internet socket entry.\n");
        Bus.SocketTypeNode[TukanBusSocketType_Internet] = NULL;
    }

    sleep(1);
    
    base_vector_it_set(VecIt, pVec);
    while(VecIt != base_vector_it_end(pVec))
    {
        pNode = (TukanBusNode_t *)(*VecIt);

        if(pNode->Status != TukanBusStatus_Offline)
        {
            TukanBusDisconnect(pNode, pHdr, "System shutdown.");
        }

        ++ VecIt;
    }

    sleep(1);

    NuDaemonStop();

    return;
}

static void OnTimer(void *Argu)
{
    NuLogFlush(Bus.Log);

    return;
}

static void Usage(char *Prog)
{
    printf("Usage: %s --tbase <TbasePath> [--Daemon]\n", Prog);

    return;
}

int main(int Argc, char **Argv)
{
    NuSocket_t          *pSocket = NULL;
    NuSocketProtocol_t  Proto;
    NuIni_t             *pIni = NULL;
    char                Buf[256] = "\0", Today[12] = "\0";
    char                *pIniValue = NULL;
    char                *pTbase = NULL;
    TukanBusNode_t      InternetSocket = {.Version = TukanBusSocketType_Internet};
    TukanBusNode_t      DomainSocket = {.Version = TukanBusSocketType_Domain};

    if(Argc < 3)
    {
        Usage(Argv[0]);
        return 0;
    }
    else
    {
        while(-- Argc)
        {
            if(!strcmp(*Argv, "--tbase"))
            {
                pTbase = *(++ Argv);
            }

            if(!strcmp(*Argv, "--Daemon"))
            {
                NuDaemonStart();
                NuDaemonSetOnSignalCB(&OnSignal, NULL);
                NuDaemonAllowSignal(SIGQUIT);
            }

            ++ Argv;
        }
    
        if(!pTbase)
        {
            Usage(Argv[0]);
            return 0;
        }
    }

    sprintf(Buf, "%s%s%s", pTbase, pTbase[strlen(pTbase) - 1] == '/' ? "" : "/", TukanBusIniName);
    if(NuIniNew(&pIni, Buf) != NU_OK)
    {
        printf("Open %s failed.\n", Buf);
        return 0;
    }

    if(!(pIniValue = NuIniFind(pIni, TukanBusIniSession, TukanBusIniPropertyThreadNo)))
    {
        printf("%s::%s::%s Not found.\n", TukanBusIniName, TukanBusIniSession, TukanBusIniPropertyThreadNo);
        return 0;
    }

    if(NuSocketNew(&pSocket, strtol(pIniValue, (char **)NULL, 10), &OnLog) < 0)
    {
        printf("NuSocketNew Error!\n");
        return 0;
    }

    Bus.Socket = pSocket;

    NuGetToday2(Today);
    sprintf(Buf, "%s%sLog/%s", pTbase, pTbase[strlen(pTbase) - 1] == '/' ? "" : "/", Today);
    NuLogOpen(&(Bus.Log), Buf, "Bus");
    NuLogSetThreadSafe(Bus.Log);

    NuLog(Bus.Log, "---------- TukanBus starts ----------\n");
    NuLog(Bus.Log, "tbase[%s]\n", pTbase);

    NuHashNew(&(Bus.Nodes), 10);
    NuHashNew(&(Bus.Topics), 10);
    base_vector_new(&(Bus.AllNodes), 20);
    NuLockInit(&(Bus.NodeLock), &NuLockType_Mutex);
    NuLockInit(&(Bus.TopicLock), &NuLockType_Mutex);

    sprintf(Buf, "%s%s%s", pTbase, pTbase[strlen(pTbase) - 1] == '/' ? "" : "/", TukanBusClntIniName);
    if(NuIniNew(&(Bus.ClntIni), Buf) != NU_OK)
    {
        NuLog(Bus.Log, "Open %s failed.\n", TukanBusClntIniName);
        return 0;
    }

    Bus.BuiltInTopic_Online = _GetTopic(TukanBusBuiltInTopic_Online, TukanBusBuiltInTopicLen_Online, 1);
    Bus.BuiltInTopic_Offline = _GetTopic(TukanBusBuiltInTopic_Offline, TukanBusBuiltInTopicLen_Offline, 1);

    NuBufferNew(&(Bus.TopicNodeBuffer), sizeof(TukanBusTopicNode_t), 20);

    Proto.OnConnect = &OnConnectCB;
    Proto.OnDataArrive = &OnDataArriveCB;
    Proto.OnRemoteTimeout = &OnRemoteTimeoutCB;
    Proto.OnLocalTimeout = &OnLocalTimeoutCB;
    Proto.OnDisconnect = &OnDisconnectCB;

    if((pIniValue = NuIniFind(pIni, TukanBusIniPropertyType_Internet, TukanBusIniPropertyEnable)) != NULL)
    {
        if(strtol(pIniValue, (char **)NULL, 10) == 1)
        {
            if(!(pIniValue = NuIniFind(pIni, TukanBusIniPropertyType_Internet, TukanBusIniPropertyInternetPort)))
            {
                NuLog(Bus.Log, "%s::%s::%s Not found.\n", TukanBusIniName, TukanBusIniPropertyType_Internet, TukanBusIniPropertyInternetPort);
                return 0;
            }
            Bus.SocketTypeNode[TukanBusSocketType_Internet] = NuSocketAdd(pSocket, &InetStrmSvr, &Proto, "", 0, "127.0.0.1", strtol(pIniValue, (char **)NULL, 10), &InternetSocket);
        
            NuLog(Bus.Log, "Start Internet service in port[%s]\n", pIniValue);
        }
        else
        {
            Bus.SocketTypeNode[TukanBusSocketType_Internet] = NULL;
        }
    }

    if((pIniValue = NuIniFind(pIni, TukanBusIniPropertyType_Domain, TukanBusIniPropertyEnable)) != NULL)
    {
        if(strtol(pIniValue, (char **)NULL, 10) == 1)
        {
            if(!(pIniValue = NuIniFind(pIni, TukanBusIniPropertyType_Domain, TukanBusIniPropertyDomainSocketPath)))
            {
                NuLog(Bus.Log, "%s::%s::%s Not found.\n", TukanBusIniName, TukanBusIniPropertyType_Domain, TukanBusIniPropertyDomainSocketPath);
                return 0;
            }
            Bus.SocketTypeNode[TukanBusSocketType_Domain] = NuSocketAdd(pSocket, &UnixStrmSvr, &Proto, "", 0, pIniValue, 0, &DomainSocket);
            NuLog(Bus.Log, "Start Internet service in address[%s]\n", pIniValue);
        }
        else
        {
            Bus.SocketTypeNode[TukanBusSocketType_Domain] = NULL;
        }
    }

    NuIniFree(pIni);

    NuTimerRegister(1, 0, &OnTimer, NULL);
    NuTimerNew(1);
    NuTimerStop();
    NuTimerFree();

    NuLog(Bus.Log, "---------- TukanBus stops ----------\n");
    NuLogFlush(Bus.Log);
    NuLogClose(Bus.Log);

    NuSocketFree(Bus.Socket);
    
    NuHashFree(Bus.Nodes);
    NuHashFree(Bus.Topics);

    base_vector_free(Bus.AllNodes);

    NuBufferFree(Bus.TopicNodeBuffer);

    return 0;
}

