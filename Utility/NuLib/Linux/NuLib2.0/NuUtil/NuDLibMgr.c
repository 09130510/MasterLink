
#include <dlfcn.h>
#include "NuCommon.h"
#include "NuBuffer.h"
#include "NuHash.h"
#include "NuStr.h"
#include "NuLock.h"
#include "NuFile.h"
#include "NuCBMgr.h"

typedef struct _NuDLibMgrItem_t
{
    NuStr_t             *Name;
    void                *Hdlr;
    NuCBMgrTrigger_t    *UnLoadEvent;
} NuDLibMgrItem_t;

#define NuDLibMgr_HashSize   32

typedef struct _NuDLibMgr_t
{
    NuHash_t    *htDLibs;           /* key = name, val = NuDLibItem_t */
    NuBuffer_t  *MemBuf;            /* for all name, key & items */
    NuStr_t     *KeyCache;
    NuLock_t    Lock;
} NuDLibMgr_t;

static NuDLibMgr_t *_DLibMgr = NULL;

/* ----- Internal functions ----- */
/* ============================================================================== */
static void _NuDLibMgrInit(void)
{
    if(!_DLibMgr)
    {
        _DLibMgr = (NuDLibMgr_t *)malloc(sizeof(NuDLibMgr_t));

        NuHashNew(&(_DLibMgr->htDLibs), NuDLibMgr_HashSize);

        NuBufferNew(&(_DLibMgr->MemBuf), sizeof(NuDLibMgrItem_t), 128);

        NuStrNew(&(_DLibMgr->KeyCache), "\0");

        NuLockInit(&(_DLibMgr->Lock), &NuLockType_Mutex);
    }

    return;
}

/* ----- public functions ----- */
/* ============================================================================== */
int NuDLibMgrLoad(char *AliasName, char *DLibPath, int Flag, char **Err)
{
    int             RC = NU_OK;
    NuDLibMgrItem_t *pDLibMgrItem = NULL;

    unsigned int    Idx = 0;
    NuHashItem_t    *pHashItem = NULL;
    void            *pHdlr = NULL;

    *Err = NULL;

    if(!_DLibMgr)
    {
        _NuDLibMgrInit();
    }

    if(NuIsFile(DLibPath) != NU_OK)
    {
        NUGOTO(RC, NU_FAIL, EXIT);
    }

    if(!(pHdlr = dlopen(DLibPath, RTLD_NOW | Flag)))
    {
        *Err = dlerror();
        NUGOTO(RC, NU_FAIL, EXIT);
    }

    NuLockLock(&(_DLibMgr->Lock));

    if(!(pHashItem = NuHashSearch(_DLibMgr->htDLibs, AliasName, strlen(AliasName), &Idx)))
    {
        pDLibMgrItem = (NuDLibMgrItem_t *)NuBufferGet(_DLibMgr->MemBuf);
        NuStrNew(&(pDLibMgrItem->Name), AliasName);
        pDLibMgrItem->UnLoadEvent = NULL;

        pDLibMgrItem->Hdlr = pHdlr; 

        NuHashAdd(_DLibMgr->htDLibs, NuStrGet(pDLibMgrItem->Name), NuStrSize(pDLibMgrItem->Name), pDLibMgrItem, sizeof(NuDLibMgrItem_t), Idx);
    }
    else
    {
        pDLibMgrItem = (NuDLibMgrItem_t *)NuHashGetValue(pHashItem);
        if(pDLibMgrItem->Hdlr != pHdlr)
        {
            NUGOTO(RC, NU_FAIL, EXIT);
        }

        pDLibMgrItem->Hdlr = pHdlr;
    }

EXIT:
    NuLockUnLock(&(_DLibMgr->Lock));
    return RC;
}

int NuDLibMgrLoadToGlobal(char *AliasName, char *DLibPath, char **Err)
{
    return NuDLibMgrLoad(AliasName, DLibPath, RTLD_GLOBAL, Err);
}

int NuDLibMgrLoadToLocal(char *AliasName, char *DLibPath, char **Err)
{
    return NuDLibMgrLoad(AliasName, DLibPath, RTLD_LOCAL, Err);
}

void NuDLibMgrUnLoad(char *AliasName, char **Err)
{
    unsigned int    Idx = 0;
    NuHashItem_t    *pHashItem = NULL;
    NuDLibMgrItem_t *pDLibMgrItem = NULL;

    *Err = NULL;

    if(!_DLibMgr)
    {
        _NuDLibMgrInit();
    }

    NuLockLock(&(_DLibMgr->Lock));

    /* remove from hash */
    if((pHashItem = NuHashSearch(_DLibMgr->htDLibs, AliasName, strlen(AliasName), &Idx)) != NULL)
    {
        NuHashOneTimeRmvItem(_DLibMgr->htDLibs, pHashItem, Idx);

        pDLibMgrItem = (NuDLibMgrItem_t *)NuHashGetValue(pHashItem);

        if(pDLibMgrItem->UnLoadEvent != NULL)
        {
            NuCBMgrRaiseEvent(pDLibMgrItem->UnLoadEvent, AliasName);
            NuCBMgrDelTrigger(pDLibMgrItem->UnLoadEvent);
            pDLibMgrItem->UnLoadEvent = NULL;
        }

        dlclose(pDLibMgrItem->Hdlr);
        *Err = dlerror();

        pDLibMgrItem->Hdlr = NULL;
    }

    NuLockUnLock(&(_DLibMgr->Lock));
    return;
}

