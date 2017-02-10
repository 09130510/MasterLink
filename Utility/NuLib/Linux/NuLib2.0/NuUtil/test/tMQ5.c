
#include "NuMQ.h"

int
main(int argc, char **argv)
{
	int		iRC = 0;
	char	Msg[100] = "\0";
	size_t	Size = 10;
	NuMQ_t	*pMQ = NULL;

	iRC = NuMQNew(&pMQ);
	if(iRC < 0)
	{
		printf("NuMQNew!\n");
		return 0;
	}

	NuMQEnqueue(pMQ, (void *)"abcdefghijkl", 12);
printf("%s", (char *)(pMQ->EnQ->Item));
	NuMQDequeue(pMQ, (void *)Msg, &Size);

	printf("[%d](%s)\n", (int)Size, Msg);

	return 0;
}

