
#include "NuLog.h"

int main(int argc, char **argv)
{
	int i = 0, iCnt = 100;
	NuLog_t *pLog = NULL;
	char szData[128] = {0};

	memset(&szData, 'A', 127);
	szData[126] = '\n';
	szData[127] = '\0';

	if (argc > 1)
	{
		iCnt = atoi(argv[1]);
	}

//	NuLogOpen3(&pLog, "./data", "inotify", NUTRUE, 1 /* MB */, enFileStream);
	NuLogOpen3(&pLog, "./data", "inotify", NUTRUE, 1 /* MB */, enMMapStream);

	for (i = 0; i < iCnt; i++)
	{
		NuLog(pLog, szData);
		NuLogFlush(pLog);
		sleep(1);
	}
	
	NuLogClose(pLog);

	return 0;
}
