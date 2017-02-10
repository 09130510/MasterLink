#include "NuLog.h"

/* static function     */
/* ====================================================================== */
static int _NuLogOpen(NuLog_t **pLog, char *pPath, char *pFileName, int AutoFlush, size_t FileSz, int StreamType)
{
    int iRC = 0;
    (*pLog) = NULL;

    if (pPath == NULL || pFileName == NULL)
        return NU_FAIL;

    (*pLog) = (NuLog_t *)malloc(sizeof(NuLog_t));
    if ((*pLog) == NULL)
        NUGOTO(iRC, NU_MALLOC_FAIL, EXIT);  

    (*pLog)->FStream = NULL;
    (*pLog)->LogBuf = NULL;
    (*pLog)->AutoFlush = AutoFlush;
    (*pLog)->ErrBuf = NULL;
    (*pLog)->BinBuf = NULL;

    iRC = NuCreateRecursiveDir(pPath);
    NUCHKRC(iRC, EXIT);

    iRC = NuStrmNew(&((*pLog)->FStream), StreamType, FileSz, pPath, pFileName);
    NUCHKRC(iRC, EXIT);

    (*pLog)->LogBuf = (char *)calloc(NUSTRBUFSIZ, sizeof(char));
    if ((*pLog)->LogBuf == NULL)
        NUGOTO(iRC, NU_MALLOC_FAIL, EXIT);  

    memcpy((*pLog)->LogBuf + 15, NuLogLogTag, NuLogLogTagLen);
    (*pLog)->LogBufStart = (*pLog)->LogBuf + NuLogPrefixLen;

    (*pLog)->ErrBuf = (char *)calloc(NUSTRBUFSIZ, sizeof(char));
    if ((*pLog)->ErrBuf == NULL)
        NUGOTO(iRC, NU_MALLOC_FAIL, EXIT);  

    memcpy((*pLog)->ErrBuf + 15, NuLogErrorTag, NuLogErrorTagLen);
    (*pLog)->ErrBufStart = (*pLog)->ErrBuf + NuLogPrefixLen;

    (*pLog)->BinBuf = (char *)calloc(NUSTRBUFSIZ, sizeof(char));
    if ((*pLog)->BinBuf == NULL)
        NUGOTO(iRC, NU_MALLOC_FAIL, EXIT);  

    memcpy((*pLog)->BinBuf + 15, NuLogBinTag, NuLogBinTagLen);
    (*pLog)->BinBufStart = (*pLog)->BinBuf + NuLogPrefixLen;

    NuLockInit(&((*pLog)->Lock), &NuLockType_NULL);

    iRC = NU_OK;

EXIT:
    if (iRC < 0)
        NuLogClose((*pLog));

    return iRC;
}

/* Log function        */
/* ====================================================================== */
int NuLogOpen(NuLog_t **pLog, char *pPath, char *pFileName)
{
    return _NuLogOpen(pLog, pPath, pFileName, NUFALSE, (1000 * NuMBSz), enFileStream);
}

int NuLogOpen2(NuLog_t **pLog, char *pPath, char *pFileName, int AutoFlush)
{
    return _NuLogOpen(pLog, pPath, pFileName, AutoFlush, (1000 * NuMBSz), enFileStream);
}

int NuLogOpen3(NuLog_t **pLog, char *pPath, char *pFileName, int AutoFlush, size_t FileMBSz, int StreamType)
{
    return _NuLogOpen(pLog, pPath, pFileName, AutoFlush, FileMBSz * (NuMBSz), StreamType);
}

void NuLogClose(NuLog_t *pLog)
{
    if (pLog != NULL)
    {
        if (pLog->FStream != NULL)
        {
            NuStrmFree(pLog->FStream);
            pLog->FStream = NULL;
        }

        NuLockDestroy(&(pLog->Lock));

        if(pLog->LogBuf != NULL)
            free(pLog->LogBuf);

        if(pLog->ErrBuf != NULL)
            free(pLog->ErrBuf);

        if(pLog->BinBuf != NULL)
            free(pLog->BinBuf);

        free(pLog);
    }

    return;
}

