
#include "NuHangerQ.h"
/* Queue function */
/* ====================================================================== */
int NuHQNew(NuHQ_t **pHQ, int HQNo)
{
    int         iRC = NU_OK, Cnt = 0;
    NuHQ_Msg_t  *pMsg = NULL;

    if(!((*pHQ) = (NuHQ_t *)malloc(sizeof(NuHQ_t) + sizeof(NuHQ_Msg_t) * (HQNo))))
    {
        return NU_MALLOC_FAIL;
    }

    (*pHQ)->EnQ = pMsg = &((*pHQ)->Msg);

    for(Cnt = 0; Cnt < HQNo; ++ Cnt)
    {
        pMsg->Item = NULL;
        pMsg->DataLen = 0;
        pMsg->Next = pMsg + 1;
        ++ pMsg;
    }

    pMsg->Item = NULL;
    pMsg->DataLen = 0;
    pMsg->Next = (*pHQ)->EnQ;

    (*pHQ)->DeQ = pMsg;

    iRC = NuLockInit(&((*pHQ)->EnQLock), &NuLockType_Mutex);
    NUCHKRC(iRC, EXIT);

    iRC = NuLockInit(&((*pHQ)->DeQLock), &NuLockType_Mutex);
    NUCHKRC(iRC, EXIT);

    iRC = NuBlockingNew(&((*pHQ)->QFullBlocking), &NuBlocking_CondVar, NULL);
    NUCHKRC(iRC, EXIT);

    iRC = NuBlockingNew(&((*pHQ)->QEmptyBlocking), &NuBlocking_CondVar, NULL);
    NUCHKRC(iRC, EXIT);

EXIT:
    if(iRC < 0)
    {
        NuHQFree(*pHQ);
    }

    return iRC;
}

void NuHQFree(NuHQ_t *pHQ)
{
    if(pHQ != NULL)
    {
        NuLockDestroy(&(pHQ->EnQLock));
        NuLockDestroy(&(pHQ->DeQLock));
        NuBlockingFree(pHQ->QFullBlocking, NULL);
        NuBlockingFree(pHQ->QEmptyBlocking, NULL);

        free(pHQ);
    }

    return;
}

int NuHQEnqueue(NuHQ_t *pHQ, void *Item, size_t Len)
{
    NuHQ_Msg_t  *pEnQ = NULL;

    NuLockLock(&(pHQ->EnQLock));

    while(NuHQIsFull(pHQ))
    {
        NuBlockingBlock(pHQ->QFullBlocking, NULL);
    }

    pEnQ = pHQ->EnQ;

    pEnQ->Item = Item;
    pEnQ->DataLen = Len;

    pHQ->EnQ = pEnQ->Next;

    NuBlockingWake(pHQ->QEmptyBlocking, NULL);

    NuLockUnLock(&(pHQ->EnQLock));

    return NU_OK;
}

void *NuHQDequeueItem(NuHQ_t *pHQ)
{
    void        *ptr = NULL;
    NuHQ_Msg_t  *Tmp = NULL;

    NuLockLock(&(pHQ->DeQLock));

    while(NuHQIsEmpty(pHQ))
    {
        NuBlockingBlock(pHQ->QEmptyBlocking, NULL);
    }

    Tmp = pHQ->DeQ->Next;

    pHQ->DeQ = Tmp;

    NuBlockingWake(pHQ->QFullBlocking, NULL);
    ptr = Tmp->Item;

    NuLockUnLock(&(pHQ->DeQLock));

    return ptr;
}

int NuHQDequeue(NuHQ_t *pHQ, void **Item, size_t *Len)
{
    NuHQ_Msg_t  *Tmp = NULL;

    NuLockLock(&(pHQ->DeQLock));

    while(NuHQIsEmpty(pHQ))
    {
        NuBlockingBlock(pHQ->QEmptyBlocking, NULL);
    }

    Tmp = pHQ->DeQ->Next;
    *Len = Tmp->DataLen;
    *Item = Tmp->Item;

    pHQ->DeQ = Tmp;

    NuBlockingWake(pHQ->QFullBlocking, NULL);

    NuLockUnLock(&(pHQ->DeQLock));

    return NU_OK;
}

void NuHQClear(NuHQ_t *pHQ)
{
    NuLockLock(&(pHQ->EnQLock));
    NuLockLock(&(pHQ->DeQLock));

    pHQ->EnQ = pHQ->DeQ->Next;

    NuLockUnLock(&(pHQ->DeQLock));
    NuLockUnLock(&(pHQ->EnQLock));

    return;
}

