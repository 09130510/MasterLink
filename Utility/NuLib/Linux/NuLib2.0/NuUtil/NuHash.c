#include "NuHash.h"

/* Internal function */
/* ============================================================================== */

static unsigned int _NuHashGetPrimeNum(int Num)
{
	int	Cnt = 0;
	int PrimeNum[] = { 17, 37, 79, 163, 331, 673, 1361, 2729, 5471, 10949, 21911, 43853, 87719, 175447, 350899, 701819, 1403641, 2807303, 5614657, 11229331, 22458671, 44917381, 89834777, 179669557, 359339171, 718678369, 1437356741, 2147483647 };

	while(Cnt < 28)
	{
		if(Num <= PrimeNum[Cnt])
			return PrimeNum[Cnt];

		Cnt ++;
	}

	return PrimeNum[27];
}

static unsigned int _hash_fn(void *key, int ilen)
{
	char *p = key;
	unsigned int hash = 5381;
	char *e = p + (ilen);

	while(p != e)
		hash = ((hash << 5) + hash) + (*p++);

	return hash;
}

static int _NuHashCompareFn(void *v1, void *v2)
{
	int iRC = 0;
	NuHashKey_t *Key1 = (NuHashKey_t *)v1;
	NuHashKey_t *Key2 = (NuHashKey_t *)v2;

	if(Key1->Len != Key2->Len)
		NUGOTO(iRC, -1, EXIT);

	iRC = memcmp(Key1->Content, Key2->Content, Key1->Len);

EXIT:
	return iRC;
}

static void NuHashKeySetData(NuHashKey_t *pHashKey, void *Key, size_t Klen)
{
	pHashKey->Content = Key;
	pHashKey->Len = Klen;

	return;
}

static NuHashItem_t *_GetItemFromBuffer(NuHash_t *pHash, void *Val, size_t Vlen)
{
	NuHashItem_t	*pItem = (NuHashItem_t *)NuBufferGet(pHash->ItemBuffer); 

	base_list_node_init(&(pItem->Node));

	NuHashItemSetData(pItem, Val, Vlen);

	pItem->Hook = NULL;

	return pItem;
}

static NuHashGroup_t *_GetGroupFromBuffer(NuHash_t *pHash, void *Key, size_t Klen)
{
	NuHashGroup_t	*pGroup = (NuHashGroup_t *)NuBufferGet(pHash->GroupBuffer);

	pGroup->Next = pGroup->Prev = NULL;

	base_list_init(&(pGroup->List));
	NuHashKeySetData(&(pGroup->Key), Key, Klen);

	return pGroup;
}

/* ----- Constructor/Distructor ----- */
/* ============================================================================== */
int NuHashNew(NuHash_t **pHash, int Num)
{
	return NuHashNew2(pHash, Num, NULL, NULL);
}

int NuHashNew2(NuHash_t **pHash, int Num, NuHashFn Hash_fn, NuCompareFn Compare_fn) 
{
	int				iRC = 0;
	unsigned int	default_size = _NuHashGetPrimeNum(Num);

	(*pHash) = NULL;
	(*pHash) = (NuHash_t *)malloc(sizeof(NuHash_t));
	if((*pHash) == NULL)
		NUGOTO(iRC, NU_MALLOC_FAIL, EXIT);

	(*pHash)->items = (NuHashGroup_t **)calloc(default_size, sizeof(NuHashGroup_t *));
	if((*pHash)->items == NULL)
		NUGOTO(iRC, NU_MALLOC_FAIL, EXIT);

	iRC = NuBufferNew(&((*pHash)->ItemBuffer), sizeof(NuHashItem_t), default_size);
	if(iRC < 0)
		NUGOTO(iRC, NU_MALLOC_FAIL, EXIT);

	iRC = NuBufferNew(&((*pHash)->GroupBuffer), sizeof(NuHashGroup_t), default_size);
	if(iRC < 0)
		NUGOTO(iRC, NU_MALLOC_FAIL, EXIT);

	(*pHash)->num = 0;
	(*pHash)->anum = default_size;
	(*pHash)->Hash_fn = (Hash_fn == NULL) ? &_hash_fn : Hash_fn;
	(*pHash)->Compare_fn = (Compare_fn == NULL) ? &_NuHashCompareFn : Compare_fn;

EXIT:
	if(iRC < 0)
		NuHashFree(*pHash);
	else
		iRC = default_size;

	return iRC;
}

