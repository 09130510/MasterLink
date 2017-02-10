
#include "NuDBM.h"
#include <time.h>

#define TOTAL   1000000

enum _MyTableSchema
{
    MyTable_Col1 = 0,
    MyTable_Col2,
    MyTable_Col3,
    MyTable_Col4,
    MyTable_Col5,
    MyTable_Col6,
    MyTable_Col7,
    MyTable_Col8,
    MyTable_Col9,
    MyTable_Col10
};

int main(int argc, char **argv)
{
    int                 Cnt = 0;
    int                 RC = 0;
    char                col[1000] = "\0", *Col = NULL;
    NuTable_t           *Table = NULL;
    NuIndex_t           *pPKey = NULL;
    NuIndex_t           *pIndex = NULL;
    NuRecordSet_t       *pRS = NULL;
    NuDBMConnection_t   ConnId;

    clock_t             start = 0, end = 0;
    float               total = 0;

    RC = NuDBMNew();
    if(RC < 0)
    {
        printf("NuDBMNew!!\n");
        return 0;
    }

    Table = NuDBMAddTable("MyTable", TOTAL, 10, "Col1", 15, "Col2", 12, "Col3", 10, "Col4", 10, "Col5", 20, "Col6", 10, "Col7", 20, "Col8", 10, "Col9", 10, "Col10", 10);
//    Table = NuDBMAddTable("MyTable", TOTAL, 10, 15, 12, 10, 10, 20, 10, 20, 10, 10, 10);

    pPKey = NuTableAddIndex(Table, NuIndexKind_PKey, 2, 0, 6);
    if(!pPKey)
    {
        printf("NuTableAddPKey!!\n");
        return 0;
    }

    pIndex = NuTableAddIndex(Table, NuIndexKind_Index, 1, 7);
    if(!pIndex)
    {
        printf("NuTableAddIndex!!\n");
        return 0;
    }

    NuDBMOpen(".");
    printf("Open!!!\n");

    ConnId = NuDBMConnect();
    printf("ConnectId[%d]\n", (int)ConnId);

    pRS = NuRSGet(ConnId, Table);

    start = clock();

    for(Cnt = 0; Cnt < TOTAL; Cnt ++)
    {
        NuRSSetTran(pRS);
        sprintf(col, "%08d", Cnt);
//        NuTableInsert(pTable, col, col, "abcdefgh", "12345678", col, col, "Col6", "9876543210", col, col);
        NuRSInsert(pRS);
        NuRSDataSetByRow(pRS, col, col, "abcdefgh", "12345678", col, col, "Col6", "9876543210", col, col);
        NuRSCommit(pRS);
    }
 
    end = clock();

    total = (float)(end - start)/CLOCKS_PER_SEC;
    printf("Insert[%d] end - start = [%f]\n", TOTAL, total);

    start = clock();

    for(Cnt = 0; Cnt < TOTAL; Cnt ++)
    {
        sprintf(col, "%08d", Cnt);
        NuRSSelectByIndex(pRS, pPKey, col, "Col6");
    }

    end = clock();

    total = (float)(end - start)/CLOCKS_PER_SEC;
    printf("Select[%d] end - start = [%f]\n", TOTAL, total);

    start = clock();

    for(Cnt = 0; Cnt < TOTAL; Cnt ++)
    {
        sprintf(col, "%08d", Cnt);

        NuRSSetTran(pRS);
        NuRSSelectByIndex(pRS, pPKey, col, "Col6");
//      NuRSDataSetByRow(pRS, NULL, NULL, "======", NULL, NULL, NULL, NULL, NULL, NULL, NULL);
        NuRSDataSet(pRS, 2, "======", 6);
        NuRSCommit(pRS);
//      printf("commit[%d]\n", NuRSCommit(pRS));
    }

    end = clock();

    total = (float)(end - start)/CLOCKS_PER_SEC;
    printf("Update Non-index[%d] end - start = [%f]\n", TOTAL, total);

    RC = NuRSSelectByIndex(pRS, pPKey, "00012345", "Col6");
    if(RC < 0)
    {
        printf("NuRSSelectByIndex(%d)!!\n", RC);
        return 0;
    }

    Col = NuRSDataGet(pRS, 2);
    printf("Select Result[%s]\n", Col);


    start = clock();

    for(Cnt = 0; Cnt < TOTAL; Cnt ++)
    {
        sprintf(col, "%08d", Cnt);
        NuRSSetTran(pRS);
        NuRSSelectByIndex(pRS, pPKey, col, "Col6");
//      NuRSDataSetByRow(pRS, NULL, NULL, NULL, NULL, NULL, NULL, "Col7", NULL, NULL, NULL);
        NuRSDataSet(pRS, 6, "Col7", 4);
        NuRSCommit(pRS);
    }

    end = clock();

    total = (float)(end - start)/CLOCKS_PER_SEC;
    printf("Update index[%d] end - start = [%f]\n", TOTAL, total);


    RC = NuRSSelectByIndex(pRS, pPKey, "00012345", "Col7");
    if(RC < 0)
    {
        printf("NuRSSelectByIndex(%d)!!\n", RC);
        return 0;
    }

    Col = NuRSDataGet(pRS, 6);
    printf("Select Result6[%s]\n", Col);
/*
    start = clock();

    for(Cnt = 0; Cnt < TOTAL; Cnt ++)
    {
        sprintf(col, "%08d", Cnt);
        NuRSSelectByIndex(pRS, 0, col, "Col7");
        NuTableUpdate(pRS, 1, 6, "Col8");
    }


    end = clock();

    total = (float)(end - start)/CLOCKS_PER_SEC;
    printf("NuUpdate index[%d] end - start = [%f]\n", TOTAL, total);

    RC = NuRSSelectByIndex(pRS, 0, "00012345", "Col8");
    if(RC < 0)
    {
        printf("NuRSSelectByIndex8(%d)!!\n", RC);
        return 0;
    }

    Col = NuRSDataGet(pRS, 6);
    printf("Select Result8[%s]\n", Col);

*/
    start = clock();

    NuRSSetTran(pRS);
    NuRSSelectByIndex(pRS, pIndex, "9876543210");

    for(Cnt = 0; Cnt < TOTAL; Cnt ++)
    {
        NuRSDataSet(pRS, 7, "0123456789", 10);
        NuRSNextData(pRS);
    }

    NuRSCommit(pRS);
    end = clock();

    total = (float)(end - start)/CLOCKS_PER_SEC;
    printf("Update index in one commit[%d] end - start = [%f]\n", TOTAL, total);


    RC = NuRSSelectByIndex(pRS, pPKey, "00012345", "Col7");
    if(RC < 0)
    {
        printf("NuRSSelectByIndex(%d)!!\n", RC);
        return 0;
    }

    Col = NuRSDataGet(pRS, 7);
    printf("Select Result7[%s]\n", Col);

    NuDBMFree();

    return 1;
}

