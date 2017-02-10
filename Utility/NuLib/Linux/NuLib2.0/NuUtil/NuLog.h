/** @file NuLog.h  
*   @brief Log object             
*   @note Log object
*                                         */
#ifndef _NULOG_H
#define _NULOG_H

#ifdef __cplusplus
extern "C" {
#endif

#include "NuCommon.h"
#include "NuLock.h"
#include "NuTime.h"
#include "NuFile.h"
#include "NuStream.h"

/* macro */
/* -------------------------------------------------------------------- */
#define NuLogLogTag         " [MSG] "
#define NuLogLogTagLen      sizeof(NuLogLogTag) - 1
#define NuLogErrorTag       " [ERR] "
#define NuLogErrorTagLen    sizeof(NuLogErrorTag) - 1
#define NuLogBinTag         " [WRT] "
#define NuLogBinTagLen      sizeof(NuLogBinTag) - 1

#define NuLogPrefixLen      sizeof("hh:mm:ss.SSSSSS [TAG] ") - 1
#define NuLogActualLogLen   NUSTRBUFSIZ - NuLogPrefixLen

#define NuMBSz              (1024 * 1024)

typedef struct _NuLog_t
{
    NuStrm_t        *FStream;
    NuLock_t        Lock;
    int             AutoFlush;    /* NUFALSE : not auto , NUTRUE : auto */
    char            *LogBuf;
    char            *LogBufStart;
    char            *ErrBuf;
    char            *ErrBufStart;
    char            *BinBuf;
    char            *BinBufStart;
} NuLog_t;

int NuLogOpen(NuLog_t **pLog, char *pPath, char *pFileName);
int NuLogOpen2(NuLog_t **pLog, char *pPath, char *pFileName, int AutoFlush);
int NuLogOpen3(NuLog_t **pLog, char *pPath, char *pFileName, int AutoFlush, size_t FileSz, int StreamType);

void NuLogClose(NuLog_t *pLog);
void NuLogSetAutoFlush(NuLog_t *pLog);
int  NuLogSetThreadSafe(NuLog_t *pLog);

void NuLog(NuLog_t *pLog, const char *Format, ...);
void NuLogV(NuLog_t *pLog, const char *Format, va_list ArguList);
void NuLogMsg(NuLog_t *pLog, const char *Msg);

void NuErr(NuLog_t *pLog, const char *Format, ...);
void NuErrV(NuLog_t *pLog, const char *Format, va_list ArguList);
void NuErrMsg(NuLog_t *pLog, const char *Msg);

void NuLogFlush(NuLog_t *pLog);

int NuWriteLineN(NuLog_t *pLog, void *pData, size_t len);

#ifdef __cplusplus
}
#endif

#endif /* _NULOG_H */