void NuHashFree(NuHash_t *pHash)
{
	if(pHash->items != NULL)
		free(pHash->items);

	if(pHash->ItemBuffer != NULL)
		NuBufferFree(pHash->ItemBuffer);

	if(pHash->GroupBuffer != NULL)
		NuBufferFree(pHash->GroupBuffer);

	if(pHash != NULL)
		free(pHash);

	pHash = NULL;
	return;
}

void NuHashClear(NuHash_t *pHash)
{
	pHash->num = 0;
	memset(pHash->items, 0, (pHash->anum) * sizeof(NuHashGroup_t *));
	NuBufferClear(pHash->GroupBuffer);
	NuBufferClear(pHash->ItemBuffer);
	return;
}

NuHashItem_t *NuHashSearch(NuHash_t *pHash, void *key, size_t klen, unsigned int *idx)
{
	unsigned int		hash = 0;
	NuHashKey_t			SearchKey;
	NuHashGroup_t		*pGroup = NULL;
	base_list_node_t	*pNode = NULL;

	hash = pHash->Hash_fn(key, klen);
	GET_INDEXFOR(*idx, hash, pHash->anum);

	NuHashKeySetData(&SearchKey, key, klen);

	if((pGroup = pHash->items[*idx]) != NULL)
	{
		do
		{
			if(pHash->Compare_fn(&(pGroup->Key), &SearchKey) == 0)
			{
				if((pNode = base_list_get_head(&(pGroup->List))) != NULL)
					return (NuHashItem_t *)pNode;

				return NULL;
			}
		}
		while((pGroup = pGroup->Next) != NULL);
	}

	return NULL;
}

NuHashItem_t *NuHashAdd(NuHash_t *pHash, void *Key, size_t Klen, void *Value, size_t Vlen, unsigned int idx)
{
	NuHashGroup_t	*pGroup = _GetGroupFromBuffer(pHash, Key, Klen), *pGroupFirst = pHash->items[idx];
	NuHashItem_t	*pAddItem = _GetItemFromBuffer(pHash, Value, Vlen);

	base_list_insert_head(&(pGroup->List), &(pAddItem->Node));

	if(pGroupFirst != NULL)
	{
		pGroupFirst->Prev = pGroup;
		pGroup->Next = pGroupFirst;
	}

	pHash->items[idx] = pGroup;

	++ (pHash->num);

	return pAddItem;
}

NuHashItem_t *NuMultiHashAdd(NuHash_t *pHash, NuHashItem_t *pItem, void *Value, size_t Vlen)
{
	NuHashItem_t	*pAddItem = _GetItemFromBuffer(pHash, Value, Vlen);

	base_list_insert_head(base_list_get_list(&(pItem->Node)), &(pAddItem->Node));

	++ (pHash->num);

	return pAddItem;
}

NuHashGroup_t *NuHashRmvItem(NuHash_t *pHash, NuHashItem_t *pItem, unsigned int idx)
{
	NuHashGroup_t	*pGroup = (NuHashGroup_t *)(base_list_get_list(&(pItem->Node)));

	-- (pHash->num);

	if(base_list_remove_node(&(pItem->Node)) == 0)
	{
		if(pGroup->Prev != NULL)
			pGroup->Prev->Next = pGroup->Next;
		else
			pHash->items[idx] = pGroup->Next;

		if(pGroup->Next != NULL)
			pGroup->Next->Prev = pGroup->Prev;

		return pGroup;
	}

	return NULL;
}

