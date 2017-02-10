
#include <stdlib.h>
#include <stdio.h>
#include <math.h>
#include <time.h>

#include "NuNum.h"

#define	TOTAL	1000000

int
main(int argc, char **argv)
{
	int		Cnt = 0;
	NuNum_t	*P = NULL, *Q = NULL, *R = NULL;
	clock_t	start, end;

	NuNumNew(&P, 100);
	NuNumNew(&Q, 100);
	NuNumNew(&R, 100);

	NuNumFromStr(P, "123.45");
	NuNumFromStr(Q, "45.678");

	sleep(1);

	start = clock();

	for(Cnt = 0; Cnt < TOTAL; Cnt ++)
	{
		NuNumAdd(P, Q, R);
	}

	end = clock();

	printf("+[%f]\n", (double)(end - start)/CLOCKS_PER_SEC);

	sleep(1);

	start = clock();

	for(Cnt = 0; Cnt < TOTAL; Cnt ++)
	{
		NuNumSubstract(P, Q, R);
	}

	end = clock();

	printf("-[%f]\n", (double)(end - start)/CLOCKS_PER_SEC);

	sleep(1);

	start = clock();

	for(Cnt = 0; Cnt < TOTAL; Cnt ++)
	{
		NuNumMultiply(P, Q, R);
	}

	end = clock();

	printf("*[%f]\n", (double)(end - start)/CLOCKS_PER_SEC);

	sleep(1);

	start = clock();

	for(Cnt = 0; Cnt < TOTAL; Cnt ++)
	{
		NuNumDivide(P, Q, R, 2);
	}

	end = clock();

	printf("/[%f]\n", (double)(end - start)/CLOCKS_PER_SEC);

	return 0;
}


