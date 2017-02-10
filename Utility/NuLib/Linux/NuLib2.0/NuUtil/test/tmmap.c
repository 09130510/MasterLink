#include "NuMMap.h"

#if 1
int main(int argc, char **argv)
{
	char      szPath[] = "./mmap";
	NuMMap_t *pmmap = NULL;
	char      szData[11+1] = {0};
	int       i = 0;
	int       len = 0;
	char     *ptr = NULL;

	NuMMapNew(&pmmap, szPath, "a+", 1024, PAGE_READWRITE , FILE_MAP_ALL_ACCESS);
	NuMMapSetReMapMode(pmmap, NuMMapReMapSegment);

	for(i = 0; i < 100000; i++)
	{
		len = sprintf(szData, "%d", i);
		NuMMapWriteLine(pmmap, szData, len);
		/*
		NuMMapWriteN(pmmap, szData, len);
		*/
	}

	for(; i > 0; i--)
	{
		len = sprintf(szData, "%d", i);
		NuMMapGet(pmmap, (len + 1), (void *)&ptr);
		memcpy(ptr, szData, len);
		ptr[len] = '\n';
	}

	NuMMapFree(pmmap);
	return 0;
}
#endif

#if 0
int main(int argc, char **argv)
{
	char      szPath[] = "./mmap";
	NuMMap_t *pmmap = NULL;
	char      szData[11+1] = {0};
	int       i = 0;
	int       len = 0;
	char     *ptr = NULL;

	NuMMapNew(&pmmap, szPath, "a+", 1024, PAGE_READWRITE , FILE_MAP_ALL_ACCESS);

	for(i = 0; i < 100000; i++)
	{
		len = sprintf(szData, "%06d", i);
		NuMMapWriteN(pmmap, szData, len);
	}

	NuMMapFree(pmmap);
	return 0;
}
#endif

#if 0
int main(int argc, char **argv)
{
	char      szPath[] = "./mmap";
	NuMMap_t *pmmap = NULL;
	char      szData[11+1] = {0};
	int       i = 0;
	int       len = 0;
	char     *ptr = NULL;

	NuMMapNew(&pmmap, szPath, "a+", 600017, PAGE_READWRITE , FILE_MAP_ALL_ACCESS);

	ptr = (char *)NuMMapGetAddr(pmmap);

	while (*ptr != 0x00)
	{
		memcpy(szData, ptr, 6);
		szData[6] = '\0';
		printf("[%s]\n", szData);
		ptr += 6;
	}

	NuMMapFree(pmmap);
	return 0;
}
#endif
