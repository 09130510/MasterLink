
#include "NuHash.h"
#include "NuLock.h"

#ifndef _NUHASHMAP_H
#define _NUHASHMAP_H

#ifdef __cplusplus
extern "C" {
#endif


/** 
 * @defgroup NuHashMapAPI [Utility] hash/multihash map object
 *  Simple hash/multihash map object. It provides a storagable hash table, which means it saves a copy of key and value, respectively, inside the hash table.\n
 *  @{
 * */

/** 
 * @brief Hashmap struct.
 * @note The main object of hashmap.
 */ 
typedef struct _NuHashMap_t
{
    NuHash_t    *pHash;
    NuBuffer_t  *KeyBuffer;
    NuBuffer_t  *ValBuffer;

    int         default_klen;
    int         default_vlen;

    NuLock_t    Lock;

    NuDestroyFn FreeKey_fn;
    NuDestroyFn FreeValue_fn;
} NuHashMap_t;

/** 
 * @brief Iterator for hashmap.
 * @note The main object of hashmap iterator.
 */ 
typedef struct _NuHashMapIterator_t
{
    NuHashIterator_t    HashIt;
    NuHashMap_t         *pMap;
} NuHashMapIterator_t;

/* ----- Map functions ----- */
/* ============================================================================== */
/** 
 * @brief Variation of constructor of hashmap. 
 * @note Using the default key size, value size, hash function, compare function, key free function and value free function instead of setting it.
 *
 * @param  [out] pHash     : Pointer address to NuHashMap_t.
 * @param  [in]  Num       : Approximation of number of buckets.
 *
 * @return int
 * @retval 0 : Success.
 * @retval others : Fail.
 */
int NuHashMapNew(NuHashMap_t **pMap, int Num);

/** 
 * @brief Variation of constructor of hashmap.
 * @note Using the default key size and value size instead of setting it.
 *
 * @param  [out] pHash        : Pointer address to NuHashMap_t.
 * @param  [in]  Num          : Approximation of number of buckets.
 * @param  [in]  Hash_fn      : Function used for calculating hash value.
 * @param  [in]  Compare_fn   : Function used for comparing keys when hash value corrupts.
 * @param  [in]  FreeKey_fn   : Function used for free key.
 * @param  [in]  FreeValue_fn : Function used for free value.
 *
 * @return int
 * @retval 0 : Success
 * @retval others : Fail
 */
int NuHashMapNew2(NuHashMap_t **pMap, int Num, NuHashFn Hash_fn, 
                                             NuCompareFn Compare_fn,
                                             NuDestroyFn FreeKey_fn,
                                             NuDestroyFn FreeValue_fn);

/** 
 * @brief Constructor of hashmap.
 * @note Create a hashmap object.
 *
 * @param  [out] pHash        : Pointer address to NuHashMap_t.
 * @param  [in]  Num          : Approximation of number of buckets.
 * @param  [in]  klen         : Maximum length of Key.
 * @param  [in]  vlen         : Maximum length of value.
 * @param  [in]  Hash_fn      : Function used for calculating hash value.
 * @param  [in]  Compare_fn   : Function used for comparing keys when hash value corrupts.
 * @param  [in]  FreeKey_fn   : Function used for free key.
 * @param  [in]  FreeValue_fn : Function used for free value.
 *
 * @return int
 * @retval 0 : Success
 * @retval others : Fail
 */
int NuHashMapNew3(NuHashMap_t **pMap, int Num, size_t klen, size_t vlen, NuHashFn Hash_fn, 
                                                                       NuCompareFn Compare_fn,
                                                                       NuDestroyFn FreeKey_fn,
                                                                       NuDestroyFn FreeValue_fn);

/** 
 * @brief Distructor of hashmap. 
 * @note Terminate a hashmap object.
 *
 * @param  [in] pHash     : Pointer to NuHashMap_t.
 *
 * @return void
 */
void NuHashMapFree(NuHashMap_t *pMap);

/** 
 * @brief Set to thread-safe mode.
 * @note In the thread-safe mode, each modification of hashmap would be contained in an internal lock.
 *
 * @param  [in] pHash     : Pointer to NuHashMap_t.
 *
 * @return int 
 * @retval 0 : Success
 * @retval others : Fail
 */
int NuHashMapSetThreadSafe(NuHashMap_t *pMap);

/** 
 * @brief Add a mapping to the hashmap. 
 *
 * @param  [in] pHash     : Pointer to NuHashMap_t.
 * @param  [in] key       : Pointer to the key.
 * @param  [in] klen      : Length of the key.
 * @param  [in] val       : Pointer to the value.
 * @param  [in] vlen      : Length of the value.
 *
 * @return int 
 * @retval 0 : Success
 * @retval others : Fail
 */
int NuHashMapAdd(NuHashMap_t *pMap, void *key, size_t klen, void *val, size_t vlen);

/** 
 * @brief Remove a mapping from the hashmap. 
 *
 * @param  [in] pHash     : Pointer to NuHashMap_t.
 * @param  [in] key       : Pointer to the key.
 * @param  [in] klen      : Length of the key.
 *
 * @return void 
 */
void NuHashMapRmv(NuHashMap_t *pMap , void *key, size_t klen);

/** 
 * @brief Update a value of mapping in the hashmap. 
 *
 * @param  [in] pHash     : Pointer to NuHashMap_t.
 * @param  [in] key       : Pointer to the key.
 * @param  [in] klen      : Length of the key.
 * @param  [in] val       : Pointer to the value.
 * @param  [in] vlen      : Length of the value.
 *
 * @return int 
 * @retval 0 : Success
 * @retval others : Fail
 */
int NuHashMapUpd(NuHashMap_t *pMap , void *key, size_t klen, void *val, size_t vlen);

/** 
 * @brief Add a mapping to the multi hashmap. 
 *
 * @param  [in] pHash     : Pointer to NuHashMap_t.
 * @param  [in] key       : Pointer to the key.
 * @param  [in] klen      : Length of the key.
 * @param  [in] val       : Pointer to the value.
 * @param  [in] vlen      : Length of the value.
 *
 * @return int 
 * @retval 0 : Success
 * @retval others : Fail
 */
void NuMultiHashMapAdd(NuHashMap_t *pMap, void *key, size_t klen, void *val, size_t vlen);

/** 
 * @brief Remove a mapping from the multi hashmap. 
 *
 * @param  [in] pHash     : Pointer to NuHashMap_t.
 * @param  [in] key       : Pointer to the key.
 * @param  [in] klen      : Length of the key.
 *
 * @return void 
 */
void NuMultiHashMapRmv(NuHashMap_t *pMap , void *key, size_t klen);

/** 
 * @brief Update a mapping in the multi hashmap. 
 *
 * @param  [in] pHash     : Pointer to NuHashMap_t.
 * @param  [in] key       : Pointer to the key.
 * @param  [in] klen      : Length of the key.
 * @param  [in] val       : Pointer to the value.
 * @param  [in] vlen      : Length of the value.
 *
 * @return int 
 * @retval 0 : Success
 * @retval others : Fail
 */
int NuMultiHashMapUpd(NuHashMap_t *pMap , void *key, size_t klen, void *val, size_t vlen);

/* ----- Iterator functions ----- */
/* ============================================================================== */
/** 
 * @brief Start the iterator from the scan mode. 
 * @note While the scan mode, following operations of iterator would be sequentially through the bucket array.
 *
 * @param  [in]  pHashMap  : Pointer to NuHashMap_t to be modified.
 * @param  [in]  pItPtr    : Pointer to hashmap iterator.
 *
 * @return int
 * @retval 0 : Success.
 * @retval Others : Failed.
 */
int NuHashMapItGetAll(NuHashMap_t *pMap, NuHashMapIterator_t *pItPtr);

/** 
 * @brief Start the iterator from the selecting mode. 
 * @note While the selecting mode, following operations of iterator would be only in the same key mapping.
 *
 * @param  [in]  pHashMap  : Pointer to NuHashMap_t to be modified.
 * @param  [in]  key       : Pointer to the key used for searching.
 * @param  [in]  klen      : Length of the key.
 * @param  [in]  pItPtr    : Pointer to hashmap iterator.
 *
 * @return int
 * @retval 0 : Success.
 * @retval Others : Failed.
 */
int NuHashMapItFind(NuHashMap_t *pMap, void *key, size_t klen, NuHashMapIterator_t *pItPtr);

/** 
 * @brief Moving next same hash value group to the current position of iterator. 
 * @note If there is no same hash value group any more, it would not take any action and return failed.
 *
 * @param  [in]  pItPtr    : Pointer to hashmap iterator.
 *
 * @return int
 * @retval 0 : Success.
 * @retval Others : Failed.
 */
int NuHashMapItNext(NuHashMapIterator_t *pItPtr);

/**
 * @brief Moving last same key item to the current position of iterator. 
 * @note If there is no same hash key item any more, it would not take any action and return failed.
 *
 * @param  [in]  pItPtr    : Pointer to hashmap iterator.
 *
 * @return int
 * @retval 0 : Success.
 * @retval Others : Failed.
 */
int NuHashMapItPrev(NuHashMapIterator_t *pItPtr);

/**
 * @brief Lock the hashmap which the iterator lies on. 
 *
 * @param  [in]  pItPtr    : Pointer to hashmap iterator.
 *
 * @return void
 */
void NuHashMapItLock(NuHashMapIterator_t *pItPtr);

/**
 * @brief Unlock the hashmap which the iterator lies on. 
 *
 * @param  [in]  pItPtr    : Pointer to hashmap iterator.
 *
 * @return void
 */
void NuHashMapItUnLock(NuHashMapIterator_t *pItPtr);

/**
 * @brief Get the value to the current position of iterator. 
 *
 * @param  [in]  pItPtr    : Pointer to hashmap iterator.
 *
 * @return void *
 */
void *NuHashMapItGetValue(NuHashMapIterator_t *pItPtr);

/**
 * @brief Set the value to the current position of iterator. 
 *
 * @param  [in]  pItPtr    : Pointer to hashmap iterator.
 * @param  [in]  value     : Pointer to the value used for setting.
 * @param  [in]  vlen      : Length of the value.
 *
 * @return void
 */
int NuHashMapItSetValue(NuHashMapIterator_t *pItPtr, void *val, size_t vlen);

/**
 * @brief Remove the item in the current position of iterator. 
 *
 * @param  [in]  pItPtr    : Pointer to hashmap iterator.
 *
 * @return void
 */
void NuHashMapItRmvByItem(NuHashMapIterator_t *pItPtr);

/**
 * @brief Print out all item of the iterator.
 * @note Debug using.
 *
 * @param  [in]  pItPtr    : Pointer to hashmap iterator.
 *
 * @return void
 */
void ShowAllMapData(NuHashMap_t *pMap);

#define NuHashMapGetNum(pMap)   (pMap)->pHash->num
#define NuHashMapGetANum(pMap)  (pMap)->pHash->anum

/** @} */

#ifdef __cplusplus
}
#endif

#endif /* _NUHASHMAP_H */

