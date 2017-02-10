
#include "NuCommon.h"
#include "NuUtil.h"
#include "NuBuffer.h"

#ifndef _NUHASH_H
#define _NUHASH_H

#ifdef __cplusplus
extern "C" {
#endif

/** 
 * @defgroup NuHashAPI [Utility] hash/multihash object
 *  Simple hash/multihash object. NuHash will only pointer to the key/value, but not keep copy of them.\n
 *  @{
 * */


#define HASH_DEFAULT_VALUE_SIZE	64
#define HASH_DEFAULT_KEY_SIZE	16

/** 
 * @brief Hash item struct.
 * @note The object used for indicating the value.
 */ 
typedef struct _NuHashItem_t
{
	base_list_node_t	Node;
	void				*Value;
	size_t				Vlen;
	void				*Hook;
} NuHashItem_t;

/** 
 * @brief Hash key struct.
 * @note Key information.
 */ 
typedef struct _NuHashKey_t
{
	void				*Content;
	size_t				Len;
} NuHashKey_t;

/** 
 * @brief Hash group struct.
 * @note For the multi-hash usage(mapping serveral values into a key).
 */ 
typedef struct _NuHashGroup_t
{
	base_list_t				List;
	NuHashKey_t				Key;
	struct _NuHashGroup_t	*Prev;
	struct _NuHashGroup_t	*Next;
} NuHashGroup_t;

/* hash table */
/* ----------------------------------------------------------- */
/** 
 * @brief Hash main struct.
 */ 
typedef struct _NuHash_t
{
	NuHashGroup_t	**items;

	unsigned int	num;
	unsigned int	anum;

	NuHashFn		Hash_fn;
	NuCompareFn		Compare_fn;
	NuBuffer_t		*ItemBuffer;
	NuBuffer_t		*GroupBuffer;
} NuHash_t;

#define	NuHashIteratorModeKey	0
#define	NuHashIteratorModeAll	1

/** 
 * @brief Hash iterator struct.
 * @note Object for operation in hash table with implemented functions.
 */ 
typedef struct _NuHashIterator_t
{
	int				iMode;        /* 0:show same key, 1:show all data */
	int				iIdx;
	NuHash_t		*pHash;
	NuHashGroup_t	*pGroup;
	NuHashItem_t	*pItem;
} NuHashIterator_t;

/* ----- Constructor/Distructor ----- */
/* ============================================================================== */
/** 
 * @brief Variation of constructor of hash table. 
 * @note Using the default hash function and compare function instead of setting it.
 *
 * @param  [out] pHash     : Pointer address to NuHash_t.
 * @param  [in]  Num       : Approximation of number of buckets.
 *
 * @return int
 * @retval 0 : Success.
 * @retval others : Fail.
 */
int NuHashNew(NuHash_t **pHash, int Num);

/** 
 * @brief Constructor of hash table. 
 * @note Create a hash table object.
 *
 * @param  [out] pHash     : Pointer address to NuHash_t.
 * @param  [in]  Num       : Approximation of number of buckets.
 * @param  [in]  Hash_fn   : Function used for calculating hash value.
 * @param  [in]  Compare_fn: Function used for comparing keys when hash value corrupts.
 *
 * @return int
 * @retval 0 : Success
 * @retval others : Fail
 */
int NuHashNew2(NuHash_t **pHash, int Num, NuHashFn Hash_fn, NuCompareFn Compare_fn); 

/** 
 * @brief Distructor of hash table. 
 * @note Terminate a hash table object.
 *
 * @param  [in] pHash     : Pointer to NuHash_t.
 *
 * @return void
 */
void NuHashFree(NuHash_t *pHash);

/** 
 * @brief Reset the hash table. 
 * @note Preserve all the structure of the hash table, but not the data included.
 *
 * @param  [in] pHash     : Pointer address to NuHash_t.
 *
 * @return void
 */
void NuHashClear(NuHash_t *pHash);

/* ----- Operation functions ----- */
/* ============================================================================== */
/** 
 * @brief Search the hash table through the set hash function. 
 * @note Nuccessarily function before modify operations to the hash table.
 *
 * @param  [in]  pHash     : Pointer to NuHash_t to be searched.
 * @param  [in]  key       : Pointer to the key used for searching.
 * @param  [in]  klen      : Length of the key.
 * @param  [out]  idx      : Order of bucket which the key mapped into.
 *
 * @return NuHashItem_t
 * @retval NULL : Could not found.
 * @retval Others : Pointer to the (first) hash item which the input key mapped.
 */
NuHashItem_t *NuHashSearch(NuHash_t *pHash, void *key, size_t klen, unsigned int *idx);

/** 
 * @brief Add a mapping to the hash table. 
 * @note It is only for the unique key hash table, make sure the key has not existed in the hash table, that is, search the first and confirm the result be failed.
 *
 * @param  [in]  pHash     : Pointer to NuHash_t to be added.
 * @param  [in]  key       : Pointer to the key used for adding the mapping.
 * @param  [in]  klen      : Length of the key.
 * @param  [in]  value     : Pointer to the value used for adding the mapping.
 * @param  [in]  vlen      : Length of the value.
 * @param  [in]  idx       : Gotten from the search result.
 *
 * @return NuHashItem_t
 * @retval NULL : Add failed, possibily reason is the key-mapping aleady exists.
 * @retval Others : Pointer to the hash item which the newly-added mapping.
 */
NuHashItem_t *NuHashAdd(NuHash_t *pHash, void *key, size_t klen, void *value, size_t vlen, unsigned int idx);

/** 
 * @brief Add a mapping to the hash table, multi-version. 
 * @note It is only for the multi key hash table, make sure the key has already existed in the hash table, that is, search the first and confirm the result be success.
 *
 * @param  [in]  pHash     : Pointer to NuHash_t to be added.
 * @param  [in]  pItem     : Pointer to hash item which has the same key in the hash table.
 * @param  [in]  value     : Pointer to the value used for adding the mapping.
 * @param  [in]  vlen      : Length of the value.
 *
 * @return NuHashItem_t
 * @retval NULL : Add failed, possibily reason is the key-mapping does not exists.
 * @retval Others : Pointer to the hash item which the newly-added mapping.
 */
NuHashItem_t *NuMultiHashAdd(NuHash_t *pHash, NuHashItem_t *pItem, void *Value, size_t Vlen);

/** 
 * @brief Remove a mapping from the hash table. 
 * @note For the scalability reason, this action will only take the mapping out from the hash table, but not really move the item/group to the buffer for further reusing. Call the remove-item/group-compleate function later to complete the removing.
 *
 * @param  [in]  pHash     : Pointer to NuHash_t to be modified.
 * @param  [in]  pItem     : Pointer to hash item which need to be removed.
 * @param  [in]  idx       : Gotten from the search result.
 *
 * @return NuHashGroup_t
 * @retval NULL : The mapping does not exist in the hash table, or there is mapping with sane key still in the hash table.
 * @retval Others : Pointer to the hash group which is removed after removing the item.
 */
NuHashGroup_t *NuHashRmvItem(NuHash_t *pHash, NuHashItem_t *pItem, unsigned int idx);

/** 
 * @brief Completing the remove. 
 * @note Put the hash item back to the buffer.
 *
 * @param  [in]  pHash     : Pointer to NuHash_t to be modified.
 * @param  [in]  pItem     : Pointer to hash item which need to be completed removed.
 *
 * @return void
 */
void NuHashRmvItemComplete(NuHash_t *pHash, NuHashItem_t *pItem);

/** 
 * @brief Completing the remove. 
 * @note Put the hash group back to the buffer.
 *
 * @param  [in]  pHash     : Pointer to NuHash_t to be modified.
 * @param  [in]  pGroup    : Pointer to hash group which need to be completed removed.
 *
 * @return void
 */
void NuHashRmvGroupComplete(NuHash_t *pHash, NuHashGroup_t *pGroup);

/** 
 * @brief Series combination of the removing actions. 
 * @note As its name, just call this function one time to do all the removing jobs.
 *
 * @param  [in]  pHash     : Pointer to NuHash_t to be modified.
 * @param  [in]  pItem     : Pointer to hash item which need to be completed removed.
 * @param  [in]  idx       : Gotten from the search result.
 *
 * @return void
 */
void *NuHashOneTimeRmvItem(NuHash_t *pHash, NuHashItem_t *pItem, unsigned int idx);

void NuHashItemSetData(NuHashItem_t *pItem, void *pVal, size_t vlen);

/** 
 * @brief Moving to the earlier same key mapping. 
 *
 * @param  [in]  pItem     : Pointer to hash item which need to be moved.
 *
 * @return NuHashItem_t
 * @retval NULL   : The input hash item is alredy the rightmost/oldest same key item.
 * @retval Others : The right one.
 */
NuHashItem_t *NuHashRight(NuHashItem_t *pItem);

/** 
 * @brief Moving to the later same key mapping. 
 *
 * @param  [in]  pItem     : Pointer to hash item which need to be moved.
 *
 * @return NuHashItem_t
 * @retval NULL   : The input hash item is alredy the leftmost/newest same key item.
 * @retval Others : The left one.
 */
NuHashItem_t *NuHashLeft(NuHashItem_t *pItem);

/** 
 * @brief Moving to the earliest same key mapping. 
 *
 * @param  [in]  pItem     : Pointer to hash item which need to be moved.
 *
 * @return NuHashItem_t
 * @retval ANY : The rightmost/earliest item of the same key mapping.
 */
NuHashItem_t *NuHashRightMost(NuHashItem_t *pItem);

/** 
 * @brief Moving to the oldest same key mapping. 
 *
 * @param  [in]  pItem     : Pointer to hash item which need to be moved.
 *
 * @return NuHashItem_t
 * @retval ANY : The leftmost/oldest item of the same key mapping.
 */
NuHashItem_t *NuHashLeftMost(NuHashItem_t *pItem);

#define	NuHashGetKey(pItem)		((NuHashGroup_t *)(base_list_get_list(&((pItem)->Node))))->Key.Content
#define	NuHashGetKeyLen(pItem)	((NuHashGroup_t *)(base_list_get_list(&((pItem)->Node))))->Key.Len
#define	NuHashGetValue(pItem)	(pItem)->Value
#define	NuHashGetVlen(pItem)	(pItem)->Vlen
#define	NuHashGetCnt(pItem)		base_list_items_cnt(base_list_get_list(&((pItem)->Node)))

/* ----- Iterator functions ----- */
/* ============================================================================== */
/** 
 * @brief Start the iterator from the scan mode. 
 * @note While the scan mode, following operations of iterator would be sequentially through the bucket array.
 *
 * @param  [in]  pHash     : Pointer to NuHash_t to be modified.
 * @param  [in]  pItPtr    : Pointer to hash iterator.
 *
 * @return int
 * @retval 0 : Success.
 * @retval Others : Failed.
 */
int NuHashItGetAll(NuHash_t *pHash, NuHashIterator_t *pItPtr);

/** 
 * @brief Start the iterator from the selecting mode. 
 * @note While the selecting mode, following operations of iterator would be only in the same key mapping.
 *
 * @param  [in]  pHash     : Pointer to NuHash_t to be modified.
 * @param  [in]  key       : Pointer to the key used for searching.
 * @param  [in]  klen      : Length of the key.
 * @param  [in]  pItPtr    : Pointer to hash iterator.
 *
 * @return int
 * @retval 0 : Success.
 * @retval Others : Failed.
 */
int NuHashItFind(NuHash_t *pHash, void *key, size_t klen, NuHashIterator_t *pItPtr);

/** 
 * @brief Moving next same hash value group to the current position of iterator. 
 * @note If there is no same hash value group any more, it would not take any action and return failed.
 *
 * @param  [in]  pItPtr    : Pointer to hash iterator.
 *
 * @return int
 * @retval 0 : Success.
 * @retval Others : Failed.
 */
int NuHashItNext(NuHashIterator_t *pItPtr);

/**
 * @brief Moving last same key item to the current position of iterator. 
 * @note If there is no same hash key item any more, it would not take any action and return failed.
 *
 * @param  [in]  pItPtr    : Pointer to hash iterator.
 *
 * @return int
 * @retval 0 : Success.
 * @retval Others : Failed.
 */
int NuHashItPrev(NuHashIterator_t *pItPtr);

/**
 * @brief Moving the latest key item to the current position of iterator. 
 *
 * @param  [in]  pItPtr    : Pointer to hash iterator.
 *
 * @return int
 * @retval 0 : Success.
 * @retval Others : Failed.
 */
int NuHashItTheFirst(NuHashIterator_t *pItPtr);

/**
 * @brief Get the key to the current position of iterator. 
 *
 * @param  [in]  pItPtr    : Pointer to hash iterator.
 *
 * @return void *
 */
void *NuHashItGetKey(NuHashIterator_t *pItPtr);

/**
 * @brief Get the value to the current position of iterator. 
 *
 * @param  [in]  pItPtr    : Pointer to hash iterator.
 *
 * @return void *
 */
void *NuHashItGetValue(NuHashIterator_t *pItPtr);

/**
 * @brief Set the value to the current position of iterator. 
 *
 * @param  [in]  pItPtr    : Pointer to hash iterator.
 * @param  [in]  value     : Pointer to the value used for setting.
 * @param  [in]  vlen      : Length of the value.
 *
 * @return void
 */
void NuHashItSetValue(NuHashIterator_t *pItPtr, void *val, size_t vlen);

/**
 * @brief Remove the item in the current position of iterator. 
 *
 * @param  [in]  pItPtr    : Pointer to hash iterator.
 *
 * @return void
 */
void NuHashItRmvByItem(NuHashIterator_t *pItPtr);

/**
 * @brief Reset the iterator to the initial situation. 
 *
 * @param  [in]  pItPtr    : Pointer to hash iterator.
 *
 * @return void
 */
void NuHashItReset(NuHashIterator_t *pItPtr);

#define	NuHashItGetItem(pIt)	(pIt)->pItem
#define	NuHashItGetGroup(pIt)	(pIt)->pGroup
#define	NuHashItGetIdx(pIt)		(pIt)->iIdx

/**
 * @brief Print out all item of the iterator.
 * @note Debug using.
 *
 * @param  [in]  pItPtr    : Pointer to hash iterator.
 *
 * @return void
 */
void ShowAllHashData(NuHash_t *pHash);

/* ----- macro define ----- */
/* ============================================================================== */
#define GET_INDEXFOR(NU_Idx, NU_hashval, NU_num) \
    (NU_Idx) = (NU_hashval) % (NU_num)
/** @} */

#ifdef __cplusplus
}
#endif

#endif /* _NUHASH_H */

