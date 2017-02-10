#ifndef _NUSTREAM_H
#define _NUSTREAM_H

#ifdef __cplusplus
extern "C" {
#endif

#include <stdio.h>
#include <stdarg.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>

#include "NuUtil.h"
#include "NuStr.h"
#include "NuCStr.h"
#include "NuTime.h"
#include "NuFile.h"
//#include "NuStreamAdapter.h"

/* -----------------------------------
 * enumerator
 * ----------------------------------- */
typedef enum _StreamType
{
	enMMapStream = 0,
	enFileStream     
} StremType;
/* -----------------------------------
 * define 
 * ----------------------------------- */
typedef int    (*StrmCB_Fn)(void *);
typedef int    (*StrmAllocCB_Fn)(void *, char *, size_t);
typedef size_t (*StrmWrtCB_Fn)(void *, void *, size_t);
typedef size_t (*StrmReadCB_Fn)(void *, size_t, void **);

/* internal */
typedef int    (*_WrtNCB_Fn)(void *, void *, size_t);

typedef struct _NuStrmFnType_t
{
	StrmCB_Fn      Flush;
	StrmCB_Fn      SeekToEnd;
	StrmCB_Fn      Free;
	StrmWrtCB_Fn   WriteN;
	StrmAllocCB_Fn Alloc;
} NuStrmFnType_t;
/* ----------------------------------- */

/** 
 * A structure to rotate
 */ 
typedef struct _NuStrm_t
{
	base_vector_t       *vStrm;              /* stream list */
	void                *Stream;             /* Current Stream */
	int                 StreamType;
	size_t              AllocSz;
	size_t              LeftSz;
	int                 MaxVer; 

	NuStr_t             *StrmName;
	NuStr_t             *Dir;
	NuStr_t             *File;

	NuDirList_t         DirList;

	StrmWrtCB_Fn        CB_WriteN;          /* callback : Write n byte data to stream */

	StrmCB_Fn           CB_Flush;           /* callback : Strm flow, please implement stream flush  */
	StrmCB_Fn           CB_SeekToEnd;       /* callback : Strm flow, please implement stream flush  */

	StrmAllocCB_Fn      CB_Alloc;
	StrmCB_Fn           CB_Free;

	_WrtNCB_Fn           CB_IntWriteN;
} NuStrm_t;

#define NuStrmGetMaxSz(STRM)        (STRM)->MaxSz
#define NuStrmGetDataSz(STRM)       (STRM)->DataSz

/** 
 * rotate object new function
 * @param[out] pStrm       : output struct pointer
 * @param[in ] MaxSz     : MaxSz
 * @param[in ] Stream    : stream
 * @param[in ] WriteN_Fn : callback function for write
 * @param[in ] Rivise_Fn : callback function for rivise
 * @param[in ] Flush_Fn  : callback function for rivise
 */ 
int NuStrmNew(NuStrm_t **pStrm, int StreamType,
                size_t MaxSz, char *pDir, char *pName);

/**
 * rotate object free function
 * @param[in ] pStrm       : struct pointer
 */
void NuStrmFree(NuStrm_t *pStrm);

/**
 * rotate object write function
 * @param[in ] pStrm       : struct pointer
 * @param[in ] data      : write data
 * @param[in ] len       : write data length
 */
int NuStrmWriteN(NuStrm_t *pStrm, void *data, size_t len);

/**
 * rotate object flush function
 * @param[in ] pStrm       : struct pointer
 */
#define NuStrmFlush(STRM)          (STRM)->CB_Flush((STRM))

#ifdef __cplusplus
}
#endif

#endif /* _NUSTREAM_H */

