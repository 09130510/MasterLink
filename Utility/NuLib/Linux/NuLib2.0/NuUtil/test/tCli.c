#include "NuTime.h"
#include "NuStr.h"
#include "NuThread.h"
#include "NuNetUtil.h"
#include "NuMMap.h"

#define TEST_PORT 56789
int main()
{
	int iRC = 0;
	SOCKET SockFD = 0;
	char szIP[15+1] = "127.0.0.1";
	char *p = NULL;

	NuMMap_t *pMMap = NULL;

	NuMMapNew(&pMMap, NULL, "", sizeof(szIP)+1, PROT_READ|PROT_WRITE, MAP_PRIVATE | MAP_ANONYMOUS);

	p = (char *)NuMMapGetAddr(pMMap);
	strcpy(p, szIP);

	iRC = NuTCPConnect(&SockFD, szIP, TEST_PORT);
	if (iRC < 0)
	{
		printf("connect fail [%d][%d]\n", iRC, errno);
		return -1;
	}

	NuSetTCPNoDelay(SockFD, 1);

	printf("connect [%d]\n", SockFD);


	while(1)
	{
		/*
		iRC = NuTCPSend(SockFD, szIP, sizeof(szIP));
		*/
		iRC = NuTCPSend(SockFD, p, 16);
		printf("iRC[%d][%s][%ld]\n", iRC, szIP, sizeof(szIP));
		sleep(1);
	}

	return 0;
}
