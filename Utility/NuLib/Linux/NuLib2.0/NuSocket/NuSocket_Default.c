
/* Default Type */
static int DefaultTypeCB(NuSocketTypeNode_t *TypeNode)
{
    return NuSocketTypeCBPass;
}

static int DefaultTypeCB_NoProtocolCB(NuSocketTypeNode_t *TypeNode)
{
    return NuSocketTypeCBDone;
}

static int DefaultOnEventCB(NuSocketTypeNode_t *TypeNode)
{
    if(TypeNode->ByteLeftForRecv <= 0)
    {
        ioctl(TypeNode->InFD, FIONREAD, &(TypeNode->ByteLeftForRecv));
    }

    return (TypeNode->ByteLeftForRecv > 0) ? NuSocketTypeCBPass: NuSocketTypeCBError;
}

static int DefaultSend(NuSocketTypeNode_t *TypeNode, void *Msg, size_t MsgLen)
{
    int WriteLen = NuSocketTypeCBError;
    int FD = TypeNode->OutFD;

    if(FD > 0)
    {
        do
        {
            if((WriteLen = write(FD, Msg, MsgLen)) <= 0)
            {
                if(errno == EINTR)
                {
                    continue;
                }
                else if(errno == EWOULDBLOCK || errno == EAGAIN)
                {
                    WriteLen = NuSocketTypeCBPass;
                }
                else
                {
                    WriteLen = NuSocketTypeCBError;
                }
            }
        }
        while(0);
    }

    return WriteLen;
}

static int DefaultRecv(NuSocketTypeNode_t *TypeNode, void *Msg, size_t MsgLen)
{
    int     ReadLen = NuSocketTypeCBError;
    int     FD = TypeNode->InFD;

    if(FD > 0)
    {
        do
        {
            if((ReadLen = read(FD, Msg, MsgLen)) < 0)
            {
                if(errno == EINTR)
                {
                    continue;
                }
                else if(errno == EWOULDBLOCK || errno == EAGAIN)
                {
                    ReadLen = NuSocketTypeCBPass;
                }
                else
                {
                    ReadLen = NuSocketTypeCBError;
                }
            }
            else if(ReadLen == 0)
            {
                ReadLen = NuSocketTypeCBError;
            }
        }
        while(0);
    }

    return ReadLen;
}

static NuSocketType_t DefaultNullType = {"DefaultNullType", 0, &DefaultTypeCB, &DefaultTypeCB, &DefaultOnEventCB, &DefaultSend, &DefaultRecv};

static NuSocketType_t *TypeCompletion(NuSocketType_t *Type)
{
    if(!Type)
    {
        Type = &DefaultNullType;
    }
    else
    {
        if(!(Type->SetOnline))
        {
            if(Type->Property & NuSocketTypeProperty_NoOnConnect)
            {
                Type->SetOnline = &DefaultTypeCB_NoProtocolCB;
            }
            else
            {
                Type->SetOnline = &DefaultTypeCB;
            }
        }

        if(!(Type->SetOffline))
        {
            if(Type->Property & NuSocketTypeProperty_NoOnDisconnect)
            {
                Type->SetOffline = &DefaultTypeCB_NoProtocolCB;
            }
            else
            {
                Type->SetOffline = &DefaultTypeCB;
            }
        }
        
        if(!(Type->OnEvent))
        {
            Type->OnEvent = &DefaultOnEventCB;
        }
 
        if(!(Type->Send))
        {
            Type->Send = &DefaultSend;
        }

        if(!(Type->Recv))
        {
            Type->Recv = &DefaultRecv;
        }
    }

    return Type;
}


/* Default Protocol */
static void DefaultProtocolCB(NuSocketNode_t *Node, void *Argu)
{
    return;
}

static NuSocketProtocol_t DefaultNullProtocol = {&DefaultProtocolCB, &DefaultProtocolCB, &DefaultProtocolCB, &DefaultProtocolCB, &DefaultProtocolCB};

static NuSocketProtocol_t *ProtocolCompletion(NuSocketProtocol_t *Protocol)
{
    if(!Protocol)
    {
        Protocol = &DefaultNullProtocol;
    }
    else
    {
        if(!(Protocol->OnConnect))
        {
            Protocol->OnConnect = &DefaultProtocolCB;
        }

        if(!(Protocol->OnDataArrive))
        {
            Protocol->OnDataArrive = &DefaultProtocolCB;
        }

        if(!(Protocol->OnRemoteTimeout))
        {
            Protocol->OnRemoteTimeout = &DefaultProtocolCB;
        }

        if(!(Protocol->OnLocalTimeout))
        {
            Protocol->OnLocalTimeout = &DefaultProtocolCB;
        }

        if(!(Protocol->OnDisconnect))
        {
            Protocol->OnDisconnect = &DefaultProtocolCB;
        }
    }

    return Protocol;
}

/* Defailt LogCB */
static void DefaultLogCB(char *Format, va_list ArguList, void *Argu)
{
    return;
}

