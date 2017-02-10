#include "NuCommon.h"
#include "NuCStr.h"
#include "NuIPCQ.h"

typedef struct DeQMsg_t
{
	NuIPCQMsgHdr_t Hdr;
	char Text[10240];
} DeQMsg_t;


void Usage(char *exe)
{
	printf("Usage: %s --QID <0x00000010> --FILE <file>\n\n", exe);
}

int main(int argc, char **argv)
{
	int iRC = 0;
	int i = 0;
	char *ptr = NULL;
	char szQID[10+1] = {0};
	FILE *fd = NULL;
	key_t QKey = 0;
	NuIPCQ_t *pQ = NULL;

	DeQMsg_t Data;

	for (i = 1; i < argc; i++)
	{
		ptr = argv[i];

		if (!strcmp(ptr, "--QID"))
		{
			++i;
			strcpy(szQID, argv[i]);
			QKey = (key_t)NuCStrHex2UInt(szQID);
		}
		else if (!strcmp(ptr, "--FILE"))
		{
			++i;
			fd = fopen(argv[i], "a+");
		}
	}

	if (szQID[0] == '\0')
	{
		Usage(argv[0]);
		return 0;
	}
	if (fd == NULL)
		fd = stdout;

	iRC = NuIPCQInit(&pQ, &QKey);
	if (iRC < 0)
		return -1;

	memset(&Data, 0x00, sizeof(DeQMsg_t));
	iRC = NuIPCQDequeue(pQ, 0, &Data, sizeof(DeQMsg_t));
	while( iRC > 0)
	{
		fprintf(fd, "MsgType=%ld ,Text=%s\n", Data.Hdr.MsgType, Data.Text);
		fflush(fd);
		memset(&Data, 0x00, sizeof(DeQMsg_t));
		iRC = NuIPCQDequeue(pQ, 0, &Data, sizeof(DeQMsg_t));
	}

	return 0;
}


