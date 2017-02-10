#include "NuTime.h"

/* base function */
/* ====================================================================== */
#ifdef _WIN32
#define NuGmTime(ptm, t)	(ptm) = gmtime((t))
#else
#define NuGmTime(ptm, t)	gmtime_r((t), ptm)
#endif

#ifdef _WIN32
#define NuLocalTime(ptm, t)	(ptm) = localtime((t))
#else
#define NuLocalTime(ptm, t)	localtime_r((t), ptm)
#endif

/* Time Function */
/* ====================================================================== */

#ifdef _WIN32
int gettimeofday(struct timeval *tvp, struct timezone *tzp)
{
    struct _timeb tb;
    
    _ftime(&tb);
    if (tvp)
    {
        tvp->tv_sec = tb.time;
        tvp->tv_usec = tb.millitm * 1000;
    }
    if (tzp)
    {
        tzp->tz_minuteswest = tb.timezone;
        tzp->tz_dsttime = tb.dstflag;
    }
	return 0;
}
#endif

/* Date */
/* ====================================================================== */
void NuGetToday(char *pToday)
{
    time_t		now = time(NULL);
    struct tm	*ptm = NULL;

#ifndef _WIN32
    struct tm	ltimer;
#endif

#ifdef _WIN32
    ptm = localtime(&now);
#else
    ptm = localtime_r(&now, &ltimer);
#endif

	/* YYYYMMDD */
	NuCStrPrintInt(pToday, ptm->tm_year + 1900, 4);
	NuCStrPrintInt(pToday + 4, ptm->tm_mon + 1, 2);
	NuCStrPrintInt(pToday + 6, ptm->tm_mday, 2);

    return;
}

void NuGetToday2(char *pToday)
{
    time_t		now = time(NULL);
    struct tm	*ptm = NULL;

#ifndef _WIN32
    struct tm	ltimer;
#endif

#ifdef _WIN32
    ptm = localtime(&now);
#else
    ptm = localtime_r(&now, &ltimer);
#endif

	/* YYYY-MM-DD */
	NuCStrPrintInt(pToday, ptm->tm_year + 1900, 4);
	*(pToday + 4) = '-';
	NuCStrPrintInt(pToday + 5, ptm->tm_mon + 1, 2);
	*(pToday + 7) = '-';
	NuCStrPrintInt(pToday + 8, ptm->tm_mday, 2);

    return;
}

/* Date */
/* ====================================================================== */
void NuGetTime(char *pTime)
{
    time_t			now, tmp;
	struct timeval	tv;
    struct timezone	tz;

#ifdef _WIN32
    struct tm		*ptm = NULL;
#else
#endif
    gettimeofday(&tv, &tz);

#ifdef _WIN32
    now = tv.tv_sec;
    ptm = localtime(&now);

	/* hh:mm:ss.SSSSSS */
	NuCStrPrintInt(pTime, ptm->tm_hour, 2);
	*(pTime + 2) = ':';
	NuCStrPrintInt(pTime + 3, ptm->tm_min, 2);
	*(pTime + 5) = ':';
	NuCStrPrintInt(pTime + 6, ptm->tm_sec, 2);
	*(pTime + 8) = '.';
	NuCStrPrintInt(pTime + 9, (int)tv.tv_usec, 6);

#else
	now = (tv.tv_sec + SecInHour(8)) % 86400;

	/* hh:mm:ss.SSSSSS */
	NuCStrPrintInt(pTime, (int)(tmp = now / 3600), 2);
	*(pTime + 2) = ':';

	now -= tmp * 3600;
	NuCStrPrintInt(pTime + 3, (int)(tmp = now / 60), 2);
	*(pTime + 5) = ':';
	NuCStrPrintInt(pTime + 6, (int)(now - tmp * 60), 2);
	*(pTime + 8) = '.';
	NuCStrPrintInt(pTime + 9, (int)tv.tv_usec, 6);
#endif

    return;
}

void NuGetTime2(char *pTime)
{
    time_t		now = time(NULL), tmp;

#ifdef _WIN32
    struct tm	*ptm = NULL;
#endif

#ifdef _WIN32
    ptm = localtime(&now);

	/* hh:mm:ss */
	NuCStrPrintInt(pTime, ptm->tm_hour, 2);
	*(pTime + 2) = ':';
	NuCStrPrintInt(pTime + 3, ptm->tm_min, 2);
	*(pTime + 5) = ':';
	NuCStrPrintInt(pTime + 6, ptm->tm_sec, 2);
#else
	now = (now + SecInHour(8)) % 86400;

	/* hh:mm:ss */
	NuCStrPrintInt(pTime, (int)(tmp = now / 3600), 2);
	*(pTime + 2) = ':';

	now -= tmp * 3600;
	NuCStrPrintInt(pTime + 3, (int)(tmp = now / 60), 2);
	*(pTime + 5) = ':';
	NuCStrPrintInt(pTime + 6, (int)(now - tmp * 60), 2);
#endif

    return;
}

