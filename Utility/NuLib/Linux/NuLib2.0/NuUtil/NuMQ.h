
#include "NuCommon.h"
#include "NuLock.h"
#include "NuBlocking.h"

#ifndef _NUMQ_H
#define _NUMQ_H

#ifdef __cplusplus
extern "C" {
#endif

/* Maximum number of queue */
#define NuMQ_DEFAULT_NUM    32
/* Default item size of each message, realloc if size of enqueue exceed. */
#define NuMQ_DEFAULT_SIZE   256
/* Default mType */
#define NuMQ_DEFAULT_MTYPE  0

typedef struct _NuMQ_Msg_t
{
    char                mType;
    size_t              DataLen;
    struct _NuMQ_Msg_t  *Next;
    char                Item[1];
} NuMQ_Msg_t;

typedef struct _NuMQ_t
{
    size_t          MsgSize;
    NuMQ_Msg_t      *EnQ;
    NuMQ_Msg_t      *DeQ;
    NuLock_t        EnQLock;
    NuLock_t        DeQLock;
    NuBlocking_t    *QFullBlocking;
    NuBlocking_t    *QEmptyBlocking;
    NuMQ_Msg_t      Msg;
} NuMQ_t;

typedef void (*NuMQ_EnQCB)(void *, size_t *, void *);

/* Initiate and terminate functions. */
int NuMQNew(NuMQ_t **pMQ);

int NuMQNew2(NuMQ_t **pMQ, int MQNo, size_t MsgSize);

void NuMQFree(NuMQ_t *pMQ);

int NuMQEnqueueCB(NuMQ_t *pMQ, char mType, NuMQ_EnQCB EnQFn, void *Argu);
int NuMQEnqueue(NuMQ_t *pMQ, char mType, void *Item, size_t Len);
int NuMQDequeue(NuMQ_t *pMQ, char *mType, void *Item, size_t *Len);

/* Clear the queue. */
void NuMQClear(NuMQ_t *pMQ);

#define NuMQIsFull(pMQ)     ((pMQ)->EnQ == (pMQ)->DeQ)
#define NuMQIsEmpty(pMQ)    ((pMQ)->DeQ->Next == (pMQ)->EnQ)
/** @} */

#ifdef __cplusplus
}
#endif

#endif /* _NUMQ_H */

