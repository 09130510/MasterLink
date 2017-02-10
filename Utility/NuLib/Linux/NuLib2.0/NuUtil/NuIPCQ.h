
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/msg.h>
#include "NuCommon.h"

#ifndef  _NUIPCQ_H_
#define  _NUIPCQ_H_

#ifdef  __cplusplus
extern "C" {
#endif

typedef struct _NuIPCQ_t
{
	int		Id;
	key_t	Key;
} NuIPCQ_t;

typedef struct _NuIPCQMsgHdr_t
{
	long	MsgType;
} NuIPCQMsgHdr_t;

#define NuIPCQMsgHdrSz (sizeof(NuIPCQMsgHdr_t))

typedef struct _NuIPCQMsg_t
{
	NuIPCQMsgHdr_t	Hdr;
	char			Data[1];
} NuIPCQMsg_t;

#define	NuIPCQ_READ		0400
#define	NuIPCQ_WRITE	0200

#define	NuIPCQ_ACCESS	(NuIPCQ_READ | NuIPCQ_WRITE | (NuIPCQ_READ >> 3) | (NuIPCQ_WRITE >> 3))

/* Create/Attach an IPC Queue with specified key. */
int NuIPCQInit(NuIPCQ_t **pQ, key_t *pKey);
/* Create/Attach an IPC queue only for forked processes using. */
int NuIPCQInitPrivate(NuIPCQ_t **pQ);

/* EnQueue. */
int NuIPCQEnqueue(NuIPCQ_t *pQ, long MsgType, void *pMsg, size_t Len);
int NuIPCQBlockingEnqueue(NuIPCQ_t *pQ, long MsgType, void *pMsg, size_t Len);

/* DeQueue. */
int NuIPCQDequeue(NuIPCQ_t *pQ, long MsgType, void *pMsg, size_t Len);
int NuIPCQBlockingDequeue(NuIPCQ_t *pQ, long MsgType, void *pMsg, size_t Len);

/* Remove the IPC Queue. */
int NuIPCQRemoveByQ(NuIPCQ_t *pQ);

/* Reset the IPC Queue. */
int NuIPCQRest(NuIPCQ_t **pQ);

#ifdef __cplusplus
}
#endif

#endif /* _NUIPCQ_H */

