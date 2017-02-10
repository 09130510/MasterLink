#include "NuTime.h"
#include "NuStr.h"
#include "NuThread.h"
#include "NuNetUtil.h"

#define TEST_PORT 56789

typedef struct _client_t
{
	int *Quit;
	SOCKET clientFD;
} client_t;

NUTHD_FUNC HandlConnect(void *arg)
{
	int  iRC = 0;
	client_t *pCli = (client_t *)arg;
	char szTmp[1024+1] = {0};
	int  iLen = 16;
	NuThread_t TID = NuThdSelf();

	while(!(*(pCli->Quit)))
	{
		memset(szTmp, 0, iLen);
//		iRC = NuTCPRecvOneTime(pCli->clientFD, szTmp, iLen, 5000000);
		iRC = NuTCPRecv(pCli->clientFD, szTmp, iLen, 5000000);
		if (iRC == NU_SELECT_FAIL)
		{
			printf("select fail[%ld][%d][%d][%s]\n", TID, iRC, errno, szTmp);
			goto EXIT;
		}
		else if (iRC == NU_TIMEOUT)
		{
			printf("timeout[%ld]iRC[%d][%d][%d]\n", TID, iRC, errno, pCli->clientFD);
		}
		else if (iRC == NU_RECV_FAIL)
		{
			printf("recv fail[%ld]iRC[%d][%s]\n", TID, iRC, szTmp);
			goto EXIT;
		}
		else
		{
			printf("else[%ld]iRC[%d][%s]\n", TID, iRC, szTmp);
		}
	}
EXIT:
	NuThdReturn();
	return 0;
}

int main()
{
	int iRC = 0;
	SOCKET SockFD = 0;
	SOCKET CliSockFD = 0;
	char szIP[15+1] = {0};
	client_t *pCli = NULL;
	int Quit = 0;
	NuThread_t TID;
	iRC = NuInitTCPServer(&SockFD, TEST_PORT);

	if (iRC < 0)
		return -1;

	NuSetNonBlockMode(SockFD, 1);
	while(1)
	{
		iRC = NuAcceptConnect(SockFD, &CliSockFD, szIP);
		if (iRC < 0)
		{
			if (iRC == NU_TIMEOUT)
			{
//				printf("timeout[%d]\n", iRC);
				continue;
			}
			printf("fail connect[%d]\n", iRC);
			continue;
		}

		printf("connect[%d][%s][%d]\n", iRC, szIP, CliSockFD);
		pCli = NULL;
		pCli = (client_t *)malloc(sizeof(client_t));
		pCli->Quit = &Quit;
		pCli->clientFD = CliSockFD;
		/*
		NuTCPClose(CliSockFD);
		*/
		NuThdCreate(HandlConnect, (void *)pCli, &TID);
		printf("create thread[%ld]\n", TID);

	}

	return 0;
}
