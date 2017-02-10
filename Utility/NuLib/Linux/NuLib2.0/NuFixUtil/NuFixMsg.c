

#include "NuFixMsg.h"

struct _NuFixMsg_t
{
    NuHash_t        *Hash;
    base_vector_t   *Header;
    base_vector_t   *Body;
    base_vector_t   *AllTag;
    NuBuffer_t      *InfoBuffer;
    NuStr_t         *InMsg;
    NuStr_t         *OutMsg;
};
size_t NuFixMsgSz = sizeof(struct _NuFixMsg_t);

typedef struct _NuFixMsgInfo_t
{
    int                     Tag;
    char                    TagStr[NuFixMsgDefaultTagStrLen + 2];
    int                     Kind;
    NuHashItem_t            *pItem;
    struct _NuFixMsgInfo_t  *pBelong;
    NuStr_t                 *Str;
    int                     CurrentRepeatingInstance;
    base_vector_t           *RepeatingGroupMember;
} NuFixMsgInfo_t;

#define NuFixMsgDelimiterStr        "\001"
#define NuFixMsgTagValSepStr        "="
#define NuFixMsgShellEight          "8"NuFixMsgTagValSepStr
#define NuFixMsgShellNine           NuFixMsgDelimiterStr"9=00000"NuFixMsgDelimiterStr
#define NuFixMsgShellTen            "10"NuFixMsgTagValSepStr

/* ====================================================================== */
/* Internal function                                                      */
/* ---------------------------------------------------------------------- */
static unsigned int fixmsg_hash_fn(void *key, int ilen)
{
    return *((unsigned int *)key);
}

static int fixmsg_cmp_fn(void *v1, void *v2)
{
    return *(int *)(((NuHashKey_t *)v1)->Content) - *(int *)(((NuHashKey_t *)v2)->Content);
}

static int _TagCompare(const void *v1, const void *v2)
{
    return (*(NuFixMsgInfo_t **)v1)->Tag - (*(NuFixMsgInfo_t **)v2)->Tag;
}

static NuFixMsgInfo_t *GetInfoFromBuffer(NuFixMsg_t *pFixMsg, int Tag, size_t ValLen, int Kind)
{
    NuFixMsgInfo_t      *pInfo = (NuFixMsgInfo_t *)NuBufferGet(pFixMsg->InfoBuffer);

    pInfo->Tag = Tag;
    sprintf(pInfo->TagStr, "%d%c", Tag, NuFixMsgTagValSep);
    pInfo->Kind = Kind;
    NuStrNew2(&(pInfo->Str), ValLen, NULL);
    pInfo->RepeatingGroupMember = NULL;
    pInfo->CurrentRepeatingInstance = 0;
    pInfo->pBelong = NULL;

    base_vector_push(pFixMsg->AllTag, pInfo);

    return pInfo;
}

static void _FieldAssign(NuHashItem_t *pItem, char *pVal, size_t Len)
{
    pItem->Value = pVal;
    pItem->Vlen = Len;

    return;
}

static void _FieldSet(NuHashItem_t *pItem, char *pVal, size_t Len)
{
    NuStr_t *pStr = ((NuFixMsgInfo_t *)(pItem->Hook))->Str;

    NuStrNCpy(pStr, pVal, Len);
    pItem->Value = NuStrGet(pStr);
    pItem->Vlen = NuStrSize(pStr);

    return;
}

static void _FieldSprintf(NuHashItem_t *pItem, char *Format, va_list ArguList)
{
    NuStr_t *pStr = ((NuFixMsgInfo_t *)(pItem->Hook))->Str;

    NuStrVPrintf(pStr, Format, ArguList);
    pItem->Value = NuStrGet(pStr);
    pItem->Vlen = NuStrSize(pStr);

    return;
}

static void _FieldRemove(NuHashItem_t *pItem)
{
    pItem->Value = NULL;
    pItem->Vlen = 0;

    return;
}

