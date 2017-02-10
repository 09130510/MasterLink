
#include "NuCommon.h"
#include "NuUtil.h"
#include "NuLock.h"

typedef struct _NuCBMgrTrigger_t
{
    base_vector_t       *EventVec;
    NuLock_t            Lock;
} NuCBMgrTrigger_t;

typedef struct _NuEventHdlr_t
{
    NuCBMgrTrigger_t    *Trigger;
    NuCBFn              Fn;
    void                *Argu;
} NuEventHdlr_t;

static NuEventHdlr_t *_GetEventHdlr(NuCBMgrTrigger_t *pTrigger, NuCBFn CBFn, void *Argu)
{
    NuEventHdlr_t   *pHdlr = (NuEventHdlr_t *)malloc(sizeof(NuEventHdlr_t));
    if(!pHdlr)
    {
        return NULL;
    }

    pHdlr->Trigger = pTrigger;
    pHdlr->Fn = CBFn;
    pHdlr->Argu = Argu;

    return pHdlr;
}

NuCBMgrTrigger_t *NuCBMgrAddTrigger(void)
{
    NuCBMgrTrigger_t    *pTrigger = (NuCBMgrTrigger_t *)malloc(sizeof(NuCBMgrTrigger_t));

    if(!pTrigger)
    {
        return NULL;
    }

    if(base_vector_new(&(pTrigger->EventVec), 10) < 0)
    {
        free(pTrigger);
        return NULL;
    }

    if(NuLockInit(&(pTrigger->Lock), &NuLockType_Spin) < 0)
    {
        free(pTrigger);
        return NULL;
    }

    return pTrigger;
}

void NuCBMgrDelTrigger(NuCBMgrTrigger_t *pTrigger)
{
    if(pTrigger->EventVec != NULL)
    {
        base_vector_free(pTrigger->EventVec);
    }

    NuLockDestroy(&(pTrigger->Lock));

    free(pTrigger);
    return;
}

NuEventHdlr_t *NuCBMgrRegisterEvent(NuCBMgrTrigger_t *pTrigger, NuCBFn CBFn, void *Argu)
{
    NuEventHdlr_t       *pHdlr = NULL;
    base_vector_t       *pVec = pTrigger->EventVec;
    base_vector_it      VecIt;

    base_vector_it_set(VecIt, pVec);
    while(VecIt != base_vector_it_end(pVec))
    {
        if(((pHdlr = (NuEventHdlr_t *)(*VecIt))->Fn) == &NuCBFn_Default)
        {
            pHdlr->Argu = Argu;
            pHdlr->Fn = CBFn;

            return pHdlr;
        }

        ++ VecIt;
    }

    pHdlr = _GetEventHdlr(pTrigger, CBFn, Argu);

    NuLockLock(&(pTrigger->Lock));
    base_vector_push(pVec, pHdlr);
    NuLockUnLock(&(pTrigger->Lock));

    return pHdlr;
}

void NuCBMgrUnRegisterEvent(NuEventHdlr_t *pHdlr)
{
    pHdlr->Fn = &NuCBFn_Default;
    pHdlr->Argu = NULL;

    return;
}

void NuCBMgrResetTrigger(NuCBMgrTrigger_t *pTrigger)
{
    base_vector_t   *pVec = pTrigger->EventVec;
    base_vector_it  VecIt;

    base_vector_it_set(VecIt, pVec);
    while(VecIt != base_vector_it_end(pVec))
    {
        NuCBMgrUnRegisterEvent(*VecIt);
        ++ VecIt;
    }

    return;
}

void NuCBMgrRaiseEvent(NuCBMgrTrigger_t *pTrigger, void *Argu)
{
    base_vector_t   *pVec = pTrigger->EventVec;
    base_vector_it  VecIt;
    NuEventHdlr_t   *pHdlr = NULL;

    NuLockLock(&(pTrigger->Lock));
    base_vector_it_set(VecIt, pVec);
    while(VecIt != base_vector_it_end(pVec))
    {
        pHdlr = (NuEventHdlr_t *)(*VecIt);

        (pHdlr->Fn)(Argu, pHdlr->Argu);

        ++ VecIt;
    }
    NuLockUnLock(&(pTrigger->Lock));

    return;
}


