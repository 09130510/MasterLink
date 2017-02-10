#include "NuTime.h"
#include "NuHashMap.h"

void PrintTime()
{
    char Buf[24] = {0};
    GetTime(Buf);
    printf("[%s]\n", Buf);
}

void Usage()
{
	printf("----------------------------------\n");
	printf("NuHashMap test                    \n");
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

	NuHashMapNew(&pMap, 10);
	NuHashMapSetThreadSafe(pMap);

	Usage();

	while(iFlag)
	{
		printf("==>");
		scanf("%s", szCmd);

		switch(szCmd[0])
		{
			case '0':
				printf("pMap anum[%u] num[%u]\n", NuHashMapGetANum(pMap), NuHashMapGetNum(pMap));
				break;
			case '1':
				printf("Input key=>");
				scanf("%s", key);
				printf("Input val=>");
				scanf("%s", val);
                
				iRC = NuHashMapAdd(pMap, key, strlen(key), val, strlen(val)+1);
				printf("Add RC[%d]\n", iRC);

				break;
			case '2':
				printf("Input key=>");
				scanf("%s", key);
				NuHashMapRmv(pMap, key, strlen(key));
				break;
			case '3':
				printf("Input key=>");
				scanf("%s", key);
				printf("Input val=>");
				scanf("%s", val);
				iRC = NuHashMapUpd(pMap, key, strlen(key), val, strlen(val)+1);
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
				scanf("%s", key);
                iRC = NuHashMapFind(pMap, key, strlen(key), &IT);
                printf("iRC[%d]\n", iRC);
				break;
			case 'n':
                iRC = NuHashMapNext(&IT);
                printf("iRC[%d] next\n", iRC);
				break;
			case 'p':
                iRC = NuHashMapPrev(&IT);
                printf("iRC[%d] prev\n", iRC);
				break;
			case 'l':
				break;
			case 'r':
				break;
			case '9':
                if (NuHashMapGetData(&IT) == NULL)
                    printf("NULL\n");
                else
                    printf("Idx[%d][%s]\n", IT.HashIt.iIdx, (char *)NuHashMapGetData(&IT));
				break;
			case 'A':
                NuHashMapRmvByItem(&IT);
				break;
			case 'G':
                NuHashMapGetAll(pMap, &IT);
				break;
			case 'B':
				printf("Input val=>");
				scanf("%s", val);
                iRC = NuHashMapSetData(&IT, val, strlen(val)+1);
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
