
#include "NuBuffer.h"

int
main(int argc, char **argv)
{
	int			Cnt = 0;
	void		*ptr = NULL;
	NuBuffer_t	*pBuf = NULL;

	NuBufferInitiate(&pBuf, 100, 10);
	
	for(Cnt = 0; Cnt < 1000000; Cnt ++)
	{
		NuBufferGet(pBuf, &ptr);
	}

	return 0;

}

