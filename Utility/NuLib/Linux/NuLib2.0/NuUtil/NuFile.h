#ifndef _NUFILE_H
#define _NUFILE_H

#ifdef __cplusplus
extern "C" {
#endif

#include <stdio.h>
#include <stdarg.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <limits.h>
#include <math.h>
#include <sys/stat.h>
#include <sys/types.h>
#include <dirent.h>

#ifdef linux
#include <ctype.h>
#endif

#ifdef _WIN32
#include <direct.h>
#endif

#include "NuCommon.h"
#include "NuStr.h"

/* macro */
/* -------------------------------------------------------------------- */
#ifdef _WIN32

#ifndef NUFILE_SEPARATOR
#define NUFILE_SEPARATOR        '\\'
#endif
#ifndef NUFILE_SEPARATOR_STR
#define NUFILE_SEPARATOR_STR    "\\"
#endif

#ifndef NULINE_END
#define NULINE_END              0x0d
#endif

#ifndef NULINE_END_STR
#define NULINE_END_STR          "\r"
#endif

#define mkdir    		        _mkdir
#define rmdir    		        _rmdir
#define chdir    		        _chdir
#define stat                    _stat

#else

#ifndef NUFILE_SEPARATOR
#define NUFILE_SEPARATOR        '/'
#endif
#ifndef NUFILE_SEPARATOR_STR
#define NUFILE_SEPARATOR_STR    "/"
#endif

#ifndef NULINE_END
#define NULINE_END              '\n' 
#endif

#ifndef NULINE_END_STR
#define NULINE_END_STR          "\n"
#endif

#endif
/* -------------------------------------------------------------------- */
int NuIsDir(char *pPath);
int NuIsFile(char *pFile);
void NuPathCombine(NuStr_t *pStr, char *pDirPath, char *pName);

int NuCreateDir(char *pPath);
int NuCreateRecursiveDir(char *pPath);

size_t NuFileGetSize(int fd_no);
int    NuFileSetSize(int fd_no, unsigned int len);

/* list folder file */
/* -------------------------------------------------------------------- */
typedef struct _NuDirList_t
{
	struct dirent **FList;
	int           Cnt;
	int           CurIdx;
} NuDirList_t;

int   NuDirListInit(NuDirList_t *pDList, char *pPath);
void  NuDirListClear(NuDirList_t *pDList);
int   NuDirListNext(NuDirList_t *pDList);
#define NuDirListGetName(DLIST)        ( ((DLIST)->Cnt < 0) ? NULL : (DLIST)->FList[(DLIST)->CurIdx]->d_name )
#define NuDirListCnt(DLIST)           (DLIST)->Cnt
/* -------------------------------------------------------------------- */

#ifdef __cplusplus
}
#endif

#endif /* _NUFILE_H */
