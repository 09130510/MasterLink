#include "NuTools.h"

/* string function */
/* ====================================================================== */
int NuSplitOpen(NuStrVec_t *pStrVec)
{
    int iRC = 0;

    pStrVec->pvec = NULL;
    pStrVec->pstr = NULL;

    iRC = base_vector_new(&(pStrVec->pvec), 10);
    if (iRC < 0)
        return iRC;

    iRC = NuStrNew(&(pStrVec->pstr), NULL);
    if (iRC < 0)
        return iRC;

    return NU_OK;
}

void NuSplitClose(NuStrVec_t *pStrVec)
{
    base_vector_free(pStrVec->pvec);
    NuStrFree(pStrVec->pstr);
    return;
}

int NuSplit(char *str, char cSep, NuStrVec_t *pStrVec)
{
	NuStr_t	*pStr = pStrVec->pstr;
    NuStrCpy(pStr, str);

	return NuSplitNoCpy(NuStrGet(pStr), cSep, pStrVec);
}

int NuSplitNoCpy(char *str, char cSep, NuStrVec_t *pStrVec)
{
    char			*ptr = NULL;
	base_vector_t	*pVec = pStrVec->pvec;

    base_vector_clear(pVec);
    base_vector_push(pVec, ptr = str);

    while(*ptr != '\0')
    {
        if(*ptr == cSep)
        {
            *ptr = '\0';
            base_vector_push(pVec, ++ ptr);
        }
		else
			++ ptr;
    }
	
    return base_vector_get_cnt(pVec);
}

int NuSplitByStr(char *str, char *sep, NuStrVec_t *pStrVec)
{
	NuStr_t			*pStr = pStrVec->pstr;
	NuStrCpy(pStr, str);
	return NuSplitByStrNoCpy(NuStrGet(pStr), sep, pStrVec);
}

int NuSplitByStrNoCpy(char *str, char *sep, NuStrVec_t *pStrVec)
{
    char			*ptr = NULL;
	char			*p_cmp = sep;
	char			*p1 = NULL;
	char			*p2 = NULL;
	base_vector_t	*pVec = pStrVec->pvec;

	base_vector_clear(pVec);

    base_vector_push(pVec, ptr = str);

    while(*ptr != '\0')
    {
        if(*ptr == *p_cmp)
		{
			p1 = ptr;
			p2 = p1;

			while (p_cmp != '\0')
			{
				if (*p2 == *p_cmp)
				{
					++p2;
					++p_cmp;

					if (*p_cmp == '\0')
					{
						while(p1 != p2)
						{
							*p1 = '\0';
							++ p1;
							++ ptr;
						}
						base_vector_push(pVec, ptr);
						ptr = p2;
						p_cmp = sep;
					}
				}
				else
					break;
			}
			ptr = p2;
			p_cmp = sep;
		}
		else
		{
			++ptr;
		}
    }

	return base_vector_get_cnt(pVec);
}

char *NuSplitGetByIndex(NuStrVec_t *pStrVec, int index)
{
    return (char *)base_vector_get_by_index(pStrVec->pvec, index);
}

/* ---------------------------------------------------------------------- */
int WriteToPidFile(char *pPath, char *procName, char *Instance)
{
	FILE	*fd = NULL;
	NuStr_t *pStr = NULL;

	if(NuIsDir(pPath) < 0)
	{
		if(NuCreateRecursiveDir(pPath) < 0)
			return -1;
	}

	NuStrNew2(&pStr, 128, pPath);

//	NuStrCat(pStr, pPath);
	if(NuStrGetChr(pStr, (NuStrSize(pStr) - 1)) != NUFILE_SEPARATOR)
		NuStrCatChr(pStr, NUFILE_SEPARATOR);

	NuStrCat(pStr, procName);
	if(Instance != NULL)
	{
		if(*Instance != '\0')
		{
			NuStrCatChr(pStr, '_');
			NuStrCat(pStr, Instance);
		}
	}

	NuStrCat(pStr, ".pid");

	if((fd = fopen(NuStrGet(pStr), "w+")) == NULL)
	{
		NuStrFree(pStr);
		return -1;
	}

	fprintf(fd, "%d\n", getpid());

	fclose(fd);
	NuStrFree(pStr);

	return 0;
}

/* ---------------------------------------------------------------------- */

