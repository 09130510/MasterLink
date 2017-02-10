//#define NET40_BEFORE
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;
#if !NET40_BEFORE
using System.Collections.Concurrent;
#endif

namespace NuDotNet
{

#if NET40_BEFORE
    public class NuQueue<T>
    {
        #region private variable
        private readonly int _maxCnt = 1000;
        private readonly bool _limit = true;
        private Queue<T> _queue;
        #endregion

        #region --  construct / destruct  --
        /// <summary>
        /// 建構式
        /// </summary>
        public NuQueue()
        {
            _maxCnt = 0;
            _limit = false;
            _queue = new Queue<T>();
        }
        /// <summary>
        /// 建構式 
        /// </summary>
        /// <param name="maxCnt">queue 容量個數</param>
        public NuQueue(int maxCnt)
        {
            _maxCnt = maxCnt;
            _limit = true;
            _queue = new Queue<T>(_maxCnt);
        }

        ~NuQueue() { }
        #endregion

        #region property
        /// <summary>
        /// 資料個數
        /// </summary>
        public int Count { get { return _queue.Count; } }
        #endregion

        #region Enqueue
        /// <summary>
        /// 加入資料到queue中
        /// </summary>
        /// <param name="item"></param>
        public void Enqueue(T item)
        {
            lock (_queue)
            {

                while (_limit &&
                       (_queue.Count >= _maxCnt))
                {
                    Monitor.Wait(_queue);
                }

                _queue.Enqueue(item);

                if (_limit &&
                    (_queue.Count == 1))
                {
                    Monitor.PulseAll(_queue);
                }

            }
        }

        public bool TryEnqueue(T item)
        {
            lock (_queue)
            {
                if (_limit &&
                       (_queue.Count >= _maxCnt))
                {
                    return false;
                }

                _queue.Enqueue(item);

                if (_limit &&
                    (_queue.Count == 1))
                {
                    Monitor.PulseAll(_queue);
                }
            }
            return true;
        }
        #endregion

        #region Dequeue
        /// <summary>
        /// 從queue中取出資料  (blocking)
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            lock (_queue)
            {
                T item = default(T);
                while (_limit && 
                       (_queue.Count == 0))
                    Monitor.Wait(_queue);

                if (_queue.Count != 0)
                    item = _queue.Dequeue();

                if (_maxCnt != 0)
                    if (_limit && 
                        (_queue.Count == (_maxCnt - 1)))
                        Monitor.PulseAll(_queue); 
                        //Monitor.Pulse(_queue); 
                        

                return item;
            }
        }
        
        /// <summary>
        /// 從queue中取出資料  (blocking)
        /// </summary>
        /// <param name="item">Data</param>
        public void Dequeue(out T item)
        {
            lock (_queue)
            {
                while (_limit &&
                       (_queue.Count == 0))
                    Monitor.Wait(_queue);

                if (_queue.Count != 0)
                    item = _queue.Dequeue();
                else
                    item = default(T);

                if (_limit &&
                    (_queue.Count == (_maxCnt - 1)))
                    Monitor.PulseAll(_queue);
            }
        }

        /// <summary>
        /// 從queue中取出資料, 等待一段時間後無資料則return false
        /// </summary>
        /// <param name="item">out Data</param>
        /// <param name="ms_wait">wait millisecond</param>
        /// <returns>bool</returns>
        public bool Dequeue(out T item, int ms_wait)
        {
            lock (_queue)
            {
                if (_limit &&
                    (_queue.Count == 0))
                {
                    Monitor.Wait(_queue, ms_wait);
                }

                if (_queue.Count == 0)
                {
                    item = default(T);
                    return false;
                }

                item = _queue.Dequeue();

                if (_limit &&
                    (_queue.Count == (_maxCnt - 1)))
                    Monitor.PulseAll(_queue);

                return true;
            }
        }

        /// <summary>
        /// 嘗試取出資料, 如果無資料則馬上回復false
        /// </summary>
        /// <param name="item">out Data</param>
        /// <returns></returns>
        public bool TryDequeue(out T item)
        {
            lock (_queue)
            {
                if (_queue.Count > 0)
                {
                    item = _queue.Dequeue();
                    return true;
                }
                else
                {
                    item = default(T);
                    return false;
                }
                
            }
        }
        #endregion

        #region TrimExcess
        /// <summary>
        /// Trim queue buffer
        /// </summary>
        public void TrimExcess()
        {
            lock (_queue)
            {
                _queue.TrimExcess();
            }
            return;
        }
        #endregion
    }
#else
    public class NuQueue<T> : IDisposable
    {
        #region private variable
        private bool bDisposed = false;
        private BlockingCollection<T> m_que;
        private CancellationTokenSource m_token;
        #endregion

        #region --  construct / destruct  --
        /// <summary>
        /// 建構式
        /// </summary>
        public NuQueue()
        {
            m_que = new BlockingCollection<T>();
            m_token = new CancellationTokenSource();
        }
        /// <summary>
        /// 建構式 
        /// </summary>
        /// <param name="maxCnt">queue 容量個數</param>
        public NuQueue(int maxCnt)
        {
            m_que = new BlockingCollection<T>(new ConcurrentQueue<T>(), maxCnt);
            m_token = new CancellationTokenSource();
        }

        ~NuQueue() 
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); //要求系統不要呼叫指定物件的完成項。
        }

        protected virtual void Dispose(bool IsDisposing)
        {
            if (bDisposed)
                return;

            if (IsDisposing)
            {
                if (m_que != null) 
                    m_que.Dispose();
            }
            bDisposed = true;
        }
        #endregion

        #region property
        /// <summary>
        /// 資料個數
        /// </summary>
        public int Count { get { return m_que.Count; } }
        #endregion

        #region Enqueue
        /// <summary>
        /// 加入資料到queue中
        /// </summary>
        /// <param name="item"></param>
        public void Enqueue(T item)
        {
            m_que.Add(item);
        }

        public bool TryEnqueue(T item)
        {
            return m_que.TryAdd(item);
        }
        #endregion

        #region Dequeue
        /// <summary>
        /// 從queue中取出資料  (blocking)
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            try
            {
                return m_que.Take(m_token.Token);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// 從queue中取出資料  (blocking)
        /// </summary>
        /// <param name="item">Data</param>
        public void Dequeue(out T item)
        {
            item = m_que.Take(m_token.Token);
        }

        /// <summary>
        /// 從queue中取出資料, 等待一段時間後無資料則return false
        /// </summary>
        /// <param name="item">out Data</param>
        /// <param name="ms_wait">wait millisecond</param>
        /// <returns>bool</returns>
        public bool Dequeue(out T item, int ms_wait)
        {
            return m_que.TryTake(out item, ms_wait, m_token.Token);
        }

        /// <summary>
        /// 嘗試取出資料, 如果無資料則馬上回復false
        /// </summary>
        /// <param name="item">out Data</param>
        /// <returns></returns>
        public bool TryDequeue(out T item)
        {
            return m_que.TryTake(out item, 0, m_token.Token);
        }
        #endregion

        #region TrimExcess
        /// <summary>
        /// Trim queue buffer
        /// </summary>
        public void TrimExcess()
        {
        }
        #endregion

        /// <summary>
        /// 解除鎖有Blocking狀態
        /// </summary>
        public void CancelAllBlocking()
        {
            m_token.Cancel();
        }
    }
#endif
}