void NuHashRmvItemComplete(NuHash_t *pHash, NuHashItem_t *pItem)
{
	NuBufferPut(pHash->ItemBuffer, pItem);
	return;
}

void NuHashRmvGroupComplete(NuHash_t *pHash, NuHashGroup_t *pGroup)
{
	NuBufferPut(pHash->GroupBuffer, pGroup);
	return;
}

void *NuHashOneTimeRmvItem(NuHash_t *pHash, NuHashItem_t *pItem, unsigned int idx)
{
	NuHashGroup_t	*pGroup = NULL;
	void			*Key = NULL;

	pGroup = NuHashRmvItem(pHash, pItem, idx);
	if(pGroup != NULL)
	{
		Key = pGroup->Key.Content;
		NuBufferPut(pHash->GroupBuffer, pGroup);
	}

	NuBufferPut(pHash->ItemBuffer, pItem);

	return Key;
}

void NuHashItemSetData(NuHashItem_t *pItem, void *pVal, size_t vlen)
{
	pItem->Value = pVal;
	pItem->Vlen = vlen;

	return;
}

NuHashItem_t *NuHashRight(NuHashItem_t *pItem)
{
	base_list_node_t	*pNode = base_list_node_next(&(pItem->Node));

	return (pNode == NULL) ? (NULL) : ((NuHashItem_t *)pNode);
}

NuHashItem_t *NuHashLeft(NuHashItem_t *pItem)
{
	base_list_node_t	*pNode = base_list_node_prev(&(pItem->Node));

	return (pNode == NULL) ? (NULL) : ((NuHashItem_t *)pNode);
}

NuHashItem_t *NuHashRightMost(NuHashItem_t *pItem)
{
	base_list_node_t	*pNode = base_list_get_tail(base_list_get_list(&(pItem->Node)));

	return (pNode == NULL) ? (NULL) : ((NuHashItem_t *)pNode);
}

NuHashItem_t *NuHashLeftMost(NuHashItem_t *pItem)
{
	base_list_node_t	*pNode = base_list_get_head(base_list_get_list(&(pItem->Node)));

	return (pNode == NULL) ? (NULL) : ((NuHashItem_t *)pNode);
}

/* ----- Iterator functions ----- */
/* ============================================================================== */
static void _SetNewGroup(NuHashIterator_t *pItPtr, NuHashGroup_t *pGroup)
{
	pItPtr->pGroup = pGroup;
	pItPtr->pItem = (NuHashItem_t *)(base_list_get_head(&(pGroup->List)));

	return;
}

static int _FindNextBucket(NuHash_t *pHash, NuHashIterator_t *pItPtr)
{
	int				ANum = pHash->anum;
	int				Cnt = pItPtr->iIdx + 1;
	NuHashGroup_t	**ppGroup = pHash->items + Cnt, *pGroup = NULL;

	while(Cnt < ANum)
	{
		if((pGroup = *ppGroup) != NULL)
		{
			if(base_list_items_cnt(&(pGroup->List)) > 0)
			{
				pItPtr->iIdx = Cnt;
				_SetNewGroup(pItPtr, pGroup);
				return 1;
			}
		}

		++ ppGroup;
		++ Cnt;
	}

	return 0;
}

int NuHashItGetAll(NuHash_t *pHash, NuHashIterator_t *pItPtr)
{
	pItPtr->iMode = NuHashIteratorModeAll;
	pItPtr->iIdx = -1;
	pItPtr->pHash = pHash;

	return (_FindNextBucket(pHash, pItPtr)) ? NU_OK : NU_MAPIT_END;
}

