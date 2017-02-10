
#include <stdlib.h>

#include "NuNum.h"

int
main(int argc, char **argv)
{
	char	Str[100] = "0";
	int		AccuDigit = 3;
	NuNum_t	*A = NULL;
	NuNum_t	*B = NULL;
	NuNum_t	*C = NULL;

	NuNumNew(&A, 100);
	NuNumNew(&B, 100);
	NuNumNew(&C, 100);

	if(argc > 3)
	{
		NuNumFromStr(A, argv[1]);
		NuNumFromStr(B, argv[2]);
		AccuDigit = atoi(argv[3]);
	}
	else
	{
		NuNumFromStr(A, "15123");
		NuNumFromStr(B, "-64");
	}

	NuNumAdd(A, B, C);

	NuNumToStr(A, Str);
	printf("A[%s]", Str);

	NuNumToStr(B, Str);
	printf(" + B[%s]", Str);

	NuNumToStr(C, Str);
	printf(" = C[%s]\n", Str);

	NuNumSubstract(A, B, C);

	NuNumToStr(A, Str);
	printf("A[%s]", Str);

	NuNumToStr(B, Str);
	printf(" - B[%s]", Str);

	NuNumToStr(C, Str);
	printf(" = C[%s]\n", Str);

	NuNumMultiply(A, B, C);

	NuNumToStr(A, Str);
	printf("A[%s]", Str);

	NuNumToStr(B, Str);
	printf(" * B[%s]", Str);

	NuNumToStr(C, Str);
	printf(" = C[%s]\n", Str);

	NuNumDivide(A, B, C, AccuDigit);

	NuNumToStr(A, Str);
	printf("A[%s]", Str);

	NuNumToStr(B, Str);
	printf(" / B[%s]", Str);

	NuNumToStr(C, Str);
	printf(" = C[%s]\n", Str);

	NuNumToStr(A, Str);
	printf("A[%s] << 3", Str);

	NuNumLeftShift(A, 3);

	NuNumToStr(A, Str);
	printf(" = A[%s]\n", Str);


	NuNumToStr(A, Str);
	printf("A[%s] >> 10", Str);

	NuNumRightShift(A, 10);

	NuNumToStr(A, Str);
	printf(" = A[%s]\n", Str);

/*
	NuNumFromStr(A, "5");
	NuNumFromStr(B, "0");
	AccuDigit = 4;
	NuNumSubstract(A, B, C);
	NuNumToStr(C, Str);
	printf("C[%s]\n", Str);

	NuNumFromStr(A, "0");
	NuNumFromStr(B, "0");
	NuNumDivide(A, B, C, AccuDigit);
*/

	NuNumFromStr(A, "008197.00");
	NuNumToStr(A, Str);
	printf("A[%s]\n", Str);

	NuNumFromStr(B, "008200.00");
	NuNumToStr(B, Str);
	printf("B[%s]\n", Str);

	NuNumSubstract(A, B, C);

	NuNumToStr(C, Str);
	printf("C[%s]\n", Str);


	return 0;
}

