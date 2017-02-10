
#include "NuUtil.h"
#include "NuBuffer.h"

#define	NUM	1000000

int
main(int argc, char **argv)
{
	int							Cnt = 0;
	void						*Buf = NULL;
	base_list_t					list;
	base_list_node_t			*pnode = NULL;
	NuBuffer_t					*pBuf = NULL;
	clock_t						start, end;

	base_list_init(&list);
	NuBufferInitiate(&pBuf, sizeof(base_list_node_t), NUM);

	start = clock();
	for(Cnt = 0; Cnt < NUM; Cnt ++)
	{
		NuBufferGet(pBuf, &Buf);
		pnode = (base_list_node_t *)Buf;
		base_list_insert_tail(&list, pnode);
	}
	end = clock();

	printf("%d InsTail\n", NUM);
	printf("[%f]micro sec per InsTail\n", (float)(end - start)/CLOCKS_PER_SEC);

	start = clock();
	for(Cnt = 0; Cnt < NUM; Cnt ++)
	{
		base_list_remove_tail(&list, &pnode);
		NuBufferPut(pBuf, pnode);
	}
	end = clock();

	printf("%d RmvTail\n", NUM);
	printf("[%f]micro sec per RmvTail\n", (float)(end - start)/CLOCKS_PER_SEC);

	return 0;
}

