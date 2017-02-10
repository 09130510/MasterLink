
#include "NuCommon.h"
#include "NuUtil.h"

int main(int argc, char **argv)
{
    int iRC = 0;

    clock_t start = 0, end = 0;
    float total = 0;
    int i = 0;
    void *pdata;

    mem_pool_t *pmem = NULL;
    iRC = mem_pool_open(&pmem, 8, 1000000);
    printf("alloc iRC[%d]\n", iRC);

    start = clock();

    for(i = 0; i < 1000000; i++)
    {
        pdata = mem_pool_get(pmem, 8);
//        mem_pool_resize(pmem, &pdata, 8, 8);
        mem_pool_put(pmem, pdata, 8);
    }

    end = clock();
    total = (float)(end - start)/CLOCKS_PER_SEC;
    printf("[%d] start - end = [%f]\n", 1000000, total);

    iRC = mem_pool_close(pmem);
    printf("free iRC[%d]\n", iRC);
	return 0;

}

