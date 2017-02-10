
#include "NuSocketType_InetStrmSvr.h"
#include "NuSocketType_UnixStrmSvr.h"

#include "TukanBus_Protocol.h"

#include "NuTimer.h"
#include "NuLog.h"
#include "NuUtil.h"
#include "NuHash.h"
#include "NuLock.h"
#include "NuCStr.h"
#include "NuIni.h"
#include "NuDaemon.h"
#include "NuSocket.h"

#include "stdarg.h"

#ifndef _TUKANBUS_H
#define _TUKANBUS_H

#ifdef __cplusplus
extern "C" {
#endif

typedef struct _TukanBusNode_t
{
    char                    ID[TukanBusProtocolLen_ID];
    size_t                  IDLen;
    struct _NuSocketNode_t  *SocketNode;
    int                     Status;
    int                     Version;
} TukanBusNode_t;

typedef struct _TukanBusTopicNode_t
{
    TukanBusNode_t          *Node;
    int                     SubVersion;
} TukanBusTopicNode_t;

typedef struct _TukanBusTopic_t
{
    char                    ID[TukanBusProtocolLen_ID];
    size_t                  IDLen;
    base_vector_t           *Subscribers;
    NuLock_t                Lock;
} TukanBusTopic_t;

typedef struct _TukanBus_t
{
    NuLog_t                 *Log;
    NuSocket_t              *Socket;
    NuHash_t                *Nodes;
    NuHash_t                *Topics;
    base_vector_t           *AllNodes;
    NuLock_t                NodeLock;
    NuLock_t                TopicLock;
    NuIni_t                 *ClntIni;
    NuBuffer_t              *TopicNodeBuffer;
    TukanBusTopic_t         *BuiltInTopic_Online;
    TukanBusTopic_t         *BuiltInTopic_Offline;
    struct _NuSocketNode_t  *SocketTypeNode[TukanBusSocketTypeNo];
} TukanBus_t;

typedef enum _TukanBusStatus
{
    TukanBusStatus_Offline = 0,
    TukanBusStatus_Online,
    TukanBusStatus_InEvaluate,
    TukanBusStatusNo
} TukanBusStatus;

#define TukanBusIniName                     "Bus.ini"
#define TukanBusClntIniName                 "BusClnt.ini"

#define TukanBusIniPropertyAddr             "Addr"
#define TukanBusIniPropertyPort             "Port"
#define TukanBusIniPropertyType             "Type"
#define TukanBusIniPropertyType_Internet    "Internet"
#define TukanBusIniPropertyType_Domain      "Domain"
#define TukanBusIniPropertyWildcard         "*"

#define TukanBusIniSession                  "Bus"
#define TukanBusIniPropertyThreadNo         "ThreadNo"
#define TukanBusIniPropertyEnable           "Enable"
#define TukanBusIniPropertyInternetPort     "Port"
#define TukanBusIniPropertyDomainSocketPath "Path"

#ifdef __cplusplus
}
#endif

#endif /* _TUKANBUS_H */

