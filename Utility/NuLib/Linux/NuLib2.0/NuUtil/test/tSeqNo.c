#include "NuTime.h"
#include "NuSeqNo.h"

#define SEQNO_LEN  10
#define TEST_CNT   10 
/* ********************************************
 * Test String SeqNo
 * ********************************************/
void SeqNoStrTest(char *Path, char *Name)
{
	char szSeqNo[SEQNO_LEN + 1] = {0};
	NuSeqNo_t *pSeqNo = NULL;
	int i = 0;

	printf(" ==== SeqNo [String] Testing ====\n");
	NuSeqNoNew(&pSeqNo, &NuSeqNoType_String, SEQNO_LEN, Path, Name);

	for(i = 0; i < TEST_CNT; i++)
	{
		NuSeqNoPop(pSeqNo, (void *)szSeqNo);
		printf("[%s]\n", szSeqNo);

	}

	NuSeqNoFree(pSeqNo);
}
/* ********************************************
 * Test String SeqNo (Customize)
 * ********************************************/
typedef struct _decorate_t
{
	char prefix[4+1];
	int  prefix_len;
	char suffix[4+1];
	int  suffix_len;
} decorate_t;

void Decorate_Init(decorate_t *pde, char *pToday, char *pSuffix)
{
	char       *ptr;
	char        szToday[10+1];
	static char szMM[12+1] = "123456789ABC";
	static char szDD[31+1] = "123456789ABCDEFGHIJKLMNOPQRSTUV";

	strcpy(szToday, pToday);
	szToday[4] = szToday[7] = '\0';

	ptr = szToday + 2;
	memcpy(pde->prefix, ptr, 2);
	ptr += 3;
	pde->prefix[2] = szMM[atoi(ptr)];
	ptr += 3;
	pde->prefix[3] = szDD[atoi(ptr)];
	pde->prefix[4] = '\0';
	pde->prefix_len = 4;

	strcpy(pde->suffix, pSuffix);
	pde->suffix_len = strlen(pSuffix);
	return;
}

int OrderID_Init(NuSeqNo_t *pSeqNo, decorate_t *pde)
{
	char *pCurSeqNo = NULL;

	if (pSeqNo->iLength < (pde->suffix_len + pde->prefix_len))
		return NU_FAIL;

	pCurSeqNo = (char *)malloc(sizeof(char) * (pSeqNo->iLength + 1));

	NuGetCurNo(pSeqNo, pCurSeqNo);
	/* set current/max/min number */
	if (pCurSeqNo[pSeqNo->iLength - 1] != pde->suffix[0])
	{
		memset(pCurSeqNo, '0', pSeqNo->iLength);
		pCurSeqNo[pSeqNo->iLength] = '\0';
		memcpy(pCurSeqNo, pde->prefix, 4);
		pCurSeqNo[pSeqNo->iLength - 1] = pde->suffix[0];

		NuSetCurNo(pSeqNo, pCurSeqNo);
		NuSetMinNo(pSeqNo, pCurSeqNo);
	}
	else
	{
		memset(pCurSeqNo, '0', pSeqNo->iLength);
		pCurSeqNo[pSeqNo->iLength] = '\0';
		memcpy(pCurSeqNo, pde->prefix, 4);
		pCurSeqNo[pSeqNo->iLength - 1] = pde->suffix[0];

		NuSetMinNo(pSeqNo, pCurSeqNo);
	}

	memset(pCurSeqNo, 'z', pSeqNo->iLength);
	pCurSeqNo[pSeqNo->iLength] = '\0';
	memcpy(pCurSeqNo, pde->prefix, 4);
	pCurSeqNo[pSeqNo->iLength - 1] = pde->suffix[0];
	NuSetMaxNo(pSeqNo, pCurSeqNo);


//	printf("[%s][%s][%s]\n", (char *)(pSeqNo->pCurSeqNo), (char *)(pSeqNo->pMinSeqNo),(char *)(pSeqNo->pMaxSeqNo));
	/* ------------------------------------------ */
	free(pCurSeqNo);

	return 0;
}

int OrderID_NextFn(void *CurSeqNo, int iSeqNoLen, void *NewSeqNo, void *arg)
{
	short s_flg = 1;
	char *ptr = NULL;
	decorate_t *pde = (decorate_t *)arg;
	char *pCurSeqNo = (char *)CurSeqNo;
	char *pNewSeqNo = (char *)NewSeqNo;
	memcpy(pNewSeqNo, pCurSeqNo, iSeqNoLen);
	pNewSeqNo[iSeqNoLen] = '\0';

	ptr = pNewSeqNo + iSeqNoLen - 1 - pde->suffix_len;

	while(s_flg == 1)
	{
		if (*ptr == 122)
		{
			*ptr = 48;
			ptr--;
			continue;
		}
		else if (*ptr == 57)
			*ptr = 65;
		else if (*ptr == 90)
			*ptr = 97;
		else
			++(*ptr);
		s_flg = 0;
	}

	/* ptr maybe overwrite prefix */
	if ((ptr - pNewSeqNo) < pde->prefix_len)
		return -1;
	return 0;


}


void SeqNoCustStringTest(char *Path, char *Name)
{
	char szSeqNo[SEQNO_LEN + 1] = {0};
	NuSeqNo_t *pSeqNo = NULL;
	int i = 0;
	decorate_t OrderID_dec;

	printf(" ==== SeqNo [String] Testing ====\n");
	Decorate_Init(&OrderID_dec, "2014-04-18", "A");

	NuSeqNoNew2(&pSeqNo, &NuSeqNoType_String, SEQNO_LEN, Path, Name, 
	           &OrderID_NextFn, NULL, (void *)&OrderID_dec);

	OrderID_Init(pSeqNo, &OrderID_dec);
	for(i = 0; i < TEST_CNT; i++)
	{
		NuSeqNoPop(pSeqNo, (void *)szSeqNo);
		printf("[%s]\n", szSeqNo);

	}

	NuSeqNoFree(pSeqNo);
}

/* ********************************************
 * Test Int SeqNo
 * ********************************************/
void SeqNoIntTest(char *Path, char *Name)
{
	int iSeqNo = 0;
	NuSeqNo_t *pSeqNo = NULL;
	int i = 0;

	printf(" ==== SeqNo [Int] Testing ====\n");
	NuSeqNoNew(&pSeqNo, &NuSeqNoType_Int, sizeof(int), Path, Name);

	for(i = 0; i < TEST_CNT; i++)
	{
		NuSeqNoPop(pSeqNo, (void *)&iSeqNo);
		printf("[%d]\n", iSeqNo);

	}

	NuSeqNoFree(pSeqNo);
}
/* ********************************************
 * main           
 * ********************************************/
int main()
{
	char *pPath = "./data";

	SeqNoStrTest(pPath, "StrTest");
	SeqNoCustStringTest(pPath, "CustStrTest");
	SeqNoIntTest(pPath, "IntTest");
	return 0;
}
