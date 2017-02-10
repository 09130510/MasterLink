
#include "NuIPCQ.h"

typedef struct _Msg_t
{
	NuIPCQMsgHdr_t	Hdr;
	char			Msg[1];
} Msg_t;

#define	MsgSize	sizeof(Msg_t)
#define	MsgType_df	10

int
main(int argc, char **argv)
{
	int			iRC = 0;
	NuIPCQ_t	*pQ = NULL;
	key_t		Key = 0x00000100;
	Msg_t		Msg1;
	char		Str[10] = "\0";
	char		*pStr = Msg1.Msg;

	strcpy(pStr + 1, "SSSSS");

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

	strcpy(Msg1.Msg, "1234567890hahaha@");
	printf("[msg=%s]\n", Msg1.Msg);

	iRC = NuIPCQEnqueue(pQ, MsgType_df, pMsg1, MsgSize);
	if(iRC < 0)
	{
		printf("Send!! %d\n", iRC);
		return -1;
	}

	return 0;
}