static void _GroupAdd(NuFixMsg_t *pFixMsg, NuFixMsgInfo_t *pLeading)
{
    NuHash_t        *pHash = pFixMsg->Hash;
    NuHashItem_t    *pItem = NULL;
    NuFixMsgInfo_t  *pInfo = NULL;
    base_vector_t   *pVec = pLeading->RepeatingGroupMember;
    base_vector_it  VecIt;
    int             Kind = (pLeading->Kind & NuFixMsgKindHeader) ? (NuFixMsgKindHeader|NuFixMsgKindRepeatingGroupMember) : (NuFixMsgKindBody|NuFixMsgKindRepeatingGroupMember);

    base_vector_it_set(VecIt, pVec);
    while(VecIt != base_vector_it_end(pVec))
    {
        pItem = ((NuFixMsgInfo_t *)(*VecIt))->pItem;
        pInfo = GetInfoFromBuffer(pFixMsg, ((NuFixMsgInfo_t *)(pItem->Hook))->Tag, NuFixMsgDefaultValueLen, Kind);
        pInfo->pItem = NuMultiHashAdd(pHash, pItem, NULL, 0);
        pInfo->pBelong = pLeading;
        pInfo->pItem->Hook = pInfo;

        ++ VecIt;
    }

    return;
}

/* ---------------------------------------------------------------------- */
int NuFixMsgInit(NuFixMsg_t *pFixMsg)
{
    int         iRC = NU_OK;

    pFixMsg->Hash = NULL;
    iRC = NuHashNew2(&(pFixMsg->Hash), NuFixMsgDefaultTagNum, &fixmsg_hash_fn, &fixmsg_cmp_fn);
    NUCHKRC(iRC, EXIT);

    pFixMsg->Header = NULL;
    iRC = base_vector_new(&(pFixMsg->Header), 30);
    NUCHKRC(iRC, EXIT);

    pFixMsg->Body = NULL;
    iRC = base_vector_new(&(pFixMsg->Body), 70);
    NUCHKRC(iRC, EXIT);

    pFixMsg->AllTag = NULL;
    iRC = base_vector_new(&(pFixMsg->AllTag), NuFixMsgDefaultTagNum);
    NUCHKRC(iRC, EXIT);

    pFixMsg->InfoBuffer = NULL;
    iRC = NuBufferNew(&(pFixMsg->InfoBuffer), sizeof(NuFixMsgInfo_t), NuFixMsgDefaultTagNum);
    NUCHKRC(iRC, EXIT);

    pFixMsg->InMsg = NULL;
    iRC = NuStrNew2(&(pFixMsg->InMsg), 512, NULL);
    NUCHKRC(iRC, EXIT);

    pFixMsg->OutMsg = NULL;
    iRC = NuStrNew2(&(pFixMsg->OutMsg), 512, NULL);
    NUCHKRC(iRC, EXIT);

    NuFixMsgTemplateAddTag(pFixMsg, 8, NuFixMsgVerLen, 0, NuFixMsgKindShell);
    NuFixMsgFieldSet(pFixMsg, 8, NuFixMsgDefaultVer, strlen(NuFixMsgDefaultVer));

EXIT:
    if(iRC < 0)
    {
        NuFixMsgDestroy(pFixMsg);
    }

    return iRC;
}

void NuFixMsgDestroy(NuFixMsg_t *pFixMsg)
{
    void            *ptr = NULL;
    NuFixMsgInfo_t  *pInfo = NULL;
    base_vector_t   *pVec = NULL;

    if(pFixMsg != NULL)
    {
        if((pVec = pFixMsg->AllTag) != NULL)
        {
            while(base_vector_pop(pVec, &ptr) >= 0)
            {
                pInfo = (NuFixMsgInfo_t *)(ptr);
                NuStrFree(pInfo->Str);
                if(pInfo->RepeatingGroupMember != NULL)
                {
                    base_vector_free(pInfo->RepeatingGroupMember);
                }
            }

            base_vector_free(pVec);
        }

        base_vector_free(pFixMsg->Header);
        base_vector_free(pFixMsg->Body);

        if(pFixMsg->Hash != NULL)
        {
            NuHashFree(pFixMsg->Hash);
        }

        if(pFixMsg->InMsg != NULL)
        {
            NuStrFree(pFixMsg->InMsg);
        }

        if(pFixMsg->OutMsg != NULL)
        {
            NuStrFree(pFixMsg->OutMsg);
        }

        if(pFixMsg->InfoBuffer != NULL)
        {
            NuBufferFree(pFixMsg->InfoBuffer);
        }
    }

    return;
}

int NuFixMsgNew(NuFixMsg_t **pFixMsg)
{
	int RC = NU_OK;

    (*pFixMsg) = NULL;
    if(!((*pFixMsg) = (NuFixMsg_t *)malloc(sizeof(NuFixMsg_t))))
    {
        return NU_MALLOC_FAIL;
    }

	if((RC = NuFixMsgInit(*pFixMsg)) < 0)
    {
		free(*pFixMsg);
    }

    return RC;
}

