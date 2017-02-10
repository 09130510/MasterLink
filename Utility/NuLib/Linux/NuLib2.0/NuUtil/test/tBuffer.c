
#include "NuList.h"
#include "NuBuffer.h"

int
main(int argc, char **argv)
{
	int				Cnt = 0, Test = 1000000, PreAlloc = 0, Ratio = 3;
	clock_t			start, end;
	void			*voidNodeIn = NULL;
	NuList_t		*pList = NULL, *pList2 = NULL;
	NuListNode_t	*pNodeIn = NULL, *pNodeOut = NULL;
	NuBuffer_t		*pBuf = NULL;

	if(argc > 1)
		Test = atoi(argv[1]);

	if(argc > 2)
		Ratio = atoi(argv[2]);

	PreAlloc = Test / Ratio;

	NuListNew(&pList, NULL);
/*
	for(Cnt = 0; Cnt < Test; Cnt ++)
	{
		NuListNodeNew(&pNodeIn, NULL);
		NuListInsTail(pList, pNodeIn);
	}
*/
	start = clock();

	for(Cnt = 0; Cnt < Test; Cnt ++)
	{
	/*
		NuListNodeNew(&pNodeIn, NULL);
	*/
		pNodeIn = (NuListNode_t *)malloc(sizeof(NuListNode_t));
		NuListInsTail(pList, pNodeIn);
		NuListRmvHead(pList, &pNodeOut);
		NuListNodeFree(pNodeOut);
	}

	end = clock();

	printf("%d Get\n", Test);
	printf("[%f] sec\n", (float)(end - start)/CLOCKS_PER_SEC);
	printf("==========================================================================\n");

	sleep(1);
	NuBufferInitiate(&pBuf, sizeof(NuListNode_t), PreAlloc);
	NuListNew(&pList2, NULL);
/*
	for(Cnt = 0; Cnt < Test; Cnt ++)
	{
		NuListNodeNew(&pNodeIn, NULL);
		NuListInsTail(pList2, pNodeIn);
	}
*/

	start = clock();
	
	for(Cnt = 0; Cnt < Test; Cnt ++)
	{
		NuBufferGet(pBuf, &voidNodeIn);
		NuListInsTail(pList2, (NuListNode_t *)voidNodeIn);
		NuListRmvHead(pList, &pNodeOut);
		NuBufferPut(pBuf, pNodeOut);
	}

	end = clock();

	printf("%d Get in PreAlloc %d\n", Test, PreAlloc);
	printf("[%f] sec\n", (float)(end - start)/CLOCKS_PER_SEC);
	printf("==========================================================================\n");

	return 0;

}

