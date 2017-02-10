#ifndef _NUSEQNO_H
#define _NUSEQNO_H

#ifdef __cplusplus
extern "C" {
#endif

#include "NuUtil.h"
#include "NuLock.h"
#include "NuFile.h"
#include "NuTime.h"
#include "NuCommon.h"
#include "NuStr.h"
#include "NuCStr.h"
#include "NuMMap.h"

/* struct define                        */
/* ------------------------------------ */
typedef struct _NuSeqNo_t NuSeqNo_t;

typedef int (*NuSeqNoFn)(NuSeqNo_t *pObj, void *pArgu);
typedef int (*NuSeqNoNextFn)(void *pCurSeqNo, int SeqNoLen, void *NextSeqNo, void *Argu);
/*
    SeqNo1 > SeqNo2     > 0
    SeqNo1 = SeqNo2     = 0
    SeqNo1 < SeqNo2     < 0
 */
typedef int (*NuSeqNoCmpFn)(void *pSeqNo1, int SeqNoLen, void *pSeqNo2);

typedef struct _NuSeqNoType_t
{
	NuSeqNoFn     SeqNoInit;
	NuSeqNoFn     SetMaxNo;
	NuSeqNoFn     SetMinNo;
	NuSeqNoFn     SetCurNo;
	NuSeqNoFn     GetCurNo;
	NuSeqNoNextFn Next;
	NuSeqNoCmpFn  Compare;
	NuSeqNoFn     SeqNoPop;
	NuSeqNoFn     SeqNoPush;
} NuSeqNoType_t;

struct _NuSeqNo_t
{
	char          szCreateDate[10+1];
	char          szCreateTime[8+1];

	char		  *pSeqFilePos1;
	char		  *pSeqFilePos2;
	NuMMap_t	  *pSeqFile;
	NuLock_t      Lock;
	NuStr_t       *pName;
	NuStr_t       *pPath;
	void		  *pCurSeqNo;
	void		  *pMaxSeqNo;
	void		  *pMinSeqNo;
	int           iLength;
	NuSeqNoType_t Type;
	void          *arg;
};

/* ------------------------------------ */
/* global variable                      */
/* ------------------------------------ */
extern NuSeqNoType_t NuSeqNoType_Int;
extern NuSeqNoType_t NuSeqNoType_String;
/* ------------------------------------ */
/* global function                      */
/* ------------------------------------ */
int  NuSeqNoNew(NuSeqNo_t **pObj, NuSeqNoType_t *Type, int SeqNoLen, char *szFilePath, char *szSeqNoName);
int  NuSeqNoNew2(NuSeqNo_t **pObj, NuSeqNoType_t *Type, int SeqNoLen, char *szFilePath, char *szSeqNoName, NuSeqNoNextFn Next_fn, NuSeqNoCmpFn Compare_fn, void *arg);
void NuSeqNoFree(NuSeqNo_t *pSeqNo);

void NuSeqNoSetThreadSafe(NuSeqNo_t *pSeqNo);

int  NuSetMaxNo(NuSeqNo_t *pObj, void *pSeqNo);
int  NuSetMinNo(NuSeqNo_t *pObj, void *pSeqNo);
int  NuSetCurNo(NuSeqNo_t *pObj, void *pSeqNo);

int  NuGetCurNo(NuSeqNo_t *pObj, void *pSeqNo);

//void NuSetNextFn(NuSeqNo_t *pObj, NuSeqNoNextFn Next_fn);
//void NuSetCompareFn(NuSeqNo_t *pObj, NuSeqNoCmpFn Cmpare_fn);

int  NuSeqNoPop(NuSeqNo_t *pObj, void *pSeqNo);
int  NuSeqNoPush(NuSeqNo_t *pObj, void *pSeqNo);

#define NuSeqNoSZ(NUSEQ_NO)     (NUSEQ_NO)->iLength
void NuSeqNoFlush(NuSeqNo_t *pObj);

#ifdef __cplusplus
}
#endif

#endif /* _NUSEQNO_H */