void NuFixMsgFree(NuFixMsg_t *pFixMsg)
{
    if(pFixMsg != NULL)
    {
        free(pFixMsg);
    }

    return;
}

void NuFixMsgClear(NuFixMsg_t *pFixMsg)
{
    base_vector_t   *pVec = pFixMsg->AllTag;
    base_vector_it  VecIt;
    NuFixMsgInfo_t  *pInfo = NULL;

    base_vector_it_set(VecIt, pVec);
    while(VecIt != base_vector_it_end(pVec))
    {
        pInfo = (NuFixMsgInfo_t *)(*VecIt);
        _FieldRemove(pInfo->pItem);
        pInfo->CurrentRepeatingInstance = 0;
    
        ++ VecIt;
    }

    NuStrClear(pFixMsg->InMsg);
    NuStrClear(pFixMsg->OutMsg);

    NuFixMsgFieldSet(pFixMsg, 8, NuFixMsgDefaultVer, strlen(NuFixMsgDefaultVer));

    return;
}

int NuFixMsgTemplateAddTag(NuFixMsg_t *pFixMsg, int Tag, size_t ExpectLen, int BelongGroup, int Kind)
{
    unsigned int    idx = 0;
    NuHash_t        *pHash = pFixMsg->Hash;
    NuHashItem_t    *pItem = NuHashSearch(pHash, &Tag, sizeof(int), &idx);
    NuFixMsgInfo_t  *pInfo = NULL;

    if(pItem != NULL)
    {
        return NU_DUPLICATE;
    }

    pInfo = GetInfoFromBuffer(pFixMsg, Tag, ExpectLen, Kind);
    pInfo->pItem = pItem = NuHashAdd(pHash, &(pInfo->Tag), sizeof(int), NULL, 0, idx);
    pItem->Hook = pInfo;

    if(Kind & NuFixMsgKindHeader)
    {
        if(Kind & NuFixMsgKindRepeatingGroupMember)
        {
            if(!(pItem = NuHashSearch(pHash, &BelongGroup, sizeof(int), &idx)))
            {
                NuFixMsgTemplateAddTag(pFixMsg, BelongGroup, NuFixMsgDefaultValueLen, 0, NuFixMsgKindHeader|NuFixMsgKindRepeatingGroupLeader);
                pItem = NuHashSearch(pHash, &BelongGroup, sizeof(int), &idx);
            }

            pInfo->pBelong = (NuFixMsgInfo_t *)(pItem->Hook);
            base_vector_push(pInfo->pBelong->RepeatingGroupMember, pInfo);
        }
        else
        {
            if(Kind & NuFixMsgKindRepeatingGroupLeader)
            {
                base_vector_new(&(pInfo->RepeatingGroupMember), 10);
            }

            base_vector_push(pFixMsg->Header, pInfo);
        }
    }
    else if(Kind & NuFixMsgKindBody)
    {
        if(Kind & NuFixMsgKindRepeatingGroupMember)
        {
            if(!(pItem = NuHashSearch(pHash, &BelongGroup, sizeof(int), &idx)))
            {
                NuFixMsgTemplateAddTag(pFixMsg, BelongGroup, NuFixMsgDefaultValueLen, 0, NuFixMsgKindBody|NuFixMsgKindRepeatingGroupLeader);
                pItem = NuHashSearch(pHash, &BelongGroup, sizeof(int), &idx);
            }

            pInfo->pBelong = (NuFixMsgInfo_t *)(pItem->Hook);
            base_vector_push(pInfo->pBelong->RepeatingGroupMember, pInfo);
        }
        else
        {
            if(Kind & NuFixMsgKindRepeatingGroupLeader)
            {
                base_vector_new(&(pInfo->RepeatingGroupMember), 10);
            }

            base_vector_push(pFixMsg->Body, pInfo);
        }
    }
    else
    {
        return NU_FAIL;
    }

    return NU_OK;
}

