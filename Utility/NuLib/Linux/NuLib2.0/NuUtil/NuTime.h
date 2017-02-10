#ifndef _NUTIME_H
#define _NUTIME_H

#ifdef __cplusplus
extern "C" {
#endif

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <sys/types.h>

#ifdef _WIN32
#include <shlwapi.h>
#include <sys/timeb.h>
#include <Windows.h>
#else
#include <sys/time.h>	/* for gettimeofday */
#include <errno.h>
#endif

#include "NuCommon.h"
#include "NuCStr.h"

#ifdef _WIN32
struct timezone
{
    long tz_minuteswest;
    long tz_dsttime;
};

int gettimeofday(struct timeval *tvp, struct timezone *tzp);
#endif

/* base function */
struct tm *NuGmTime(const time_t *t);
struct tm *NuLocalTime(const time_t *t);

/* Date */
void NuGetToday(char *pToday);	/* YYYYMMDD */
void NuGetToday2(char *pToday);	/* YYYY-MM-DD */

/* Time */
void NuGetTime(char *pToday);
void NuGetTime2(char *pToday);
void NuGetTime_HHMMSS(char *pTime);

/* tools */
time_t NuHMSToTime(char *pHms);

/* DateTime */
#define NuDateTimeHR	3600    /* 1 hr  = 60 * 60 = 3600 sec      */
#define NuDateTimeDay	86400   /* 1 day = 60 * 60 *24 = 86400 sec */

#define	SecInHour(hr)	(hr)*3600

typedef struct _NuDateTime_t
{
    struct tm *ptm;
	int        ms;
	int        us;
	int        MoveHour;
	int        IsLocal;		/* NUTRUE : Local, NUFALSE : UTC */
} NuDateTime_t;

int NuDateTimeNew(NuDateTime_t **pDateTime, int IsLocal);
void NuDateTimeFree(NuDateTime_t *pDateTime);

void NuDateTimeInit(NuDateTime_t *pDateTime, int MoveHour, int IsLocal);

int NuDateTimeSet(NuDateTime_t *pDateTime);
#define NuDateTimeSet2(pDateTime, year, month, day, hour, min, sec)	NuDateTimeSet3((pDateTime), (year), (month), (day), (hour), (min), (sec), 0, 0)
void NuDateTimeSet3(NuDateTime_t *pDateTime, int year, int month, int day, int hour, int min, int sec, int ms, int us);

/* 
 * len = 19. yyyy-mm-dd hh:mm:ss
 * len = 23. yyyy-mm-dd hh:mm:ss.sss
 * len = 26. yyyy-mm-dd hh:mm:ss.ssssss
 * len = 17. yyyy-mm-dd-hh:mm:ss
 */
typedef enum _DateTime_FMT
{
	DATETIME_FMT1 = 0,
	DATETIME_FMT2,
	DATETIME_FMT3,
	DATETIME_FMT4
} DateTime_FMT;

void NuDateTimeGet(NuDateTime_t *pDateTime, char *pRtnStr, DateTime_FMT Format);
#define NuDateTimeGet1(NuDTime, pRtnTime)   NuDateTimeGet( (NuDTime), (pRtnTime), DATETIME_FMT1 )
#define NuDateTimeGet2(NuDTime, pRtnTime)   NuDateTimeGet( (NuDTime), (pRtnTime), DATETIME_FMT2 )
#define NuDateTimeGet3(NuDTime, pRtnTime)   NuDateTimeGet( (NuDTime), (pRtnTime), DATETIME_FMT3 )
#define NuDateTimeGet4(NuDTime, pRtnTime)   NuDateTimeGet( (NuDTime), (pRtnTime), DATETIME_FMT4 )

#ifdef __cplusplus
}
#endif

#endif /* _NUTIME_H */

