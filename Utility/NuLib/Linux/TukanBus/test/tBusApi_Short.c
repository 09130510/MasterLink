
#include "TukanBusApi.h"
#include "stdlib.h"
#include "stdarg.h"
#include <signal.h>

struct _TukanBusApi_t   *pApi = NULL;
static void OnLogon(char *ID, size_t IDLen, char *Msg, size_t MsgLen, void *Argu)
{
    printf("-->LogOn!\n");
    sleep(1);
    TukanBusApiDisconnect(pApi, "Bye");
    printf("Bye!\n");
    return;
}

static void OnLogout(char *ID, size_t IDLen, char *Msg, size_t MsgLen, void *Argu)
{
    printf("-->LogOut!\n");
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
    printf("Usage: %s Who\n", Prog);
    return;
}

static void OnSignal(int Sig)
{
    printf("Recved Signal[%d]\n", Sig);

    return;
}

int main(int Argc, char **Argv)
{
    TukanBusApiCB_t         CB;

    signal(SIGPIPE, &OnSignal);

    if(Argc < 2)
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
        sleep(10);
    }

    return 0;
}

