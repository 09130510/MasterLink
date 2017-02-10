
#include "NuFMnt.h"


/* ------------------------------------------------ */
/*  static function                                 */
/* ------------------------------------------------ */
static void _SetMonitor(NuFMnt_t *pFMnt)
{
	/*
	pFMnt->watch_fd = inotify_add_watch(pFMnt->ino_fd, NuStrGet(pFMnt->File), 
	                                    IN_MODIFY|IN_DELETE|IN_CREATE|IN_DELETE_SELF|IN_ATTRIB);
	*/
	pFMnt->watch_fd = inotify_add_watch(pFMnt->ino_fd, NuStrGet(pFMnt->File), IN_ALL_EVENTS);
}
static void _RmvMonitor(NuFMnt_t *pFMnt)
{
	inotify_rm_watch(pFMnt->ino_fd, pFMnt->watch_fd);
	pFMnt->watch_fd = -1;
}
/* ------------------------------------------------ */
/*  public function                                 */
/* ------------------------------------------------ */
int NuFMntNew(NuFMnt_t **pFMnt, char *pFile, size_t bufsz)
{
	if (((*pFMnt) = (NuFMnt_t *)malloc(sizeof(NuFMnt_t))) == NULL)
		return NU_FAIL;
	
	(*pFMnt)->buf = (char *)calloc(bufsz, sizeof(char));
	(*pFMnt)->bufsz = bufsz;
	(*pFMnt)->ino_fd = -1;
	(*pFMnt)->watch_fd = -1;
	(*pFMnt)->cycle.tv_sec = 1;
	(*pFMnt)->cycle.tv_usec = 0;
	(*pFMnt)->File = NULL;

	if (NuStrNew(&((*pFMnt)->File), pFile) < 0)
		return NU_FAIL;

//	NuStrCat((*pFMnt)->File, pFile);
	return NU_OK;
}

int NuFMntNew2(NuFMnt_t **pFMnt, char *pFile)
{
	return NuFMntNew(pFMnt, pFile, NuFMntDefaultBufSz);
}

void NuFMntFree(NuFMnt_t *pFMnt)
{
	if (pFMnt == NULL)
		return;

	if (pFMnt->watch_fd != -1)
		inotify_rm_watch(pFMnt->ino_fd, pFMnt->watch_fd);

	if (pFMnt->ino_fd != -1)
		close(pFMnt->ino_fd);

	if (pFMnt->File != NULL)
		NuStrFree(pFMnt->File);

	free(pFMnt);
	return;
}

void NuFMntSetCycle(NuFMnt_t *pFMnt, int sec, int ms)
{
	pFMnt->sec = sec;
	pFMnt->usec = ms;
}

int NuFMntActive(NuFMnt_t *pFMnt)
{
	if (NuIsFile(NuStrGet(pFMnt->File)) != 0)
		return NuFMntFileNotExists;
	
	pFMnt->ino_fd = inotify_init();
    _SetMonitor(pFMnt);

	return NU_OK;
}

int NuFMntSelect(NuFMnt_t *pFMnt)
{
	FD_ZERO(&(pFMnt->fds));
	FD_SET(pFMnt->ino_fd, &(pFMnt->fds));
	pFMnt->cycle.tv_sec = pFMnt->sec;
	pFMnt->cycle.tv_usec = pFMnt->usec;

	if (select(pFMnt->ino_fd + 1, &(pFMnt->fds), NULL, NULL, &(pFMnt->cycle)) > 0)
	{
		pFMnt->len = pFMnt->readn = 0;

		while ( (pFMnt->len = read(pFMnt->ino_fd, pFMnt->buf, pFMnt->bufsz)) < 0  )
		{
			switch(errno)
			{
				case EINVAL:
					pFMnt->bufsz *= 2;
					pFMnt->buf = (char *)realloc(pFMnt->buf, pFMnt->bufsz);
					if (pFMnt->buf == NULL)
						return NuFMntError;
					continue;
				case EINTR:
					continue;
				default:
					return NuFMntError;
			}
		}

		while (pFMnt->readn < pFMnt->len)
		{
			pFMnt->ev = (struct inotify_event *)&(pFMnt->buf[pFMnt->readn]);
			pFMnt->readn += (sizeof(struct inotify_event *) + pFMnt->ev->len);

			if (pFMnt->ev->mask & IN_MODIFY)
				return NuFMntModify;

			if (pFMnt->ev->mask & IN_DELETE_SELF)
			{
				if (NuIsFile(NuStrGet(pFMnt->File)) == 0)
				{
					_SetMonitor(pFMnt);
					return NuFMntModify;
				}
				else
					return NuFMntDelete;
			}

			if (pFMnt->ev->mask & IN_ATTRIB)
			{
				_RmvMonitor(pFMnt);
				_SetMonitor(pFMnt);
				return NuFMntAttribChange;
			}

			if (pFMnt->ev->mask & IN_DELETE)
				return NuFMntDelete;

			if (pFMnt->ev->mask & IN_Q_OVERFLOW ||
			    pFMnt->ev->mask & IN_UNMOUNT)
				return NuFMntError;

			if (pFMnt->ev->mask & IN_IGNORED)
				continue;
		}
	}
	return NuFMntTimeout;
}
