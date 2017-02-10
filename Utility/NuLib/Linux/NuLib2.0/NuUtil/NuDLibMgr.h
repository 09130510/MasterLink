
#include "NuCommon.h"

#ifndef _NUDLIBMGR_H
#define _NUDLIBMGR_H

#ifdef __cplusplus
extern "C" {
#endif

int NuDLibMgrLoad(char *AliasName, char *DLibPath, int Flag, char **Err);
int NuDLibMgrLoadToGlobal(char *AliasName, char *DLibPath, char **Err);
int NuDLibMgrLoadToLocal(char *AliasName, char *DLibPath, char **Err);

void NuDLibMgrUnLoad(char *AliasName, char **Err);

void *NuDLibMgrGetFn(char *AliasName, char *FnName, char **Err);

void NuDLibMgrRegisterUnLoadEvent(char *AliasName, NuCBFn UnLoadCB, void *CBArgu);

#ifdef __cplusplus
}
#endif

#endif /* _NUDLIBMGR_H */