void NuGetTime_HHMMSS(char *pTime)
{
    time_t			now, tmp;
    struct timeval	tv;
    struct timezone	tz;

#ifdef _WIN32
    struct tm		*ptm = NULL;
#else
#endif

    gettimeofday(&tv, &tz);

#ifdef _WIN32
    now = tv.tv_sec;
    ptm = localtime(&now);

	/* hhmmss */
	NuCStrPrintInt(pTime, ptm->tm_hour, 2);
	NuCStrPrintInt(pTime + 2, ptm->tm_min, 2);
	NuCStrPrintInt(pTime + 4, ptm->tm_sec, 2);
#else
	now = (tv.tv_sec + SecInHour(8)) % 86400;

	/* hhmmss */
	NuCStrPrintInt(pTime, (int)(tmp = now / 3600), 2);

	now -= tmp * 3600;
	NuCStrPrintInt(pTime + 2, (int)(tmp = now / 60), 2);
	NuCStrPrintInt(pTime + 4, (int)(now - tmp * 60), 2);
#endif

    return;
}

time_t NuHMSToTime(char *pHms)
{
    time_t		now = time(NULL);
    struct tm	*ptm = NULL;
#ifndef _WIN32
    struct tm	ltimer;
#endif

	/* HH:MM:SS */
	if (strlen(pHms) < 8)
		return 0;

#ifdef _WIN32
    ptm = localtime(&now);
#else
    ptm = localtime_r(&now, &ltimer);
#endif

	ptm->tm_hour = ((*pHms - '0') * 10) + *(pHms + 1) - '0';
	ptm->tm_min = ((*(pHms + 3) - '0') * 10) + *(pHms + 4) - '0';
	ptm->tm_sec = ((*(pHms + 6) - '0') * 10) + *(pHms + 7) - '0';

    return mktime(ptm);
}


/* base function */
/* ====================================================================== */
int NuDateTimeNew(NuDateTime_t **pDateTime, int IsLocal)
{
	int	iRC = NU_OK;

	(*pDateTime) = (NuDateTime_t *)malloc(sizeof(NuDateTime_t));
	if ((*pDateTime) == NULL)
		NUGOTO(iRC, NU_MALLOC_FAIL, EXIT);

#ifndef _WIN32
	(*pDateTime)->ptm = (struct tm *)malloc(sizeof(struct tm));
	if((*pDateTime)->ptm == NULL)
		NUGOTO(iRC, NU_MALLOC_FAIL, EXIT);
#endif

	NuDateTimeInit((*pDateTime), 0, IsLocal);

EXIT:
	if(iRC < 0)
		NuDateTimeFree(*pDateTime);

	return iRC;
}

void NuDateTimeFree(NuDateTime_t *pDateTime)
{
	if (pDateTime != NULL)
	{
#ifndef _WIN32
		if(pDateTime->ptm != NULL)
			free(pDateTime->ptm);	
#endif
		free(pDateTime);
	}
	return;
}

void NuDateTimeInit(NuDateTime_t *pDateTime, int MoveHour, int IsLocal)
{
	pDateTime->ms = pDateTime->us = 0;
	pDateTime->MoveHour = MoveHour;
	pDateTime->IsLocal = IsLocal;
	return;
}

int NuDateTimeSet(NuDateTime_t *pDateTime)
{
    time_t			now = time(NULL) + (pDateTime->MoveHour * NuDateTimeHR);
    struct timeval	tv;
    struct timezone	tz;

	if (pDateTime->IsLocal == NUTRUE) /* local */
		NuLocalTime(pDateTime->ptm, &now);
	else	/* UTC   */
		NuGmTime(pDateTime->ptm, &now);

	if (gettimeofday(&tv, &tz) < 0)
		return NU_FAIL;

	pDateTime->us = (int)(tv.tv_usec - (pDateTime->ms = (int)(tv.tv_usec / 1000)) * 1000);

    return NU_OK;
}

