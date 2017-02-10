#ifndef _NUFMNT_H
#define _NUFMNT_H

#ifdef __cplusplus
extern "C" {
#endif

#include <stdio.h>
#include <stdarg.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <limits.h>
#include <errno.h>
#include <sys/select.h>
#include <sys/inotify.h>
#include "NuStr.h"
#include "NuFile.h"

/* ------------------------------------------------ */
/*  code define                                     */
/* ------------------------------------------------ */
#define NuFMntAttribChange   3
#define NuFMntDelete         2
#define NuFMntModify         1
#define NuFMntTimeout        0
#define NuFMntClose         -1
#define NuFMntFileNotExists -2
#define NuFMntError         -99

#define NuFMntDefaultBufSz   64
/* ------------------------------------------------ */
/*  type define                                     */
/* ------------------------------------------------ */
typedef struct _NuFMnt_t
{
	NuStr_t              *File;
	int                  ino_fd;
	int                  watch_fd;
	struct timeval       cycle;
	int                  sec;
	int                  usec;
	struct inotify_event *ev;

	fd_set               fds;
	char                 *buf;
	size_t               bufsz;
	int                  len;
	int                  readn;
} NuFMnt_t;

/* ------------------------------------------------ */
/*  public function                                 */
/* ------------------------------------------------ */
int NuFMntNew(NuFMnt_t **pFMnt, char *pFile, size_t bufsz);
int NuFMntNew2(NuFMnt_t **pFMnt, char *pFile);
void NuFMntFree(NuFMnt_t *pFMnt);
void NuFMntSetCycle(NuFMnt_t *pFMnt, int sec, int ms);

int NuFMntActive(NuFMnt_t *pFMnt);
int NuFMntSelect(NuFMnt_t *pFMnt);

#ifdef __cplusplus
}
#endif

#endif /* _NUFMNT_H */
