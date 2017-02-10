
#include "NuCommon.h"

#ifndef _NUCSTR_H
#define _NUCSTR_H

#ifdef __cplusplus
extern "C" {
#endif

/* extensible string */
/** @defgroup NuCStrAPI [Utility] common string function
 *  it provide more efficient and useful function.
 *  @{
 * */

/** 
 * @brief convert hex string to unsigned int.
 *
 * @note Hex string format is "0x38000306"
 *
 * @param  [in] szHex       : Hex string
 *
 * @return unsigned int
 */ 
unsigned int NuCStrHex2UInt(char *szHex);

/** 
 * @brief replace the character in specify range
 *
 * @note it will wreck the input string.
 *
 * @param  [in] pstr        : input string.
 * @param  [in] cChr        : character which be replaced
 * @param  [in] cNewChr     : character which replace
 * @param  [in] start       : the start position that input string be affected
 * @param  [in] len         : affect length
 *
 * @return void
 */ 
void NuCStrReplaceRangeChr(char *pstr, char cChr, char cNewChr, int start, size_t len);

/** 
 * @brief replace the character
 *
 * @note it will wreck the input string.
 *
 * @param  [in] pstr        : input string.
 * @param  [in] cChr        : character which be replaced
 * @param  [in] cNewChr     : character which replace
 *
 * @return void
 */ 
void NuCStrReplaceChr(char *pstr, char cChr, char cNewChr);

/** 
 * @brief right trim the specify character 
 *
 * @note it will wreck the input string.
 *
 * @param  [in] pstr        : input string.
 * @param  [in] cChr        : character which be replaced
 *
 * @return void
 */ 
void NuCStrRTrimChr(char *pstr, char cChr);

/** 
 * @brief left trim the specify character 
 *
 * @note it will wreck the input string.
 *
 * @param  [in] pstr        : input string.
 * @param  [in] cChr        : character which be replaced
 *
 * @return void
 */ 
void NuCStrLTrimChr(char *pstr, char cChr);

/** 
 * @brief sprintf like
 *
 * @note it will wreck the input string and input string not allow be a format parameter.
 *
 * @param  [in] pstr        : input string
 * @param  [in] format      : string format 
 * @param  [in] ...         : format parameter
 *
 * @return int
 * @retval length : string length
 */ 
int NuCStrPrintf(char *pstr, const char *format, ...);

/** 
 * @brief convert int to string
 *
 * @note it will wreck the input string.\n
 *       NuCStrPrintfInt(pstr, 12, 5) like sprintf(pstr, "%05d", 12) \n
 *       if you assign length equal 0, it means "%d"
 *
 * @param  [in] pStr        : input string
 * @param  [in] Value       : integet
 * @param  [in] Length      : string length, left padding '0'
 *
 * @return int
 * @retval length : string length
 */ 
int NuCStrPrintInt(char *pStr, int Value, int Length);

/** 
 * @brief convert long to string
 *
 * @note it will wreck the input string.\n
 *       NuCStrPrintfLong(pstr, 12, 5) like sprintf(pstr, "%05ld", 12) \n
 *       if you assign length equal 0, it means "%ld"
 *
 * @param  [in] pstr        : input string
 * @param  [in] Value       : long    
 * @param  [in] Length      : string length, left padding '0'
 *
 * @return int
 * @retval length : string length
 */ 
int NuCStrPrintLong(char *pStr, long Value, int Length);

int NuCStrIsNumeric(char *pStr);
/** @} */

#ifdef __cplusplus
}
#endif

#endif /* _NUSCTR_H */

