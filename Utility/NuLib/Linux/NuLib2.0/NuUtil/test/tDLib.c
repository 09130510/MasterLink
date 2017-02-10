
#include "NuDLibMgr.h"

#define UNLOAD_ARGU "is going to be unloaded."

static void _FnUnloadCB(void *FnName, void *MyArgu)
{
    printf("[%s] %s\n", (char *)FnName, (char *)MyArgu);

    return;
}

typedef int (*SoFn)(int, int);
typedef void (*SoFn2)(void *);
int main(int argc, char **argv)
{
    int x = 10, y = 20;
    void ** ptr;
    char *Fun = "Fun";
    char *Fun2 = "Fun2";
    int iRC = 0;
    SoFn CBFn;

    iRC = NuDLibMgrLoadToGlobal(Fun, "../tmp/Fun.so");
    printf("add Fun  [%d]\n", iRC);
    iRC = NuDLibMgrLoadToGlobal(Fun2, "../tmp/Fun2.so");
    printf("add Fun2 [%d]\n", iRC);

    NuDLibMgrRegisterUnLoadEvent(Fun, &_FnUnloadCB, UNLOAD_ARGU);
    NuDLibMgrRegisterUnLoadEvent(Fun2, &_FnUnloadCB, UNLOAD_ARGU);

    CBFn = NuDLibMgrGetFn(Fun, "add");
    if (CBFn != NULL)
    {
        printf("Load : %d + %d = %d\n", x, y, (CBFn)(x, y));
    }

    ptr = NuDLibMgrGetFn(Fun, "add");
    if (ptr != NULL)
    {
        printf("%d + %d = %d\n", x, y, ((SoFn)ptr)(x, y));
    }

    ptr = NuDLibMgrGetFn(Fun2, "add");
    if (ptr != NULL)
    {
        printf(" 2 * %d + %d = %d\n", x, y, ((SoFn)ptr)(x, y));
    }

    ptr = NuDLibMgrGetFn(Fun2, "Fun2_Show");
    if (ptr != NULL)
    {
        ((SoFn2)ptr)(NULL);
    }

    /* generate core dump */
    ptr = NuDLibMgrGetFn(Fun2, "Fun2_Show2");
    if (ptr != NULL)
    {
        ((SoFn2)ptr)(NULL);
    }

    NuDLibMgrUnLoad(Fun);
	NuDLibMgrUnLoad(Fun2);

    return 0;
}

