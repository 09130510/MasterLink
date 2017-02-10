#include "NuStr.h"

/* extensible string */
/* ====================================================================== */
/* static function */
static void NuStrSprintf(NuStr_t *pstr, const char *format, va_list ap)
{
    char fmt[32+1] = {0};
    int  fmt_len = 0;
    char *ptr = NULL;
    int  lnum = 0;
    char num[64+1] = {0};
    int  num_len = 0;

    while(*format != '\0')
    {
        if (*format == '%')
        {
            fmt_len = 0;
            fmt[fmt_len] = *(format);
            ++fmt_len;
            ++format;
            lnum = 0;
            while ( strchr("0123456789 +-.lL", *format) && *format != '\0' )
            {
                if (*format == 'l' || *format == 'L')
                    ++lnum;
                fmt[fmt_len] = *(format);
                ++fmt_len;
                ++format;
            }
            fmt[fmt_len] = *(format);
            ++fmt_len;
            fmt[fmt_len] = '\0';
            switch(*format)
            {
                case 's':
                    ptr = va_arg(ap, char *);
                    NuStrCat(pstr, ptr);
                    break;
                case 'd':
                    if (lnum >= 2)
                    {
                        num_len = sprintf(num, fmt, va_arg(ap, long long));
                    }
                    else if (lnum == 1)
                    {
                        num_len = sprintf(num, fmt, va_arg(ap, long));
                    }
                    else
                    {
                        num_len = sprintf(num, fmt, va_arg(ap, int));
                    }
                    NuStrNCat(pstr, num, num_len);
                    break;
                case 'c':
                case 'o':
                case 'u':
                case 'x':
                case 'X':
                    if (lnum >= 2)
                    {
                        num_len = sprintf(num, fmt, va_arg(ap, unsigned long long));
                    }
                    else if (lnum == 1)
                    {
                        num_len = sprintf(num, fmt, va_arg(ap, unsigned long));
                    }
                    else
                    {
                        num_len = sprintf(num, fmt, va_arg(ap, unsigned int));
                    }
                    NuStrNCat(pstr, num, num_len);
                    break;
                case 'f':
                case 'e':
                case 'E':
                case 'g':
                case 'G':
                    if (lnum >= 1)
                    {
                        num_len = snprintf(num, sizeof(num), fmt, va_arg(ap, long double));
                    }
                    else
                    {
                        num_len = snprintf(num, sizeof(num), fmt, va_arg(ap, double));
                    }
                    NuStrNCat(pstr, num, num_len);
                    break;
                default:
                    break;
            }
        }
        else
        {   /* append char */
            NuStrCatChr(pstr, *format);
        }

        ++format;
    }

    return;
}
/* ---------------------------------------------------------------------- */
int NuStrNew(NuStr_t **pstr, char *pval)
{
	int vlen = (pval == NULL) ? 0 : (strlen(pval) + 1);
	return NuStrNew2(pstr, vlen, pval);
}

int NuStrNew2(NuStr_t **pstr, size_t string_size, char *pval)
{
	int iRC = NU_OK;
	int default_size = NuMax(string_size, NUSTR_DEFAULT_SIZE);

	(*pstr) = (NuStr_t *)malloc(sizeof(NuStr_t));
	if ((*pstr) == NULL)
		NUGOTO(iRC, NU_MALLOC_FAIL, EXIT);

	(*pstr)->ptr = (char *)malloc(default_size);
	if ((*pstr)->ptr == NULL)
		NUGOTO(iRC, NU_MALLOC_FAIL, EXIT);

	(*pstr)->size = 0;
	(*pstr)->asize = default_size;
	*((*pstr)->ptr) = '\0';

	if (pval != NULL)
	{
		iRC = NuStrCat((*pstr), pval);
	}

EXIT:
	return iRC;
}

int NuStrFree(NuStr_t *pstr)
{
    int iRC = NU_OK;

	if(pstr == NULL)
        NUGOTO(iRC, NU_NULL, EXIT);

	if(pstr->ptr != NULL)
	{
	    free(pstr->ptr);
		pstr->ptr = NULL;
	}

    free(pstr);
	pstr = NULL;

EXIT:
	return iRC;
}

int NuStrClear(NuStr_t *pstr)
{
	if (pstr == NULL)
		return NU_NULL;

	pstr->size = 0;
	*(pstr->ptr) = '\0';

	return NU_OK;
}

int NuStrNCat(NuStr_t *pstr, const void *ptr, size_t size)
{
	int	nsize = 0;

	if(!pstr)
    {
		return NU_NULL;
    }

	if(pstr->asize < (nsize = pstr->size + size + 1))
	{
		pstr->asize = nsize;
		pstr->ptr = (char *)realloc(pstr->ptr, pstr->asize);
		if(!(pstr->ptr))
        {
			return NU_MALLOC_FAIL;
        }
	}

	memcpy(pstr->ptr + pstr->size, ptr, size);
	pstr->size += size;
	pstr->ptr[pstr->size] = '\0';

	return NU_OK;
}

int NuStrCat(NuStr_t *pStr, const char *pSrc)
{
	return NuStrNCat(pStr, pSrc, strlen(pSrc));
}

int NuStrNCpy(NuStr_t *pstr, const void *ptr, size_t size)
{
	int	iRC = NU_OK;
	int	nsize = 0;

	if (pstr == NULL)
		return NU_NULL;

	if(pstr->asize < (nsize = size + 1))
	{
		pstr->asize = nsize;
		pstr->ptr = (char *)realloc(pstr->ptr, nsize);
		if(pstr->ptr == NULL)
			return NU_MALLOC_FAIL;
	}

	memcpy(pstr->ptr, ptr, size);
	pstr->size = size;
	pstr->ptr[size] = '\0';

	return iRC;
}