int NuFixMsgTemplateAddRepeatingGroup(NuFixMsg_t *pFixMsg, int Kind, int LeadingTag, int NodeNo, ...)
{
    int             NodeTag = 0;
    size_t          NodeLen = 0;
    va_list         Argu;

    if(NuFixMsgTemplateAddTag(pFixMsg, LeadingTag, NuFixMsgDefaultTagStrLen, 0, Kind|NuFixMsgKindRepeatingGroupLeader) != NU_OK)
    {
        return NU_FAIL;
    }

    va_start(Argu, NodeNo);

    for(; NodeNo > 0; -- NodeNo)
    {
        NodeTag = va_arg(Argu, int);
        NodeLen = va_arg(Argu, size_t);

        NuFixMsgTemplateAddTag(pFixMsg, NodeTag, NodeLen, LeadingTag, Kind|NuFixMsgKindRepeatingGroupMember);
    }

    return NU_OK;
}

void NuFixMsgTemplateSort(NuFixMsg_t *pFixMsg)
{
    base_vector_sort(pFixMsg->Header, &_TagCompare);
    base_vector_sort(pFixMsg->Body, &_TagCompare);

    return;
}

NuHashItem_t *NuFixMsgFieldGet(NuFixMsg_t *pFixMsg, int Tag)
{
    unsigned int    Idx = 0;

    return NuHashSearch(pFixMsg->Hash, &Tag, sizeof(int), &Idx);
}

int NuFixMsgFieldAssign(NuFixMsg_t *pFixMsg, int Tag, char *pVal, size_t Len)
{
    NuHashItem_t    *pItem = NuFixMsgFieldGet(pFixMsg, Tag);

    if(pItem == NULL)
    {
        return NU_FAIL;
    }

    _FieldAssign(pItem, pVal, Len);

    return NU_OK;
}

int NuFixMsgFieldSet(NuFixMsg_t *pFixMsg, int Tag, char *pVal, size_t Len)
{
    NuHashItem_t    *pItem = NuFixMsgFieldGet(pFixMsg, Tag);

    if(pItem == NULL)
    {
        return NU_FAIL;
    }

    _FieldSet(pItem, pVal, Len);

    return NU_OK;
}

int NuFixMsgFieldVPrintf(NuFixMsg_t *pFixMsg, int Tag, char *Format, va_list ArguList)
{
    NuHashItem_t    *pItem = NuFixMsgFieldGet(pFixMsg, Tag);

    if(pItem == NULL)
    {
        return NU_FAIL;
    }

    _FieldSprintf(pItem, Format, ArguList);

    return NU_OK;
}

int NuFixMsgFieldPrintf(NuFixMsg_t *pFixMsg, int Tag, char *Format, ...)
{
    int     Result = 0;
    va_list ArguList;

    va_start(ArguList, Format);
    Result = NuFixMsgFieldVPrintf(pFixMsg, Tag, Format, ArguList);
    va_end(ArguList);

    return Result;
}

int NuFixMsgFieldRemove(NuFixMsg_t *pFixMsg, int Tag)
{
    NuHashItem_t    *pItem = NuFixMsgFieldGet(pFixMsg, Tag);

    if(pItem == NULL)
    {
        return NU_FAIL;
    }

    _FieldRemove(pItem);

    return NU_OK;
}

static NuHashItem_t *NuFixMsgGroupFind(NuFixMsg_t *pFixMsg, int Tag, int Idx)
{
    unsigned int    HashIdx = 0;
    NuHashItem_t    *pItem = NuHashSearch(pFixMsg->Hash, &Tag, sizeof(int), &HashIdx);
    NuFixMsgInfo_t  *pLeading = NULL;

    if(!pItem)
    {
        return NULL;
    }
 
    pLeading = ((NuFixMsgInfo_t *)(pItem->Hook))->pBelong;
    pItem = NuHashRightMost(pItem);

    if(Idx + 1 > (pLeading->CurrentRepeatingInstance))
    {
        (pLeading->CurrentRepeatingInstance) = Idx + 1;
    }

    for(; Idx > 0; -- Idx)
    {
        if((pItem = NuHashLeft(pItem)) == NULL)
        {
            break;
        }
    }

    if(Idx > 0)
    {
        for(; Idx > 0; -- Idx)
        {
            _GroupAdd(pFixMsg, pLeading);
        }

        pItem = NuHashSearch(pFixMsg->Hash, &Tag, sizeof(int), &HashIdx);
    }

    return pItem;
}

