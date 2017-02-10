#include "NuMMapStream.h"

/* internal functions */
/* ====================================================================== */

/* functions */
/* ====================================================================== */
int NuMPStrmNew(NuMPStrm_t **pMPStrm, char *pMFile, char *mode, size_t len, int prot, int flags)
{
	int fd_no = 0;
	int is_new_open = 0;

	len = NU_RALIGN_PAGE(len);

	(*pMPStrm) = (NuMPStrm_t *)malloc(sizeof(NuMPStrm_t));
	if ((*pMPStrm) == NULL)
		return NU_FAIL;

	if (pMFile == NULL)
	{
		(*pMPStrm)->MFile = NULL;
		fd_no = -1;
		(*pMPStrm)->fd_no = -1;
		(*pMPStrm)->fd = NULL;
	}
	else
	{
		NuStrNew(&((*pMPStrm)->MFile), NULL);
		if ((*pMPStrm)->MFile == NULL)
		{
			free((*pMPStrm));
			return NU_FAIL;
		}

		if (NuStrCat((*pMPStrm)->MFile, pMFile) < 0)
		{
			NuStrFree((*pMPStrm)->MFile);
			free((*pMPStrm));
			return NU_FAIL;
		}

		/* file exists */
		if (NuIsFile(NuStrGet((*pMPStrm)->MFile)) == 0)
			is_new_open = 0;
		else
			is_new_open = 1;
		/* open file for mmap mapping */
		(*pMPStrm)->fd = fopen(NuStrGet((*pMPStrm)->MFile), mode);
		if ((*pMPStrm)->fd == NULL)
		{
			NuStrFree((*pMPStrm)->MFile);
			free((*pMPStrm));
			return NU_FAIL;
		}
		fd_no = fileno((*pMPStrm)->fd);
		(*pMPStrm)->fd_no = fd_no;
		if (NuFileGetSize(fd_no) < len)
			NuFileSetSize(fd_no, len);
	}

	(*pMPStrm)->fsz = len;
	(*pMPStrm)->fd_no = fd_no;
	(*pMPStrm)->prot = prot;
	(*pMPStrm)->flags = flags;
	if ( ((*pMPStrm)->addr = mmap(NULL, len, prot, flags, fd_no, 0)) == MAP_FAILED )
		goto EXIT;

	(*pMPStrm)->start_addr = (*pMPStrm)->addr;

	if (is_new_open)
		memset((*pMPStrm)->start_addr, 0x00, len);

	return NU_OK;

EXIT:
	if (pMFile != NULL)
	{
		fclose((*pMPStrm)->fd);
		NuStrFree((*pMPStrm)->MFile);
	}
	free((*pMPStrm));
	return NU_FAIL;
}

void NuMPStrmFree(NuMPStrm_t *pMPStrm)
{
	munmap(pMPStrm->start_addr, pMPStrm->fsz);

	if (pMPStrm->MFile != NULL)
	{
		if (pMPStrm->fd != NULL)
			fclose(pMPStrm->fd);
		NuStrFree(pMPStrm->MFile);
	}
	free(pMPStrm);
}

void NuMPStrmFree2(NuMPStrm_t *pMPStrm, size_t len)
{
	munmap(pMPStrm->start_addr, len);

	if (pMPStrm->MFile != NULL)
	{
		if (pMPStrm->fd != NULL)
			fclose(pMPStrm->fd);
		NuStrFree(pMPStrm->MFile);
	}
	free(pMPStrm);
}

int NuMPStrmWriteN(NuMPStrm_t *pMPStrm, char *data, size_t len)
{
	memcpy(pMPStrm->addr, data, len);
	pMPStrm->addr += len;

	return len;
}

int NuMPStrmWriteLine(NuMPStrm_t *pMPStrm, char *data, size_t len)
{
	memcpy(pMPStrm->addr, data, len);
	pMPStrm->addr += len;
	memcpy(pMPStrm->addr, NULINE_END_STR, 1);
	pMPStrm->addr += 1;

	return (len + 1);
}

int NuMPStrmGet(NuMPStrm_t *pMPStrm, size_t len, void **pmem)
{
	(*pmem) = pMPStrm->addr;
	pMPStrm->addr += len;

	return NU_OK;
}

