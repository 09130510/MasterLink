
#include "NuCommon.h"
#include "NuUtil.h"
#include "NuStr.h"
#include "NuFile.h"
#include "NuFileStream.h"
#include "NuHash.h"

#ifndef _NUINI_H
#define _NUINI_H

#ifdef __cplusplus
extern "C" {
#endif

typedef struct _NuIni_t NuIni_t;
typedef void (*NuIniCBFn)(NuIni_t *pIni, char *Section, char *Key, char *Value, void *Argu);

int  NuIniNew(NuIni_t **pIni, char *pFilePath);
void NuIniFree(NuIni_t *pIni);
char *NuIniFind(NuIni_t *pIni, char *Section, char *Key);
int  NuIniSectionExist(NuIni_t *pIni, char *Section);
void NuIniModify(NuIni_t *pIni, char *Section, char *Key, char *Value);
void NuIniSave(NuIni_t *pIni, char *FilePath);
void NuIniTraverse(NuIni_t *pIni, char *Section, NuIniCBFn Fn, void *Argu);
char *NuIniGetFileName(NuIni_t *pIni);

#ifdef __cplusplus
}
#endif

#endif /* _NUINI_H */

