using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PATSOrder.Utility
{
    //public abstract class XBase
    //{
    //    protected ConcurrentDictionary<string, object> m_Properities = new ConcurrentDictionary<string, object>();

    //    protected T Get<T>([CallerMemberName]string name = null)
    //    {

    //        if (!m_Properities.ContainsKey(name)) { m_Properities.TryAdd(name, default(T)); }
    //        return (T)m_Properities[name];


    //    }
    //    protected T Get<T>(Action beforeAction, [CallerMemberName]string name = null)
    //    {
    //        beforeAction?.Invoke();
    //        return Get<T>(name);
    //    }

    //    protected bool SetQuietly<T>(T value, BeforeInvoker beforeAciton = null, [CallerMemberName]string name = null)
    //    {

    //        if (!m_Properities.ContainsKey(name)) { m_Properities.TryAdd(name, default(T)); }
    //        if (Equals(m_Properities[name], value)) { return false; }
    //        beforeAciton?.Invoker.Invoke(beforeAciton.Args);
    //        m_Properities[name] = value;
    //        return true;

    //    }
    //    //protected bool SetQuietly<T>(T value , Action beforeAction, Action afterAction, [CallerMemberName]string name = null)
    //    //{
    //    //    if (SetQuietly(value, beforeAction, name))
    //    //    {
    //    //        afterAction?.Invoke();
    //    //        return true;
    //    //    }
    //    //    return false;
    //    //}

    //}
}