void NuLogSetAutoFlush(NuLog_t *pLog)
{
    pLog->AutoFlush = NUTRUE;

    return;
}

int NuLogSetThreadSafe(NuLog_t *pLog)
{
    NuLockDestroy(&(pLog->Lock));
    NuLockInit(&(pLog->Lock), &NuLockType_Mutex);

    return NU_OK;
}

void NuLog(NuLog_t *pLog, const char *Format, ...)
{
    va_list ArguList;

    va_start(ArguList, Format);
    NuLogV(pLog, Format, ArguList); 
    va_end(ArguList);

    return;
}

void NuLogV(NuLog_t *pLog, const char *Format, va_list ArguList)
{
    char        *Buf = pLog->LogBuf;

    NuLockLock(&(pLog->Lock));

    NuGetTime(Buf);
    *(Buf + 15) = ' ';

    NuStrmWriteN(pLog->FStream, Buf, NuLogPrefixLen + vsnprintf(pLog->LogBufStart, NuLogActualLogLen, Format, ArguList));

    if(pLog->AutoFlush == NUTRUE)
    {
        NuStrmFlush(pLog->FStream);
    }

    NuLockUnLock(&(pLog->Lock));

    return;
}

void NuLogMsg(NuLog_t *pLog, const char *Msg)
{
    char        *Buf = pLog->LogBuf;

    NuLockLock(&(pLog->Lock));

    NuGetTime(Buf);
    *(Buf + 15) = ' ';
    strncpy(pLog->LogBufStart, Msg, NuLogActualLogLen);

    NuStrmWriteN(pLog->FStream, Buf, strlen(Buf));

    if(pLog->AutoFlush == NUTRUE)
        NuStrmFlush(pLog->FStream);

    NuLockUnLock(&(pLog->Lock));

    return;
}

void NuErr(NuLog_t *pLog, const char *Format, ...)
{
    va_list ArguList;

    va_start(ArguList, Format);
    NuErrV(pLog, Format, ArguList); 
    va_end(ArguList);

    return;
}

void NuErrV(NuLog_t *pLog, const char *Format, va_list ArguList)
{
    char        *Buf = pLog->ErrBuf;

    NuLockLock(&(pLog->Lock));

    NuGetTime(Buf);
    *(Buf + 15) = ' ';

    NuStrmWriteN(pLog->FStream, Buf, NuLogPrefixLen + vsnprintf(pLog->ErrBufStart, NuLogActualLogLen, Format, ArguList));

    if(pLog->AutoFlush == NUTRUE)
        NuStrmFlush(pLog->FStream);

    NuLockUnLock(&(pLog->Lock));

    return;
}

void NuErrMsg(NuLog_t *pLog, const char *Msg)
{
    char        *Buf = pLog->ErrBuf;

    NuLockLock(&(pLog->Lock));

    NuGetTime(Buf);
    *(Buf + 15) = ' ';

    strncpy(pLog->LogBufStart, Msg, NuLogActualLogLen);
    NuStrmWriteN(pLog->FStream, Buf, strlen(Buf));

    if(pLog->AutoFlush == NUTRUE)
        NuStrmFlush(pLog->FStream);

    NuLockUnLock(&(pLog->Lock));

    return;
}

void NuLogFlush(NuLog_t *pLog)
{
    NuLockLock(&(pLog->Lock));
    NuStrmFlush(pLog->FStream);
    NuLockUnLock(&(pLog->Lock));

    return;
}

int NuWriteLineN(NuLog_t *pLog, void *pData, size_t len)
{
    char        *Buf = pLog->BinBuf;
    size_t      size = NuMin((NuLogPrefixLen + len + 1), NUSTRBUFSIZ);

    NuLockLock(&(pLog->Lock));
 
    NuGetTime(Buf);
    *(Buf + 15) = ' ';

    memcpy(pLog->BinBufStart, (char *)pData, size);
    *(Buf + size - 1) = '\n';

    NuStrmWriteN(pLog->FStream, Buf, size);

    if(pLog->AutoFlush == NUTRUE)
        NuStrmFlush(pLog->FStream);
        
    NuLockUnLock(&(pLog->Lock));

    return size;
}

