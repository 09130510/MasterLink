
#include "NuBuffer.h"
#include "NuBufferAdapter.h"

typedef struct _MyStruct
{
	char	data[100];
	char	data2[10];
} MyStruct;

void PrintBuf(NuBuffer_t *pBuf)
{
	printf("AllocPage[%d] AllocRate[%d]\n", base_vector_get_cnt(pBuf->AllocVec), pBuf->AllocRate);
	return;
}

void PrintPtr(void *ptr)
{
	printf("[%p]\n", ptr);
	return;
}

int main(int argc, char **argv)
{
	MyStruct	*ptr = NULL;
	int			Cnt = 3;
	NuBuffer_t	*pBuf = NULL;

	printf("SizofStruct[%ld]\n", sizeof(MyStruct));
	NuBufferNew2(&pBuf, NuBufferAllocTypeMmap, sizeof(MyStruct), 3, 10, "/home/oms/Laphone/NuLib2.0/tmp/Laphone.MMAPLog");
	PrintBuf(pBuf);
	printf("BufferNodeSize[%ld]\n", pBuf->NodeSize);

	printf("Begin of while...\n");

	while(Cnt --)
	{
		ptr = NuBufferGet(pBuf);
		PrintBuf(pBuf);
		PrintPtr(ptr);

		printf(">>>%s<<<\n", ((MyStruct *)ptr)->data2);
		sprintf(((MyStruct *)ptr)->data2, "hahaha%03d", Cnt);

//		NuBufferPut(pBuf, ptr);
//		PrintBuf(pBuf);
	}

	printf("End of while...\n");

	ptr = NuBufferGet(pBuf);
	PrintPtr(ptr);
	ptr = NuBufferGet(pBuf);
	PrintPtr(ptr);
	ptr = NuBufferGet(pBuf);
	PrintPtr(ptr);
	ptr = NuBufferGet(pBuf);
	PrintPtr(ptr);
	ptr = NuBufferGet(pBuf);
	PrintPtr(ptr);
	ptr = NuBufferGet(pBuf);
	PrintPtr(ptr);
	ptr = NuBufferGet(pBuf);
	PrintPtr(ptr);
	ptr = NuBufferGet(pBuf);
	PrintPtr(ptr);
	ptr = NuBufferGet(pBuf);
	PrintPtr(ptr);
	ptr = NuBufferGet(pBuf);
	PrintPtr(ptr);
	PrintBuf(pBuf);

	printf("After of 10 get...\n");
	ptr = NuBufferGet(pBuf);
	PrintPtr(ptr);
	PrintBuf(pBuf);
	
	printf("After of one more get...\n");

	NuBufferPut(pBuf, ptr);
	PrintBuf(pBuf);

	printf("After of one put...\n");

	return 0;
}

