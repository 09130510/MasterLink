#include "NuTools.h"


int main()
{
	char szStr[] = "This|is|test|program|for|string|split";	
	char szStr2[] = "This|@is|@test|@program|@for|@string|@split";	
	NuStrVec_t StrVec;
	char cSep = '|';
	char szSep[2+1] = "|@";
	int i = 0;
	int iCnt = 0;

	NuSplitOpen(&StrVec);

	printf("orignal string : [%s]\n", szStr);
	printf("---- NuSplit --------\n");
	iCnt = NuSplit(szStr, cSep, &StrVec);
	for (i = 0; i < iCnt ; i++)
	{
		printf("%s ", NuSplitGetByIndex(&StrVec, i));
	}
	printf ("\n");
	printf("orignal string : [%s]\n", szStr);

	printf("---- NuSplitNoCpy ---\n");
	iCnt = NuSplitNoCpy(szStr, cSep, &StrVec);
	for (i = 0; i < iCnt ; i++)
	{
		printf("%s ", NuSplitGetByIndex(&StrVec, i));
	}
	printf ("\n");
	printf("orignal string : [%s]\n", szStr);

	printf("---- NuSplitByStr ---\n");
	iCnt = NuSplitByStr(szStr2, szSep, &StrVec);
	for (i = 0; i < iCnt ; i++)
	{
		printf("%s ", NuSplitGetByIndex(&StrVec, i));
	}
	printf ("\n");
	printf("orignal string : [%s]\n", szStr2);

	printf("---- NuSplitByStrNoCpy ---\n");
	iCnt = NuSplitByStrNoCpy(szStr2, szSep, &StrVec);
	for (i = 0; i < iCnt ; i++)
	{
		printf("%s ", NuSplitGetByIndex(&StrVec, i));
	}
	printf ("\n");
	printf("orignal string : [%s]\n", szStr2);

	NuSplitClose(&StrVec);
	return 0;
}