int NuHashItFind(NuHash_t *pHash, void *key, size_t klen, NuHashIterator_t *pItPtr)
{
	unsigned int	idx = 0;
	NuHashItem_t	*pItem = NuHashSearch(pHash, key, klen, &idx);

	if(pItem == NULL)
		return NU_MAPKEY_NOT_FOUND;

	pItPtr->iMode = NuHashIteratorModeKey;
	pItPtr->iIdx = idx;
	pItPtr->pHash = pHash;
	pItPtr->pItem = pItem;
	pItPtr->pGroup = (NuHashGroup_t *)(base_list_get_list(&(pItem->Node)));

	return NU_OK;
}

int NuHashItNext(NuHashIterator_t *pItPtr)
{
	base_list_node_t	*pNode = base_list_node_next(&(pItPtr->pItem->Node));
	NuHashGroup_t		*pGroup = pItPtr->pGroup->Next;

	if(pNode == NULL)
	{
		if(pItPtr->iMode == NuHashIteratorModeKey)
			return NU_MAPIT_END;

		if(pGroup == NULL)
		{
			if(!_FindNextBucket(pItPtr->pHash, pItPtr))
				return NU_MAPIT_END;
		}
		else
			_SetNewGroup(pItPtr, pGroup);
	}
	else
		pItPtr->pItem = (NuHashItem_t *)pNode;

	return NU_OK;
}

int NuHashItPrev(NuHashIterator_t *pItPtr)
{
	base_list_node_t	*pNode = base_list_node_prev(&(pItPtr->pItem->Node));

	if(pItPtr->iMode == NuHashIteratorModeAll)
		return NU_MAPIT_END;

	if(pNode == NULL)
		return NU_MAPIT_END;
	else
		pItPtr->pItem = (NuHashItem_t *)pNode;

	return NU_OK;
}

int NuHashItTheFirst(NuHashIterator_t *pItPtr)
{
	base_list_node_t	*pNode = base_list_get_head(&(pItPtr->pGroup->List));

	if(pNode != NULL)
		pItPtr->pItem = (NuHashItem_t *)pNode;
	else
		return NU_NULL;

	return NU_OK;
}

void *NuHashItGetKey(NuHashIterator_t *pItPtr)
{
	return pItPtr->pGroup->Key.Content;
}

void *NuHashItGetValue(NuHashIterator_t *pItPtr)
{
	return pItPtr->pItem->Value;
}

void NuHashItSetValue(NuHashIterator_t *pItPtr, void *val, size_t vlen)
{
	NuHashItemSetData(pItPtr->pItem, val, vlen);
	return;
}

void NuHashItRmvByItem(NuHashIterator_t *pItPtr)
{
	if (pItPtr->pItem == NULL)
		return;

	NuHashOneTimeRmvItem(pItPtr->pHash, pItPtr->pItem, pItPtr->iIdx);
	return;
}

void NuHashItReset(NuHashIterator_t *pItPtr)
{
	pItPtr->pHash = NULL;
	pItPtr->pGroup = NULL;
	pItPtr->pItem = NULL;

	return;
}

void ShowAllHashData(NuHash_t *pHash)
{
    int				Cnt = 0;
    NuHashItem_t	*pItem = NULL;
	NuHashGroup_t	*pGroup = NULL;

    for (Cnt = 0; Cnt < pHash->anum; Cnt++)
	{
		if((pGroup = pHash->items[Cnt]) != NULL)
		{
			printf("[%d]", Cnt);
        	do
        	{
				printf("  <%s>-", (char *)pGroup->Key.Content);
				pItem = (NuHashItem_t *)(base_list_get_head(&(pGroup->List)));
				while(pItem != NULL)
				{
					printf("{%s} ",  (char *)(pItem->Value));
					pItem = (NuHashItem_t *)(base_list_node_next(&(pItem->Node)));
				}

				printf("\n");
			}while (pItem != NULL);
			printf("\n");
		}
		else
			printf("[%d] NULL\n", Cnt);
	}

	return;
}

