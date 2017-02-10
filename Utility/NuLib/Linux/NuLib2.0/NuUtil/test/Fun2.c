#include <stdio.h>
#include <string.h>
#include "NuDLibMgr.h"
#include "NuFixMsg.h"

typedef struct _obj_t
{
	void *Data;
	void *Ptr;
	size_t Len;
}obj_t;

typedef int (*SoFn)(int, int);
void Fun2_Show(void *argu)
{
	printf("Fun2_Show\n");
}

void Fun2_Show2(void *argu)
{
    
	int x = 20, y = 200;
	printf("%d + %d = %d\n", x, y, x+y);
}

int add(int x, int y)
{
	return (2 * x) + y;
}

int FixHandle(obj_t *obj)
{
	char *pTag58 = "Tag 58 test !";
	NuFixMsg_t *pFixMsg = NULL;

	pFixMsg = (NuFixMsg_t *)(obj->Data);
	NuFixMsgFieldSet(pFixMsg, 58, pTag58, strlen(pTag58));

	printf("35=[%s] 122=[%s] 58=[%s]\n", NuFixMsgGetFieldStr(pFixMsg, 35),
	                                     NuFixMsgGetFieldStr(pFixMsg, 122),
	                                     NuFixMsgGetFieldStr(pFixMsg, 58));
/////////////////////////////////////////////////////////////
	obj->Ptr = realloc(obj->Ptr, 200);
	strcat(obj->Ptr, "|fun2AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQ");
	obj->Len = 200;
	return 0;
}
