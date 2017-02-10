
#include "NuFile.h"

int main(int argc, char **argv)
{
	NuStr_t	*pStr = NULL;

	if(argc < 3)
	{
		printf("Usage: %s dir path\n", argv[0]);
		return 0;
	}

	NuStrNew(&pStr);
	NuPathCombine(pStr, argv[1], argv[2]);

	printf("Result[%s]\n", NuStrGet(pStr));

	NuStrFree(pStr);

	return 0;
}

