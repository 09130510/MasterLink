
#include "NuLock.h"
#include "NuInvoker.h"

typedef struct _MyQ_t
{
    NuLock_t    Lock;
    int         HasData;
    char        *Data;
} MyQ_t;

static void MyPrint(void *Argu)
{
    printf("MyPrint(%s)\n", (char *)Argu);
    return;
}

static int Blocking(void *BlockingHdlr, NuInvokerDelegate_t *Delegate)
{
    MyQ_t   *Q = (MyQ_t *)BlockingHdlr;

    NuLockLock(&(Q->Lock));
    while(!(Q->HasData))
    {
        sleep(0);
    }

    if(Q->HasData == 1)
    {
        Delegate->Fn = &MyPrint;
        Delegate->Argu = Q->Data;

        Q->HasData = 0;
        Q->Data = NULL;
        NuLockUnLock(&(Q->Lock));
        return NU_OK;
    }
    else
    {
        printf("HasData = (%d)\n", Q->HasData);
        NuLockUnLock(&(Q->Lock));
        return NU_FAIL;
    }
}

static int Invoke(void *BlockingHdlr, NuInvokerDelegate_t *Delegate)
{ /* Single thread invoke version */
    MyQ_t   *Q = (MyQ_t *)BlockingHdlr;

    while(Q->HasData == 1)
    {
        sleep(0);
    }

    if(Q->HasData == 0)
    {
        Q->Data = (char *)(Delegate->Argu);
        Q->HasData = 1;
    }

    return NU_OK;
}

int main(int Argc, char **Argv)
{
    NuBlockingType_t    Type;
    NuInvokerGroup_t    *Group = NULL;
    MyQ_t               Q;
    NuInvokerDelegate_t Dg;

    NuLockInit(&(Q.Lock), &NuLockType_Mutex);
    Q.HasData = 0;
    Q.Data = NULL;

    Type.Init = &NuInitailFn_Default;
    Type.Destroy = &NuDestroyFn_Default;
    Type.Block = &Blocking;
    Type.Wake = &Invoke;
    Type.WakeAll = NULL;

    Group = NuInvokerAddGroup(&Type, &Q, 3, 10);
    printf("Add Group OK!\n");

    sleep(1);
    Dg.Fn = NULL;
    Dg.Argu = "First!";
    NuInvokerInvoke(Group, &Dg);

    sleep(1);
    Dg.Fn = NULL;
    Dg.Argu = "Second!";
    NuInvokerInvoke(Group, &Dg);
    
    sleep(1);
    Dg.Fn = NULL;
    Dg.Argu = "Third!";
    NuInvokerInvoke(Group, &Dg);

    sleep(1);
    printf("7-combo!\n");
    Dg.Argu = "4th!";
    NuInvokerInvoke(Group, &Dg);
    Dg.Argu = "5th!";
    NuInvokerInvoke(Group, &Dg);
    Dg.Argu = "6th!";
    NuInvokerInvoke(Group, &Dg);
    Dg.Argu = "7th!";
    NuInvokerInvoke(Group, &Dg);
    Dg.Argu = "8th!";
    NuInvokerInvoke(Group, &Dg);
    Dg.Argu = "9th!";
    NuInvokerInvoke(Group, &Dg);
    Dg.Argu = "10th!";
    NuInvokerInvoke(Group, &Dg);

    sleep(1);

    Q.HasData = 2;
    NuInvokerRemove(Group);

    NuInvokerStop();

    NuLockDestroy(&(Q.Lock));

    return 0;
}

