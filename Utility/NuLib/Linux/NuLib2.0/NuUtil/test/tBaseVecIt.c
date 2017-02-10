
#include <stdio.h>
#include <stdlib.h>
#include <time.h>

#include "NuUtil.h"

#define DEFAULT_VAL "DEFAULT"
int _Compare(const void *p1, const void *p2)
{
	int i = 0, j = 0;
	char *s1 = *(char **)p1;
	char *s2 = *(char **)p2;
	i = atoi(s1);
	j = atoi(s2);
	return i - j;
}

int main(int argc, char **argv)
{
	char *ptr = NULL;
	int  len = 10;
	int  i = 0, j = 15;
	int  r = 0;
	base_vector_t	*pVec = NULL;
	base_vector_it	VecIt;

	base_vector_new(&pVec, 10);

	printf("----------------------------\n");
	base_vector_it_set(VecIt, pVec);
	while(VecIt != base_vector_it_end(pVec))
	{
		printf("%s\n", (char *)(*VecIt));
		++ VecIt;
	}

	printf("----------------------------\n");
	base_vector_it_setend(VecIt, pVec);
	while(VecIt != base_vector_it_begin(pVec))
	{
		printf("%s\n", (char *)(*VecIt));
		-- VecIt;
	}

	srand(time(NULL));
	for (i = 0; i < j; i++)
	{
		r = (rand() % 1000);
		ptr = (char *)malloc(sizeof(char) * len);
		sprintf(ptr, "%d", r);
		base_vector_push(pVec, ptr);
	}

	printf("-begin to end---------------\n");
	base_vector_it_set(VecIt, pVec);
	while(VecIt != base_vector_it_end(pVec))
	{
		printf("%s\n", (char *)(*VecIt));
		++ VecIt;
	}
	printf("-end to begin---------------\n");
	base_vector_it_setend(VecIt, pVec);
	while(VecIt != base_vector_it_begin(pVec))
	{
		printf("%s\n", (char *)(*VecIt));
		-- VecIt;
	}

	printf("-sort-begin to end----------\n");
	base_vector_sort(pVec, &_Compare);

	base_vector_it_set(VecIt, pVec);
	while(VecIt != base_vector_it_end(pVec))
	{
		printf("%s\n", (char *)(*VecIt));
		++ VecIt;
	}

	printf("-sort-end to begin----------\n");
	base_vector_it_setend(VecIt, pVec);
	while(VecIt != base_vector_it_begin(pVec))
	{
		printf("%s\n", (char *)(*VecIt));
		-- VecIt;
	}
	printf("-change item-----------------\n");
	base_vector_it_set(VecIt, pVec);
	while(VecIt != base_vector_it_end(pVec))
	{
//		printf("%s\n", (char *)(*VecIt));
		if (atoi((char *)(*VecIt)) < 300)
		{
			*VecIt = DEFAULT_VAL;
		}
		++ VecIt;
	}
	base_vector_sort(pVec, &_Compare);

	base_vector_it_set(VecIt, pVec);
	while(VecIt != base_vector_it_end(pVec))
	{
		printf("%s\n", (char *)(*VecIt));
		++ VecIt;
	}



	return 0;
}

