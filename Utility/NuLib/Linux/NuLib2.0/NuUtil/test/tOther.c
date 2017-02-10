#include "NuUtil.h"
#include "NuTime.h"
#include "NuStr.h"
#include "NuFileStream.h"
#define	REC_NUM	1000000
#if 1
int main()
{
	int             iRC = 0;
	NuFileStream_t  *pObj = NULL;
//	char             szTmp[10+1] = {0};
//	size_t           readn = 0;
	char            *pTmp = "Write Stream test.";

	iRC = NuFStreamOpen(&pObj, "./test.txt", "a+");
	printf("open file %d\n", iRC);
/*
	while((readn = NuFStreamReadN(pObj, szTmp, 10)) != 0)
	{
		printf("[%ld]%s\n", readn, szTmp);
		readn = 0;
		
	}
*/	
	NuFStreamWriteN(pObj, "test\n", 5);
	NuFStreamWriteLineN(pObj, pTmp, strlen(pTmp));
	NuFStreamWriteLineN(pObj, pTmp, 7);
	NuFStreamPrintf(pObj, "T[%s]<%s>\n", pTmp, "test");
	NuFStreamFlush(pObj);

	NuFStreamClose(pObj);
    return 0;
}
#else
int main()
{
	char   szTmp[1024] = {0};
	NuStr_t *pStr  = NULL;

	NuStrNew(&pStr);

	NuStrCat(pStr, "   TEST   ");
	printf("[%s]\n", NuStrGet(pStr));
	NuStrRTrim(pStr);
	printf("[%s]\n", NuStrGet(pStr));
	NuStrLTrim(pStr);
	printf("[%s]\n", NuStrGet(pStr));

    NuStrAppendPrintf(pStr, "%d=%s>%c", 123, "AAA", 'a');
	printf("[%s]\n", NuStrGet(pStr));

	NuStrPrintf(pStr, "1234|56789|||10|");
	printf("original1[%s]\n", NuStrGet(pStr));
	NuStrReplaceChr(pStr, '|', '#');
	printf("         [%s]\n", NuStrGet(pStr));

	NuStrPrintf(pStr, "1234|56789|||10|");
	printf("original2[%s]\n", NuStrGet(pStr));
	NuStrReplaceRangeChr(pStr, '|', '#', 5, 6);
	printf("         [%s]\n", NuStrGet(pStr));

	NuStrFree(pStr);

	strcpy(szTmp, "1234|56789|||10|");
	printf("original [%s]\n", szTmp);
	NuReplaceChr(szTmp, '=', '#');
	printf("         [%s]\n", szTmp);
#if 0
	NuDateTime_t *pDateTime = NULL;
	char  szTmp[30] = {0};

	NuDateTimeNew(&pDateTime, NUTRUE);
	NuDateTimeSet(pDateTime);
	NuDateTimeGet1(pDateTime, szTmp);
	printf("datetime[%s]\n", szTmp);

	NuDateTimeNew(&pDateTime, NUTRUE);
	NuDateTimeSet(pDateTime);
	NuDateTimeGet4(pDateTime, szTmp);
	printf("datetime[%s]\n", szTmp);

	NuDateTimeInit(pDateTime, 0, NUFALSE);
	NuDateTimeSet(pDateTime);
	NuDateTimeGet2(pDateTime, szTmp);
	printf("datetime[%s] gmt\n", szTmp);

	NuDateTimeInit(pDateTime, 3, NUFALSE);
	NuDateTimeSet(pDateTime);
	NuDateTimeGet3(pDateTime, szTmp);
	printf("datetime[%s] gmt\n", szTmp);

	NuDateTimeInit(pDateTime, 0, NUTRUE);
	NuDateTimeSet3(pDateTime, 2011, 10, 13, 14, 40, 28, 111,222);
	NuDateTimeGet3(pDateTime, szTmp);
	printf("datetime[%s] gmt\n", szTmp);

	NuDateTimeFree(pDateTime);

	strcpy(szTmp, "000MMMoOO");
	printf("[%s]\n", szTmp);
	NuRTrimChr(szTmp, 'O');
	printf("[%s]\n", szTmp);

	strcpy(szTmp, "   MMMoOO");
	printf("[%s]\n", szTmp);
	NuLTrimChr(szTmp, ' ');
	printf("[%s]\n", szTmp);

	int len = 0;
	int i = 0;
    clock_t start = 0, end = 0;
    float total = 0;

    start = clock();
    for (i = 0; i < REC_NUM; i++)
	{
		len = NuSprintf(szTmp, "123 %s [%d][%010d] %s <%ld> #%.3f#\n", "test", 456, 999, "long", 10L, 124.3);
		//printf("[%d]<%s>\n",len, szTmp);
	}
    end = clock();
    total = (float)(end - start)/CLOCKS_PER_SEC;
    printf("NuSprintf start - end = [%f]\n", total);

    start = clock();
    for (i = 0; i < REC_NUM; i++)
	{
		len = sprintf(szTmp, "123 %s [%d][%010d] %s <%ld> #%.3f#\n", "test", 456, 999, "long", 10L, 124.3);
		//printf("[%d]<%s>\n",len, szTmp);
	}
    end = clock();
    total = (float)(end - start)/CLOCKS_PER_SEC;
    printf("start - end = [%f]\n", total);
#endif

	return 0;
}
#endif
