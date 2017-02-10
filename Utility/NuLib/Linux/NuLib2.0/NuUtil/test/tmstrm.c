#include "NuMMapStream.h"

int main(int argc, char **argv)
{
	int iRC = 0;
	NuMPStrm_t *pStrm = NULL;
	void       *ptr = NULL;

	char      szData[11+1] = {0};
	int       i = 0;
	int       len = 0;

	iRC = NuMPStrmNew(&pStrm, NULL, "", 5000 , PAGE_READWRITE, FILE_MAP_PRIVATE);
	if (iRC < 0)
		printf("new fail! [%d]\n", iRC);

	printf("memory size[%ld]\n", NuMPStrmGetSize(pStrm));

	printf("start[%ld]\n", time(NULL));
	for(i = 0; i < 8000; i++)
	{
		len = sprintf(szData, "%d\n", i);
		iRC = NuMPStrmGet(pStrm, len, &ptr);
		if (iRC < 0)
			printf("write fail\n");
	}
	printf("end[%ld]\n", time(NULL));

	NuMPStrmFree(pStrm);

	return 0;
}
