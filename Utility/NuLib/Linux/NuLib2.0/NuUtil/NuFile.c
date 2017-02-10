#include "NuFile.h"

/*File & Dir function */
/* ====================================================================== */
int NuIsDir(char *pPath)
{
    int RC = 0;
    struct stat st;

    if((RC = stat(pPath, &st)) < 0)
    {
        return -2;
    }

    if(S_ISDIR(st.st_mode))
    {
        return 0;
    }
    else
    {
        return -1;
    }
}

int NuIsFile(char *pFile)
{
    int         RC = 0;
    struct stat st;

    if((RC = stat(pFile, &st)) < 0)
    {
        return -2;
    }

    if(S_ISREG(st.st_mode))
    {
        return 0;
    }
    else
    {
        return -1;
    }
}

void NuPathCombine(NuStr_t *pStr, char *pDirPath, char *pName)
{
    NuStrClear(pStr);
    NuStrCpy(pStr, pDirPath);
    NuStrRTrimChr(pStr, NUFILE_SEPARATOR);

    NuStrCatChr(pStr, NUFILE_SEPARATOR);
    NuStrCat(pStr, pName);

    return;
}

int NuCreateDir(char *pPath)
{
    mode_t mode = S_IRWXU | S_IRWXG | S_IROTH | S_IXOTH;

    return mkdir(pPath, mode);
}

int NuCreateRecursiveDir(char *pPath)
{
    int     RC = 0;
    int     Pos = 0;
    NuStr_t *pPathTmp1 = NULL;
    NuStr_t *pPathTmp2 = NULL;

    NuStrNew(&pPathTmp1, NULL);
    NuStrNew(&pPathTmp2, NULL);

    NuStrCat(pPathTmp1, pPath);
    NuStrRTrimChr(pPathTmp1, NUFILE_SEPARATOR);

    if(!NuIsDir(NuStrGet(pPathTmp1)))
    {
        NUGOTO(RC, NU_OK, EXIT);
    }

    for(Pos = NuStrSize(pPathTmp1); Pos >= 0; -- Pos)
    {
        if(NuStrGetChr(pPathTmp1, Pos) == NUFILE_SEPARATOR)
        {
            NuStrSubStr(pPathTmp1, 0, Pos, pPathTmp2);
            break;
        }
    }

    if(Pos >= 0)
    {
        RC = NuCreateRecursiveDir(NuStrGet(pPathTmp2));
    }

    RC = NuCreateDir(pPath);

EXIT:
    if(pPathTmp1 != NULL)
    {
        NuStrFree(pPathTmp1);
    }

    if(pPathTmp2 != NULL)
    {
        NuStrFree(pPathTmp2);
    }

    return RC;
}

size_t NuFileGetSize(int fd_no)
{
    struct stat     stFileStat;

    if(fstat(fd_no, &stFileStat) < 0)
    {
        return -1;
    }

    return stFileStat.st_size;
}

int NuFileSetSize(int fd_no, unsigned int len)
{
    return ftruncate(fd_no, len);
}

/* list folder file */
/* -------------------------------------------------------------------- */
int NuDirListInit(NuDirList_t *pDList, char *pPath)
{
    if(NuIsDir(pPath) < 0)
    {
        return NU_FAIL;
    }

    if((pDList->Cnt = scandir(pPath, &(pDList->FList), 0, alphasort)) < 0)
    {
        pDList->Cnt = 0;
        return NU_FAIL;
    }

    return NU_OK;
}

void NuDirListClear(NuDirList_t *pDList)
{
    if(pDList->Cnt > 0)
    {
        while(pDList->Cnt --)
        {
            free(pDList->FList[pDList->Cnt]);
        }

        free(pDList->FList);
    }

    return;
}

int NuDirListNext(NuDirList_t *pDList)
{
    if(pDList->Cnt > 0 && pDList->Cnt > (pDList->CurIdx + 1))
    {
        ++ (pDList->CurIdx);
    }
    else
    {
        return NU_FAIL;
    }

    return NU_OK;
}

/* -------------------------------------------------------------------- */
