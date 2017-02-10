/* Header File included section */
#include "NuSeqNo.h"
#include "NuSeqNo_String.c"
#include "NuSeqNo_Int.c"

/* ----------------------------------------------------------------------------------------
 * Internal function
 * ---------------------------------------------------------------------------------------- */
static int _NuSeqNoSetFile(NuSeqNo_t *pSeqNo)
{
    int     iRC = NU_OK;
    int     Len = pSeqNo->iLength;
    int     IsNewFile = 0;
    int     iSize = NU_ALIGN8( ((Len * 2) + 10) );

    if(NuIsFile(NuStrGet(pSeqNo->pPath)) < 0)
        IsNewFile = 1;

    if((iRC = NuMMapNew(&(pSeqNo->pSeqFile), NuStrGet(pSeqNo->pPath), "a+", iSize, PAGE_READWRITE, FILE_MAP_ALL_ACCESS)) < 0)
        return iRC;

    pSeqNo->pSeqFilePos2 = (pSeqNo->pSeqFilePos1 = (char *)NuMMapGetAddr(pSeqNo->pSeqFile)) + Len + 1;

    if(IsNewFile)
        memset(pSeqNo->pSeqFilePos1, '\0', iSize);

    if(*(pSeqNo->pSeqFilePos1) == '\0')
    { /* First position is null. */
        if(*(pSeqNo->pSeqFilePos2) != '\0')
            pSeqNo->pCurSeqNo = pSeqNo->pSeqFilePos2;
        else
            memcpy(pSeqNo->pCurSeqNo = pSeqNo->pSeqFilePos1, pSeqNo->pMinSeqNo, pSeqNo->iLength);
    }
    else
    { /* First position nonnull. */
        if(*(pSeqNo->pSeqFilePos2) == '\0')
            pSeqNo->pCurSeqNo = pSeqNo->pSeqFilePos1;
        else
        {
            if((iRC = pSeqNo->Type.Compare(pSeqNo->pSeqFilePos1, Len, pSeqNo->pSeqFilePos2)) < 0)
                pSeqNo->pCurSeqNo = pSeqNo->pSeqFilePos2;
            else
                pSeqNo->pCurSeqNo = pSeqNo->pSeqFilePos1;
        }
    }

    return NU_OK;
}

/* ----------------------------------------------------------------------------------------
 * public   function
 * ---------------------------------------------------------------------------------------- */
void NuSeqNoFlush(NuSeqNo_t *pSeqNo)
{
    return;
}

void NuSeqNoSetThreadSafe(NuSeqNo_t *pObj)
{
    NuLockDestroy(&(pObj->Lock));
    NuLockInit(&(pObj->Lock), &NuLockType_Spin);
}

int  NuSeqNoNew(NuSeqNo_t **pObj, NuSeqNoType_t *Type, int SeqNoLen, char *szFilePath, char *szSeqNoName)
{
    return NuSeqNoNew2(pObj, Type, SeqNoLen, szFilePath, szSeqNoName, NULL, NULL, NULL);
}

