
#include "NuCommon.h"
#include "NuLock.h"
#include "NuBlocking.h"

#ifndef _NUHQ_H
#define _NUHQ_H

#ifdef __cplusplus
extern "C" {
#endif

/* Maximum number of queue */
#define NuHQ_DEFAULT_NUM    32

typedef struct _NuHQ_Msg_t
{
    void                *Item;
    size_t              DataLen;
    struct _NuHQ_Msg_t  *Next;
} NuHQ_Msg_t;

typedef struct _NuHQ_t
{
    NuHQ_Msg_t      *EnQ;
    NuHQ_Msg_t      *DeQ;
    NuLock_t        EnQLock;
    NuLock_t        DeQLock;
    NuBlocking_t    *QFullBlocking;
    NuBlocking_t    *QEmptyBlocking;
    NuHQ_Msg_t      Msg;
} NuHQ_t;

/* Initiate and terminate functions. */
int NuHQNew(NuHQ_t **pHQ, int HQNo);

void NuHQFree(NuHQ_t *pHQ);

int NuHQEnqueue(NuHQ_t *pHQ, void *Item, size_t Len);
void *NuHQDequeueItem(NuHQ_t *pHQ);
int NuHQDequeue(NuHQ_t *pHQ, void **Item, size_t *Len);

/* Clear the queue. */
void NuHQClear(NuHQ_t *pHQ);

#define NuHQIsFull(pHQ)     ((pHQ)->EnQ == (pHQ)->DeQ)
#define NuHQIsEmpty(pHQ)    ((pHQ)->DeQ->Next == (pHQ)->EnQ)
/** @} */

#ifdef __cplusplus
}
#endif

#endif /* _NUHQ_H */

