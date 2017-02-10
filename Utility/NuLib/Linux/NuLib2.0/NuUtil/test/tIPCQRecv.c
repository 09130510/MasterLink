
#include "NuIPCQ.h"

typedef struct _Msg_t
{
	NuIPCQMsgHdr_t	Hdr;
	char			Msg[1];
} Msg_t;

#define	MsgSize	sizeof(Msg_t)
#define	MsgType_df	0

int
main(int argc, char **argv)
{
	int			iRC = 0;
	NuIPCQ_t	*pQ = NULL;
	key_t		Key = 0x00000100;
	Msg_t		Msg2;

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

	iRC = NuIPCQDequeue(pQ, MsgType_df, pMsg2, MsgSize);
	if(iRC < 0)
	{
		printf("Recv!! %d\n", iRC);
		return -1;
	}

	printf("Recv<MSG=%s>[%ld]\n", Msg2.Msg, Msg2.Hdr.MsgType);

	return 0;
}

