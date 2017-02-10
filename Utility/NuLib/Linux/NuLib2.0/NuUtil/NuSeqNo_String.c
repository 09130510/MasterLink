#include "NuSeqNo.h"

/* ------------------------------------ */
/* static function                      */
/* ------------------------------------ */
static void _NuSeqNo_Str_Set(NuSeqNo_t *pObj, char *pSeqNo)
{
    char    *p1 = pObj->pSeqFilePos1;
    char    *p2 = pObj->pSeqFilePos2;

    if(pObj->pCurSeqNo == p1)
    {
        *p2 = '\0';
        memcpy(p2, pSeqNo, pObj->iLength);
        pObj->pCurSeqNo = p2;
        *p1 = '\0';
    }
    else
    {
        *p1 = '\0';
        memcpy(p1, pSeqNo, pObj->iLength);
        pObj->pCurSeqNo = p1;
        *p2 = '\0';
    }

    return;
}

static int NuSeqNo_Str_Init(NuSeqNo_t *pObj, void *pArgu)
{
    memset(pObj->pMaxSeqNo, '9', pObj->iLength);
    memset(pObj->pMinSeqNo, '0', pObj->iLength);
	return NU_OK;
}

static int NuSeqNo_Str_SetMax(NuSeqNo_t *pObj, void *pSeqNo)
{
    strncpy((char *)(pObj->pMaxSeqNo), (char *)pSeqNo, pObj->iLength);
	return NU_OK;
}

static int NuSeqNo_Str_SetMin(NuSeqNo_t *pObj, void *pSeqNo)
{
    strncpy((char *)(pObj->pMinSeqNo), (char *)pSeqNo, pObj->iLength);
	return NU_OK;
}

static int NuSeqNo_Str_SetCurent(NuSeqNo_t *pObj, void *pSeqNo)
{
    strncpy((char *)(pObj->pCurSeqNo), (char *)pSeqNo, pObj->iLength);
	return NU_OK;
}

static int NuSeqNo_Str_GetCurent(NuSeqNo_t *pObj, void *pSeqNo)
{
    strcpy((char *)pSeqNo, (char *)(pObj->pCurSeqNo));

	return NU_OK;
}

static int NuSeqNo_Str_Pop(NuSeqNo_t *pObj, void *pSeqNo)
{
	int iRC = 0;
    if((iRC = pObj->Type.Next(pObj->pCurSeqNo, pObj->iLength, pSeqNo, pObj->arg)) < 0)
        goto EXIT;

    if ( pObj->Type.Compare(pObj->pMaxSeqNo, pObj->iLength, pSeqNo) < 0 ||
         pObj->Type.Compare(pObj->pMinSeqNo, pObj->iLength, pSeqNo) > 0 )
        NUGOTO(iRC, NU_FAIL, EXIT);

    _NuSeqNo_Str_Set(pObj, pSeqNo);

    iRC = NU_OK;
EXIT:
    return iRC;
}

static int NuSeqNo_Str_Push(NuSeqNo_t *pObj, void *pSeqNo)
{
	int iRC = 0;

    if (pObj->Type.Compare(pObj->pMaxSeqNo, pObj->iLength, pSeqNo) <  0 ||
        pObj->Type.Compare(pObj->pMinSeqNo, pObj->iLength, pSeqNo) >  0 ||
        pObj->Type.Compare(pObj->pCurSeqNo, pObj->iLength, pSeqNo) == 0 )
        NUGOTO(iRC, NU_FAIL, EXIT);

    _NuSeqNo_Str_Set(pObj, pSeqNo);

    iRC = NU_OK;
EXIT:
    return iRC;
}

static int NuSeqNo_Str_Next(void *CurSeqNo, int SeqNoLen, void *NewSeqNo, void *arg)
{
    NuCStrPrintLong((char *)NewSeqNo, atol((char *)CurSeqNo) + 1, SeqNoLen);   
    return NU_OK;
}

static int NuSeqNo_Str_Compare(void *SeqNo1, int SeqNoLen, void *SeqNo2)
{
    return strcmp((char *)SeqNo1, (char *)SeqNo2);
}

/* ------------------------------------ */
/* global variable                      */
/* ------------------------------------ */
NuSeqNoType_t NuSeqNoType_String = {
                                      .SeqNoInit = &NuSeqNo_Str_Init,
                                      .SetMaxNo  = &NuSeqNo_Str_SetMax,
							          .SetMinNo  = &NuSeqNo_Str_SetMin, 
							          .SetCurNo  = &NuSeqNo_Str_SetCurent,
							          .GetCurNo  = &NuSeqNo_Str_GetCurent,
							          .SeqNoPop  = &NuSeqNo_Str_Pop,
							          .SeqNoPush = &NuSeqNo_Str_Push,
									  .Next      = &NuSeqNo_Str_Next,
									  .Compare   = &NuSeqNo_Str_Compare
                                   };
