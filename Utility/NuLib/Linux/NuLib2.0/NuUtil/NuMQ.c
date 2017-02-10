
#include "NuMQ.h"
/* Queue function */
/* ====================================================================== */
int NuMQNew(NuMQ_t **pMQ)
{
    return NuMQNew2(pMQ, NuMQ_DEFAULT_NUM, NuMQ_DEFAULT_SIZE);
}

int NuMQNew2(NuMQ_t **pMQ, int MQNo, size_t MsgSize)
{
    int         iRC = NU_OK, Cnt = 0;
    NuMQ_Msg_t  *pMsg = NULL;

    if(!((*pMQ) = (NuMQ_t *)malloc(sizeof(NuMQ_t) + (sizeof(NuMQ_Msg_t) + MsgSize) * MQNo)))
    {
        return NU_MALLOC_FAIL;
    }

    (*pMQ)->EnQ = pMsg = &((*pMQ)->Msg);

    for(Cnt = 0; Cnt < MQNo; ++ Cnt)
    {
        pMsg->DataLen = 0;
        pMsg->mType = NuMQ_DEFAULT_MTYPE;
        pMsg->Next = (NuMQ_Msg_t *)malloc(sizeof(NuMQ_Msg_t));
        pMsg->Next = pMsg + 1;
        ++ pMsg;
    }

    pMsg->DataLen = 0;
    pMsg->mType = NuMQ_DEFAULT_MTYPE;
    pMsg->Next = (*pMQ)->EnQ;

    (*pMQ)->DeQ = pMsg;

    iRC = NuLockInit(&((*pMQ)->EnQLock), &NuLockType_Mutex);
    NUCHKRC(iRC, EXIT);

    iRC = NuLockInit(&((*pMQ)->DeQLock), &NuLockType_Mutex);
    NUCHKRC(iRC, EXIT);

    iRC = NuBlockingNew(&((*pMQ)->QFullBlocking), &NuBlocking_CondVar, NULL);
    NUCHKRC(iRC, EXIT);

    iRC = NuBlockingNew(&((*pMQ)->QEmptyBlocking), &NuBlocking_CondVar, NULL);
    NUCHKRC(iRC, EXIT);

    (*pMQ)->MsgSize = MsgSize;

EXIT:
    if(iRC < 0)
    {
        NuMQFree(*pMQ);
    }

    return iRC;
}

void NuMQFree(NuMQ_t *pMQ)
{
    if(pMQ != NULL)
    {
        NuLockDestroy(&(pMQ->EnQLock));
        NuLockDestroy(&(pMQ->DeQLock));
        NuBlockingFree(pMQ->QFullBlocking, NULL);
        NuBlockingFree(pMQ->QEmptyBlocking, NULL);

        free(pMQ);
    }

    return;
}

int NuMQEnqueueCB(NuMQ_t *pMQ, char mType, NuMQ_EnQCB EnQFn, void *Argu)
{
    NuMQ_Msg_t  *pEnQ = NULL;

    NuLockLock(&(pMQ->EnQLock));

    while(NuMQIsFull(pMQ))
    {
        NuBlockingBlock(pMQ->QFullBlocking, NULL);
    }

    pEnQ = pMQ->EnQ;

    EnQFn(pEnQ->Item, &(pEnQ->DataLen), Argu);
    pEnQ->mType = mType;

    pMQ->EnQ = pEnQ->Next;

    NuBlockingWake(pMQ->QEmptyBlocking, NULL);

    NuLockUnLock(&(pMQ->EnQLock));


    return NU_OK;
}

int NuMQEnqueue(NuMQ_t *pMQ, char mType, void *Item, size_t Len)
{
    size_t      MLen = NuMin(Len, pMQ->MsgSize);
    NuMQ_Msg_t  *pEnQ = NULL;

    NuLockLock(&(pMQ->EnQLock));

    while(NuMQIsFull(pMQ))
    {
        NuBlockingBlock(pMQ->QFullBlocking, NULL);
    }

    pEnQ = pMQ->EnQ;

    memcpy(pEnQ->Item, Item, MLen);
    pEnQ->DataLen = MLen;
    pEnQ->mType = mType;

    pMQ->EnQ = pEnQ->Next;

    NuBlockingWake(pMQ->QEmptyBlocking, NULL);

    NuLockUnLock(&(pMQ->EnQLock));

    return NU_OK;
}

int NuMQDequeue(NuMQ_t *pMQ, char *mType, void *Item, size_t *Len)
{
    NuMQ_Msg_t  *Tmp = NULL;

    NuLockLock(&(pMQ->DeQLock));

    while(NuMQIsEmpty(pMQ))
    {
        NuBlockingBlock(pMQ->QEmptyBlocking, NULL);
    }

    Tmp = pMQ->DeQ->Next;
    *Len = NuMin(*Len, Tmp->DataLen);
    memcpy(Item, Tmp->Item, *Len);
    *mType = Tmp->mType;

    Tmp->DataLen = 0;
    Tmp->mType = NuMQ_DEFAULT_MTYPE;

    pMQ->DeQ = Tmp;

    NuBlockingWake(pMQ->QFullBlocking, NULL);
 
    NuLockUnLock(&(pMQ->DeQLock));

    return NU_OK;
}

void NuMQClear(NuMQ_t *pMQ)
{
    NuLockLock(&(pMQ->EnQLock));
    NuLockLock(&(pMQ->DeQLock));

    pMQ->EnQ = pMQ->DeQ->Next;

    NuLockUnLock(&(pMQ->DeQLock));
    NuLockUnLock(&(pMQ->EnQLock));

    return;
}

