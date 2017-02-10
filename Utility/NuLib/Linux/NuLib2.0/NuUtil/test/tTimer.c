
#include "NuTimer.h"

static void _PrintPerSec(void *Argu)
{
    printf("PrintPer(%d)Sec!!\n", *((int *)((void **)&Argu)));
    return;
}

int main(int Argc, char **Argv)
{
    NuTimerEventHdlr_t  *pEventHdlr1 = NuTimerRegister(1, &_PrintPerSec, (void *)1);
    NuTimerEventHdlr_t  *pEventHdlr2 = NuTimerRegister(2, &_PrintPerSec, (void *)2);

    printf("Main:\n");

    sleep(10);

    NuTimerUnregister(pEventHdlr1);
    printf("Second\n");

    sleep(10);

    pEventHdlr1 = NuTimerRegister(3, &_PrintPerSec, (void *)3);
    NuTimerUnregister(pEventHdlr2);
    pEventHdlr2 = NuTimerRegister(4, &_PrintPerSec, (void *)4);
    printf("Third\n");

    sleep(10);

    NuTimerFree();

    return 0;
}

