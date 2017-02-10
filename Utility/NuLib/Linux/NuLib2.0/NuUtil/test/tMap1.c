#include "NuTime.h"
#include "NuHashMap.h"

#define	REC_NUM	1000000

int main()
{
	char key[12] = {0};
#if 1
	char val[1024] = "8=FIX.4.39=40835=849=YUTAFO56=BARCHKFOS34=78352=20110701-04:56:07198=5E23037=3400409011=BFF1HYR3P01:1453=1448=9900251447=D452=517=E8783150=039=022=10055=TXFG1/H1200=18991254=138=5040=244=-12759=032=031=0151=5014=06=0.00060=20110701-04:56:0777=O58=101001-Gateway:Order was accepted by exchange.555=2600=TXFG1603=100608=FXXXXX610=201107624=2600=TXFH1603=100608=FXXXXX610=201108624=110=223";
#else
    char val[6] = "test1";
#endif

    clock_t start = 0, end = 0;
    float total = 0;
    int i = 0;
	int iRC = 0;

	NuHashMap_t  *pMap = NULL;
    NuHashMapIterator_t IT;

	NuHashMapNew3(&pMap, REC_NUM, 12, 1025, NULL, NULL, NULL, NULL);

#if 0
	NuHashMapSetThreadSafe(pMap);
#endif
    start = clock();
    for (i = 0; i < REC_NUM; i++)
    {
        sprintf(key, "%010d", i);
        NuHashMapAdd(pMap, key, sizeof(key), val, sizeof(val));
    }
    end = clock();
    total = (float)(end - start)/CLOCKS_PER_SEC;
    printf("[%d] start - end = [%f]\n", NuHashMapGetNum(pMap), total);

    start = clock();
    for (i = 0; i < REC_NUM; i++)
    {
        sprintf(key, "%010d", 10000);
        NuHashMapItFind(pMap, key, sizeof(key), &IT);
//		printf("[%s]\n", (char*)NuHashMapItGetValue(&IT));

    }
    end = clock();
    total = (float)(end - start)/CLOCKS_PER_SEC;
    printf("[%d] start - end = [%f]\n", REC_NUM, total);

	NuHashMapFree(pMap);
   

	return 0;
}
