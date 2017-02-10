#include "NuHashMap.h"

/* Internal function */
/* ============================================================================== */
static int _NuHashMapCompareFn(void *v1, void *v2)
{
    int         iRC = 0;
    NuHashKey_t *pKey1 = (NuHashKey_t *)v1;
    NuHashKey_t *pKey2 = (NuHashKey_t *)v2;

    if(pKey1->Len != pKey2->Len)
        NUGOTO(iRC, -1, EXIT);

    iRC = memcmp(pKey1->Content, pKey2->Content, pKey1->Len);
        
EXIT:
    return iRC;
}

static int _NuHashMapFn(void *val)
{
    return NU_OK;
}

/* ----- Map functions ----- */
/* ============================================================================== */
int NuHashMapNew(NuHashMap_t **pMap, int Num)
{
    return NuHashMapNew2(pMap, Num, NULL, NULL, NULL, NULL);
}

int NuHashMapNew2(NuHashMap_t **pMap, int Num, NuHashFn Hash_fn, 
                                                NuCompareFn Compare_fn,
                                                NuDestroyFn FreeKey_fn,
                                                NuDestroyFn FreeValue_fn)
{
    return NuHashMapNew3(pMap, Num, HASH_DEFAULT_KEY_SIZE, HASH_DEFAULT_VALUE_SIZE, Hash_fn, Compare_fn, FreeKey_fn, FreeValue_fn);
}

int NuHashMapNew3(NuHashMap_t **pMap, int Num, size_t klen, size_t vlen, NuHashFn Hash_fn, 
                                                                        NuCompareFn Compare_fn,
                                                                        NuDestroyFn FreeKey_fn,
                                                                        NuDestroyFn FreeValue_fn)
{
    int             iRC = NU_OK;
    unsigned int    default_size = 0;

    (*pMap) = (NuHashMap_t *)malloc(sizeof(NuHashMap_t));
    if ((*pMap) == NULL)
        NUGOTO(iRC, NU_MALLOC_FAIL, EXIT);

    default_size = iRC = NuHashNew2(&((*pMap)->pHash), Num, Hash_fn, (Compare_fn == NULL) ? &_NuHashMapCompareFn : Compare_fn);
    NUCHKRC(iRC, EXIT);

    iRC = NuBufferNew(&((*pMap)->KeyBuffer), klen, default_size);
    NUCHKRC(iRC, EXIT);

    iRC = NuBufferNew(&((*pMap)->ValBuffer), vlen, default_size);
    NUCHKRC(iRC, EXIT);

    (*pMap)->default_klen = klen;
    (*pMap)->default_vlen = vlen;
    (*pMap)->FreeKey_fn = (FreeKey_fn == NULL) ? &_NuHashMapFn : FreeKey_fn;
    (*pMap)->FreeValue_fn = (FreeValue_fn == NULL) ? &_NuHashMapFn : FreeValue_fn;

    NuLockInit(&((*pMap)->Lock), &NuLockType_NULL);
EXIT:
    if(iRC < 0)
    {
        if((*pMap)->pHash != NULL)
            NuHashFree((*pMap)->pHash);

        if((*pMap)->KeyBuffer != NULL)
            NuBufferFree((*pMap)->KeyBuffer);

        if((*pMap)->ValBuffer != NULL)
            NuBufferFree((*pMap)->ValBuffer);

        if((*pMap) != NULL)
            free((*pMap));
    }
    else
        iRC = default_size;

    return iRC;
}

int NuHashMapSetThreadSafe(NuHashMap_t *pMap)
{
    NuLockDestroy(&(pMap->Lock));
    NuLockInit(&(pMap->Lock), &NuLockType_Mutex);

    return NU_OK;
}

void NuHashMapFree(NuHashMap_t *pMap)
{
    NuBufferFree(pMap->KeyBuffer);
    NuBufferFree(pMap->ValBuffer);
    NuHashFree(pMap->pHash);

    NuLockDestroy(&(pMap->Lock));

    free(pMap);

    return;
}

int NuHashMapAdd(NuHashMap_t *pMap, void *key, size_t klen, void *val, size_t vlen)
{
    int             iRC = NU_OK;
    unsigned int    idx = 0;
    void            *KeyBuf = NULL, *ValBuf = NULL;
    NuHash_t        *pHash = pMap->pHash;

    NuLockLock(&(pMap->Lock));

    if(NuHashSearch(pHash, key, klen, &idx) == NULL)
    {
        KeyBuf = NuBufferGet(pMap->KeyBuffer);
        memcpy(KeyBuf, key, pMap->default_klen);

        ValBuf = NuBufferGet(pMap->ValBuffer);
        memcpy(ValBuf, val, pMap->default_vlen);

        NuHashAdd(pHash, KeyBuf, klen, ValBuf, vlen, idx);
    }
    else
        iRC = NU_DUPLICATE;

    NuLockUnLock(&(pMap->Lock));

    return iRC;
}

void NuMultiHashMapAdd(NuHashMap_t *pMap, void *key, size_t klen, void *val, size_t vlen)
{
    unsigned int    idx = 0;
    void            *KeyBuf = NULL, *ValBuf = NULL;
    NuHashItem_t    *pItem = NULL;
    NuHash_t        *pHash = pMap->pHash;

    NuLockLock(&(pMap->Lock));

    ValBuf = NuBufferGet(pMap->ValBuffer);
    memcpy(ValBuf, val, pMap->default_vlen);

    if((pItem = NuHashSearch(pHash, key, klen, &idx)) == NULL)
    {
        KeyBuf = NuBufferGet(pMap->KeyBuffer);
        memcpy(KeyBuf, key, pMap->default_klen);

        NuHashAdd(pHash, KeyBuf, klen, ValBuf, vlen, idx);
    }
    else
        NuMultiHashAdd(pHash, pItem, ValBuf, vlen);

    NuLockUnLock(&(pMap->Lock));

    return;
}

