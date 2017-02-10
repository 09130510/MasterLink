#include <limits.h>
#include "NuSeqNo.h"

/* ------------------------------------ */
/* static function                      */
/* ------------------------------------ */
static void _NuSeqNo_Int_Set(NuSeqNo_t *pObj, int *pSeqNo)
{
    *((int *)(pObj->pCurSeqNo)) = *pSeqNo;
    return;
}

static int NuSeqNo_Int_Init(NuSeqNo_t *pObj, void *pArgu)
{
	*((int *)(pObj->pMaxSeqNo)) = INT_MAX;
	*((int *)(pObj->pMinSeqNo)) = 0;
	return NU_OK;
}

static int NuSeqNo_Int_SetMax(NuSeqNo_t *pObj, void *pSeqNo)
{
	*((int *)(pObj->pMaxSeqNo)) = *((int *)pSeqNo);
	return NU_OK;
}

static int NuSeqNo_Int_SetMin(NuSeqNo_t *pObj, void *pSeqNo)
{
	*((int *)(pObj->pMinSeqNo)) = *((int *)pSeqNo);
	return NU_OK;
}

static int NuSeqNo_Int_SetCurent(NuSeqNo_t *pObj, void *pSeqNo)
{
	*((int *)(pObj->pCurSeqNo)) = *((int *)pSeqNo);
	return NU_OK;
}

static int NuSeqNo_Int_GetCurent(NuSeqNo_t *pObj, void *pSeqNo)
{
	*((int *)pSeqNo) = *((int *)(pObj->pCurSeqNo));
	return NU_OK;
}

static int NuSeqNo_Int_Pop(NuSeqNo_t *pObj, void *pSeqNo)
{
	int iRC = 0;

    if((iRC = pObj->Type.Next(pObj->pCurSeqNo, pObj->iLength, pSeqNo, pObj->arg)) < 0)
        goto EXIT;

    if ( pObj->Type.Compare(pObj->pMaxSeqNo, pObj->iLength, pSeqNo) < 0 ||
         pObj->Type.Compare(pObj->pMinSeqNo, pObj->iLength, pSeqNo) > 0 )
        NUGOTO(iRC, NU_FAIL, EXIT);

    _NuSeqNo_Int_Set(pObj, pSeqNo);

    iRC = NU_OK;
EXIT:
    return iRC;
}

static int NuSeqNo_Int_Push(NuSeqNo_t *pObj, void *pSeqNo)
{
	int iRC = 0;

    if (pObj->Type.Compare(pObj->pMaxSeqNo, pObj->iLength, pSeqNo) <  0 ||
        pObj->Type.Compare(pObj->pMinSeqNo, pObj->iLength, pSeqNo) >  0 ||
        pObj->Type.Compare(pObj->pCurSeqNo, pObj->iLength, pSeqNo) == 0 )
        NUGOTO(iRC, NU_FAIL, EXIT);

    _NuSeqNo_Int_Set(pObj, pSeqNo);

    iRC = NU_OK;
EXIT:
    return iRC;
}

static int NuSeqNo_Int_Next(void *CurSeqNo, int SeqNoLen, void *NewSeqNo, void *arg)
{
	*((int *)NewSeqNo) = *((int *)CurSeqNo) + 1;
    return NU_OK;
}

static int NuSeqNo_Int_Compare(void *SeqNo1, int SeqNoLen, void *SeqNo2)
{
	if (*((int *)SeqNo1) > *((int *)SeqNo2))
		return 1;
	else if (*((int *)SeqNo1) < *((int *)SeqNo2))
		return -1;
	else
		return 0;
}

/* ------------------------------------ */
/* global variable                      */
/* ------------------------------------ */
NuSeqNoType_t NuSeqNoType_Int = {
                                   .SeqNoInit = &NuSeqNo_Int_Init,
                                   .SetMaxNo  = &NuSeqNo_Int_SetMax,
                                   .SetMinNo  = &NuSeqNo_Int_SetMin, 
							       .SetCurNo  = &NuSeqNo_Int_SetCurent,
							       .GetCurNo  = &NuSeqNo_Int_GetCurent,
							       .SeqNoPop  = &NuSeqNo_Int_Pop,
							       .SeqNoPush = &NuSeqNo_Int_Push,
								   .Next      = &NuSeqNo_Int_Next,
								   .Compare   = &NuSeqNo_Int_Compare
                                };


