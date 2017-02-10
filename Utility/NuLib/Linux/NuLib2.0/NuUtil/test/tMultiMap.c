
#include "NuHashMap.h"

void Usage()
{
	printf("----------------------------------\n");
	printf("NuMultiHashMap test                    \n");
	printf("0 : Show Map information          \n");
	printf("1 : Add       2 : Rmv     3 : Upd     \n");
	printf("4 : Free                          \n");
	printf("5 : Show information              \n");
	printf("6 : Get iterator                  \n");
	printf("G : Get All data                  \n");
	printf("p : it prev   n : it next         \n");
	printf("l : it left   r : it right        \n");
	printf("9 : it show                       \n");
	printf("A : it rmv                        \n");
	printf("B : it upd                        \n");
	printf("q : quit                          \n");
	printf("----------------------------------\n");
}

int main()
{
	int iRC = 0;
	char szCmd[1+1];
	int  iFlag = 1;

	char key[32] = {0};
	char val[64] = {0};

	NuHashMap_t  *pMap = NULL;
    NuHashMapIterator_t IT;

	NuHashMapNew3(&pMap, 10, 5, 5, NULL, NULL, NULL, NULL);
	NuHashMapSetThreadSafe(pMap);

	Usage();

	while(iFlag)
	{
		printf("==>");
		scanf("%s", szCmd);

		switch(szCmd[0])
		{
			case '0':
				printf("pMap anum[%u] num[%u]\n", NuHashMapGetNum(pMap), NuHashMapGetANum(pMap));
				break;
			case '1':
				printf("Input key=>");
				memset(&key, '\0', 32);
				scanf("%s", key);
				printf("Input val=>");
				memset(&val, '\0', 64);
				scanf("%s", val);
   
   				printf("key[%s] val[%s]\n", key, val);
				NuMultiHashMapAdd(pMap, key, strlen(key) + 1, val, strlen(val)+1);

				break;
			case '2':
				printf("Input key=>");
				memset(&key, '\0', 32);
				scanf("%s", key);
				NuMultiHashMapRmv(pMap, key, strlen(key) + 1);
				break;
			case '3':
				printf("Input key=>");
				memset(&key, '\0', 32);
				scanf("%s", key);
				printf("Input val=>");
				memset(&val, '\0', 64);
				scanf("%s", val);
				iRC = NuMultiHashMapUpd(pMap, key, strlen(key) +1, val, strlen(val)+1);
				printf("Upd RC[%d]\n", iRC);
				break;
			case '4':
				NuHashMapFree(pMap);
	            NuHashMapNew(&pMap, 10);
				break;
			case '5':
                ShowAllMapData(pMap);
				break;
			case '6':
				printf("Input key=>");
				memset(&key, '\0', 32);
				scanf("%s", key);
                iRC = NuHashMapItFind(pMap, key, strlen(key) + 1, &IT);
                printf("iRC[%d]\n", iRC);
				break;
			case 'n':
                iRC = NuHashMapItNext(&IT);
                printf("iRC[%d] next\n", iRC);
				break;
			case 'p':
			/*
                iRC = NuHashMapPrev(&IT);
                printf("iRC[%d] prev\n", iRC);
			*/
				break;
			case 'l':
                iRC = NuHashMapItLeft(&IT);
                printf("iRC[%d] left\n", iRC);
				break;
			case 'r':
                iRC = NuHashMapItRight(&IT);
                printf("iRC[%d] right\n", iRC);
				break;
			case '9':
                if(NuHashMapItGetValue(&IT) == NULL)
                {
                    printf("NULL\n");
                }
                else
                {
                    printf("Idx[%d][%s]\n", IT.HashIt.iIdx, (char *)NuHashMapItGetValue(&IT));
                }
				break;
			case 'A':
                NuHashMapItRmvByItem(&IT);
				break;
			case 'G':
                NuHashMapItGetAll(pMap, &IT);
				break;
			case 'B':
				printf("Input val=>");
				memset(&val, '\0', 64);
				scanf("%s", val);
                iRC = NuHashMapItSetValue(&IT, val, strlen(val)+1);
                printf("iRC[%d] upd\n", iRC);
				break;
			case 'q':
				iFlag = 0;
				NuHashMapFree(pMap);
				break;
			default:
				Usage();
				break;
		}
	}

	return 0;
}