void NuHashMapRmv(NuHashMap_t *pMap, void *key, size_t klen)
{
    void            *pKey = NULL;
    unsigned int    idx = 0;
    NuHashItem_t    *pItem = NULL;  
    NuHash_t        *pHash = pMap->pHash;

    NuLockLock(&(pMap->Lock));

    if((pItem = NuHashSearch(pHash, key, klen, &idx)) != NULL)
    {
        NuBufferPut(pMap->ValBuffer, pItem->Value);

        if((pKey = NuHashOneTimeRmvItem(pHash, pItem, idx)) != NULL)
            NuBufferPut(pMap->KeyBuffer, pKey);
    }

    NuLockUnLock(&(pMap->Lock));

    return;
}

void NuMultiHashMapRmv(NuHashMap_t *pMap , void *key, size_t klen)
{
    void            *pKey = NULL;
    unsigned int    idx = 0;
    NuHashItem_t    *pItem = NULL, *pTmpItem = NULL;
    NuHash_t        *pHash = pMap->pHash;

    NuLockLock(&(pMap->Lock));

    pItem = NuHashSearch(pHash, key, klen, &idx);
    while(pItem != NULL)
    {
        pTmpItem = pItem;

        NuBufferPut(pMap->ValBuffer, pTmpItem->Value);
        pItem = NuHashRight(pItem);

        if((pKey = NuHashOneTimeRmvItem(pHash, pTmpItem, idx)) != NULL)
            NuBufferPut(pMap->KeyBuffer, pKey);
    }

    NuLockUnLock(&(pMap->Lock));

    return;
}

int NuHashMapUpd(NuHashMap_t *pMap, void *key, size_t klen, void *val, size_t vlen)
{
    int             iRC = NU_OK;
    unsigned int    idx = 0;
    NuHashItem_t    *pItem = NULL;

    NuLockLock(&(pMap->Lock));

    if((pItem = NuHashSearch(pMap->pHash, key, klen, &idx)) != NULL)
    {
        memcpy(pItem->Value, val, pMap->default_vlen);
        pItem->Vlen = vlen;
    }
    else
        iRC = NU_NOTFOUND;

    NuLockUnLock(&(pMap->Lock));

    return iRC;
}

int NuMultiHashMapUpd(NuHashMap_t *pMap , void *key, size_t klen, void *val, size_t vlen)
{
    unsigned int    idx = 0;
    NuHashItem_t    *pItem = NULL;

    NuLockLock(&(pMap->Lock));

    pItem = NuHashSearch(pMap->pHash, key, klen, &idx);
    while(pItem != NULL)
    {
        memcpy(pItem->Value, val, pMap->default_vlen);
        pItem->Vlen = vlen;

        pItem = NuHashRight(pItem);
    }

    NuLockUnLock(&(pMap->Lock));

    return NU_OK;
}

/* ----- Iterator functions ----- */
/* ============================================================================== */
int NuHashMapItGetAll(NuHashMap_t *pMap, NuHashMapIterator_t *pItPtr)
{
    pItPtr->pMap = pMap;
    return NuHashItGetAll(pMap->pHash, &(pItPtr->HashIt));
}

int NuHashMapItFind(NuHashMap_t *pMap, void *key, size_t klen, NuHashMapIterator_t *pItPtr)
{
    int iRC = NU_OK;

    NuLockLock(&(pMap->Lock));

    iRC = NuHashItFind(pMap->pHash, key, klen, &(pItPtr->HashIt));
    pItPtr->pMap = pMap;

    NuLockUnLock(&(pMap->Lock));
 
    return iRC;
}

int NuHashMapItPrev(NuHashMapIterator_t *pItPtr)
{
    return NuHashItPrev(&(pItPtr->HashIt));
}

int NuHashMapItNext(NuHashMapIterator_t *pItPtr)
{
    return NuHashItNext(&(pItPtr->HashIt));
}

void NuHashMapItLock(NuHashMapIterator_t *pItPtr)
{
    NuLockLock(&(pItPtr->pMap->Lock));
    return;
}

void NuHashMapItUnLock(NuHashMapIterator_t *pItPtr)
{
    NuLockUnLock(&(pItPtr->pMap->Lock));
    return;
}

void *NuHashMapItGetValue(NuHashMapIterator_t *pItPtr)
{
    return NuHashItGetValue(&(pItPtr->HashIt));
}

int NuHashMapItSetValue(NuHashMapIterator_t *pItPtr, void *val, size_t vlen)
{
    NuHashMap_t     *pMap = pItPtr->pMap;
    NuHashItem_t    *pItem = NuHashItGetItem(&(pItPtr->HashIt));

    NuLockLock(&(pMap->Lock));

    NuHashMapUpd(pMap, NuHashGetKey(pItem), NuHashGetKeyLen(pItem), val, vlen);

    NuLockUnLock(&(pMap->Lock));

    return NU_OK;
}

void NuHashMapItRmvByItem(NuHashMapIterator_t *pItPtr)
{
    NuHashItRmvByItem(&(pItPtr->HashIt));
    return;
}

void ShowAllMapData(NuHashMap_t *pMap)
{
    ShowAllHashData(pMap->pHash);
    return;
}

