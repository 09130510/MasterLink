#include "NuRow.h"

int main()
{
	int  iRC = 0;
	NuRowHdr_t *pHdr;
	NuRow_t    *pRow;

	char szDate[8+1] = "20110825";
	char szOrdID[10+1] = "0012";
	char szSymbol[6+1] = "1301";

	NuRowHdrNew(&pHdr);

	NuRowHdrAddColDef(pHdr, "TradeDate", 8);
	NuRowHdrAddColDef(pHdr, "OrderID", 10);
	NuRowHdrAddColDef(pHdr, "Symbol", 6);

	printf("length = %d\n", pHdr->RowLength);

	pRow = (NuRow_t *)malloc(sizeof(NuRow_t));
	NuRowInit(pRow);

	pRow->pBackup = (char *)malloc(sizeof(char) * pHdr->RowLength);

	NuRowHdrAddRow(pHdr, pRow);

	printf("Row=%d\n", NuRowHdrRowCnt(pHdr));
#if 0
	iRC = NuRowDataSet(pRow, 0, "20110824", 8);
	iRC = NuRowDataSet(pRow, 1, "0000000123", 10);
	iRC = NuRowDataSet(pRow, 2, "1301", 4);
#else
	/*
	iRC = NuRowDataSet2(pRow, 9, 0, "20110825", 8, 1, "0012", 4, 2, "1301", 4);
	*/
	iRC = NuRowDataSet2(pRow, 9, 0, szDate, strlen(szDate), szOrdID, strlen(szOrdID), szSymbol, strlen(szSymbol));
	printf("[%s]\n", NuRowDataGet(pRow, 0, pRow->pBackup));
	printf("[%s]\n", NuRowDataGet(pRow, 1, pRow->pBackup));
	printf("[%s]\n", NuRowDataGet(pRow, 2, pRow->pBackup));
#endif

	NuRowHdrFree(pHdr);
	return 0;
}
