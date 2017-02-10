
#include "NuSocketType_InetStrmClnt.h"
#include "NuSocketType_UnixStrmClnt.h"

#include "TukanBus_Protocol.h"

#include "NuLog.h"
#include "NuSocket.h"

#ifndef _TUKANBUSAPI_H
#define _TUKANBUSAPI_H

#ifdef __cplusplus
extern "C" {
#endif

typedef struct _TukanBusApi_t TukanBusApi_t;

typedef void (*TukanBusApiCB)(char *ID, size_t IDLen, char *Msg, size_t MsgLen, void *CBArgu);
typedef void (*TukanBusApiLog)(void *CBArgu, char *Log, ...);

typedef struct _TukanBusApiCB_t
{
    TukanBusApiCB   OnLogon;
    TukanBusApiCB   OnLogout;
    TukanBusApiCB   OnDataArrive;
    TukanBusApiLog  OnLog;
    void            *CBArgu;
} TukanBusApiCB_t;

TukanBusApi_t *TukanBusApiNew(NuSocket_t *Socket, int SocketType, char *BusAddr, int BusPort, char *Local, char *LoginID, TukanBusApiCB_t *CBs);
void TukanBusApiSubscribe(TukanBusApi_t *pApi, char *TopicName);
void TukanBusApiSubscribeOnlineNotify(TukanBusApi_t *pApi);
void TukanBusApiSubscribeOfflineNotify(TukanBusApi_t *pApi);
void TukanBusApiSend(TukanBusApi_t *pApi, char *ID, char *SendMsg);
void TukanBusApiBroadcast(TukanBusApi_t *pApi, char *ID, char *SendMsg);
void TukanBusApiDisconnect(TukanBusApi_t *pApi, char *FarewellMsg);
void TukanBusApiFree(TukanBusApi_t *pApi);

#ifdef __cplusplus
}
#endif

#endif /* _TUKANBUSAPI_H */


