#include "sys/time.h"
#include "NuFixMsg.h"

int main()
{
    struct timeval  start, end;
    struct timezone tz;
#if 0
    char szMsg[512] = "8=FIX.4.29=27335=849=YUTAFO56=BARCJPFO_234=32052=20111103-00:54:2837=34000937198=5A84211=BFN3DRD420I:1=101=992023117=E202020=0150=239=255=MXF167=FUT200=20111154=238=440=244=755859=032=431=0007558.00151=014=46=7558.00060=20111103-00:54:2858=101012-Gateway:Filled.10=165";
#else
/*
    char szMsg[512] = "8=FIX.4.39=29435=AB49=p.ord.newedge.156=p.ord.yuanta.134=481369=48452=20111008-04:06:5811=YT-XXX1A08B0001R453=1448=9921285447=D452=521=154=155=TXFJ1/K1555=2600=TXV1@XTAF608=FXXXXX610=201110624=2600=TXX1@XTAF608=FXXXXX610=201111624=160=20111008-04:06:5838=140=244=-1359=05047=294810=235";
*/
#endif
/*
    char szMsg[512] = "8=FIX.4.39=17535=D49=IVAN156=YUTAFOTEST34=8552=20111230-01:26:4011=20111230:05453=2448=9921285447=D452=2448=123447=2452=521=118=055=TXFA222=10054=138=40=244=700058=cc=Discretion|ac=629012|lc=TPE60=20111230-01:26:4059=020082=IVAN210=235";
*/
    char szMsg[512] = "8=FIX.4.39=17535=D49=IVAN156=YUTAFOTEST34=8552=20111230-01:26:4011=20111230:05453=3448=9921285447=D452=2448=123447=2452=521=118=055=TXFA222=10054=138=40=244=700058=cc=Discretion|ac=629012|lc=TPE60=20111230-01:26:4059=020082=IVAN210=235";

    char        *pVal = NULL;
    NuFixMsg_t *pFixMsg = NULL;
/*
    NuProcBindCore(0);
    */
    NuFixMsgNew(&pFixMsg);

    if(NuFixMsgTemplateAddHeader(pFixMsg, 35, 0) < 0)
        printf("TemplateAdd35!!!\n");

    NuFixMsgTemplateAddHeader(pFixMsg, 34, 10);
    NuFixMsgTemplateAddHeader(pFixMsg, 49, 10);
    NuFixMsgTemplateAddHeader(pFixMsg, 52, 10);
    NuFixMsgTemplateAddHeader(pFixMsg, 56, 20);
    NuFixMsgTemplateAddHeader(pFixMsg, 115, 20);
    NuFixMsgTemplateAddHeader(pFixMsg, 116, 20);
    
    NuFixMsgTemplateAddBody(pFixMsg, 11, 10);
    NuFixMsgTemplateAddBody(pFixMsg, 17, 10);
    NuFixMsgTemplateAddBody(pFixMsg, 18, 10);
    NuFixMsgTemplateAddBody(pFixMsg, 20, 10);
    NuFixMsgTemplateAddBody(pFixMsg, 22, 10);
    NuFixMsgTemplateAddBody(pFixMsg, 31, 10);
    NuFixMsgTemplateAddBody(pFixMsg, 32, 10);
    NuFixMsgTemplateAddBody(pFixMsg, 37, 10);
    NuFixMsgTemplateAddBody(pFixMsg, 38, 10);
    NuFixMsgTemplateAddBody(pFixMsg, 40, 10);
    NuFixMsgTemplateAddBody(pFixMsg, 41, 10);
    NuFixMsgTemplateAddBody(pFixMsg, 44, 10);
    NuFixMsgTemplateAddBody(pFixMsg, 54, 10);
    NuFixMsgTemplateAddBody(pFixMsg, 55, 10);
    NuFixMsgTemplateAddBody(pFixMsg, 58, 64);
    NuFixMsgTemplateAddBody(pFixMsg, 59, 10);
    NuFixMsgTemplateAddBody(pFixMsg, 60, 10);
    NuFixMsgTemplateAddBody(pFixMsg, 20082, 16);

printf("!!\n");
    NuFixMsgTemplateAddRepeatingGroup(pFixMsg, NuFixMsgKindBody, 453, 3, 448, 5, 447, 5, 452, 5);

printf("=================================================\n");
printf("%s\n", szMsg);
printf("-------------------------------------------------\n");
NuFixMsgParse(pFixMsg, szMsg);
printf("!!!\n");
NuFixMsgGenHB(pFixMsg);
printf("%s\n", NuFixMsgTakeOutMsg(pFixMsg));
printf("=================================================\n");

    NuFixMsgParse(pFixMsg, szMsg);
    pVal = NuFixMsgGetVal(NuFixMsgFieldGet(pFixMsg, 49));
    if(pVal != NULL)
        printf("49[%s]\n", pVal);
    pVal = NuFixMsgGetVal(NuFixMsgFieldGet(pFixMsg, 20082));
    if(pVal != NULL)
        printf("20082[%s]\n", pVal);

    pVal = NuFixMsgGetVal(NuFixMsgFieldGet(pFixMsg, 58));
    if(pVal != NULL)
        printf("58[%s]\n", pVal);

    pVal = NuFixMsgGetVal(NuFixMsgFieldGet(pFixMsg, 59));
    if(pVal != NULL)
        printf("Tag59[%s]\n", pVal);

    pVal = NuFixMsgGetVal(NuFixMsgFieldGet(pFixMsg, 38));
    if(pVal != NULL)
        printf("Tag38[%s]\n", pVal);

    pVal = NuFixMsgGetVal(NuFixMsgFieldGet(pFixMsg, 40));
    if(pVal != NULL)
        printf("Tag40[%s]\n", pVal);

    pVal = NuFixMsgGetVal(NuFixMsgFieldGet(pFixMsg, 453));
    if(pVal != NULL)
        printf("Tag453[%s]\n", pVal);

    pVal = NuFixMsgGetVal(NuFixMsgGroupGet(pFixMsg, 448, 0));
    if(pVal != NULL)
        printf("Tag448-1[%s]\n", pVal);

    pVal = NuFixMsgGetVal(NuFixMsgGroupGet(pFixMsg, 448, 1));
    if(pVal != NULL)
        printf("Tag448-2[%s]\n", pVal);

    NuFixMsgGroupAssign(pFixMsg, 448, 1, "lalala", 6);
    NuFixMsgGen(pFixMsg, NuFixMsgKindShell|NuFixMsgKindHeader|NuFixMsgKindBody);
    printf("Modified-Msg[%s]\n", NuFixMsgTakeOutMsg(pFixMsg));

    NuFixMsgGroupRemove(pFixMsg, 447, 0);
    NuFixMsgGen(pFixMsg, NuFixMsgKindShell|NuFixMsgKindHeader|NuFixMsgKindBody);
    printf("Modified-Msg2[%s]\n", NuFixMsgTakeOutMsg(pFixMsg));
        
    NuFixMsgClear(pFixMsg);
    NuFixMsgParse(pFixMsg, szMsg);

#if 1
    gettimeofday(&start, &tz);
    NuFixMsgParse(pFixMsg, szMsg);
    gettimeofday(&end, &tz);

    NuFixMsgClear(pFixMsg);

    printf("Parse)end - start = [%ld]\n", end.tv_usec - start.tv_usec);
#endif

#if 1
    NuFixMsgParse(pFixMsg, szMsg);

    gettimeofday(&start, &tz);
    pVal = NuFixMsgGetVal(NuFixMsgFieldGet(pFixMsg, 55));
    gettimeofday(&end, &tz);

    printf("GetValue)end - start = [%ld]\n", end.tv_usec - start.tv_usec);
#endif

#if 1
    gettimeofday(&start, &tz);
    NuFixMsgGen(pFixMsg, NuFixMsgKindShell|NuFixMsgKindHeader|NuFixMsgKindBody);
    gettimeofday(&end, &tz);

    printf("Gen)end - start = [%ld]\n", end.tv_usec - start.tv_usec);

#endif
    pVal = NuFixMsgGetVal(NuFixMsgFieldGet(pFixMsg, 38));
    if(pVal != NULL)
        printf("38[%s]\n", pVal);

    pVal = NuFixMsgGetVal(NuFixMsgGroupGet(pFixMsg, 448, 0));
    if(pVal != NULL)
        printf("448-1[%s]\n", pVal);

    pVal = NuFixMsgGetVal(NuFixMsgGroupGet(pFixMsg, 448, 1));
    if(pVal != NULL)
        printf("448-2[%s]\n", pVal);

    pVal = NuFixMsgGetVal(NuFixMsgFieldGet(pFixMsg, 8));
    if(pVal != NULL)
        printf("8[%s]\n", pVal);
        
    NuFixMsgGen(pFixMsg, NuFixMsgKindShell|NuFixMsgKindHeader|NuFixMsgKindBody);
    printf("Msg[%s]\n", NuFixMsgTakeOutMsg(pFixMsg));
    printf("msg[%s]\n", szMsg);

    NuFixMsgFree(pFixMsg);
    
    return 0;
}
