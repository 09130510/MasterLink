
#include "NuMQ.h"

int main(int argc, char **argv)
{
	int		iRC = 0, Len = -1, Cnt = 0;
	char	Msg[10] = "\0";
	void	*OutMsg = (void *)"IniValue";
	NuMQ_t	*pMQ = NULL;
	NuListNode_t *pNode = NULL;

	iRC = NuMQNew(&pMQ, NULL, NULL);
	if(iRC < 0)
	{
		printf("NuMQNew!\n");
		return 0;
	}

	for(Cnt = 0; Cnt < 30; Cnt ++)
	{
		Len = sprintf(Msg, "Msg%d", Cnt);
		iRC = NuMQEnqueue2(pMQ, (void *)&Msg, Len);
		if(iRC < 0)
		{
			printf("Err NuMQEnQ %s!\n", Msg);
			return 0;
		}
	}

	for(Cnt = 0; Cnt < 10; Cnt ++)
	{
		iRC = NuMQDequeue2(pMQ, &OutMsg, &Len);
		printf("(%d)Pop(%s)[Len=%d]\n", iRC, (char *)OutMsg, Len);
		free(OutMsg);
	}
/*
	iRC = NuMQDequeue(pMQ, &pNode);
	printf("(%d)FirstPop(%s)\n", iRC, (char *)(pNode->item));
	printf("%d\n", pNode->size);
*/	

	return 0;
}

