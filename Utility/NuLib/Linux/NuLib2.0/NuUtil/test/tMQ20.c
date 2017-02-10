
#include "NuMQ.h"

NuMQ_t		*pMQ = NULL;

void*
QTrans2(void *msg)
{
	int				iRC = 0, Cnt = 0;
	size_t			Len = 10;
	char			Msg[10] = "";

	for(Cnt = 0; Cnt < 50; Cnt ++)
	{
		iRC = NuMQDequeue(pMQ, (void *)Msg, &Len);
		if(iRC < 0)
		{
			printf("%s DeQ Err(%d)\n", (char *)msg, iRC);
		}
		printf("[DeQ%ld]%s\n", Len, Msg);
	}

	return NULL;
}

void*
QTrans1(void *msg)
{
	int				iRC = 0, Cnt = 0;
	char			Msg[10] = "";

	for(Cnt = 0; Cnt < 50; Cnt ++)
	{
		sprintf(Msg, "No%d!!", Cnt);
		iRC = NuMQEnqueue(pMQ, (void *)Msg, 10);
		if(iRC < 0)
		{
			printf("%s EnQ Err(%d)\n", (char *)msg, iRC);
		}
		printf("[EnQ]%s\n", Msg);
	}

	return NULL;
}

int
main(int argc, char **argv)
{
	int			iRC = 0;
	char		Msg[3] = "1";
	pthread_t	thrd1, thrd2;

	iRC = NuMQNew(&pMQ);
	if(iRC < 0)
	{
		printf("NuMQNew!\n");
		return 0;
	}
	printf("MQ OK!\n");
	printf("Start!!!\n\n");

	pthread_create(&thrd1, NULL, QTrans1, (void *)"Thrd1");
	sleep(1);
	printf("Start Thread2!!\n");
	pthread_create(&thrd2, NULL, QTrans2, (void *)"Thrd2");

	pthread_join(thrd1, NULL);
	pthread_join(thrd2, NULL);
/*
NuMQ_Msg_t	*Tmp = pMQ->pDeQ;
while(pMQ->pDeQ->Next != Tmp)
{
	printf("[%s]\n", (char *)(pMQ->pDeQ->Item));
	pMQ->pDeQ = pMQ->pDeQ->Next;
}
*/
	return 0;
}

