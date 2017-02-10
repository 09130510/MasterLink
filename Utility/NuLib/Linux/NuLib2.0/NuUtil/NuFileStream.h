#ifndef _NUFILESTREAM_H
#define _NUFILESTREAM_H

#ifdef __cplusplus
extern "C" {
#endif

#include "NuFile.h"
#include "NuStream.h"

/* -------------------------------------------------------------------- */
/* file r/w function */
/* -------------------------------------------------------------------- */
typedef struct _NuFileStream_t
{
	NuStr_t		*pFile;
	NuStr_t		*Mode;
	FILE		*FHdl;
	size_t		lPos;
	struct stat	FileStat;
	int			AutoFlush;    /* 0 : not auto , 1 : auto */
} NuFileStream_t;
#define NuFileStreamSz   sizeof(NuFileStream_t)

int NuFStreamOpen(NuFileStream_t **pFobj, char *pFile, char *mode);
void NuFStreamClose(NuFileStream_t *pFobj);
int NuFStreamReOpen(NuFileStream_t *pFobj);

void NuFStreamSetAutoFlush(NuFileStream_t *pFobj);

size_t NuFStreamReadN(NuFileStream_t *pFobj, char *pStr, size_t nBytes);
size_t NuFStreamReadN_Block(NuFileStream_t *pFobj, char *pStr, size_t nBytes);

int NuFStreamReadLine(NuFileStream_t *pFobj, char *pStr, size_t StrLen);
int NuFStreamReadLineByStr(NuFileStream_t *pFobj, NuStr_t *pStr);

int NuFStreamWriteLineN(NuFileStream_t *pFobj, char *pStr, size_t nBytes);
int NuFStreamWriteN(NuFileStream_t *pFobj, char *pStr, size_t nBytes);
int NuFStreamPutC(NuFileStream_t *pFobj, char pChr);

int NuFStreamVPrintf(NuFileStream_t *pFobj, char *fmt, va_list ap);
int NuFStreamPrintf(NuFileStream_t *pFobj, char *fmt, ...);

void NuFStreamFlush(NuFileStream_t *pFobj);

int  NuFStreamSeek(NuFileStream_t *pFobj, long offset, int whence);
long NuFStreamTell(NuFileStream_t *pFobj);

int NuFStreamCheck(NuFileStream_t *pFobj);

int NuFStreamLastModifyTime(NuFileStream_t *pFobj, char *pTime);

/* dump pData to file */
void NuFStreamDump(NuFileStream_t *pFobj, char *pData, size_t DataLen);

#define NuFStreamEOF(NU_FStream)		feof((NU_FStream)->FHdl)
#define	NuFStreamPath(NU_FStream)		NuStrGet((NU_FStream)->pFile)

#define	NuFStreamGetFileDes(NU_FStream)	fileno((NU_FStream)->FHdl)

#define NuFStreamGetSize(NU_FStream)   NuFileGetSize(fileno((NU_FStream)->FHdl))


#ifdef __cplusplus
}
#endif

#endif /* _NUFILESTREAM_H */

