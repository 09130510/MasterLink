#ifndef _NUTHREAD_H
#define _NUTHREAD_H

#ifdef __cplusplus
extern "C" {
#endif

#ifdef _WIN32
#include <process.h>
#else
#define _GNU_SOURCE
#include <pthread.h>
#include <signal.h>
#include <sched.h>
#endif
#include "NuCommon.h"
#ifdef _WIN32
typedef unsigned int ( __stdcall *NuThdFn )( void * );
#define NUTHD_FUNC unsigned int __stdcall
typedef uintptr_t NuThread_t;

#else
/*
typedef void *(*NuThdFn) (void *);
#define NUTHD_FUNC  NuThdFn
*/
typedef void *VOID_PTR;
typedef VOID_PTR (* NuThdFn)(void *); 
#define NUTHD_FUNC VOID_PTR


typedef pthread_t NuThread_t;

#endif
/* -----------------------------------------------------------------*/
int	NuThdCreate(NuThdFn Proc_fn, void *arg , NuThread_t *ThdHdl);
int	NuThdCreate2(NuThdFn Proc_fn, void *arg ,unsigned int StackSize, NuThread_t *ThdHdl);
int	NuThdJoin(NuThread_t ThdHdl);
int	NuThdDetach(NuThread_t ThdHdl);
int	NuThdKill(NuThread_t ThdHdl, int Sig);
int NuThdReturn();
NuThread_t NuThdSelf();
int NuThdYield();

/* cpu_id = 0~n, core number */
int NuThdBindCore(pthread_t tid, int cpu_id);

int NuProcBindCore(int cpu_id);

#ifdef __cplusplus
}
#endif

#endif /* _NUTHREAD_H */

