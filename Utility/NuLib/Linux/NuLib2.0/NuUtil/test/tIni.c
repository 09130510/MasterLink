
#include "stdlib.h"
#include "NuIni.h"

#define PrintResult(Section, Key)           printf("%s::%s[%s]\n", (Section), (Key), NuIniFind(pIni, (Section), (Key)))

#define PrintModify(Section, Key, Value)    do \
                                            { \
                                                printf("Mod(%s, %s, %s)\n", (Section), (Key), (char *)(Value)); \
                                                NuIniModify(pIni, (Section), (Key), (Value)); \
                                            } \
                                            while(0)


int main(int Argc, char **Argv)
{
    NuIni_t *pIni = NULL;
    char    *pNULL = NULL;

    NuIniNew(&pIni, "tIni.ini");
	printf("SESSION1 exist ? [%c]\n", (NuIniSectionExist(pIni, "SESSION1") == NUTRUE) ? 'Y' : 'N');
	printf("SESSION2 exist ? [%c]\n", (NuIniSectionExist(pIni, "SESSION2") == NUTRUE) ? 'Y' : 'N');
	printf("SESSION3 exist ? [%c]\n", (NuIniSectionExist(pIni, "SESSION3") == NUTRUE) ? 'Y' : 'N');
/*
    printf("-------------------------------\n");
    PrintResult("Normal", "Key1");
    PrintResult("Normal", "Key2");
    printf("-------------------------------\n");
    PrintResult("Isolated", "Key1");
    PrintResult("Isolated", "Key2");
    PrintResult("Isolated", "K e y 2");
    printf("-------------------------------\n");
    PrintResult("Incomplete", "Key1");
    PrintResult("Incomplete", "Key");
    PrintResult("Incomplete", "");
    printf("-------------------------------\n");
    PrintResult("Duplicate", "Key1");
    PrintResult("Duplicate", "Key2");
    PrintResult("Duplicate", "Key1");

    printf("-------------------------------\n");
    PrintModify("Normal", "Key1", "Value1.Modify");
    PrintResult("Normal", "Key1");

    PrintModify("Normal", "Key2", "Value2.Modify");
    PrintResult("Normal", "Key2");

    PrintModify("Add", "Key1", "Value1.Add");
    PrintResult("Add", "Key1");

    PrintModify("Add", "Key2", "Value2.Add");
    PrintResult("Add", "Key2");
    
    PrintModify("Normal", "Key2", pNULL);
    PrintResult("Normal", "Key2");

    NuIniSave(pIni, "tIni.Modify.ini");
*/
    return 0;
}

