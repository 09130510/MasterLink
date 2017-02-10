
#include "NuBuffer.h"

int
main(int argc, char **argv)
{
	int			Data = 0, Cnt = 0;
	void		*Buf = NULL;
	NuBuffer_t	*pBuf = NULL;

	if(argc > 1)
		Data = atoi(argv[1]);
	else
		Data = 100;

	NuBufferInitiate(&pBuf, 10, 5);
	printf("NextLevel[%d] CurrentAlloc[%d] PreAllocRate[%d]\n", pBuf->NextLevel, pBuf->CurrentAlloc, pBuf->PreAllocRate);
	printf("Start!!!\n");


	for(Cnt = 0; Cnt < Data; Cnt ++)
	{
		NuBufferGet(pBuf, &Buf);
	}

printf("Get over!\n");
printf("NextLevel[%d] CurrentAlloc[%d] PreAllocRate[%d]\n", pBuf->NextLevel, pBuf->CurrentAlloc, pBuf->PreAllocRate);
	for(Cnt = 0; Cnt < 3; Cnt ++)
	{
		NuBufferPut(pBuf, Buf);
	}

printf("Put over!\n");
printf("NextLevel[%d] CurrentAlloc[%d] PreAllocRate[%d]\n", pBuf->NextLevel, pBuf->CurrentAlloc, pBuf->PreAllocRate);
	for(Cnt = 0; Cnt < Data; Cnt ++)
	{
		NuBufferGet(pBuf, &Buf);
	}

	NuBufferFree(pBuf);
	return 0;

}

