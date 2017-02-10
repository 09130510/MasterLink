
#include "NuIni.h"

typedef struct _NuIniNode_t
{
    NuStr_t         *Key;
    size_t          KeySectionLen;
    NuStr_t         *Value;
} NuIniNode_t;

struct _NuIni_t
{
    NuStr_t         *FileName;
    NuHash_t        *Hash;
    base_vector_t   *AllNodes;
    base_vector_t   *Sections;
};

static NuHashItem_t *_NuIniSearch(NuHash_t *Hash, char *Section, char *Key, unsigned int *Idx)
{
    char            KeyBuffer[4096] = "\0";
    int             KeyLen = 0;

    KeyLen = sprintf(KeyBuffer, "[%s]%s", Section, Key);

    return NuHashSearch(Hash, KeyBuffer, KeyLen, Idx);
}

static NuIniNode_t *_NuIniGetANode(NuHash_t *Hash, char *Section, char *Key, char *Value)
{
    unsigned int    Idx = 0;
    NuIniNode_t     *pNode = (NuIniNode_t *)malloc(sizeof(NuIniNode_t));

    NuStrNew(&(pNode->Key), NULL);
    NuStrNew(&(pNode->Value), NULL);

    NuStrPrintf(pNode->Key, "[%s]%s", Section, Key);
    pNode->KeySectionLen = strlen(Section) + 2;

    NuStrCpy(pNode->Value, Value);

    /* [Section]Key -> Node */
    if(!_NuIniSearch(Hash, Section, Key, &Idx))
    {
        NuHashAdd(Hash, NuStrGet(pNode->Key), NuStrSize(pNode->Key), pNode, sizeof(pNode), Idx);
        return pNode;
    }
    else
    {
        free(pNode);
        return NULL;
    }
}

static void _NuIniAddSection(NuIni_t *pIni, NuIniNode_t *Node)
{
    unsigned int    Idx = 0;
    NuHashItem_t    *pItem = NuHashSearch(pIni->Hash, NuStrGet(Node->Key), Node->KeySectionLen, &Idx);

    if(!pItem)
    {
        NuHashAdd(pIni->Hash, NuStrGet(Node->Key), Node->KeySectionLen, Node, sizeof(Node), Idx);
        base_vector_push(pIni->Sections, Node);
    }
    else
    {
        NuMultiHashAdd(pIni->Hash, pItem, Node, sizeof(Node));
    }

}

static void _NuIniFileFormater(char *Str)
{
    NuCStrReplaceChr(Str, '#', '\0');
    NuCStrReplaceChr(Str, ';', '\0');
    NuCStrReplaceChr(Str, '\n', '\0');
    NuCStrRTrimChr(Str, ' ');
    NuCStrLTrimChr(Str, ' ');

    return;
}

static void _NuIniLoad(NuIni_t *pIni)
{
    char            Line[2048] = "\0";
    char            *ptr = NULL;
    NuStr_t         *Section = NULL;
    NuIniNode_t     *pNode = NULL;
    NuFileStream_t  *File = NULL;

    NuStrNew(&Section, NULL);

    NuFStreamOpen(&File, NuStrGet(pIni->FileName), "r");

	while(NuFStreamReadLine(File, Line, 2048) != NU_READFAIL)
    {
        _NuIniFileFormater(Line);
        switch(*Line)
        {
        case '\0':
            break;
        case '[':
            if((ptr = strchr(Line, ']')) != NULL)
            {
                *ptr = '\0';

                _NuIniFileFormater(Line + 1);
                NuStrCpy(Section, Line + 1);
            }
            break;
        default:
            if((ptr = strchr(Line, '=')) != NULL)
            {
                *ptr = '\0';

                _NuIniFileFormater(Line);
                _NuIniFileFormater(++ ptr);

                if((pNode = _NuIniGetANode(pIni->Hash, NuStrGet(Section), Line, ptr)) != NULL)
                {
                    _NuIniAddSection(pIni, pNode);
                }
            }
            break;
        }
    }

    if(File != NULL)
    {
	    NuFStreamClose(File);
        File = NULL;
    }

    NuStrFree(Section);

    return;
}

int NuIniNew(NuIni_t **pIni, char *pFilePath)
{
    int RC = NU_OK;

    if(NuIsFile(pFilePath) < 0)
    {
        NUGOTO(RC, NU_FAIL, EXIT);
    }

    if(!((*pIni) = (NuIni_t *)malloc(sizeof(NuIni_t))))
    {
        NUGOTO(RC, NU_FAIL, EXIT);
    }

    (*pIni)->AllNodes = NULL;
    base_vector_new(&((*pIni)->AllNodes), 100);

    (*pIni)->Sections = NULL;
    base_vector_new(&((*pIni)->Sections), 10);

    if(NuHashNew(&((*pIni)->Hash), 100) < NU_OK)
    {
        NUGOTO(RC, NU_FAIL, EXIT);
    }

    NuStrNew(&((*pIni)->FileName), pFilePath);

    _NuIniLoad(*pIni);

EXIT:
    if(RC < 0)
    {
        NuIniFree((*pIni));
    }

    return RC;
}

