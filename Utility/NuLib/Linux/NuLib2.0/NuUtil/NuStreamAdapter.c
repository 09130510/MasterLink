#include "NuStream.h"
#include "NuMMapStream.h"
#include "NuFileStream.h"

/* internal functions */
/* ====================================================================== */

/* functions */
/* ====================================================================== */
/* ---------------------------------------------
 * for NuMMapStream
 * --------------------------------------------- */
static int MPStrmAllocFn(void *Argv, char *pFile, size_t len)
{
	NuStrm_t   *pStrm = (NuStrm_t *)Argv;
	NuMPStrm_t *pMPStrm = NULL;
	char       *pe = NULL;
	size_t     allocSz = 0;

	allocSz = NU_RALIGN_PAGE(len);

	NuMPStrmNew(&pMPStrm, pFile, "a+", allocSz, PAGE_READWRITE , FILE_MAP_ALL_ACCESS);

	if (pMPStrm == NULL)
		return NU_FAIL;

	pStrm->LeftSz = allocSz;
	pe = (char *)pMPStrm->addr;

	if (*pe =='\0') 
	{
		memset(pMPStrm->addr, MMAP_END, MMAP_END_SZ);
	}

	/* for end of file */
	pStrm->LeftSz -= MMAP_END_SZ;

	pStrm->Stream = (void *)pMPStrm;
	return NU_OK;
}

static int MPStrmFreeFn(void *Argv)
{
	NuStrm_t   *pStrm = (NuStrm_t *)Argv;
	void       *ptr = NULL;
	NuMPStrm_t *pMPStrm = NULL;

	if (pStrm->vStrm != NULL)
	{
		while ( base_vector_pop(pStrm->vStrm, &ptr) != NU_EMPTY )
		{
			pMPStrm = (NuMPStrm_t *)ptr;
			NuMPStrmFree(pMPStrm);
		}
	}

	return NU_OK;
}

static int MPStrmFlushFn(void *Argv)
{
	NuStrm_t   *pStrm = (NuStrm_t *)Argv;
	NuMPStrm_t *pMPStrm = (NuMPStrm_t *)pStrm->Stream;
	NuMPStrmSync(pMPStrm, FILE_MAP_ALL_ACCESS);	
	return NU_OK;
}

static int MPStrmSeekToEndFn(void *Argv)
{
	void  *ptr = NULL;
    int   *pe = NULL;
	size_t seek_len = 0;

	NuStrm_t   *pStrm = (NuStrm_t *)Argv;
	NuMPStrm_t *pMPStrm = (NuMPStrm_t *)pStrm->Stream;

	ptr = NuMPStrmGetAddr(pMPStrm);
	pe = ptr;
	while (*pe != MMAP_END)
	{
		--pStrm->LeftSz;
		pe = ++ptr;
	}

	seek_len = (NU_RALIGN_PAGE(pStrm->AllocSz) - pStrm->LeftSz - MMAP_END_SZ);
	if (seek_len > 0)
		NuMPStrmGet(pMPStrm,seek_len, &ptr);

	return pStrm->LeftSz;
}

static size_t MPStrmWriteNFn(void *Argv, void *data, size_t len)
{
	NuStrm_t   *pStrm = (NuStrm_t *)Argv;
	NuMPStrm_t *pMPStrm = (NuMPStrm_t *)(pStrm->Stream);

	NuMPStrmWriteN(pMPStrm, data, len);

	memset(pMPStrm->addr, MMAP_END, MMAP_END_SZ);

	return len;
}

NuStrmFnType_t NuStrmMmap = {&MPStrmFlushFn, &MPStrmSeekToEndFn, &MPStrmFreeFn, &MPStrmWriteNFn, &MPStrmAllocFn};

/* ---------------------------------------------
 * for NuFileStream
 * --------------------------------------------- */
static int FStrmAllocFn(void *Argv, char *pFile, size_t len)
{
	NuStrm_t       *pStrm = (NuStrm_t *)Argv;
	NuFileStream_t *pFStrm = NULL;

	
	NuFStreamOpen(&pFStrm, pFile, "a+");
	if (pFStrm == NULL)
		return NU_FAIL;

	pStrm->LeftSz = len;

	pStrm->Stream = (void *)pFStrm;
	return NU_OK;
}

static int FStrmFreeFn(void *Argv)
{
	NuStrm_t       *pStrm = (NuStrm_t *)Argv;
	NuFileStream_t *pFStrm = NULL;
	void           *ptr = NULL;

	if (pStrm->vStrm != NULL)
	{
		while ( base_vector_pop(pStrm->vStrm, &ptr) != NU_EMPTY )
		{
			pFStrm = (NuFileStream_t *)ptr;
			NuFStreamClose(pFStrm);
		}
	}

	return NU_OK;
}

static int FStrmFlushFn(void *Argv)
{
	NuStrm_t       *pStrm = (NuStrm_t *)Argv;
	NuFileStream_t *pFStrm = (NuFileStream_t *)(pStrm->Stream);

	NuFStreamFlush(pFStrm);
	return NU_OK;
}

static int FStrmSeekToEndFn(void *Argv)
{
	NuStrm_t       *pStrm   = (NuStrm_t *)Argv;
	NuFileStream_t *pFStrm = NULL;

	pFStrm = (NuFileStream_t *)base_vector_get_by_index(pStrm->vStrm, base_vector_get_cnt(pStrm->vStrm) - 1);

	NuFStreamSeek(pFStrm, 0L, SEEK_END);

	pStrm->LeftSz = pStrm->AllocSz - NuFStreamGetSize(pFStrm);

	return pStrm->LeftSz;
}

static size_t FStrmWriteNFn(void *Argv, void *data, size_t len)
{
	NuStrm_t       *pStrm = (NuStrm_t *)Argv;
	NuFileStream_t *pFStrm = (NuFileStream_t *)(pStrm->Stream);

	return NuFStreamWriteN(pFStrm, data, len);
}

NuStrmFnType_t NuStrmFile = {&FStrmFlushFn, &FStrmSeekToEndFn, &FStrmFreeFn, &FStrmWriteNFn, &FStrmAllocFn};

