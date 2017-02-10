
#include "TukanBusApi.h"
#include "stdlib.h"
#include "stdarg.h"
#include <signal.h>

#define SCALE   10000

static int  Start = 0;

static void OnLogon(char *ID, size_t IDLen, char *Msg, size_t MsgLen, void *Argu)
{
    printf("-->LogOn!\n");
    Start = 1;
    return;
}

static void OnLogout(char *ID, size_t IDLen, char *Msg, size_t MsgLen, void *Argu)
{
    printf("-->LogOut!\n");
    Start = 0;
    return;
}

static void OnDataArrive(char *ID, size_t IDLen, char *Msg, size_t MsgLen, void *Argu)
{
    printf("-->DataArrive!\n");
    return;
}

static void OnLog(void *Argu, char *Msg, ...)
{
    va_list ListArgu;

    va_start(ListArgu, Msg);
    vprintf(Msg, ListArgu);
    va_end(ListArgu);

    return;
}

static void Usage(char *Prog)
{
    printf("Usage: %s Who ToWho\n", Prog);
    return;
}

static void OnSignal(int Sig)
{
    printf("Recved Signal[%d]\n", Sig);

    return;
}

int main(int Argc, char **Argv)
{
    int                     Cnt = 0;
    TukanBusApiCB_t         CB;
    struct _TukanBusApi_t   *pApi = NULL;

    signal(SIGPIPE, &OnSignal);

    if(Argc < 3)
    {
        Usage(Argv[0]);
        exit(0);
    }

    CB.OnLogon = &OnLogon;
    CB.OnLogout = &OnLogout;
    CB.OnDataArrive = &OnDataArrive;
    CB.OnLog = &OnLog;
    CB.CBArgu = NULL;

    if((pApi = TukanBusApiNew(NULL, TukanBusSocketType_Internet, "10.220.35.49", 5566, "0", Argv[1], &CB)) == NULL)
    {
        printf("TukanBusApiNew failed!\n");
        return 0;
    }

    while(1)
    {
        sleep(5);
        if(Start)
        {
            Cnt = SCALE;
            while(Cnt --)
            {
                TukanBusApiSend(pApi, Argv[2], "Hello, are you threr? How's going?");
                TukanBusApiSend(pApi, "@MyTopic", "Have told hello.");
            }
        }
    }

    return 0;
}

