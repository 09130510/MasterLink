#include "NuStr.h"


void Usage()
{
	printf("----------------------------------\n");
	printf("NuStr test                        \n");
	printf("1 : NuStrCat                      \n");
	printf("2 : NuStrPrintf                   \n");
	printf("3 : NuStrAppendPrintf             \n");
	printf("4 : NuStrClear                    \n");
	printf("5 : Show information              \n");
	printf("q : quit                          \n");
	printf("----------------------------------\n");
}

int main()
{
	char szCmd[1+1];
	int  iFlag = 1;

	char Buf[128] = {0};

	NuStr_t *pstr = NULL;

	NuStrNew(&pstr);

	Usage();

	while(iFlag)
	{
		printf("==>");
		scanf("%s", szCmd);

		switch(szCmd[0])
		{
			case '1':
				printf("Input=>");
				scanf("%s", Buf);
				NuStrCat(pstr, Buf);
				break;
			case '2':
				printf("Input=>");
				scanf("%s", Buf);
				NuStrPrintf(pstr, Buf);
				break;
			case '3':
				printf("Input=>");
				scanf("%s", Buf);
				NuStrAppendPrintf(pstr, Buf);
				break;
			case '4':
				NuStrClear(pstr);
				break;
			case '5':
				printf("string  size[%d]\n", NuStrSize(pstr));
				printf("string asize[%d]\n", pstr->asize);
				printf("string      [%s]\n", NuStrGet(pstr));
				break;
			case 'q':
				iFlag = 0;
				break;
			default:
				Usage();
				break;
		}
	}



	NuStrFree(pstr);
	return 0;
}
