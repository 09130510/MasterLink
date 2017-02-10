#include <stdio.h>
#include <stdarg.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <limits.h>
#include <math.h>
#include <ctype.h>

#include "NuCommon.h"

#ifndef _NUSTR_H
#define _NUSTR_H

#ifdef __cplusplus
extern "C" {
#endif

#define	NUSTRBUFSIZ			4096
#define NUSTR_DEFAULT_SIZE	16

/* extensible string */
typedef struct _NuStr_t
{
	char *ptr;		/* string */
	int  size;		/* string size */
	int  asize;		/* max allocate size */
} NuStr_t;

int NuStrNew(NuStr_t **pstr, char *pval);
int NuStrNew2(NuStr_t **pstr, size_t string_size, char *pval);

NuStr_t *NuStrDup(NuStr_t *pstr);
int NuStrFree(NuStr_t *pstr);
int NuStrClear(NuStr_t *pstr);

int NuStrCat(NuStr_t *pStr, const char *pSrc);
int NuStrNCat(NuStr_t *pstr, const void *ptr, size_t size);
int NuStrCatChr(NuStr_t *pstr, const char chr);

int NuStrNCpy(NuStr_t *pstr, const void *ptr, size_t size);
int NuStrCpy(NuStr_t *pStr, const char *pSrc);

int NuStrSubStr(NuStr_t *pstr, int start, int cnt, NuStr_t *pout);
int NuStrSubStr2(NuStr_t *pstr, int start, int cnt, char *str);

int NuStrPrintf(NuStr_t *pStr, const char *Format, ...);
int NuStrVPrintf(NuStr_t *pStr, const char *Format, va_list ArguList);

int NuStrAppendPrintf(NuStr_t *pstr, const char *format, ...);
int NuStrAppendPrintf2(NuStr_t *pstr, const char *format, ...);

/* NuStr tools */
int NuStrCmp(NuStr_t *pstr1, NuStr_t *pstr2);
void NuStrRTrimChr(NuStr_t *pstr, char cChr);
void NuStrRTrim(NuStr_t *pstr);
void NuStrLTrim(NuStr_t *pstr);
void NuStrReplaceRangeChr(NuStr_t *pstr, char cChr, char cNewChr, int start, int end);
void NuStrReplaceChr(NuStr_t *pstr, char cChr, char cNewChr);
NU_INLINE char *NuStrGet(NuStr_t *pstr);
NU_INLINE char NuStrGetChr(NuStr_t *pstr, int Idx);
NU_INLINE int NuRemoveByte(NuStr_t *pStr, int len);
NU_INLINE int NuStrSize(NuStr_t *pStr);

#define	NuStrGetInt(NUSTR)		atoi((NUSTR)->ptr)
#define	NuStrGetDouble(NUSTR)	atof((NUSTR)->ptr)
/** @} */

#ifdef __cplusplus
}
#endif

#endif /* _NUSTR_H */