int NuFixMsgGroupAssign(NuFixMsg_t *pFixMsg, int Tag, int Idx, char *pVal, size_t Len)
{
    NuHashItem_t    *pItem = NuFixMsgGroupFind(pFixMsg, Tag, Idx);

    if(!pItem)
    {
        return NU_FAIL;
    }

    _FieldAssign(pItem, pVal, Len);

    return NU_OK;
}

int NuFixMsgGroupSet(NuFixMsg_t *pFixMsg, int Tag, int Idx, char *pVal, size_t Len)
{
    NuHashItem_t    *pItem = NuFixMsgGroupFind(pFixMsg, Tag, Idx);

    if(!pItem)
    {
        return NU_FAIL;
    }

    _FieldSet(pItem, pVal, Len);

    return NU_OK;
}

int NuFixMsgGroupVPrintf(NuFixMsg_t *pFixMsg, int Tag, int Idx, char *Format, va_list ArguList)
{
    NuHashItem_t    *pItem = NuFixMsgGroupFind(pFixMsg, Tag, Idx);

    if(!pItem)
    {
        return NU_FAIL;
    }

    _FieldSprintf(pItem, Format, ArguList);

    return NU_OK;
}

int NuFixMsgGroupPrintf(NuFixMsg_t *pFixMsg, int Tag, int Idx, char *Format, ...)
{
    int     Result = 0;
    va_list ArguList;

    va_start(ArguList, Format);
    Result = NuFixMsgGroupVPrintf(pFixMsg, Tag, Idx, Format, ArguList);
    va_end(ArguList);

    return NU_OK;
}

int NuFixMsgGroupRemove(NuFixMsg_t *pFixMsg, int Tag, int Idx)
{
    unsigned int    idx = 0;
    NuHashItem_t    *pItem = NuHashSearch(pFixMsg->Hash, &Tag, sizeof(int), &idx);

    if(pItem == NULL)
    {
        return NU_FAIL;
    }

    pItem = NuHashRightMost(pItem);

    for(; Idx > 0; -- Idx)
    {
        if((pItem = NuHashLeft(pItem)) == NULL)
        {
            return NU_OK;
        }
    }

    _FieldRemove(pItem);
    return NU_OK;
}

int NuFixMsgInstanceRemove(NuFixMsg_t *pFixMsg, int LeadingTag, int Idx)
{
    int             Cnt = 0;
    unsigned int    idx = 0;
    NuHashItem_t    *pItem = NuHashSearch(pFixMsg->Hash, &LeadingTag, sizeof(int), &idx);
    base_vector_t   *pVec = NULL;
    base_vector_it  VecIt;

    if(pItem == NULL)
    {
        return NU_FAIL;
    }

    base_vector_it_set(VecIt, pVec = ((NuFixMsgInfo_t *)(pItem->Hook))->RepeatingGroupMember);
    while(VecIt != base_vector_it_end(pVec))
    {
        pItem = NuHashRightMost(((NuFixMsgInfo_t *)(*VecIt))->pItem);
        for(Cnt = 0; Cnt < Idx; ++ Cnt)
        {
            if((pItem = NuHashLeft(pItem)) == NULL)
            {
                return NU_FAIL;
            }
        }

        _FieldRemove(pItem);

        ++ VecIt;
    }

    return NU_OK;
}

NuFixTag_t *NuFixMsgGroupGet(NuFixMsg_t *pFixMsg, int Tag, int Idx)
{
    unsigned int    idx = 0;
    NuHashItem_t    *pItem = NuHashSearch(pFixMsg->Hash, &Tag, sizeof(int), &idx);

    if(pItem == NULL)
    {
        return NULL;
    }

    pItem = NuHashRightMost(pItem);

    for(; Idx > 0; -- Idx)
    {
        if((pItem = NuHashLeft(pItem)) == NULL)
        {
            return NULL;
        }
    }

    return pItem;
}

char *NuFixMsgGetVal(NuHashItem_t *pItem)
{
    if(pItem == NULL)
    {
        return NULL;
    }

    return (char *)(pItem->Value);
}

size_t NuFixMsgGetSize(NuHashItem_t *pItem)
{
    if(pItem == NULL)
    {
        return 0;
    }

    return pItem->Vlen;
}

int NuFixMsgGetKind(NuHashItem_t *pItem)
{
    if(pItem == NULL)
    {
        return -1;
    }

    return ((NuFixMsgInfo_t *)(pItem->Hook))->Kind;
}

