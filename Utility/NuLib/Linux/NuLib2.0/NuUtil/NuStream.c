#include "NuStream.h"
#include "NuStreamAdapter.c"

/* internal functions */
/* ====================================================================== */

static void _GenFileName(NuStr_t *pFile, char *pDir, char *pName, int Ver )
{
	if (pDir[strlen(pDir)-1] == NUFILE_SEPARATOR)
		NuStrPrintf(pFile, "%s%s.%d", pDir, pName, Ver);
	else
		NuStrPrintf(pFile, "%s%c%s.%d", pDir, 
		                     NUFILE_SEPARATOR, pName, Ver);
}

static void _GetLastVer(NuStrm_t *pStrm, char *pDir, char *pName)
{
	int iVersion = 0;
	NuDirList_t *pLst = &(pStrm->DirList);
	char *ptr = NULL;
	char *pVer = NULL;
	size_t len = strlen(pName);

	pStrm->MaxVer = iVersion;
	NuDirListInit(pLst, pDir);

	if (NuDirListCnt(pLst) > 0)
	{
		do 
		{
			ptr = NuDirListGetName(pLst);
			if ( memcmp( ptr, pName, len) == 0)
			{
				pVer = strrchr(ptr, '.');
				++pVer;
				if (NuCStrIsNumeric(pVer) == NU_OK)
				{
					if (pVer != NULL)
					{
						if (pStrm->MaxVer < atoi(pVer))
							pStrm->MaxVer = atoi(pVer);
					}
				}
			}
		} while (NuDirListNext(pLst) == NU_OK);
	}

	NuDirListClear(pLst);
}

int _WriteExtenFn(void *Argu, void *data, size_t len)
{
	NuStrm_t *pStrm = (NuStrm_t *)Argu;

	if ( len >  pStrm->LeftSz )
	{
		pStrm->MaxVer++;
		_GenFileName(pStrm->File, NuStrGet(pStrm->Dir), NuStrGet(pStrm->StrmName), pStrm->MaxVer);

		if (pStrm->CB_Alloc((void *)pStrm, NuStrGet(pStrm->File), pStrm->AllocSz) < 0)
			return NU_FAIL;
		base_vector_push(pStrm->vStrm, pStrm->Stream);
	}

	if ( pStrm->CB_WriteN(pStrm, data, len) )
	{
		pStrm->LeftSz -= len;
		return NU_OK;
	}
	else
		return NU_FAIL;
}

int _WriteFn(void *Argu, void *data, size_t len)
{
	NuStrm_t *pStrm = (NuStrm_t *)Argu;

	if ( pStrm->CB_WriteN(pStrm, data, len) )
	{
		pStrm->LeftSz -= len;
		return NU_OK;
	}
	else
		return NU_FAIL;
}

/* global   functions */
/* ====================================================================== */

int NuStrmNew(NuStrm_t **pStrm, int StreamType,
                size_t AllocSz, char *pDir, char *pName)
{
	int iRC = 0;
	int i   = 0;

	(*pStrm) = NULL;
	if ( ((*pStrm) = (NuStrm_t *)malloc(sizeof(NuStrm_t))) == NULL)
		return NU_MALLOC_FAIL;
	
	memset((*pStrm), 0, sizeof(NuStrm_t));

	if ( base_vector_new(&((*pStrm)->vStrm), 8) < 0 )
		NUGOTO(iRC, NU_FAIL, EXIT);

	(*pStrm)->MaxVer = 0;
	(*pStrm)->StreamType = StreamType;

	if (StreamType == enFileStream)
	{
		(*pStrm)->AllocSz      = AllocSz;
		(*pStrm)->CB_Alloc     = NuStrmFile.Alloc;
		(*pStrm)->CB_Free      = NuStrmFile.Free;
		(*pStrm)->CB_SeekToEnd = NuStrmFile.SeekToEnd;
		(*pStrm)->CB_Flush     = NuStrmFile.Flush;
		(*pStrm)->CB_WriteN    = NuStrmFile.WriteN;
		(*pStrm)->CB_IntWriteN = (AllocSz <= 0) ? &_WriteFn : &_WriteExtenFn;
	}
	else if (StreamType == enMMapStream)
	{
		(*pStrm)->AllocSz = AllocSz;
		(*pStrm)->CB_Alloc     = NuStrmMmap.Alloc;
		(*pStrm)->CB_Free      = NuStrmMmap.Free;
		(*pStrm)->CB_SeekToEnd = NuStrmMmap.SeekToEnd;
		(*pStrm)->CB_Flush     = NuStrmMmap.Flush;
		(*pStrm)->CB_WriteN    = NuStrmMmap.WriteN;
		if (AllocSz <= 0)
			NUGOTO(iRC, NU_FAIL, EXIT);
		else 
			(*pStrm)->CB_IntWriteN = &_WriteExtenFn;

	}
	else
		NUGOTO(iRC, NU_FAIL, EXIT);

	if (NuStrNew2(&((*pStrm)->File), 64, NULL) != 0)
		NUGOTO(iRC, NU_MALLOC_FAIL, EXIT);

	if (NuStrNew2(&((*pStrm)->Dir), 64, NULL) != 0)
		NUGOTO(iRC, NU_MALLOC_FAIL, EXIT);
	else
		NuStrCat((*pStrm)->Dir, pDir);

	if (NuStrNew2(&((*pStrm)->StrmName), 32, NULL) != 0)
		NUGOTO(iRC, NU_MALLOC_FAIL, EXIT);
	else
		NuStrCat((*pStrm)->StrmName, pName);

	_GetLastVer((*pStrm), pDir, pName);

	for(i = 0; i <= (*pStrm)->MaxVer; i++)
	{
		_GenFileName((*pStrm)->File, pDir, pName, i);
		if ((*pStrm)->CB_Alloc((void *)(*pStrm), NuStrGet((*pStrm)->File), (*pStrm)->AllocSz) < 0)
			NUGOTO(iRC, NU_FAIL, EXIT);
		
		base_vector_push((*pStrm)->vStrm, (*pStrm)->Stream);
	}
	(*pStrm)->CB_SeekToEnd((void *)(*pStrm));
	return NU_OK;

EXIT:
	NuStrmFree((*pStrm));
	return iRC;
}

void NuStrmFree(NuStrm_t *pStrm)
{
	if (pStrm == NULL)
		return;

	pStrm->CB_Free(pStrm);

	if (pStrm->StrmName != NULL)
		NuStrFree(pStrm->StrmName);

	if (pStrm->Dir != NULL)
		NuStrFree(pStrm->Dir);

	if (pStrm->File != NULL)
		NuStrFree(pStrm->File);

	base_vector_free(pStrm->vStrm);
	free(pStrm);		
}

int NuStrmWriteN(NuStrm_t *pStrm, void *data, size_t len)
{
	/*
	if ( len >  pStrm->LeftSz )
	{
		pStrm->MaxVer++;
		_GenFileName(pStrm->File, NuStrGet(pStrm->Dir), NuStrGet(pStrm->StrmName), pStrm->MaxVer);

		if (pStrm->CB_Alloc((void *)pStrm, NuStrGet(pStrm->File), pStrm->AllocSz) < 0)
			return NU_FAIL;
		base_vector_push(pStrm->vStrm, pStrm->Stream);
	}

	if ( pStrm->CB_WriteN(pStrm, data, len) )
	{
		pStrm->LeftSz -= len;
		return NU_OK;
	}
	else
		return NU_FAIL;
	*/
	return pStrm->CB_IntWriteN( (void *)pStrm, data, len);
}

