
#include "NuUtil.h"


int
main(int argc, char **argv)
{
	base_list_t			ListA;
	base_list_t			ListB;
	base_list_node_t	Node1, Node2, Node3;
	base_list_node_t	*pNode = NULL;


	base_list_init(&ListA);
	base_list_init(&ListB);

	base_list_node_init(&Node1);
	base_list_node_init(&Node2);
	base_list_node_init(&Node3);

	base_list_insert_tail(&ListA, &Node1);
	base_list_insert_tail(&ListA, &Node2);
	base_list_insert_tail(&ListA, &Node3);

	printf("ListA[%d]\n", ListA.cnt);
	printf("ListB[%d]\n", ListB.cnt);
/*	
	base_list_remove_head(&ListA, &pNode);
	base_list_insert_tail(&ListB, pNode);

	base_list_remove_head(&ListA, &pNode);
	base_list_insert_tail(&ListB, pNode);
*/

	base_list_transfer_tail(&ListA, &ListB);
	printf("ListA[%d]\n", ListA.cnt);
	printf("ListB[%d]\n", ListB.cnt);

	return 0;
}

