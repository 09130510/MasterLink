#include "NuUtil.h"
#include "NuTime.h"
#include "NuStr.h"
#include "NuFile.h"
#include "NuLog.h"

typedef struct st
{
	int i1;
	char szBuf[120+1];
	int i2;
}st;

int main()
{
	NuLog_t  *pLog = NULL;
//	char      szTmp[10+1] = {0};
//	st        obj;
//	st       *pobj = &obj;
	int      i = 0;

#if 0	
	NuLogOpen3(&pLog, "./data", "testlog.mmap", NUTRUE, 5 /* MB */, enMMapStream);

	for (i = 0; i < 5000; i++)
	{
		NuLog(pLog, "test1234\n");
		NuErr(pLog, "ERR-test1234\n");
		memset(pobj, 0, sizeof(st));
		pobj->i1 = 100;
		pobj->i2 = 123;
		memcpy(pobj->szBuf, "123456", 6);
		NuWriteLineN(pLog, pobj, sizeof(st));

		memset(pobj, 0, sizeof(st));
		pobj->i1 = 41;
		pobj->i2 = 32;
		strcpy(pobj->szBuf, "4132");
		NuWriteLineN(pLog, pobj, sizeof(st));

		memset(pobj, 0, sizeof(st));
		pobj->i1 = 99;
		pobj->i2 = 123;
		strcpy(pobj->szBuf, "99123");
		NuWriteLineN(pLog, pobj, sizeof(st));
	}

	NuLogClose(pLog);
#else
//	NuLogOpen3(&pLog, "./data", "Broadcast", NUTRUE, 1 /* MB */, enFileStream);
	NuLogOpen3(&pLog, "./data", "Broadcast", NUTRUE, 1 /* MB */, enMMapStream);
/*
	for (i = 0; i < 300000; i++)
	{
		NuLog(pLog, "aaaaaaaatest1234111111111111111111\n");
	}
*/
	for (i = 0; i < 30; i++)
	{
		NuLog(pLog, "ivan\n");
	}
	
	NuLogClose(pLog);
#endif
    return 0;

}
