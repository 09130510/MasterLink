#include "NuThread.h"

/* Thread function */
/* ====================================================================== */
int  NuThdCreate(NuThdFn Proc_fn, void *arg , NuThread_t *ThdHdl)
{
    NuThread_t tid;
#ifdef _WIN32
    unsigned int addr = 0;
    tid  = _beginthreadex(NULL, 0, Proc_fn, (void *)arg, 0, &addr );
    if (tid == 0)
        return NU_FAIL;
#else
    if ( pthread_create( &tid, NULL, (void *(*)(void *))Proc_fn, arg ) != 0 ) 
        return NU_FAIL;
#endif
    memcpy(ThdHdl, &tid, sizeof(NuThread_t));

    return NU_OK;
}

int  NuThdCreate2(NuThdFn Proc_fn, void *arg ,unsigned int StackSize, NuThread_t *ThdHdl)
{
    NuThread_t tid;
#ifdef _WIN32
    unsigned int addr = 0;
    tid  = _beginthreadex(NULL, 0, Proc_fn, (void *)arg, 0, &addr );
    if (tid == 0)
        return NU_FAIL;
#else
	pthread_attr_t ThreadAttr;
	if ( pthread_attr_init(&ThreadAttr) != 0)
		return NU_FAIL;

	if (StackSize < 128*1024)
		StackSize = 128*1024;

	if (pthread_attr_setstacksize(&ThreadAttr, StackSize) != 0)
		return NU_FAIL;

    if ( pthread_create( &tid, &ThreadAttr, (void *(*)(void *))Proc_fn, arg ) != 0 ) 
        return NU_FAIL;
#endif
    memcpy(ThdHdl, &tid, sizeof(NuThread_t));

    return NU_OK;
}

int  NuThdJoin(NuThread_t ThdHdl)
{
#ifdef _WIN32
  WaitForSingleObject((void*)ThdHdl, INFINITE );
  CloseHandle((HANDLE)ThdHdl);
#else
  pthread_join((pthread_t)ThdHdl, 0 );
#endif
  return NU_OK;
}

int  NuThdDetach(NuThread_t ThdHdl)
{
#ifdef _WIN32
#else
  	pthread_detach((pthread_t)ThdHdl);
#endif
  return NU_OK;
}

int NuThdKill(NuThread_t ThdHdl, int Sig)
{
#ifdef _WIN32
	return NU_OK;
#else
	return pthread_kill((pthread_t)ThdHdl, Sig);
#endif
}

int NuThdReturn()
{
#ifdef _WIN32
    _endthreadex(0); 
#else
    pthread_exit(0);
#endif
    return NU_OK;
}

NuThread_t NuThdSelf()
{
	NuThread_t tid;
#ifdef _WIN32
    tid = (NuThread_t)GetCurrentThreadId();
#else
    tid = (NuThread_t)pthread_self();
#endif

	return tid;
}

int NuThdYield()
{
#ifdef _WIN32
	Sleep(0);
	return 0;
#else
	return sched_yield();
#endif
}

int NuThdBindCore(pthread_t tid, int cpu_id)
{
#ifdef _GNU_SOURCE
	cpu_set_t cpuset;

	if (cpu_id > CPU_SETSIZE)
		return NU_FAIL;

	CPU_ZERO(&cpuset);
	CPU_SET(cpu_id, &cpuset);

	if (pthread_setaffinity_np(tid, sizeof(cpu_set_t), &cpuset) != 0)
		return NU_FAIL;

#endif
	return NU_OK;	
}

int NuProcBindCore(int cpu_id)
{
#ifdef _GNU_SOURCE
	cpu_set_t cpuset;

	if (cpu_id > CPU_SETSIZE)
		return NU_FAIL;

	CPU_ZERO(&cpuset);
	CPU_SET(cpu_id, &cpuset);
	/* set cpu handle this process 
	 * If pid is zero, then the calling process is used
	 * */
	if (sched_setaffinity(0, sizeof(cpu_set_t), &cpuset) != 0)
		return NU_FAIL;

#endif
	return NU_OK;	
}