void *NuDLibMgrGetFn(char *AliasName, char *FnName, char **Err)
{
    void            *pFn = NULL;
    unsigned int    SoIdx = 0, FnIdx = 0;
    NuHashItem_t    *pHashItem = NULL;
    NuDLibMgrItem_t *pDLibMgrItem = NULL;
    NuStr_t         *pKey = NULL;

    *Err = NULL;

    if(!_DLibMgr)
    {
        _NuDLibMgrInit();
    }

    NuLockLock(&(_DLibMgr->Lock));

    pKey = _DLibMgr->KeyCache;

    NuStrClear(pKey);
    NuStrCpy(pKey, AliasName);
    NuStrCat(pKey, "::");
    NuStrCat(pKey, FnName);

    if(!(pHashItem = NuHashSearch(_DLibMgr->htDLibs, NuStrGet(pKey), NuStrSize(pKey), &FnIdx)))
    { /* Not exists, try load it. */
        if((pHashItem = NuHashSearch(_DLibMgr->htDLibs, AliasName, strlen(AliasName), &SoIdx)) != NULL)
        {
            pDLibMgrItem = (NuDLibMgrItem_t *)NuBufferGet(_DLibMgr->MemBuf);
            NuStrNew(&(pDLibMgrItem->Name), NuStrGet(pKey));
            pDLibMgrItem->UnLoadEvent = NULL;

            pDLibMgrItem->Hdlr = dlsym(((NuDLibMgrItem_t *)NuHashGetValue(pHashItem))->Hdlr, FnName);
            if(!((*Err) = dlerror()))
            {
                NuStrClear(pKey);
                NuStrCpy(pKey, AliasName);
                NuStrCat(pKey, "::");

                if(!(pHashItem = NuHashSearch(_DLibMgr->htDLibs, NuStrGet(pKey), NuStrSize(pKey), &SoIdx)))
                {
                    NuHashAdd(_DLibMgr->htDLibs, NuStrGet(pDLibMgrItem->Name), NuStrSize(pKey), pDLibMgrItem, sizeof(NuDLibMgrItem_t), SoIdx);
                }
                else
                {
                    NuMultiHashAdd(_DLibMgr->htDLibs, pHashItem, pDLibMgrItem, sizeof(NuDLibMgrItem_t));
                }

                pHashItem = NuHashAdd(_DLibMgr->htDLibs, NuStrGet(pDLibMgrItem->Name), NuStrSize(pDLibMgrItem->Name), pDLibMgrItem, sizeof(NuDLibMgrItem_t), FnIdx);
 
                pFn = pDLibMgrItem->Hdlr;
            }
            else
            { /* Load fn failed. */
                NuStrFree(pDLibMgrItem->Name);
                NuBufferPut(_DLibMgr->MemBuf, pDLibMgrItem);
                pFn = NULL;
            }
        }
    }
    else
    { /* Found. */
        pDLibMgrItem = (NuDLibMgrItem_t *)NuHashGetValue(pHashItem);
        if(!(pFn = pDLibMgrItem->Hdlr))
        { /* Might be unloaded or reloaded */
            if((pHashItem = NuHashSearch(_DLibMgr->htDLibs, AliasName, strlen(AliasName), &SoIdx)) != NULL)
            {
                pDLibMgrItem->Hdlr = dlsym(((NuDLibMgrItem_t *)NuHashGetValue(pHashItem))->Hdlr, FnName);
                if(!((*Err) = dlerror()))
                {
                    pFn = pDLibMgrItem->Hdlr;
                }
            }
        }
    }

    NuLockUnLock(&(_DLibMgr->Lock));

    return pFn;
}

void NuDLibMgrRegisterUnLoadEvent(char *AliasName, NuCBFn UnLoadCB, void *CBArgu)
{
    unsigned int    Idx = 0;
    NuHashItem_t    *pHashItem = NULL;
    NuDLibMgrItem_t *pDLibMgrItem = NULL;
    NuStr_t         *pKey = NULL;

    if(!_DLibMgr)
    {
        _NuDLibMgrInit();
    }

    NuLockLock(&(_DLibMgr->Lock));

    pKey = _DLibMgr->KeyCache;

    NuStrClear(pKey);
    NuStrCpy(pKey, AliasName);
    
    if(UnLoadCB != NULL)
    {
        if((pHashItem = NuHashSearch(_DLibMgr->htDLibs, NuStrGet(pKey), NuStrSize(pKey), &Idx)) != NULL)
        {
            pDLibMgrItem = (NuDLibMgrItem_t *)NuHashGetValue(pHashItem);

            if(!(pDLibMgrItem->UnLoadEvent))
            {
                pDLibMgrItem->UnLoadEvent = NuCBMgrAddTrigger();
            }

            NuCBMgrRegisterEvent(pDLibMgrItem->UnLoadEvent, UnLoadCB, CBArgu);
        }
    }

    NuLockUnLock(&(_DLibMgr->Lock));

    return;
}

