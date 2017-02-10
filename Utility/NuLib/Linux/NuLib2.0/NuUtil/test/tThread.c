#include <stdio.h>
#include "NuList.h"
#include "NuThread.h"
#include "NuMutex.h"
#include "NuEvent.h"

typedef struct thdArg_t
{
    NuThread_t TID;
    NuEvent_t *Event;
    int        PrintFlag;
    int        EventFlag;
} thdArg_t;

void Usage()
{
	printf("----------------------------------\n");
	printf("NuStr test                        \n");
	printf("1 : Create Thread                 \n");
	printf("2 : GetHead PID                   \n");
	printf("3 : GetNext PID                   \n");
	printf("4 : Modify                        \n");
	printf("5 : Show Thread information       \n");
	printf("6 : Send Signal                   \n");
	printf("7 : Send Broadcast                \n");
	printf("q : quit                          \n");
	printf("----------------------------------\n");
}

NUTHD_FUNC HandleProc(void *arg)
{
    int i = 0;
    thdArg_t *pArg = (thdArg_t *)arg;
    
    while (i >= 0)
    {
        if (pArg->EventFlag == 1)
		{
			/*
            NuEventWait(pArg->Event);
			*/
			NuEventTimeWait(pArg->Event, 1);
		}

        if (pArg->PrintFlag == 1)
            printf("[%lu] %d\n", (unsigned long)pArg->TID, i);
        i++;
        usleep(1);
    }

    NuThdReturn();
    return NULL;
}

int main()
{
	char szCmd[1+1];
	char szCmd2[1+1];
	int  iFlag = 1;
/*
	char Buf[128] = {0};
 */
    NuEvent_t       Event;

    NuListNode_t *pNode = NULL;
    NuListNode_t *pCur = NULL;
    NuListNode_t *pCur_Ctrl = NULL;
    NuList_t     *pList = NULL;
    thdArg_t     *pArg = NULL;
    thdArg_t     *pArg_R = NULL;
    NuThread_t    TID;


    NuListNew(&pList, NULL);

    NuEventOpen(&Event);

	Usage();

	while(iFlag)
	{
		printf("==>");
		scanf("%s", szCmd);

		switch(szCmd[0])
		{
			case '1':
                pArg = (thdArg_t *)malloc(sizeof(thdArg_t));
                pArg->PrintFlag = 1;
                pArg->EventFlag = 1;
                pArg->Event = &Event;
                NuThdCreate(HandleProc, (void *)pArg, &TID);

                pArg->TID = TID;

                NuListNodeNew(&pNode, NULL);
                NuListNodeSet(pNode, pArg, sizeof(thdArg_t));

                NuListInsTail(pList, pNode);

                pNode = NULL;
				break;
			case '2':
                pCur_Ctrl = NuListGetHead(pList);
                if (pCur_Ctrl != NULL)
                {
                    pArg_R = (thdArg_t *)pCur_Ctrl->item;
                    printf("Get TID=%lu PrintFlag=%d\n", (unsigned long)pArg_R->TID, pArg_R->PrintFlag);
                }
                else
                {
                    printf("NULL\n");
                }

				break;
			case '3':
                if (pCur_Ctrl == NULL)
                {
                    printf("Ctrl NULL\n");
                    break;
                }

                pCur_Ctrl = NuListNodeGetNext(pCur_Ctrl);
                if (pCur_Ctrl != NULL)
                {
                    pArg_R = (thdArg_t *)pCur_Ctrl->item;
                    printf("Get TID=%lu PrintFlag=%d\n", (unsigned long)pArg_R->TID, pArg_R->PrintFlag);
                }
                else
                {
                    printf("NULL\n");
                }
				break;
			case '4':
                printf("Open Printf (Y/N):");
		        scanf("%s", szCmd2);
                if (szCmd2[0] == 'Y')
                    pArg_R->PrintFlag = 1;
                else
                    pArg_R->PrintFlag = 0;

                printf("Open Event  (Y/N):");
		        scanf("%s", szCmd2);
                if (szCmd2[0] == 'Y')
                    pArg_R->EventFlag = 1;
                else
                    pArg_R->EventFlag = 0;
				break;
			case '5':
                printf("Thread Cnt[%d]\n", NuListGetItemNo(pList));
                pCur = NuListGetHead(pList);
                while(pCur != NULL)
                {
                    pArg_R = (thdArg_t *)pCur->item;
                    printf("Get TID=%lu PrintFlag=%d\n", (unsigned long)pArg_R->TID, pArg_R->PrintFlag);
                    pCur = NuListNodeGetNext(pCur);
                }
				break;
			case '6':
                NuEventSignal(&Event);
                break;
			case '7':
                NuEventBroadcast(&Event);
                break;
			case 'q':
				iFlag = 0;
				break;
			default:
				Usage();
				break;
		}
	}

	return 0;
}
