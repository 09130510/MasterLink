
#include "NuFixMsg.h"

int main()
{
	NuFixMsg_t *pFixMsg = NULL;
	char       *pMsg = "8=FIX.4.39=7735=549=HiFOCli56=YUTAFOTEST34=1252=20140327-02:13:3458=FIX Service Stop10=001";

	NuFixMsgNew(&pFixMsg);

	if(NuFixMsgTemplateAddHeader(pFixMsg, 35, 1) < 0)
		printf("TemplateAdd35!!!\n");

	NuFixMsgTemplateAddHeader(pFixMsg, 49, 10);
	NuFixMsgTemplateAddHeader(pFixMsg, 52, 10);
	NuFixMsgTemplateAddHeader(pFixMsg, 56, 20);
	NuFixMsgTemplateAddHeader(pFixMsg, 115, 20);
	NuFixMsgTemplateAddHeader(pFixMsg, 116, 20);
	
	NuFixMsgTemplateAddBody(pFixMsg, 11, 10);
	NuFixMsgTemplateAddBody(pFixMsg, 17, 10);
	NuFixMsgTemplateAddBody(pFixMsg, 20, 10);
	NuFixMsgTemplateAddBody(pFixMsg, 21, 10);
	NuFixMsgTemplateAddBody(pFixMsg, 31, 10);
	NuFixMsgTemplateAddBody(pFixMsg, 32, 10);
	NuFixMsgTemplateAddBody(pFixMsg, 37, 10);
	NuFixMsgTemplateAddBody(pFixMsg, 40, 10);
	NuFixMsgTemplateAddBody(pFixMsg, 41, 10);
	NuFixMsgTemplateAddBody(pFixMsg, 59, 10);

	if(NuFixMsgTemplateAddRepeatingGroup(pFixMsg, NuFixMsgKindBody, 453, 3, 448, 5, 447, 5, 452, 5) < 0)
		printf("Add Group Template!!\n");

	if(NuFixMsgTemplateAddRepeatingGroup(pFixMsg, NuFixMsgKindBody, 555, 8, 600, 10, 602, 10, 603, 1, 
	                                                   608, 7, 610, 6, 612, 10, 624, 1, 
													   637, 10) < 0)
		printf("Add 555-Group fail!!!\n");


	NuFixMsgGenMsg(pFixMsg);
	printf("[%s]\n", NuFixMsgTakeOutMsg(pFixMsg));

	NuFixMsgFieldAssign(pFixMsg, 453, "2", 1);
	NuFixMsgGroupSet(pFixMsg, 448, 2,  "test", strlen("test"));
	NuFixMsgGroupSet(pFixMsg, 448, 1,  "test1", strlen("test1"));
	NuFixMsgGenMsg(pFixMsg);
	printf("[%s]\n", NuFixMsgTakeOutMsg(pFixMsg));

#if 0
	if(NuFixMsgFieldAssign(pFixMsg, 35, "D", 1) < 0)
		printf("Assign35!!!\n");

	NuFixMsgFieldAssign(pFixMsg, 49, "Laphone", 7);
	NuFixMsgFieldAssign(pFixMsg, 52, "2012-02-14", 10);
	NuFixMsgFieldAssign(pFixMsg, 56, "YUANTA", 6);
	NuFixMsgFieldAssign(pFixMsg, 115, "Tag115", 6);
	NuFixMsgFieldAssign(pFixMsg, 116, "Tag116", 6);

	NuFixMsgFieldAssign(pFixMsg, 11, "1234567890", 10);
	NuFixMsgFieldAssign(pFixMsg, 17, "abcdefghijk", 11);
	NuFixMsgFieldAssign(pFixMsg, 20, "0", 1);
	NuFixMsgFieldAssign(pFixMsg, 21, "1", 1);
	NuFixMsgFieldAssign(pFixMsg, 31, "0", 1);
	NuFixMsgFieldAssign(pFixMsg, 32, "0", 1);
	NuFixMsgFieldSet(pFixMsg, 37, "1qaz2wsx3edc", 12);
	NuFixMsgFieldSet(pFixMsg, 40, "1", 1);
	NuFixMsgFieldSet(pFixMsg, 41, "8ik,7ujm6yhn", 12);
	NuFixMsgFieldAssign(pFixMsg, 59, "0", 1);
	NuFixMsgFieldAssign(pFixMsg, 453, "2", 1);
//	NuFixMsgGroupAdd(pFixMsg, 453);
	
	if(NuFixMsgGroupAssign(pFixMsg, 448, 1, "aaa", 3) < 0)
	{
		printf("448-0!!\n");
	}
	NuFixMsgGroupAssign(pFixMsg, 447, 1, "bbb", 3);
	NuFixMsgGroupAssign(pFixMsg, 452, 1, "ccc", 3);
	NuFixMsgGroupAssign(pFixMsg, 448, 0, "ddd", 3);
	NuFixMsgGroupAssign(pFixMsg, 447, 0, "eee", 3);
	NuFixMsgGroupAssign(pFixMsg, 452, 0, "fff", 3);


	NuFixMsgFieldAssign(pFixMsg, 555, "1", 1);
	NuFixMsgGroupAssign(pFixMsg, 600, 1, "ddd600", 6);
	NuFixMsgGroupAssign(pFixMsg, 624, 1, "eee624", 6);
	NuFixMsgGroupAssign(pFixMsg, 602, 1, "fff602", 6);

	NuFixMsgGenMsg(pFixMsg);
//	printf("11=[%s]\n", (char *)(NuFixMsgGet(pFixMsg, 11)->Value));

	printf("[%s]\n", NuFixMsgTakeOutMsg(pFixMsg));

	printf("----------------------------------------\n");
	NuFixMsgClear(pFixMsg);
	NuFixMsgParse(pFixMsg, pMsg);
	NuFixMsgFieldSet(pFixMsg, 49, "TAG49", 5);
	NuFixMsgFieldSet(pFixMsg, 56, "TAG56", 5);

	NuFixMsgGenMsg(pFixMsg);
	printf("[%s]\n", NuFixMsgTakeOutMsg(pFixMsg));


	NuFixMsgFree(pFixMsg);
#endif
	return 0;
}

