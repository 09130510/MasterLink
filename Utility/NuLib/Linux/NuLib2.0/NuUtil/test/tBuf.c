
#include "NuBuffer.h"

int
main(int argc, char **argv)
{
	int			Data = 0, Cnt = 0;
	void		*Buf = NULL;
	clock_t		start, end;
	NuBuffer_t		*pBuf = NULL;
	NuBufferNode_t	*pNode = NULL;

	if(argc > 1)
		Data = atoi(argv[1]);
	else
		Data = 100;

	NuBufferInitiate(&pBuf, 10, 5);
	printf("NextLevel[%d] CurrentAlloc[%d] PreAllocRate[%d]\n", pBuf->NextLevel, pBuf->CurrentAlloc, pBuf->PreAllocRate);
/*
	while(pNode != pBuf->Put)
	{
		Cnt ++;
		printf("[%d]\n", Cnt);
		printf("(%d)\n", pNode->Next == NULL);
		pNode = pNode->Next;
	}
*/
	printf("Start!!!\n");

	start = clock();

	for(Cnt = 0; Cnt < Data; Cnt ++)
	{
		NuBufferGet(pBuf, &Buf);
//		NuBufferPut(pBuf, Buf);
	}

	end = clock();

	printf("%d Get\n", Data);
	printf("[%f] sec\n", (float)(end - start)/CLOCKS_PER_SEC);
	printf("==========================================================================\n");

/*
	printf("NextLevel[%d] CurrentAlloc[%d] PreAllocRate[%d]\n", pBuf->NextLevel, pBuf->CurrentAlloc, pBuf->PreAllocRate);
	Cnt = 0;

	pNode = pBuf->Get->Next;
	while(pNode != pBuf->Get)
	{
		Cnt ++;
		printf("[%d]\n", Cnt);
		printf("(%d)\n", pNode->Next == NULL);
		pNode = pNode->Next;
	}
*/
	return 0;

}

