#include "NuTime.h"
#include "NuHashMap.h"

void PrintTime()
{
    char Buf[24] = {0};
    GetTime(Buf);
    printf("[%s]\n", Buf);
}

int main()
{
	char key[12] = {0};
    char val[60] = "test1000000000000000P";

    int i = 0, iRC = 0;

	NuHashMap_t  *pMap = NULL;
    NuHashMapIterator_t IT;

	NuHashMapNew3(&pMap, 3, 12, 6, NULL, NULL, NULL, NULL);

    for (i = 0; i < 10; i++)
    {
        sprintf(key, "%010d", i);
        sprintf(val, "test100000000000000%d", i);
        NuHashMapAdd(pMap, key, strlen(key), val, strlen(val)+1);
    }

	ShowAllHashData(pMap->pHash);

    for (i = 0; i < 10; i++)
    {
        sprintf(key, "%010d", 2);
        iRC = NuHashMapFind(pMap, key, 10, &IT);
		if(iRC < 0) printf("!!Find (%d)\n", iRC);
    }

	NuHashMapFree(pMap);
   

	return 0;
}
