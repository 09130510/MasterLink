#include <stdio.h>
#include "NuFixMsg.h"

typedef struct _obj_t
{
	void *Data;
	void *Ptr;
	size_t Len;
}obj_t;

int add(int x, int y)
{
	return x + y;
}

int FixHandle(obj_t *obj)
{
	char *ptr = NULL;
	NuFixMsg_t *pFixMsg = NULL;


//	NuFixMsgNew((NuFixMsg_t **)(&(obj->Data)));
	NuFixMsgNew(&pFixMsg);
	obj->Data = (void *)pFixMsg;
//////////////////////////////////////////////////////////
	pFixMsg = (NuFixMsg_t *)obj->Data;

	NuFixMsgTemplateAddHeader(pFixMsg,   8, 10);
	NuFixMsgTemplateAddHeader(pFixMsg,   9, 4);
	NuFixMsgTemplateAddHeader(pFixMsg,  35, 32);
	NuFixMsgTemplateAddHeader(pFixMsg,  49, 16);
	NuFixMsgTemplateAddHeader(pFixMsg,  56, 16);
	NuFixMsgTemplateAddHeader(pFixMsg, 115, 16);
	NuFixMsgTemplateAddHeader(pFixMsg, 128, 16);
	NuFixMsgTemplateAddHeader(pFixMsg,  34, 10);
	NuFixMsgTemplateAddHeader(pFixMsg,  50, 16);
	NuFixMsgTemplateAddHeader(pFixMsg,  57, 16);
	NuFixMsgTemplateAddHeader(pFixMsg, 116, 16);
	NuFixMsgTemplateAddHeader(pFixMsg, 129, 16);
	NuFixMsgTemplateAddHeader(pFixMsg,  43, 1);
	NuFixMsgTemplateAddHeader(pFixMsg,  97, 1);
	NuFixMsgTemplateAddHeader(pFixMsg,  52, 21);
	NuFixMsgTemplateAddHeader(pFixMsg, 122, 21);

	NuFixMsgFieldSet(pFixMsg, 35, "D", 1);
	NuFixMsgFieldSet(pFixMsg, 122, "test", 4);

	NuFixMsgTemplateAddBody(pFixMsg, 112,  32);
	NuFixMsgTemplateAddBody(pFixMsg,   7,  10);
	NuFixMsgTemplateAddBody(pFixMsg,  16,  10);
	NuFixMsgTemplateAddBody(pFixMsg,  45,  10);
	NuFixMsgTemplateAddBody(pFixMsg, 371,  32);
	NuFixMsgTemplateAddBody(pFixMsg, 372,  32);
	NuFixMsgTemplateAddBody(pFixMsg, 123,   1);
	NuFixMsgTemplateAddBody(pFixMsg,  36,  10);
	NuFixMsgTemplateAddBody(pFixMsg,  58, 128);
	NuFixMsgTemplateAddBody(pFixMsg,  98,   1);
	NuFixMsgTemplateAddBody(pFixMsg, 108,   3);

	ptr = NuFixMsgGetFieldStr(pFixMsg, 122);

	printf("35=[%s] 122=[%s]\n", NuFixMsgGetFieldStr(pFixMsg, 35), ptr);
////////////////////////////////////////////////////////////
    obj->Ptr = (void *)malloc(sizeof(char) * 100);
	obj->Len = 10;
	strcpy((char *)(obj->Ptr), "test-fun1");
	return 0;
}
