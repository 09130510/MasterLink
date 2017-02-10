
#include "TukanBus_Protocol.h"

static char TukanBusHdrCheck(TukanBusHdr_t *pHdr, int *IDLen, int *MsgLen)
{
    if(pHdr->Checker[0] == TukanBusProtocolChecker_Value)
    {
        *IDLen = TukanBusGetLen(pHdr->IDLen);
        *MsgLen = TukanBusGetLen(pHdr->MsgLen);
    
        if(*IDLen < 0 || *IDLen > TukanBusProtocolLen_ID)
        {
            return '\0';
        }
        else
        {
            return pHdr->MsgType[0];
        }
    }

    return '\0';
}

static size_t TukanBusMsg(TukanBusHdr_t *pHdr, char MsgType, char *ID, int IDLen, char *Msg, int MsgLen)
{
    pHdr->MsgType[0] = MsgType;
    NuCStrPrintInt(pHdr->IDLen, IDLen, TukanBusProtocolLen_HdrIDLen);
    NuCStrPrintInt(pHdr->MsgLen, MsgLen, TukanBusProtocolLen_HdrMsgLen);
    pHdr->Checker[0] = TukanBusProtocolChecker_Value;
    memcpy(pHdr->ID, ID, IDLen);

    if(MsgLen != 0)
    {
        memcpy(pHdr->ID + IDLen, Msg, MsgLen);
    }

    return TukanBusHdrSize + IDLen + MsgLen;
}

