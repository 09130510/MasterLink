
#include "NuCBMgr.h"

void a1(void *Argu, void *Argu2)
{
    printf("%s:%s:::a1!!!\n", (char *)Argu, (char *)Argu2);
    return;
}

void a2(void *Argu, void *Argu2)
{
    printf("%s:%s:::a2!!!\n", (char *)Argu, (char *)Argu2);
    return;
}

void b(void *Argu, void *Argu2)
{
    printf("%s:%s:::b!!!\n", (char *)Argu, (char *)Argu2);
    return;
}

int main(int argc, char **argv)
{
    NuCBMgrTrigger_t    *pTriggerA = NuCBMgrAddTrigger();
    NuCBMgrTrigger_t    *pTriggerB = NuCBMgrAddTrigger();
    NuEventHdlr_t       *pHdlra1 = NULL, *pHdlra2 = NULL, *pHdlrb = NULL;
    char                A[2] = "A", B[2] = "B";
    char                *MainArgu = "MainArgu";

    if(!pTriggerA || !pTriggerB)
    {
        printf("Trigger init failed.\n");
        return 0;
    }

    printf("A!\n");
    NuCBMgrRaiseEvent(pTriggerA, MainArgu);

    printf("B!\n");
    NuCBMgrRaiseEvent(pTriggerB, MainArgu);

    printf("a1->A\n");
    pHdlra1 = NuCBMgrRegisterEvent(pTriggerA, &a1, A);

    printf("A!\n");
    NuCBMgrRaiseEvent(pTriggerA, MainArgu);

    printf("B!\n");
    NuCBMgrRaiseEvent(pTriggerB, MainArgu);

    printf("b->B\n");
    pHdlrb = NuCBMgrRegisterEvent(pTriggerB, &b, B);

    printf("A!\n");
    NuCBMgrRaiseEvent(pTriggerA, MainArgu);

    printf("B!\n");
    NuCBMgrRaiseEvent(pTriggerB, MainArgu);

    printf("a2->A\n");
    pHdlra2 = NuCBMgrRegisterEvent(pTriggerA, &a2, A);

    printf("A!\n");
    NuCBMgrRaiseEvent(pTriggerA, MainArgu);

    printf("B!\n");
    NuCBMgrRaiseEvent(pTriggerB, MainArgu);

    printf("a1><\n");
    NuCBMgrUnRegisterEvent(pHdlra1);
    
    printf("A!\n");
    NuCBMgrRaiseEvent(pTriggerA, MainArgu);

    printf("B!\n");
    NuCBMgrRaiseEvent(pTriggerB, MainArgu);

    printf("a1->B\n");
    pHdlra1 = NuCBMgrRegisterEvent(pTriggerB, &a1, B);

    printf("A!\n");
    NuCBMgrRaiseEvent(pTriggerA, MainArgu);

    printf("B!\n");
    NuCBMgrRaiseEvent(pTriggerB, MainArgu);

    printf("a1->A\n");
    pHdlra1 = NuCBMgrRegisterEvent(pTriggerA, &a1, A);

    printf("A!\n");
    NuCBMgrRaiseEvent(pTriggerA, MainArgu);

    printf("B!\n");
    NuCBMgrRaiseEvent(pTriggerB, MainArgu);

    printf("===========================End===============================\n");
    return 0;
}

