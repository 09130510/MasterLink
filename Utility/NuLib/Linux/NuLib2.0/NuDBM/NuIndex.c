
static int NuIndexGenerateKeyV(char *Key, NuIndex_t *pIndex, va_list Column)
{
    int     Cnt = 0, Len = 0;
    char    *pKey = Key;
    char    *pColumn = NULL;

    for(Cnt = 0; Cnt < pIndex->ColumnNo; ++ Cnt)
    {
        Len = strlen(pColumn = va_arg(Column, char *));

        strcpy(pKey, pColumn);
        pKey += (Len + 1);
    }

    return pKey - Key;
}

static int NuIndexGenerateKeyByRowData(char *Key, NuIndex_t *pIndex, char *pRowData)
{
    int             Cnt = 0, Len = 0;
    char            *pKey = Key;
    char            *pColumn = NULL;

    for(Cnt = 0; Cnt < pIndex->ColumnNo; ++ Cnt)
    {
        Len = strlen(pColumn = pRowData + pIndex->Column[Cnt]);

        strcpy(pKey, pColumn);
        pKey += (Len + 1);
    }

    return pKey - Key;
}

static int NuIndexAdd(NuIndex_t *pIndex, NuRow_t *Row, char *RowData)
{
    unsigned int    Idx = 0;
    NuHashItem_t    *pItem = NULL, *pItem2 = NULL;
    char            *Key = (char *)NuBufferGet(pIndex->KeyBuffer);
    int             Klen = NuIndexGenerateKeyByRowData(Key, pIndex, RowData);

    if(!(pItem = NuHashSearch(pIndex->IndexMap, Key, Klen, &Idx)))
    {
        NuHashAdd(pIndex->IndexMap, Key, Klen, Row, 0, Idx);
    }
    else
    {
        if(pIndex->IsUnique)
        {
            pItem2 = pItem;

            do
            {
                if(((NuRow_t *)(NuHashGetValue(pItem2)))->TranID >= 0)
                {
                    NuBufferPut(pIndex->KeyBuffer, Key);
                    return NuDBMErr_UniqueIndexDup;
                }
            }
            while((pItem2 = NuHashRight(pItem2)) != NULL);
        }

        NuMultiHashAdd(pIndex->IndexMap, pItem, Row, 0);
    }

    return NU_OK;
}

static void NuIndexRmv(NuIndex_t *pIndex, char *RowData)
{
    int             Klen = NuIndexGenerateKeyByRowData(pIndex->pSearchKey, pIndex, RowData);
    unsigned int    Idx = 0;
    NuHashItem_t    *pItem = NULL;
    NuHashGroup_t   *pGroup = NULL;
    NuIndexTrash_t  *pIndexTrash = &(pIndex->Trash);

    if((pItem = NuHashSearch(pIndex->IndexMap, pIndex->pSearchKey, Klen, &Idx)) != NULL)
    {
        pGroup = NuHashRmvItem(pIndex->IndexMap, pItem, Idx);

        if(pGroup != NULL)
        {
            base_vector_push(pIndexTrash->GroupVec[pIndexTrash->Using], pGroup);
        }

        if(pItem != NULL)
        {
            base_vector_push(pIndexTrash->ItemVec[pIndexTrash->Using], pItem);
        }
    }

    return;
}

static NuRow_t *NuIndexSearch(NuIndex_t *pIndex, void *key, int klen, NuHashIterator_t *pIt)
{
    NuRow_t *pRow = NULL;

    if(NuHashItFind(pIndex->IndexMap, key, klen, pIt) == NU_MAPKEY_NOT_FOUND)
    {
        return NULL;
    }

    do
    {
        pRow = (NuRow_t *)NuHashItGetValue(pIt);
        if(pRow->TranID != NuRowTranID_DELETED)
        {
            break;
        }
    }
    while(NuHashItNext(pIt) == NU_OK);

    return (pRow->TranID < 0) ? (NULL) : (pRow);
}

static NuRow_t *NuIndexGetAll(NuIndex_t *pIndex, NuHashIterator_t *pIt)
{
    NuRow_t *pRow = NULL;

    if(NuHashItGetAll(pIndex->IndexMap, pIt) == NU_MAPIT_END)
    {
        return NULL;
    }

    do
    {
        pRow = (NuRow_t *)NuHashItGetValue(pIt);
        if(pRow->TranID != NuRowTranID_DELETED)
        {
            return pRow;
        }
    }
    while(NuHashItNext(pIt) == NU_OK);

    return NULL;
}

static int NuIndexInsert(NuTable_t *pTable, NuRow_t *pRow)
{
    int             RC = NU_OK;
    base_vector_t   *pVec = pTable->Index;
    base_vector_it  VecIt;
    NuIndex_t       *pIndex = NULL;

    base_vector_it_set(VecIt, pVec);
    while(VecIt != base_vector_it_end(pVec))
    {
        pIndex = (NuIndex_t *)(*VecIt);

        NuLockLock(&(pIndex->Lock));

        if((RC = NuIndexAdd(pIndex, pRow, pRow->Data)) < 0)
        {
            NuLockUnLock(&(pIndex->Lock));

            do
            {
                -- VecIt;
                pIndex = (NuIndex_t *)(*VecIt);

                NuLockLock(&(pIndex->Lock));

                NuIndexRmv(pIndex, pRow->Data);

                NuLockUnLock(&(pIndex->Lock));
            }
            while(VecIt != base_vector_it_begin(pVec));

            break;
        }

        NuLockUnLock(&(pIndex->Lock));
    
        ++ VecIt;
    }

    return RC;
}

static int NuIndexUpdate(NuTable_t *pTable, NuRow_t *pRow, unsigned int EffectIndex)
{
    int             RC = NU_OK;
    NuIndex_t       *pIndex = NULL;
    base_vector_t   *pVec = pTable->Index;
    base_vector_it  VecIt;

    if(!EffectIndex)
    {
        return NU_OK;
    }

    /* Try to add all the effected indexes. */
    base_vector_it_set(VecIt, pVec);
    while(VecIt != base_vector_it_end(pVec))
    {
        pIndex = (NuIndex_t *)(*VecIt);

        if(pIndex->IndexFlag & EffectIndex)
        {
            NuLockLock(&(pIndex->Lock));

            NuIndexRmv(pIndex, pRow->Data);

            if(NuIndexAdd(pIndex, pRow, pRow->Update_Data) != NU_OK)
            {
                NuIndexAdd(pIndex, pRow, pRow->Data);
                RC = NuDBMErr_UpdateFail;
                NuLockUnLock(&(pIndex->Lock));

                do
                {
                    -- VecIt;
                    pIndex = (NuIndex_t *)(*VecIt);

                    if(pIndex->IndexFlag & EffectIndex)
                    {
                        NuLockLock(&(pIndex->Lock));

                        NuIndexRmv(pIndex, pRow->Update_Data);
                        if(pRow->TranID > 0)
                        {
                            NuIndexAdd(pIndex, pRow, pRow->Data);
                        }

                        NuLockUnLock(&(pIndex->Lock));
                    }
                }
                while(VecIt != base_vector_it_begin(pVec));

                break;
            }

            NuLockUnLock(&(pIndex->Lock));
        }

        ++ VecIt;
    }

    return RC;
}

