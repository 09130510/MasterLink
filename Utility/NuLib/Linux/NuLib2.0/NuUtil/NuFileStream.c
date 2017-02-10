#include "NuFileStream.h"

/* -------------------------------------------------------------------- */
/* file r/w function                                                    */
/* -------------------------------------------------------------------- */
int NuFStreamOpen(NuFileStream_t **pFobj, char *pFile, char *mode)
{
	int iRC = NU_OK;

	(*pFobj) = (NuFileStream_t *)malloc(sizeof(NuFileStream_t));
	if ((*pFobj) == NULL)
		NUGOTO(iRC, NU_MALLOC_FAIL, EXIT);

	memset((*pFobj), 0, sizeof(NuFileStream_t));
	NuStrNew(&((*pFobj)->pFile), pFile);
//	NuStrCpy((*pFobj)->pFile, pFile);

	NuStrNew(&((*pFobj)->Mode), mode);
//	NuStrCpy((*pFobj)->Mode, mode);

	(*pFobj)->lPos = 0;
	(*pFobj)->AutoFlush = 0;

	(*pFobj)->FHdl = fopen(pFile, mode);
	if ((*pFobj)->FHdl == NULL)
		NUGOTO(iRC, NU_OPENFILEFAIL, EXIT);

	if(stat(NuStrGet((*pFobj)->pFile), &((*pFobj)->FileStat)) < 0)
		NUGOTO(iRC, NU_FILEUNSTABLE, EXIT);

EXIT:
	if (iRC < 0)
	{
		NuFStreamClose(*pFobj);
		(*pFobj) = NULL;
	}

	return iRC;
}

void NuFStreamClose(NuFileStream_t *pFobj)
{
	if(pFobj != NULL)
	{
		if (pFobj->FHdl != NULL)
			fclose(pFobj->FHdl);

		if (pFobj->pFile != NULL)
			NuStrFree(pFobj->pFile);

		if (pFobj->Mode != NULL)
			NuStrFree(pFobj->Mode);

		free(pFobj);
	}

	return;
}

int NuFStreamReOpen(NuFileStream_t *pFobj)
{
	FILE	*pFHdlr = pFobj->FHdl;
	char	*pFilePath = NuStrGet(pFobj->pFile);

	if(pFHdlr != NULL)
	{
		fclose(pFHdlr);
		pFobj->FHdl = NULL;
	}

	pFobj->lPos = 0;
	pFHdlr = pFobj->FHdl = fopen(pFilePath, NuStrGet(pFobj->Mode));
	if(pFHdlr == NULL)
		return NU_OPENFILEFAIL;

	if(stat(pFilePath, &(pFobj->FileStat)) < 0)
	{
		fclose(pFHdlr);
		return NU_FILEUNSTABLE;
	}

	return NU_OK;
}

void NuFStreamSetAutoFlush(NuFileStream_t *pFobj)
{
	pFobj->AutoFlush = 1;
}

size_t NuFStreamReadN(NuFileStream_t *pFobj, char *pStr, size_t nBytes)
{
	size_t readn = fread(pStr, sizeof(char), nBytes, pFobj->FHdl);

	pStr[readn] = '\0';
	return readn;
}

size_t NuFStreamReadN_Block(NuFileStream_t *pFobj, char *pStr, size_t nBytes)
{
	int    chr   = 0;
	size_t readn = 0;
	char   *p    = pStr;

	do
	{
		chr = fgetc(pFobj->FHdl);
		if (chr == EOF)
		{
			pStr[readn] = '\0';
			fseek(pFobj->FHdl, (pFobj->lPos + readn), SEEK_SET);
			sleep(1);
		}
		else
		{
			*p = chr;
			readn++;
			p++;
		}
	} while ( nBytes > readn );
	pFobj->lPos += readn;
	pStr[readn] = '\0';
	return readn;
}

int NuFStreamReadLine(NuFileStream_t *pFobj, char *pStr, size_t StrLen)
{
	int		chr = 0;
	long	pos_cur = NuFStreamTell(pFobj);
	size_t	Len = StrLen;

	do
	{
		if ((chr = fgetc(pFobj->FHdl)) == EOF)
		{
			/* think ?
			 * must seek to position that before read?
			 * */
			NuFStreamSeek(pFobj, pos_cur, SEEK_SET);
			return NU_READFAIL;
		}
		
		*pStr = chr;

		++ pStr;
		-- Len;
	} while ( (chr != NULINE_END) && Len );

	*pStr = '\0';
	return StrLen - Len;
}

int NuFStreamReadLineByStr(NuFileStream_t *pFobj, NuStr_t *pStr)
{
	int    chr = 0;
	long   pos_cur = NuFStreamTell(pFobj);

	NuStrClear(pStr);
	do
	{
		if ((chr = fgetc(pFobj->FHdl)) == EOF)
		{
			/* think ?
			 * must seek to position that before read?
			 * */
			NuFStreamSeek(pFobj, pos_cur, SEEK_SET);
			return NU_READFAIL;
		}

		NuStrCatChr(pStr, chr);

	} while ( (chr != NULINE_END) );

	return NuStrSize(pStr);
}