static void _NuFixMsgParse(NuFixMsg_t *pFixMsg, char *Msg)
{
    int             Tag = 0, RepeatingInstance = 0, CurrentInstance = 0, Cnt = 0;
    unsigned int    idx = 0;
    char            *ptr = NuStrGet(pFixMsg->InMsg);
    char            *pTagStart = NULL, *pVal = NULL;
    NuHashItem_t    *pItem = NULL;
    NuFixMsgInfo_t  *pInfo = NULL, *pLeading = NULL;
    NuHash_t        *pHash = pFixMsg->Hash;

    if(*ptr == NuFixMsgDelimiter)
    {
        pTagStart = ++ ptr;
    }
    else
    {
        pTagStart = ptr;
    }

    while((ptr = strchr(ptr, NuFixMsgTagValSep)) != NULL)
    {
        *ptr = NuFixMsgTerminator;
        Tag = atoi(pTagStart);

        if((pItem = NuHashSearch(pHash, &Tag, sizeof(int), &idx)) == NULL)
        {
            if((ptr = strchr(++ ptr, NuFixMsgDelimiter)) != NULL)
            {
                pTagStart = ++ ptr;
            }
            else
            {
                break;
            }

            continue;
        }
        else if(*(pVal = ++ ptr) == NuFixMsgDelimiter)
        {
            pTagStart = ++ ptr;
            continue;
        }

        if((ptr = strchr(ptr, NuFixMsgDelimiter)) == NULL)
        {
            break;
        }

        *ptr = NuFixMsgTerminator;
        pInfo = (NuFixMsgInfo_t *)(pItem->Hook);

        if(pInfo->Kind & NuFixMsgKindRepeatingGroupMember)
        {
            if(RepeatingInstance > 0)
            {
                pItem = NuHashRightMost(pItem);
                if(CurrentInstance > 0)
                {
                    for(Cnt = 1; Cnt < CurrentInstance; ++ Cnt)
                    {
                        pItem = NuHashLeft(pItem);
                    }

                    if(NuHashGetValue(pItem) != NULL)
                    {
                        if(++ CurrentInstance > RepeatingInstance)
                        {
                            -- CurrentInstance;
                        }
                        else
                        {
                            if(CurrentInstance > NuHashGetCnt(pItem))
                            {
                                _GroupAdd(pFixMsg, pLeading);
                            }

                            _FieldAssign(NuHashLeft(pItem), pVal, ptr - pVal);
                        }
                    }
                    else
                    {
                        _FieldAssign(pItem, pVal, ptr - pVal);
                    }
                }
                else
                {
                    ++ CurrentInstance;
                    _FieldAssign(pItem, pVal, ptr - pVal);
                }
            }
        }
        else
        {
            if(pLeading != NULL)
            {
                pLeading->CurrentRepeatingInstance = CurrentInstance;
                pLeading = NULL;
            }

            _FieldAssign(pItem, pVal, ptr - pVal);

            if(pInfo->Kind & NuFixMsgKindRepeatingGroupLeader)
            {
                CurrentInstance = 0;
                pLeading = pInfo;
                RepeatingInstance = atoi(pVal);
            }
        }

        pTagStart = ++ ptr;
    }

    if(pLeading != NULL)
    {
        pLeading->CurrentRepeatingInstance = CurrentInstance;
        pLeading = NULL;
    }

    return;
}

void NuFixMsgParse(NuFixMsg_t *pFixMsg, char *Msg)
{
    NuStrCpy(pFixMsg->InMsg, Msg);
    _NuFixMsgParse(pFixMsg, Msg);
}

void NuFixMsgParseByLen(NuFixMsg_t *pFixMsg, char *Msg, size_t MsgLen)
{
    NuStrNCpy(pFixMsg->InMsg, Msg, MsgLen);
    _NuFixMsgParse(pFixMsg, Msg);
}

static void _AppendTag(NuStr_t *pStr, NuFixMsgInfo_t *Tag, char *pVal, size_t ValLen)
{
    NuStrCat(pStr, Tag->TagStr);
    NuStrNCat(pStr, pVal, ValLen);
    NuStrCatChr(pStr, NuFixMsgDelimiter);

    return;
}