void NuIniFree(NuIni_t *pIni)
{
    base_vector_t   *pVec = NULL;
    base_vector_it  VecIt;

    if(pIni != NULL)
    {
        if(pIni->FileName != NULL)
        {
            NuStrFree(pIni->FileName);
            pIni->FileName = NULL;
        }

        if(pIni->Hash != NULL)
        {
            NuHashFree(pIni->Hash);
            pIni->Hash = NULL;
        }

        if(pIni->AllNodes)
        {
            base_vector_it_set(VecIt, pVec = pIni->AllNodes);
            while(VecIt != base_vector_it_end(pVec))
            {
                free(*VecIt);
                ++ VecIt;
            }

            base_vector_free(pIni->AllNodes);
            pIni->AllNodes = NULL;
        }

        if(pIni->Sections)
        {
            base_vector_free(pIni->Sections);
            pIni->Sections = NULL;
        }
    }

    return;
}

char *NuIniFind(NuIni_t *pIni, char *Section, char *Key)
{
    unsigned int    Idx = 0;
    NuHashItem_t    *pItem = _NuIniSearch(pIni->Hash, Section, Key, &Idx);
    NuIniNode_t     *pNode = NULL;

    if(pItem != NULL)
    {
        pNode = (NuIniNode_t *)NuHashGetValue(pItem);
        if(pNode->KeySectionLen > 0)
        {
            return NuStrGet(pNode->Value);
        }
    }

    return NULL;
}

int  NuIniSectionExist(NuIni_t *pIni, char *Section)
{
	char *ptr = NuIniFind(pIni, Section, "");
	return (ptr == NULL) ? NUFALSE : NUTRUE;
}

void NuIniModify(NuIni_t *pIni, char *Section, char *Key, char *Value)
{
    unsigned int    Idx = 0;
    NuHashItem_t    *pItem = _NuIniSearch(pIni->Hash, Section, Key, &Idx);
    NuIniNode_t     *pNode = NULL;

    if(!pItem)
    {
        if(Value != NULL)
        {
            pNode = _NuIniGetANode(pIni->Hash, Section, Key, Value);
            _NuIniAddSection(pIni, pNode);
        }
    }
    else
    {
        pNode = (NuIniNode_t *)NuHashGetValue(pItem);
        if(!Value)
        {
            pNode->KeySectionLen = 0;
        }
        else
        {
            NuStrCpy(pNode->Value, Value);
        }
    }

    return;
}

static void _NuIniSave_Key(NuIni_t *pIni, char *Section, char *Key, char *Value, void *Argu)
{
    NuFileStream_t  *File = (NuFileStream_t *)Argu;

	NuFStreamPrintf(File, "%s = %s\n", Key, Value);

    return;
}

static void _NuIniSave_Section(NuIni_t *pIni, char *Section, char *Key, char *Value, void *Argu)
{
    NuFileStream_t  *File = (NuFileStream_t *)Argu;

	NuFStreamPrintf(File, "[%s]\n", Section);

    NuIniTraverse(pIni, Section, &_NuIniSave_Key, Argu);

    NuFStreamPutC(File, ';');
    NuFStreamPutC(File, '\n');
    return;
}

void NuIniSave(NuIni_t *pIni, char *FilePath)
{
    NuFileStream_t  *File = NULL;
    NuStr_t         *pFilePath = NULL;

    NuStrNew(&(pFilePath), NULL);

    if(!FilePath)
    {
        FilePath = NuStrGet(pIni->FileName);
    }

    NuStrPrintf(pFilePath, "%s.NuIniTmp", FilePath);

    NuFStreamOpen(&File, NuStrGet(pFilePath), "w+");
    NuFStreamPutC(File, '\n');

    NuIniTraverse(pIni, NULL, &_NuIniSave_Section, File);

    NuFStreamPutC(File, '\n');
    NuFStreamClose(File);

    rename(NuStrGet(pFilePath), FilePath);

    NuStrFree(pFilePath);
    
    return;
}

void NuIniTraverse(NuIni_t *pIni, char *Section, NuIniCBFn Fn, void *Argu)
{
    base_vector_t   *pVec = pIni->Sections;
    base_vector_it  VecIt;
    NuIniNode_t     *pNode = NULL;
    NuStr_t         *Str = NULL;
    NuHashItem_t    *pItem = NULL;
    unsigned int    Idx = 0;

    NuStrNew(&Str, NULL);

    if(!Section)
    { /* Traverse all Section */
        base_vector_it_set(VecIt, pVec);
        while(VecIt != base_vector_it_end(pVec))
        {
            pNode = (NuIniNode_t *)(*VecIt);
            NuStrNCpy(Str, NuStrGet(pNode->Key) + 1, pNode->KeySectionLen - 2);

            Fn(pIni, NuStrGet(Str), NULL, NULL, Argu);
            ++ VecIt;
        }
    }
    else
    { /* Traverse all Key under Seciton */
        NuStrPrintf(Str, "[%s]", Section);

        if((pItem = NuHashSearch(pIni->Hash, NuStrGet(Str), NuStrSize(Str), &Idx)) != NULL)
        {
            pItem = NuHashRightMost(pItem);

            do
            {
                pNode = (NuIniNode_t *)NuHashGetValue(pItem);

                if(pNode->KeySectionLen > 0)
                {
                    Fn(pIni, Section, NuStrGet(pNode->Key) + pNode->KeySectionLen, NuStrGet(pNode->Value), Argu);
                }
            }
            while((pItem = NuHashLeft(pItem)) != NULL);
        }


    }

    NuStrFree(Str);
    return;
}

char *NuIniGetFileName(NuIni_t *pIni)
{
	return NuStrGet(pIni->FileName);
}

