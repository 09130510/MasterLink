
#include "NuIPCQ.h"

typedef struct _Msg_t
{
	NuIPCQMsgHdr_t	Hdr;
	char			Msg[100];
} Msg_t;

#define	MsgSize	sizeof(Msg_t)
#define	MsgType	10

int
main(int argc, char **argv)
{
	int			iRC = 0;
	NuIPCQ_t	*pQ = NULL;
	key_t		Key = ftok("tIPCQ.o", 5);
	Msg_t		Msg2;
	char		*pMsg2 = (char *)&Msg2;

	if(Key == (key_t)-1)
	{
		printf("Key!!\n");
		return -1;
	}

	iRC = NuIPCQInit(&pQ, &Key);
	if(iRC < 0)
	{
		printf("Q!!\n");
		return -1;
	}

	iRC = NuIPCQBlockingDequeue(pQ, MsgType, pMsg2, MsgSize);
	if(iRC < 0)
	{
		printf("Recv!! %d\n", iRC);
		return -1;
	}

	printf("Recv<MSG=%s>\n", Msg2.Msg);

	return 0;
}