int  NuSeqNoNew2(NuSeqNo_t **pObj, NuSeqNoType_t *Type, int SeqNoLen, char *szFilePath, char *szSeqNoName, 
                 NuSeqNoNextFn Next_fn, NuSeqNoCmpFn Compare_fn, void *arg)
{
    int      iRC = 0;

    if (SeqNoLen <= 0)
        NUGOTO(iRC, NU_PARAMERROR, EXIT);

    if (szFilePath[0] == '\0')
        NUGOTO(iRC, NU_PARAMERROR, EXIT);

    (*pObj) = (NuSeqNo_t *)malloc( sizeof(NuSeqNo_t) );
    if ((*pObj) == NULL)
    {
        NUGOTO(iRC, NU_MALLOC_FAIL, EXIT);
    }
    else
    {
        (*pObj)->pName = NULL;
        (*pObj)->pPath = NULL;
        (*pObj)->pCurSeqNo = NULL;
        (*pObj)->pMaxSeqNo = NULL;
        (*pObj)->pMinSeqNo = NULL;
        (*pObj)->pSeqFile = NULL;
        (*pObj)->pSeqFilePos1 = NULL;
        (*pObj)->pSeqFilePos2 = NULL;
        (*pObj)->arg = arg;
    	(*pObj)->iLength = SeqNoLen;

		memcpy(&((*pObj)->Type), Type, sizeof(NuSeqNoType_t));
		if (Next_fn != NULL)
			(*pObj)->Type.Next = Next_fn;
		if (Compare_fn != NULL)
			(*pObj)->Type.Compare = Compare_fn;
	}

    NuLockInit(&((*pObj)->Lock), &NuLockType_NULL);

    NuStrNew( &((*pObj)->pName), NULL );
    NuStrNew( &((*pObj)->pPath), NULL );

    (*pObj)->pMaxSeqNo = NULL;
    (*pObj)->pMaxSeqNo = (char *)malloc(sizeof(char) * SeqNoLen);

    (*pObj)->pMinSeqNo = NULL;
    (*pObj)->pMinSeqNo = (char *)malloc(sizeof(char) * SeqNoLen);

    if ((*pObj)->pName     == NULL ||
        (*pObj)->pMaxSeqNo == NULL ||
        (*pObj)->pMinSeqNo == NULL ||
        (*pObj)->pPath     == NULL  )
    {
        NUGOTO(iRC, NU_MALLOC_FAIL, EXIT);
    }

	(*pObj)->Type.SeqNoInit((*pObj), NULL);

    if(NuIsDir(szFilePath) < 0)
        NuCreateRecursiveDir(szFilePath);

    NuStrCat((*pObj)->pName, szSeqNoName);
    NuStrPrintf((*pObj)->pPath, "%s%c%s.seq", szFilePath, NUFILE_SEPARATOR, szSeqNoName);

    iRC = _NuSeqNoSetFile((*pObj));

    if (iRC < 0)
        goto EXIT;
    iRC = NU_OK;
EXIT:

    return iRC;
}

void NuSeqNoFree(NuSeqNo_t *pObj)
{
    NuLockLock(&(pObj->Lock));

    NuSeqNoFlush(pObj);

    NuStrFree(pObj->pName);
    NuStrFree(pObj->pPath);
    
    if(pObj->pMaxSeqNo != NULL)
        free(pObj->pMaxSeqNo);

    if(pObj->pMinSeqNo != NULL)
        free(pObj->pMinSeqNo);

    NuMMapFree(pObj->pSeqFile);

    NuLockUnLock(&(pObj->Lock));

    NuLockDestroy(&(pObj->Lock));

    free(pObj);
    pObj = NULL;
}

int NuSetMaxNo(NuSeqNo_t *pObj, void *pSeqNo)
{
	NuLockLock(&(pObj->Lock));
	pObj->Type.SetMaxNo(pObj, pSeqNo);
	NuLockUnLock(&(pObj->Lock));
    return NU_OK;
}

int NuSetMinNo(NuSeqNo_t *pObj, void *pSeqNo)
{
	NuLockLock(&(pObj->Lock));
	pObj->Type.SetMinNo(pObj, pSeqNo);
	NuLockUnLock(&(pObj->Lock));
    return NU_OK;
}

int NuSetCurNo(NuSeqNo_t *pObj, void *pSeqNo)
{
	NuLockLock(&(pObj->Lock));
	pObj->Type.SetCurNo(pObj, pSeqNo);
	NuLockUnLock(&(pObj->Lock));
    return NU_OK;
}

int NuGetCurNo(NuSeqNo_t *pObj, void *pSeqNo)
{
	int iRC = NU_OK;
	NuLockLock(&(pObj->Lock));
    iRC = pObj->Type.GetCurNo(pObj, pSeqNo);
	NuLockUnLock(&(pObj->Lock));
	return iRC;

}

int NuSeqNoPop(NuSeqNo_t *pObj, void *pSeqNo)
{
	int iRC = NU_OK;
	NuLockLock(&(pObj->Lock));
	iRC = pObj->Type.SeqNoPop(pObj, pSeqNo);
	NuLockUnLock(&(pObj->Lock));
	return iRC;
}

int  NuSeqNoPush(NuSeqNo_t *pObj, void *pSeqNo)
{
	int iRC = NU_OK;
	NuLockLock(&(pObj->Lock));
	iRC = pObj->Type.SeqNoPush(pObj, pSeqNo);
	NuLockUnLock(&(pObj->Lock));
	return iRC;
}