int NuFStreamWriteLineN(NuFileStream_t *pFobj, char *pStr, size_t nBytes)
{
	size_t writen = fwrite( pStr, sizeof(char), nBytes, pFobj->FHdl);

	if (writen == 0)
		return NU_OK;

	if (writen == EOF)
		return NU_WRITEFAIL;

	if (pFobj->AutoFlush == 1)
		fflush(pFobj->FHdl);

	writen += (int)fputc( NULINE_END, pFobj->FHdl);

	pFobj->lPos += writen;

	return writen;
}

int NuFStreamWriteN(NuFileStream_t *pFobj, char *pStr, size_t nBytes)
{
	size_t writen = fwrite( pStr, sizeof(char), nBytes, pFobj->FHdl);

	if (writen == 0)
		return NU_OK;

	if (writen == EOF)
		return NU_WRITEFAIL;

	if (pFobj->AutoFlush == 1)
		fflush(pFobj->FHdl);

	pFobj->lPos += writen;

	return writen;
}

int NuFStreamPutC(NuFileStream_t *pFobj, char pChr)
{
	size_t writen = 0;

	writen = (int)fputc( pChr, pFobj->FHdl);

	if (writen == 0)
		return NU_OK;

	if (writen == EOF)
		return NU_WRITEFAIL;

	if (pFobj->AutoFlush == 1)
		fflush(pFobj->FHdl);

	pFobj->lPos += writen;

	return writen;
}

int NuFStreamVPrintf(NuFileStream_t *pFobj, char *fmt, va_list ap)
{
	size_t writen = 0;

	writen = vfprintf(pFobj->FHdl, fmt, ap);

	if (pFobj->AutoFlush == 1)
		fflush(pFobj->FHdl);

	pFobj->lPos += writen;

	return writen;
}

int NuFStreamPrintf(NuFileStream_t *pFobj, char *fmt, ...)
{
	va_list	arg;
	size_t  cnt = 0;

	va_start(arg, fmt);
	cnt = NuFStreamVPrintf(pFobj, fmt, arg);
	va_end(arg);

	return cnt;
}

void NuFStreamFlush(NuFileStream_t *pFobj)
{
	fflush(pFobj->FHdl);
}

int  NuFStreamSeek(NuFileStream_t *pFobj, long offset, int whence)
{
    return fseek(pFobj->FHdl, offset, whence);
}

long NuFStreamTell(NuFileStream_t *pFobj)
{
    return ftell(pFobj->FHdl);
}

int NuFStreamCheck(NuFileStream_t *pFobj)
{
	struct stat	Stat;
	struct stat	*pStat = &(pFobj->FileStat);

	if(stat(NuStrGet(pFobj->pFile), &Stat) < 0)
		return NU_FILENOTEXIST;

	if((Stat.st_ino != pStat->st_ino) || (Stat.st_dev != pStat->st_dev))
		return NU_FILEUNSTABLE;

	return NU_OK;
}

int NuFStreamLastModifyTime(NuFileStream_t *pFobj, char *pTime)
{
	struct stat	Stat;
	char        *pFilePath = NuStrGet(pFobj->pFile);

	if(stat(pFilePath, &Stat) < 0)
		return NU_FAIL;

	strftime(pTime, 18, "%Y%m%d %H:%M:%S", localtime(&(Stat.st_mtime)));
	return NU_OK;
}

void NuFStreamDump(NuFileStream_t *pFobj, char *pData, size_t DataLen)
{
	char  szHex[100] = {0};
	char  szTmp[100] = {0};
	char  szWrite[200] = {0};
	char *ptr        = NULL;
	int   i	         = 0;
	int   j	         = 1;

	ptr = pData;

	for (i = 0; i < DataLen; i++)
	{
		if ( (j != 20) && (i != DataLen - 1) )
		{
			sprintf(szHex, "%s%02x ", szHex, *ptr);

			if ((*ptr == 0x0a) || (*ptr == 0x0d))
				sprintf(szTmp, "%s%s ", szTmp, "\\n");
			else if ((*ptr = 0x00))
				sprintf(szTmp, "%s%c ", szTmp, '.');
			else
				sprintf(szTmp, "%s%c ", szTmp, *ptr);

			j++;
		}
		else
		{
			sprintf(szWrite, "%s     %s", szHex, szTmp);
			if (pFobj == NULL)
				printf("%s\n", szWrite);
			else
				fputs(szWrite, pFobj->FHdl);

			j = 1;
			szHex[0] = '\0';
			szTmp[0] = '\0';
		}

		ptr++;
	}

	return;
}

/* -------------------------------------------------------------------- */
/* for NuRotate function                                                */
/* -------------------------------------------------------------------- */

