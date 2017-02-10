
#ifndef _TUKANBUS_COMMON_H
#define _TUKANBUS_COMMON_H

#ifdef __cplusplus
extern "C" {
#endif

#define TukanBusBuiltInTopic_Online         "@Online"
#define TukanBusBuiltInTopicLen_Online      (sizeof(TukanBusBuiltInTopic_Online) - 1)
#define TukanBusBuiltInTopic_Offline        "@Offline"
#define TukanBusBuiltInTopicLen_Offline     (sizeof(TukanBusBuiltInTopic_Offline) - 1)

#define TukanBusSendRetryTime               3

#ifdef __cplusplus
}
#endif

#endif /* _TUKANBUS_COMMON_H */

