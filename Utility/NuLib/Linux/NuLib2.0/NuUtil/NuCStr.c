
#include <ctype.h>
#include "NuCommon.h"

#define	DecimalTable	"9876543210123456789"
#define	HEXVAL(x)		(((x) >= 'A') ? ((x)-'A'+10) : (x) - '0')

/* c string function */
/* ====================================================================== */
unsigned int NuCStrHex2UInt(char *szHex)
{
	char szHexVal[32] = {0};          /* 0x38000306 */
	int  len = 0;
	unsigned int rlng = 0;
	char *p = NULL;

	strcpy(szHexVal, szHex);

	len = strlen(szHexVal);

	if (len < 3 || len > 10)
	{
		return -1;
	}

	if ( !(szHexVal[0] == '0' && (szHexVal[1] == 'x' || szHexVal[1] == 'X')) )
	{
		return -1;
	}

	for (p = (szHexVal + 2); (p - szHexVal) < len && *p != '\0'; p++)
	{
		rlng = rlng * 16 + HEXVAL( toupper(*p) );
	}
	return rlng;
}

void NuCStrReplaceRangeChr(char *pstr, char cChr, char cNewChr, int start, size_t len)
{
	char *d = (pstr = pstr + start) + len;

	while(pstr != d)
	{
		if (*pstr == cChr)
			*pstr = cNewChr;
		++ pstr;
	}
	return;
}

void NuCStrReplaceChr(char *pstr, char cChr, char cNewChr)
{
	while(*pstr != '\0')
	{
		if(*pstr == cChr)
			*pstr = cNewChr;
		++ pstr;
	}
	return;
}

void NuCStrRTrimChr(char *pstr, char cChr)
{
	char	*p = pstr + strlen(pstr) - 1;

	while(*p == cChr && p > pstr)
		-- p;

	*(++ p) = '\0';

	return;
}

void NuCStrLTrimChr(char *pstr, char cChr)
{
	char *e = pstr + strlen(pstr);
	char *s = pstr;

	while(*s == cChr && s < e)
		++ s;

	memmove(pstr, s, e - s + 1);

	return;
}

int NuCStrPrintf(char *pstr, const char *format, ...)
{
    char fmt[32+1] = {0};
    int  fmt_len = 0;
    char *ptr = NULL;
    int  lnum = 0;
    char num[64+1] = {0};
    int  num_len = 0;
	int  rtn_len = 0;

	va_list ap;

	va_start(ap, format);

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

					while(*ptr != '\0')
					{
						*pstr = *ptr;
						++ ptr;
						++ pstr;
						++ rtn_len;
					}
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
					
					ptr = num;
					while(*ptr != '\0')
					{
						*pstr = *ptr;
						++ptr;
						++pstr;
						++rtn_len;
					}
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

					ptr = num;
					while(*ptr != '\0')
					{
						*pstr = *ptr;
						++ptr;
						++pstr;
						++rtn_len;
					}
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
					ptr = num;
					while(*ptr != '\0')
					{
						*pstr = *ptr;
						++ptr;
						++pstr;
						++rtn_len;
					}
                    break;
                default:
                    break;
            }
        }
        else
        {   /* append char */
			*pstr = *format;
			++rtn_len;
			++pstr;
        }

        ++format;
    }
	*pstr = '\0';
	va_end(ap);

    return rtn_len;
}

int NuCStrPrintInt(char *pStr, int Value, int Length)
{
	char	*ptr = pStr, *ptr1 = pStr, tmp_char;
	int		tmp_value = 0, len = 0;

	*ptr = '\0';

	do
	{
		tmp_value = Value;
		Value /= 10;
		*ptr ++ = DecimalTable[9 + (tmp_value - Value * 10)];
	}
    while(Value);

	if(tmp_value < 0)
		*ptr ++ = '-';

	len = ptr - ptr1;
	*ptr -- = '\0';

	while(ptr1 < ptr)
	{
		tmp_char = *ptr;
		*ptr -- = *ptr1;
		*ptr1 ++ = tmp_char;
	}

	if(len < Length)
	{
		if(*pStr == '-')
			++ pStr;

		memmove(pStr + Length - len, pStr, len + 1);
		memset(pStr, '0', Length - len);
	
		return Length;
	}

	return len;
}

int NuCStrPrintLong(char *pStr, long Value, int Length)
{
	char	*ptr = pStr, *ptr1 = pStr, tmp_char;
	int		len = 0;
	long	tmp_value = 0;

	*ptr = '\0';

	do
	{
		tmp_value = Value;
		Value /= 10;
		*ptr ++ = DecimalTable[9 + (tmp_value - Value * 10)];
	}
    while(Value);

	if(tmp_value < 0)
		*ptr ++ = '-';

	len = ptr - ptr1;
	*ptr -- = '\0';

	while(ptr1 < ptr)
	{
		tmp_char = *ptr;
		*ptr -- = *ptr1;
		*ptr1 ++ = tmp_char;
	}

	if(len < Length)
	{
		if(*pStr == '-')
			++ pStr;

		memmove(pStr + Length - len, pStr, len + 1);
		memset(pStr, '0', Length - len);
	
		return Length;
	}

	return len;
}

int NuCStrIsNumeric(char *pStr)
{
    if (*pStr == '.')
    {
        ++ pStr;
    }

    do
	{
        if (!isdigit(*pStr))
        {
            return NU_FAIL;
        }

        ++ pStr;
    }
	while(*pStr != '\0');

    return NU_OK;
}

