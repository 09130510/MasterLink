#include "NuStream.h"
#if 0
int main(int argc, char **argv)
{
	int iRC = 0;
	NuStrm_t *pStrm = NULL;

	char      szData[11+1] = {0};
	int       i = 0;
	int       len = 0;
/*
	iRC = NuStrmNew(&pStrm, enMMapStream, 5000, "./data", "data.mmap");
*/
	iRC = NuStrmNew(&pStrm,  enFileStream, 5000, "./data", "data.log");
	if (iRC < 0)
		printf("new fail! [%d]\n", iRC);
/*
	for(i = 0; i < 1890; i++)
*/
#if 1
	printf("start[%ld]\n", time(NULL));
	for(i = 0; i < 3000; i++)
	{
		len = sprintf(szData, "%d\n", i);
		iRC = NuStrmWriteN(pStrm, szData, len);
		if (iRC < 0)
			printf("write fail\n");
	}
	printf("end[%ld]\n", time(NULL));
#endif
	NuStrmFree(pStrm);

	return 0;
}
#else
int main(int argc, char **argv)
{
	int     iRC = 0;
	NuStrm_t *pStrm = NULL;
    clock_t start = 0, end = 0, i = 0;
    float   total = 0;
	char    szData[128+1] = {0};
	size_t  len = sizeof(szData);

	memset(szData, 'E', sizeof(szData)-1);
	szData[127] = '\n';
	szData[128] = '\0';
	len = strlen(szData);

	/*
	iRC = NuStrmNew(&pStrm,  enFileStream, 0, "./data", "data.log");
	iRC = NuStrmNew(&pStrm, enMMapStream, 200 * 1000000, "./data", "data.mmap");
	*/
	iRC = NuStrmNew(&pStrm, enMMapStream, 0, "./data", "data.mmap");
	if (iRC < 0)
	{
		printf("[%d]\n", iRC);
		return -1;
	}
		printf("1[%d]\n", iRC);

    start = clock();
	for(i = 0; i < 1000000; i++)
	{
		iRC = NuStrmWriteN(pStrm, szData, len);
		if (iRC < 0)
			printf("write fail\n");
		iRC = NuStrmFlush(pStrm);
		if (iRC < 0)
			printf("flush fail\n");
	}
    end = clock();
    total = (float)(end - start)/CLOCKS_PER_SEC;
    printf("NuSprintf start - end = [%f]\n", total);
	NuStrmFree(pStrm);
	return 0;
}
#endif

