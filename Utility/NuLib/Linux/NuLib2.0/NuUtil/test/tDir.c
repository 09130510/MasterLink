#include "NuFile.h"

int main(int argc, char **argv)
{
	int iRC = 0;
	NuDirList_t Lst;

	if ( (iRC = NuDirListInit(&Lst, "/home/oms/Ivan")) < 0)
		return -1;

	do{
		printf("[%d][%s]\n", NuDirListCnt(&Lst), NuDirListGetName(&Lst));
	}while (NuDirListNext(&Lst) == NU_OK);

	NuDirListClear(&Lst);
	return 0;
}
