
#ifndef _TUKANBUS_PROTOCOL_H
#define _TUKANBUS_PROTOCOL_H

#ifdef __cplusplus
extern "C" {
#endif

#define TukanBusProtocolLen_HdrMsgType  1
#define TukanBusProtocolLen_HdrIDLen    4
#define TukanBusProtocolLen_HdrMsgLen   4
#define TukanBusProtocolLen_HdrChecker  1
#define TukanBusProtocolLen_ID          32
#define TukanBusProtocolLen_Msg         4096
#define TukanBusProtocolTime_LocalHbt   27
#define TukanBusProtocolTime_RemoteHbt  33

#pragma pack(1)

typedef struct _TukanBusHdr_t
{
    char    MsgType[TukanBusProtocolLen_HdrMsgType];
    char    IDLen[TukanBusProtocolLen_HdrIDLen];
    char    MsgLen[TukanBusProtocolLen_HdrMsgLen];
    char    Checker[TukanBusProtocolLen_HdrChecker];
    char    ID[1];
} TukanBusHdr_t;

#define TukanBusHdrSize     10

typedef enum _SupportSocketType
{
    TukanBusSocketType_Domain = 0,
    TukanBusSocketType_Internet = 1,
    TukanBusSocketTypeNo
} SupportSocketType;
#pragma pack()

#define TukanBusProtocolMsgType_LogOn       'A'
#define TukanBusProtocolMsgType_LogOut      '5'
#define TukanBusProtocolMsgType_TestRequest '1'
#define TukanBusProtocolMsgType_HeartBeat   '0'
#define TukanBusProtocolMsgType_Message     'm'
#define TukanBusProtocolMsgType_SubReq      'S'
#define TukanBusProtocolMsgType_SubConfirm  's'
#define TukanBusProtocolMsgType_Reject      '3'

#define TukanBusProtocolChecker_Value       '\001'
#define TukanBusProtocolSep_Value           '|'
#define TukanBusProtocolTime_WaitForLogOn   5
#define TukanBusProtocolID_TopicPrefix      '@'
#define TukanBusProtocolID_Sep              ','


#define GetNum(Char)                        ((Char) - 48) 
#define TukanBusGetLen(Len)                 ( \
                                            (GetNum(*(Len)) * 1000) + \
                                            (GetNum(*((Len) + 1)) * 100) + \
                                            (GetNum(*((Len) + 2)) * 10) + \
                                            (GetNum(*((Len) + 3))) \
                                            )

#ifdef __cplusplus
}
#endif

#endif /* _TUKANBUS_PROTOCOL_H */