void NuDateTimeSet3(NuDateTime_t *pDateTime, int year, int month, int day, int hour, int min, int sec, int ms, int us)
{
	struct tm	tm_tmp;
    time_t		now;

	tm_tmp.tm_year = year - 1900;
	tm_tmp.tm_mon  = month - 1;
	tm_tmp.tm_mday = day;
	tm_tmp.tm_hour = hour;
	tm_tmp.tm_min  = min;
	tm_tmp.tm_sec  = sec;

	now = mktime(&tm_tmp) + (pDateTime->MoveHour * NuDateTimeHR);

	if (pDateTime->IsLocal == NUTRUE) /* local */
		NuLocalTime(pDateTime->ptm, &now);
	else	/* UTC   */
		NuGmTime(pDateTime->ptm, &now);

	pDateTime->ms = ms;
	pDateTime->us = us;

	return;
}

void NuDateTimeGet(NuDateTime_t *pDateTime, char *pRtnStr, DateTime_FMT Format)
{
	struct tm	*ptm = pDateTime->ptm;
	
	switch(Format)
	{
	case DATETIME_FMT4:
		/* YYYYMMDD-hh:mm:ss */
		NuCStrPrintInt(pRtnStr, ptm->tm_year + 1900, 4);
		NuCStrPrintInt(pRtnStr + 4, ptm->tm_mon + 1, 2);
		NuCStrPrintInt(pRtnStr + 6, ptm->tm_mday, 2);
		*(pRtnStr + 8) = '-';
		NuCStrPrintInt(pRtnStr + 9, ptm->tm_hour, 2);
		*(pRtnStr + 11) = ':';
		NuCStrPrintInt(pRtnStr + 12, ptm->tm_min, 2);
		*(pRtnStr + 14) = ':';
		NuCStrPrintInt(pRtnStr + 15, ptm->tm_sec, 2);

		break;
	case DATETIME_FMT3:
		/* YYYY-MM-DD hh:mm:ss.SSSSSS */
		NuCStrPrintInt(pRtnStr, ptm->tm_year + 1900, 4);
		*(pRtnStr + 4) = '-';
		NuCStrPrintInt(pRtnStr + 5, ptm->tm_mon + 1, 2);
		*(pRtnStr + 7) = '-';
		NuCStrPrintInt(pRtnStr + 8, ptm->tm_mday, 2);
		*(pRtnStr + 10) = ' ';
		NuCStrPrintInt(pRtnStr + 11, ptm->tm_hour, 2);
		*(pRtnStr + 13) = ':';
		NuCStrPrintInt(pRtnStr + 14, ptm->tm_min, 2);
		*(pRtnStr + 16) = ':';
		NuCStrPrintInt(pRtnStr + 17, ptm->tm_sec, 2);
		*(pRtnStr + 19) = '.';
		NuCStrPrintInt(pRtnStr + 20, pDateTime->ms, 3);
		NuCStrPrintInt(pRtnStr + 23, pDateTime->us, 3);
	
		break;
	case DATETIME_FMT2:
		/* YYYY-MM-DD hh:mm:ss.SSS */
		NuCStrPrintInt(pRtnStr, ptm->tm_year + 1900, 4);
		*(pRtnStr + 4) = '-';
		NuCStrPrintInt(pRtnStr + 5, ptm->tm_mon + 1, 2);
		*(pRtnStr + 7) = '-';
		NuCStrPrintInt(pRtnStr + 8, ptm->tm_mday, 2);
		*(pRtnStr + 10) = ' ';
		NuCStrPrintInt(pRtnStr + 11, ptm->tm_hour, 2);
		*(pRtnStr + 13) = ':';
		NuCStrPrintInt(pRtnStr + 14, ptm->tm_min, 2);
		*(pRtnStr + 16) = ':';
		NuCStrPrintInt(pRtnStr + 17, ptm->tm_sec, 2);
		*(pRtnStr + 19) = '.';
		NuCStrPrintInt(pRtnStr + 20, pDateTime->ms, 3);

		break;
	case DATETIME_FMT1:
	default:
		/* YYYY-MM-DD hh:mm:ss */
		NuCStrPrintInt(pRtnStr, ptm->tm_year + 1900, 4);
		*(pRtnStr + 4) = '-';
		NuCStrPrintInt(pRtnStr + 5, ptm->tm_mon + 1, 2);
		*(pRtnStr + 7) = '-';
		NuCStrPrintInt(pRtnStr + 8, ptm->tm_mday, 2);
		*(pRtnStr + 10) = ' ';
		NuCStrPrintInt(pRtnStr + 11, ptm->tm_hour, 2);
		*(pRtnStr + 13) = ':';
		NuCStrPrintInt(pRtnStr + 14, ptm->tm_min, 2);
		*(pRtnStr + 16) = ':';
		NuCStrPrintInt(pRtnStr + 17, ptm->tm_sec, 2);

		break;
	}

	return;
}

