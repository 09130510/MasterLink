
#include "NuMQ.h"

#define THRD_NUM		5

NuMQ_t		*pMQ = NULL;
int			Thrd_Cnt = THRD_NUM;
int			Num_Per_Thrd = 0;

void*
QTrans(void *msg)
{
	int				Cnt = 0;
	size_t			Len = 5;
	char			Msg[100] = "0";

	while(Cnt < Num_Per_Thrd)
	{
		NuMQEnqueue(pMQ, (void *)Msg, Len);
		NuMQDequeue(pMQ, (void *)Msg, &Len);
		Cnt ++;
	}

	return NULL;
}

int
main(int argc, char **argv)
{
	int			iRC = 0, Cnt = 0;
	clock_t		start, end;

	if(argc < 2)
		Thrd_Cnt = THRD_NUM;
	else
		Thrd_Cnt = atoi(argv[1]);

	pthread_t	thrd[Thrd_Cnt];

	Num_Per_Thrd = 1000000 / Thrd_Cnt;

	iRC = NuMQNew(&pMQ);
	if(iRC < 0)
	{
		printf("NuMQNew!\n");
		return 0;
	}
	printf("MQ OK!\n");

	printf("Start!!!\n\n");

	start = clock();
	for(Cnt = 0; Cnt < Thrd_Cnt; Cnt ++)
	{
		pthread_create(&thrd[Cnt], NULL, QTrans, (void *)"Thrd");
	}

	for(Cnt = 0; Cnt < Thrd_Cnt; Cnt ++)
	{
		pthread_join(thrd[Cnt], NULL);
	}
	end = clock();

	printf("%d Threads, in 1000000 EnQ and DeQ\n", Thrd_Cnt);
	printf("[%f]micro sec per (EnQ, DeQ)\n", (float)(end - start)/CLOCKS_PER_SEC);

	return 0;
}