int NuStrCpy(NuStr_t *pStr, const char *pSrc)
{
	return NuStrNCpy(pStr, pSrc, strlen(pSrc));
}

int NuStrCatChr(NuStr_t *pstr, const char chr)
{
	int   iRC = NU_OK;
	int   nsize = 0;

	if (pstr == NULL)
		return NU_FAIL;

	if(pstr->asize < (nsize = pstr->size + 2))
	{
		pstr->asize = nsize;
		pstr->ptr = (char *)realloc(pstr->ptr, pstr->asize);
		if(pstr->ptr == NULL)
			return NU_MALLOC_FAIL;
	}

	pstr->ptr[pstr->size ++] = chr;
	pstr->ptr[pstr->size] = '\0';

	return iRC;
}

int NuStrSubStr(NuStr_t *pstr, int start, int cnt, NuStr_t *pout)
{
	return NuStrNCat(pout, pstr->ptr + start, NuMin(pstr->size, cnt));
}

int NuStrSubStr2(NuStr_t *pstr, int start, int cnt, char *str)
{
	int  len = NuMin(pstr->size, cnt);

	memcpy(str, pstr->ptr + start, len);
	str[len] = '\0';

	return NU_OK;
}

NuStr_t *NuStrDup(NuStr_t *pstr)
{
	NuStr_t *pdup = NULL;
	NuStrNew(&pdup, NuStrGet(pstr));
	if (pdup == NULL)
		return NULL;

//	NuStrCat(pdup, NuStrGet(pstr));

	return pdup;
}

/* Perform formatted output into an extensible string object. */
int NuStrAppendPrintf(NuStr_t *pstr, const char *format, ...)
{
	int		len = 0;
	char	buf[NUSTRBUFSIZ] = "\0";
	va_list ap;

	va_start(ap, format);
	len = vsnprintf(buf, NUSTRBUFSIZ, format, ap);
	va_end(ap);

	NuStrNCat(pstr, buf, len);

	return NU_OK;
}

int NuStrAppendPrintf2(NuStr_t *pstr, const char *format, ...)
{
	va_list ap;

	va_start(ap, format);
    NuStrSprintf(pstr, format, ap);
	va_end(ap);

	return NU_OK;
}

int NuStrPrintf(NuStr_t *pStr, const char *Format, ...)
{
	va_list ArguList;

	va_start(ArguList, Format);
	NuStrVPrintf(pStr, Format, ArguList);
	va_end(ArguList);

	return NU_OK;
}

int NuStrVPrintf(NuStr_t *pStr, const char *Format, va_list ArguList)
{
	int		len = 0;
	char	buf[NUSTRBUFSIZ] = "\0";

	len = vsnprintf(buf, NUSTRBUFSIZ, Format, ArguList);

	NuStrClear(pStr);
	NuStrNCat(pStr, buf, len);

	return len;
}

int NuStrCmp(NuStr_t *pstr1, NuStr_t *pstr2)
{
    if (pstr1->size != pstr2->size)
        return (pstr1->size - pstr2->size);

    return memcmp(pstr1->ptr, pstr2->ptr, pstr1->size);
}

void NuStrRTrim(NuStr_t *pstr)
{
	int		Cnt = pstr->size;
	char	*p = pstr->ptr + Cnt - 1;

	for(; Cnt > 0; -- Cnt)
	{
		if(*p != ' ' && *p != '\t')
			break;

		-- p;
	}

	*(++ p) = '\0';
	pstr->size = Cnt;

	return;
}

void NuStrRTrimChr(NuStr_t *pstr, char cChr)
{
	int		Cnt = pstr->size;
	char	*p = pstr->ptr + Cnt - 1;

	for(; Cnt > 0; -- Cnt)
	{
		if(*p != cChr)
			break;

		-- p;
	}

	*(++ p) = '\0';
	pstr->size = Cnt;

	return;
}

void NuStrLTrim(NuStr_t *pstr)
{
	int		Cnt = pstr->size;
	char	*p = pstr->ptr;

	for(; Cnt > 0; -- Cnt)
	{
		if(*p != ' ' && *p != '\t')
			break;

		++ p;
	}

	memmove(pstr->ptr, p, pstr->size = Cnt);
	pstr->ptr[Cnt] = '\0';

	return;
}

void NuStrReplaceRangeChr(NuStr_t *pstr, char cChr, char cNewChr, int start, int len)
{
	char *p = pstr->ptr + start;
	char *d = p + len;
	while(p != d)
	{
		if (*p == cChr)
			*p = cNewChr;
		++p;
	}
	return;
}

void NuStrReplaceChr(NuStr_t *pstr, char cChr, char cNewChr)
{
	char *p = pstr->ptr;

	while (*p != '\0')
	{
		if (*p == cChr)
			*p = cNewChr;
		++p;
	}

	return;
}

NU_INLINE char *NuStrGet(NuStr_t *pStr)
{
	return (pStr == NULL) ? NULL : pStr->ptr;
}

NU_INLINE char NuStrGetChr(NuStr_t *pStr, int Idx)
{
	return (pStr == NULL) ? '\0' : pStr->ptr[Idx];
}

NU_INLINE int NuStrSize(NuStr_t *pStr)
{
	return (pStr == NULL) ? 0 : pStr->size;
}