static void _GenTag(NuStr_t *pStr, NuFixMsgInfo_t *Tag)
{
    int             Cnt = 0, i = 0, i2 = 0;
    char            *pVal = NULL;
    NuHashItem_t    *pItem = Tag->pItem;
    base_vector_t   *pVec = Tag->RepeatingGroupMember;
    base_vector_it  VecIt;
    NuFixMsgInfo_t  *pInfo = NULL;

    if((pVal = (char *)NuHashGetValue(pItem)) != NULL)
    {
        _AppendTag(pStr, Tag, pVal, NuHashGetVlen(pItem));

        if(Tag->Kind & NuFixMsgKindRepeatingGroupLeader)
        {
            Cnt = Tag->CurrentRepeatingInstance;

            for(i = 0; i < Cnt; ++ i)
            {
                base_vector_it_set(VecIt, pVec);
                while(VecIt != base_vector_it_end(pVec))
                {
                    pItem = ((NuFixMsgInfo_t *)(*VecIt))->pItem;
                    for(i2 = 0; i2 < i; ++ i2)
                    {
                        pItem = NuHashLeft(pItem);
                    }

                    pInfo = (NuFixMsgInfo_t *)(pItem->Hook);
                    if((pVal = (char *)NuHashGetValue(pItem)) != NULL)
                    {
                        _AppendTag(pStr, pInfo, pVal, NuHashGetVlen(pItem));
                    }

                    ++ VecIt;
                }
            }
        }
    }

    return;
}

NuStr_t *NuFixMsgGen(NuFixMsg_t *pFixMsg, int Gen)
{
    int             Len = 0;
    char            *ptr = NULL;
    base_vector_t   *pVec = NULL;
    base_vector_it  VecIt;
    NuStr_t         *pStr = pFixMsg->OutMsg;

    NuStrClear(pStr);

    if(Gen & NuFixMsgKindShell)
    {
        NuStrCat(pStr, NuFixMsgShellEight);
        NuStrCat(pStr, (char *)NuHashGetValue(NuFixMsgFieldGet(pFixMsg, 8)));
        NuStrCat(pStr, NuFixMsgShellNine);

        Len = NuStrSize(pStr);
    }

    if(Gen & NuFixMsgKindHeader)
    {
        base_vector_it_set(VecIt, pVec = pFixMsg->Header);
        while(VecIt != base_vector_it_end(pVec))
        {
            _GenTag(pStr, *VecIt);
            ++ VecIt;
        }
    }

    if(Gen & NuFixMsgKindBody)
    {
        base_vector_it_set(VecIt, pVec = pFixMsg->Body);
        while(VecIt != base_vector_it_end(pVec))
        {
            _GenTag(pStr, *VecIt);
            ++ VecIt;
        }
    }

    if(Gen & NuFixMsgKindShell)
    {
        ptr = strchr(NuStrGet(pStr), NuFixMsgDelimiter) + 3;

        ptr += NuCStrPrintInt(ptr, NuStrSize(pStr) - Len, 5);
        *ptr = NuFixMsgDelimiter;

        NuStrAppendPrintf2(pStr, "%s%03u%c", NuFixMsgShellTen, NuFixMsgGenCheckSum(NuStrGet(pStr), NuStrSize(pStr)), NuFixMsgDelimiter);
    }

    return pStr;
}

char *NuFixMsgTakeOutMsg(NuFixMsg_t *pFixMsg)
{
    return NuStrGet(pFixMsg->OutMsg);
}

size_t NuFixMsgGetOutMsgSize(NuFixMsg_t *pFixMsg)
{
    return NuStrSize(pFixMsg->OutMsg);
}

unsigned int NuFixMsgGenCheckSum(char *Msg, size_t Len)
{
    unsigned int CheckSum = 0;

    while(Len --)
    {
        CheckSum += (unsigned int)(*Msg ++);
    }

    return (CheckSum & 255);
}

void NuFixMsgTagExchange(NuFixMsg_t *pFixMsg, int Tag1, int Tag2)
{
    char            *TmpValue = NULL;
    size_t          TmpLen = 0;
    NuHashItem_t    *pItem1 = NuFixMsgFieldGet(pFixMsg, Tag1);
    NuHashItem_t    *pItem2 = NuFixMsgFieldGet(pFixMsg, Tag2);

    if(pItem1 != NULL && pItem2 != NULL)
    {
        TmpValue = pItem1->Value;
        TmpLen = pItem1->Vlen;
    
        pItem1->Value = pItem2->Value;
        pItem1->Vlen = pItem2->Vlen;

        pItem2->Value = TmpValue;
        pItem2->Vlen = TmpLen;
    }

    return;
}

