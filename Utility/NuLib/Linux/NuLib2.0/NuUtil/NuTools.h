/**************************************************
 **************************************************/

#ifndef _NUTOOLS_H
#define _NUTOOLS_H

#ifdef __cplusplus
extern "C" {
#endif

#include "NuCommon.h"
#include "NuStr.h"
#include "NuUtil.h"
#include "NuFile.h"

typedef struct _NuStrVec_t
{
    base_vector_t *pvec;
    NuStr_t       *pstr;
} NuStrVec_t;

int NuSplitOpen(NuStrVec_t *pStrVec);
void NuSplitClose(NuStrVec_t *pStrVec);

int NuSplit(char *str, char cSep, NuStrVec_t *pStrVec);
int NuSplitByStr(char *str, char *sep, NuStrVec_t *pStrVec);

/* this function will damage the input string, change cSep to 0x00 */
int NuSplitNoCpy(char *str, char cSep, NuStrVec_t *pStrVec);
int NuSplitByStrNoCpy(char *str, char *sep, NuStrVec_t *pStrVec);
char *NuSplitGetByIndex(NuStrVec_t *pStrVec, int index);

int WriteToPidFile(char *pPath, char *procName, char *Instance);

#ifdef __cplusplus
}
#endif

#endif /* _NUTOOLS_H */

