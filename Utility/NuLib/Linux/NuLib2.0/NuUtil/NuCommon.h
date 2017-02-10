#ifndef _NUCOMMON_H
#define _NUCOMMON_H

#ifdef __cplusplus
extern "C" {
#endif

#include <stdio.h>
#include <stdarg.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <sys/types.h>
#include <sys/stat.h>

#include <sys/time.h>
#include <unistd.h>

#include <limits.h>
#include <math.h>
#include <errno.h>

#ifdef _MEMWATCH
#include "memwatch.h"
#endif

#define NU_INLINE inline

/* Return MESSAAGE */
#define NU_NOT_EXIST            2
#define NU_OK                   0
#define NU_FAIL                -1 
#define NU_NULL                -2
#define NU_NOTNULL             -3
#define NU_MALLOC_FAIL         -4
#define NU_NOTOPEN             -5
#define NU_NOTCLEAR            -6
#define NU_STRNULL             -7
#define NU_CASTFAIL            -8
#define NU_NOTFOUND            -9
#define NU_PARAMERROR          -10
#define NU_DEADLCK             -11
#define NU_DUPLICATE           -12
#define NU_EMPTY               -13
#define NU_MMAPERR             -14

#define NU_FILENOTEXIST        -20
#define NU_FILEUNSTABLE        -21
#define NU_FILENOTTODAY        -22
#define NU_OPENFILEFAIL        -23
#define NU_READFAIL            -24
#define NU_WRITEFAIL           -25
#define NU_LOCALTIMEFAIL       -30
#define NU_MAPKEY_DUP          -40
#define NU_MAPKEY_NOT_FOUND    -41
#define NU_MAPIT_END           -42
#define NU_DBM_COL_TOO_LONG    -100
#define NU_FULL                -101
#define NU_DBM_LOCKFAIL        -102

/* socket */
#define NU_GETSOCKET_FAIL      -200
#define NU_BIND_FAIL           -201
#define NU_GETHOSTNAME_FAIL    -202
#define NU_SELECT_FAIL         -203
#define NU_LISTEN_FAIL         -204
#define NU_ACCEPT_FAIL         -205
#define NU_CONNECT_FAIL        -206
#define NU_DISCONNECT_FAIL     -207
#define NU_SEND_FAIL           -208
#define NU_RECV_FAIL           -209

/* Queue */
#define NU_QNOTEXIST           -300
#define NU_QACCESSFAIL         -301
#define NU_QRESOURCEDEFICIENT  -302
#define NU_CREATEQFAIL         -303
#define NU_ENQINTERRUPT        -304
#define NU_ENQFAIL             -305
#define NU_DEQFAIL             -306
#define NU_RMQFAIL             -307

#define NU_TIMEOUT             -900

#define NU_DONTKNOW_FAIL        -999

#define NUTRUE                  1
#define NUFALSE                 0

/* Macro definition section */
#ifndef NUGOTO
#define NUGOTO(xrv, xrc, xlabel) \
do { \
    (xrv) = (xrc); \
    goto xlabel; \
} while(0)
#endif

#ifndef NUCHKRC
#define NUCHKRC(xrc, xlabel) \
do { \
    if ((xrc) < 0) \
        goto xlabel; \
} while(0)
#endif

#ifndef NuMax
#define NuMax(a,b) (((a) > (b)) ? (a) : (b))
#endif

#ifndef NuMin
#define NuMin(a,b) (((a) < (b)) ? (a) : (b))
#endif

#define NuBITSET(S, A) ((S) = (S) | (A))
#define NuBITUNSET(S, A) ((S) = (S) & ~(A))
#define NuBITCHK(S, A) ((S) & (A))

typedef int (*NuInitialFn)(void *);
typedef int (*NuCompareFn)(void *, void *);
typedef int (*NuDestroyFn)(void *);
typedef void *(*NuCopyFn)(void *);
typedef unsigned int (*NuHashFn)(void *, int);
typedef void (*NuEventFn)(void *);
typedef void (*NuCBFn)(void *, void *);

extern void NuEventFn_Default(void *Argu);
extern void NuCBFn_Default(void *Argu, void *Argu2);
extern int NuInitailFn_Default(void *Argu);
extern int NuDestroyFn_Default(void *Argu);

#ifdef __cplusplus
}
#endif

#endif /* _NUCOMMON_H */

