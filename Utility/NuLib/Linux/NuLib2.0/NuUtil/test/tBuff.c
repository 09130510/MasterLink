
#include "NuBuffer.h"

int
main(int argc, char **argv)
{
	void		*vptr = NULL;
	char		*ptr = NULL;
	NuBuffer_t	*pBuf = NULL;

	NuBufferInitiate(&pBuf, 3, 5);

	NuBufferGet(pBuf, &vptr);
	ptr = (char *)vptr;
	sprintf(ptr, "1");
	NuBufferPut(pBuf, ptr);

	NuBufferGet(pBuf, &vptr);
	ptr = (char *)vptr;
	sprintf(ptr, "2");
	NuBufferPut(pBuf, ptr);

	NuBufferGet(pBuf, &vptr);
	ptr = (char *)vptr;
	sprintf(ptr, "3");
	NuBufferPut(pBuf, ptr);


	while(atoi(ptr) != 2)
	{
		NuBufferGet(pBuf, &vptr);
		ptr = (char *)vptr;
	}

	NuBufferRealloc(pBuf, &vptr, 3, 10);
	ptr = (char *)vptr;
	sprintf(ptr, "abcdefgh");

	printf("%d %s\n", sizeof(ptr), ptr);

	return 0;
}

