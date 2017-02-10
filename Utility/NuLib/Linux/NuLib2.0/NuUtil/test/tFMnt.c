
#include "NuFMnt.h"

int main()
{
	int iRC = 0;
	NuFMnt_t *pFMnt = NULL;
	char *pFile = "test";
	int Quit = 1;

	if ((iRC = NuFMntNew(&pFMnt, pFile, 12)) < 0)
	{
		printf("new fail\n");
		return 0;
	}

	NuFMntSetCycle(pFMnt, 1, 0);

	while(NuFMntActive(pFMnt) == NuFMntFileNotExists)
		sleep(1);

	while(Quit)
	{
		iRC = NuFMntSelect(pFMnt);

		switch(iRC)
		{
			case NuFMntModify:
				printf("modify\n");
				break;
			case NuFMntTimeout:
				printf("timeout\n");
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
