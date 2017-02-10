
#include "NuIPCQ.h"

/* Internal functions. */
static int _QGet(NuIPCQ_t **pQ, key_t *pKey, int Flag, int Malloc)
{
	int	Ret = NU_OK;

	if(Malloc)
	{
		*pQ = (NuIPCQ_t *)malloc(sizeof(NuIPCQ_t));
		if(*pQ == NULL)
			return NU_MALLOC_FAIL;
	}

	memcpy(&((*pQ)->Key), pKey, sizeof(key_t));

	if((Ret = msgget((*pQ)->Key, Flag | NuIPCQ_ACCESS)) < 0)
	{
		switch(errno)
		{
		case EACCES:
			Ret = NU_QACCESSFAIL;
			break;
		case ENOMEM:
		case ENOSPC:
			Ret = NU_QRESOURCEDEFICIENT;
			break;
		default:
			Ret = NU_CREATEQFAIL;
			break;
		}
	}
	else
		(*pQ)->Id = Ret;

	return Ret;
}

static int _QSend(NuIPCQ_t *pQ, long MsgType, void *pMsg, size_t Len, int Flag)
{
	int	Ret = NU_OK;

	if(pQ->Id == -1)
		return NU_QNOTEXIST;

	if(Len <= 0)
		return NU_PARAMERROR;

	if(MsgType > 0)
		((NuIPCQMsg_t *)pMsg)->Hdr.MsgType = MsgType;
	else
		return NU_PARAMERROR;

	if(msgsnd(pQ->Id, pMsg, Len - NuIPCQMsgHdrSz, Flag) < 0)
	{
		switch(errno)
		{
		case EAGAIN:
			Ret = NU_QRESOURCEDEFICIENT;
			break;
		case EIDRM:
			Ret = NU_QNOTEXIST;
			break;
		case EINTR:
			Ret = NU_ENQINTERRUPT;
			break;
		default:
			Ret = NU_ENQFAIL;
			break;
		}
	}

	return Ret;
}

static int _QRecv(NuIPCQ_t *pQ, long MsgType, void *pMsg, size_t Len, int Flag)
{
	int	Ret = NU_OK;

	if(pQ->Id == -1)
		return NU_QNOTEXIST;

	if(Len <= 0)
		return NU_PARAMERROR;

	if((Ret = msgrcv(pQ->Id, pMsg, Len - NuIPCQMsgHdrSz, MsgType, Flag)) < 0)
	{
		switch(errno)
		{
		case E2BIG:
			Ret = NU_PARAMERROR;
			break;
		case ENOMSG:
			Ret = 0;
			break;
		case EIDRM:
			Ret = NU_QNOTEXIST;
			break;
		case EINTR:
			Ret = NU_ENQINTERRUPT;
			break;
		default:
			Ret = NU_DEQFAIL;
			break;
		}
	}

	return Ret;
}

static int _QRmv(NuIPCQ_t *pQ, int Free)
{
	int	Ret = NU_OK;

	if(pQ->Id == -1)
		return NU_QNOTEXIST;
		
	if(msgctl(pQ->Id, IPC_RMID, NULL) < 0)
	{
		switch(errno)
		{
		case EACCES:
		case EPERM:
			Ret = NU_QACCESSFAIL;
			break;
		case EIDRM:
		case EINVAL:
			Ret = NU_QNOTEXIST;
			break;
		default:
			Ret = NU_RMQFAIL;
			break;
		}
	}
	else if(Free)
		free(pQ);

	return Ret;
}

/* Create/Attach an IPC Queue with specified key. */
int NuIPCQInit(NuIPCQ_t **pQ, key_t *pKey)
{
	return _QGet(pQ, pKey, IPC_CREAT, 1);
}

/* Create/Attach an IPC queue only for forked processes using. */
int NuIPCQInitPrivate(NuIPCQ_t **pQ)
{
	return _QGet(pQ, IPC_PRIVATE, IPC_CREAT, 1);
}

/* EnQueue. */
int NuIPCQEnqueue(NuIPCQ_t *pQ, long MsgType, void *pMsg, size_t Len)
{
	return _QSend(pQ, MsgType, pMsg, Len, IPC_NOWAIT);
}

/* Blocking verion of enqueue. */
int NuIPCQBlockingEnqueue(NuIPCQ_t *pQ, long MsgType, void *pMsg, size_t Len)
{
	int	iRC = NU_OK;

	while((iRC = _QSend(pQ, MsgType, pMsg, Len, 0)) < 0)
	{
		if(iRC ==  NU_QNOTEXIST || iRC == NU_ENQFAIL || iRC == NU_PARAMERROR)
			return iRC;
	}

	return iRC;
}

/* DeQueue. */
int NuIPCQDequeue(NuIPCQ_t *pQ, long MsgType, void *pMsg, size_t Len)
{
	return _QRecv(pQ, MsgType, pMsg, Len, IPC_NOWAIT);
}

/* Blocking version of dequeue. */
int NuIPCQBlockingDequeue(NuIPCQ_t *pQ, long MsgType, void *pMsg, size_t Len)
{
	int iRC = NU_OK;
	
	while((iRC = _QRecv(pQ, MsgType, pMsg, Len, 0)) < 0)
	{
		if(iRC ==  NU_QNOTEXIST || iRC == NU_ENQFAIL || NU_PARAMERROR)
			return iRC;
	}

	return iRC;
}

/* Remove the IPC Queue. */
int NuIPCQRemoveByQ(NuIPCQ_t *pQ)
{
	return _QRmv(pQ, 1);
}

/* Reset the IPC Queue. */
int NuIPCQRest(NuIPCQ_t **pQ)
{
	int		iRC = _QRmv(*pQ, 0);

	if(iRC < 0)
		return iRC;

	return _QGet(pQ, &((*pQ)->Key), IPC_CREAT, 0);
}

