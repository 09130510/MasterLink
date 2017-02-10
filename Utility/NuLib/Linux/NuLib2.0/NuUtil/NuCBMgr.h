
#include "NuCommon.h"

#ifndef _NUCBMGR_H
#define _NUCBMGR_H

#ifdef __cplusplus
extern "C" {
#endif

typedef struct _NuCBMgrTrigger_t NuCBMgrTrigger_t;
typedef struct _NuEventHdlr_t NuEventHdlr_t;

NuCBMgrTrigger_t *NuCBMgrAddTrigger(void);
void NuCBMgrDelTrigger(NuCBMgrTrigger_t *pTrigger);
void NuCBMgrResetTrigger(NuCBMgrTrigger_t *pTrigger);

NuEventHdlr_t *NuCBMgrRegisterEvent(NuCBMgrTrigger_t *pTrigger, NuCBFn CBFn, void *Argu);
void NuCBMgrUnRegisterEvent(NuEventHdlr_t *pHdlr);
void NuCBMgrRaiseEvent(NuCBMgrTrigger_t *pTrigger, void *Argu);

#ifdef __cplusplus
}
#endif

#endif /* _NUCBMGR_H */

