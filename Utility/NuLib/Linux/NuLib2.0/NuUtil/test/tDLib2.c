#include "NuUtil.h"
#include "NuDLibMgr.h"
#include "NuFixMsg.h"

typedef struct _obj_t
{
    void *Data;
    void *Ptr;
    size_t Len;
}obj_t;

typedef int (*SoFn)(int, int);
typedef int (*SoFixFn)(obj_t *);

void FixMsgInit(NuFixMsg_t *pFixMsg)
{
    NuFixMsgInit(pFixMsg);
}

void FixMsgDestroy(NuFixMsg_t *pFixMsg)
{
    if (pFixMsg != NULL)
        NuFixMsgFree(pFixMsg);
}

int main(int argc, char **argv)
{
    int x = 10, y = 20;
//  void ** ptr;
    char *Fun = "Fun";
    char *Fun2 = "Fun2";
    int iRC = 0;
    SoFn CBFn;
    SoFixFn CBFixFn1, CBFixFn2;
    base_vector_t *vec = NULL;
    base_vector_it vecIT = NULL;

    NuFixMsg_t *pFixMsg = NULL;
    obj_t obj;

    base_vector_new(&vec, 10);

    pFixMsg = (NuFixMsg_t *)malloc(NuFixMsgSz);
//  FixMsgInit(pFixMsg);

    iRC = NuDLibMgrLoadToLocal(Fun, "../tmp/Fun.so");
    printf("add Fun  [%d]\n", iRC);
    iRC = NuDLibMgrLoadToLocal(Fun2, "../tmp/Fun2.so");
    printf("add Fun2 [%d]\n", iRC);

    CBFixFn1 = NuDLibMgrGetFn(Fun, "FixHandle");
    CBFixFn2 = NuDLibMgrGetFn(Fun2, "FixHandle");

    CBFixFn1(&obj);
    CBFixFn2(&obj);

    printf("[%ld]%s\n", obj.Len, (char *)(obj.Ptr));
    free(obj.Ptr);
    /* -------------------------------------------- */
    
    printf("------------------------------------------------------------\n");
    CBFn = NuDLibMgrGetFn(Fun, "add");
    if (CBFn != NULL)
    {
        base_vector_push(vec, CBFn);
    }

    CBFn = NuDLibMgrGetFn(Fun2, "add");
    if (CBFn != NULL)
    {
        base_vector_push(vec, CBFn);
    }

    printf("x = %d, y = %d\n", x , y);
    base_vector_it_set(vecIT, vec);
    while(vecIT != base_vector_it_end(vec))
    {
        CBFn = *vecIT;
        printf("[%d]\n", CBFn(x, y));
    
        ++ vecIT;
    }

/*
    ptr = NuDLibMgrGetFun(Fun, "add");
    if (ptr != NULL)
    {
        printf("%d + %d = %d\n", x, y, ((SoFn)ptr)(x, y));
    }

    ptr = NuDLibMgrGetFun(Fun2, "add");
    if (ptr != NULL)
    {
        printf(" 2 * %d + %d = %d\n", x, y, ((SoFn)ptr)(x, y));
    }
*/
    NuDLibMgrUnLoad(Fun);
    NuDLibMgrUnLoad(Fun2);

//  NuFixMsgFree(pFixMsg);

    base_vector_free(vec);
    return 0;
}

