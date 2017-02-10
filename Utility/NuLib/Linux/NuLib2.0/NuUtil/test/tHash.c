#include "NuTime.h"
#include "NuBuffer.h"
#include "NuHashList.h"

typedef struct _data_t
{
	int Num;
	char Name[32+1];
} data_t;
#define dataSz  sizeof(data_t)
#define keySz   12 

void Usage()
{
	printf("----------------------------------\n");
	printf("NuHashList test                   \n");
	printf("0 : Show Hash information         \n");
	printf("1 : Add       2 : Rmv     3 : Upd \n");
	printf("4 : Query                         \n");
	printf("5 : Show All                      \n");
	printf("q : quit                          \n");
	printf("----------------------------------\n");
}

int main()
{
	int iRC = 0;
	char szCmd[1+1];
	int  iFlag = 1;
	char szBuf[32] = {0};

	char *pkey = NULL;
	data_t *pdata = NULL;
	void  *ptmp = NULL;

	NuHash_t   *pHash = NULL;
	NuHashItem_t *pHashItem = NULL;
	NuHashIterator_t HashIT;
	unsigned int hash_idx = 0;

	NuBuffer_t *pDataBuf = NULL;
	NuBuffer_t *pKeyBuf = NULL;


	if ( (iRC = NuBufferNew(&pDataBuf, dataSz, 64)) < 0 )
	{
		printf("data Buffer initial fail [%d]\n", iRC);
		return -1;
	}

	if ( (iRC = NuBufferNew(&pKeyBuf, keySz, 64)) < 0 )
	{
		printf("data Buffer initial fail [%d]\n", iRC);
		return -1;
	}

	if ( (iRC = NuHashNew(&pHash, 128)) < 0)
	{
		printf("create hash object fail [%d]\n", iRC);
		return -1;
	}

	Usage();

	while(iFlag)
	{
		hash_idx = 0;
		pHashItem = NULL;

		printf("==>");
		scanf("%s", szCmd);

		switch(szCmd[0])
		{
			case '0':
				printf("Hash item count : %d\n", pHash->num);
				break;
			case '1':
				printf("Input key=>");
				pkey = (char *)NuBufferGet(pKeyBuf);
				scanf("%s", pkey);

				pdata = (data_t *)NuBufferGet(pDataBuf);
				printf("Input val [Num]=>");
				scanf("%d", &(pdata->Num));
				printf("Input val [Name]=>");
				scanf("%s", pdata->Name);

				pHashItem = NuHashSearch(pHash, pkey, strlen(pkey)+1, &hash_idx);
				if (pHashItem == NULL)   /* not exists */
				{
					pHashItem = NuHashAdd(pHash, pkey, strlen(pkey)+1, pdata, dataSz, hash_idx);
					printf("Add key[%s] data[%d, %s] idx[%d]\n", 
							pkey, ((data_t *)NuHashGetValue(pHashItem))->Num, 
							      ((data_t *)NuHashGetValue(pHashItem))->Name, 
						    hash_idx);
				}				
				else
				{
					
					printf("key already exists. key[%s] data[%d, %s] idx[%d]\n", 
							pkey, ((data_t *)NuHashGetValue(pHashItem))->Num, 
							      ((data_t *)NuHashGetValue(pHashItem))->Name, 
						    hash_idx);
							      
				}

				break;
			case '2':
				printf("[Rmv] Input key=>");
				scanf("%s", szBuf);
				pHashItem = NuHashSearch(pHash, szBuf, strlen(szBuf)+1, &hash_idx);
				if (pHashItem == NULL)
					printf("key not exists. key[%s]\n", szBuf);
				else
				{
					/* return data object to data buffer */
					NuBufferPut(pDataBuf, NuHashGetValue(pHashItem));

					pkey = NuHashOneTimeRmvItem(pHash, pHashItem, hash_idx);
					/* return key object to key buffer */
					if (pkey != NULL)
						NuBufferPut(pKeyBuf, pkey);

					printf("remove key[%s]\n", szBuf);
				}
				
				break;
			case '3':
				printf("[Upd] Input key=>");
				scanf("%s", szBuf);
				pHashItem = NuHashSearch(pHash, szBuf, strlen(szBuf)+1, &hash_idx);

				if (pHashItem == NULL)
					printf("key not exists. key[%s]\n", szBuf);
				else
				{
					pdata = (data_t *)NuBufferGet(pDataBuf);
					printf("Input val [Num]=>");
					scanf("%d", &(pdata->Num));
					printf("Input val [Name]=>");
					scanf("%s", pdata->Name);

					ptmp = NuHashGetValue(pHashItem);
					NuHashItemSetData(pHashItem, pdata, dataSz);

					/* return original data object to buffer */
					NuBufferPut(pDataBuf, ptmp);

					printf("update complete!\n");
				
				}

				break;
			case '4':
				printf("[Qry] Input key=>");
				scanf("%s", szBuf);
				pHashItem = NuHashSearch(pHash, szBuf, strlen(szBuf)+1, &hash_idx);
				if (pHashItem == NULL)
				{
					printf("key not exists. key[%s]\n", szBuf);
				}
				else
				{

					printf("key[%s] data[%d, %s] idx[%d]\n", 
							pkey, ((data_t *)NuHashGetValue(pHashItem))->Num, 
							((data_t *)NuHashGetValue(pHashItem))->Name, 
							hash_idx);
				}

				break;
			case '5':
				if (pHash->num >= 0)
				{
					printf(" ----> item count : %d\n", pHash->num);

					NuHashItGetAll(pHash, &HashIT);

					do
					{
						pkey = (char *)NuHashItGetKey(&HashIT);
						pdata = (data_t *)NuHashItGetValue(&HashIT);

						printf("key[%s] value[%d, %s]\n", 
							pkey, pdata->Num, pdata->Name);
					} while(!NuHashItNext(&HashIT));
				}
				else
				{
					printf("no item !\n");
				}

				break;
			case 'q':
				iFlag = 0;
				break;
			default:
				Usage();
				break;
		}
	}
//EXIT:
	if (pHash != NULL)
		NuHashFree(pHash);

	if (pDataBuf != NULL)
		NuBufferFree(pDataBuf);

	if (pKeyBuf != NULL)
		NuBufferFree(pKeyBuf);
	
	return 0;
}

