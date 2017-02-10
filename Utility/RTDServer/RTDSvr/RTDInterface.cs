using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace RTDSvr
{
    public enum SummaryItem
    {
        BLot,
        ALot,
        BAmt,
        AAmt
    }
    public enum SubType
    { 
        Summary,
        OpenInterest
    }

    [ServiceContract(CallbackContract= typeof(IRTDClnt) )]
    public interface IRTDSvr
    {
        [OperationContract(IsOneWay =true)]
        void Register();

        [OperationContract(IsOneWay = true)]
        void Unregister();

        [OperationContract(IsOneWay = true)]
        void HeartBeat();
    }

    [ServiceContract]
    public interface IRTDClnt
    {
        [OperationContract(IsOneWay = true)]
        void ValueToClnt(string Account, string ComID, SummaryItem Item, object Value);
        [OperationContract(IsOneWay = true)]
        void HeartBeat();        
    }
}
