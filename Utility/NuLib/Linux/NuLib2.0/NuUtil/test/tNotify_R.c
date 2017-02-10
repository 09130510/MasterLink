
#include "NuFMnt.h"
#include "NuTime.h"

int main(int argc, char **argv)
{
	int iRC = 0;
	NuFMnt_t *pFMnt = NULL;
	char *pFile = NULL;
	int Quit = 1;
	char szTime[22] = {0};

	if (argc < 2)
	{
		printf("%s [file]\n\n", argv[0]);
	}
	pFile = argv[1];

	if ((iRC = NuFMntNew(&pFMnt, pFile, 12)) < 0)
	{
		printf("new fail\n");
		return 0;
	}

	NuFMntSetCycle(pFMnt, 10, 0);

	while(NuFMntActive(pFMnt) == NuFMntFileNotExists)
		sleep(1);

	while(Quit)
	{
		iRC = NuFMntSelect(pFMnt);

		NuGetTime(szTime);
		switch(iRC)
		{
			case NuFMntModify:
				printf("%s modify\n", szTime);
				break;
			case NuFMntTimeout:
//				printf("timeout\n");
				break;
			case NuFMntAttribChange:
				printf("attrib\n");
				break;
			case NuFMntDelete:
				printf("delete\n");
				Quit = 0;
				break;
			case NuFMntError:
				printf("error\n");
				Quit = 0;
				break;
			default:
				printf("other\n");
				break;
		}
	}

	NuFMntFree(pFMnt);

	return 0;
}
