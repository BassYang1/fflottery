// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.AsyncHelper
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Lottery.DAL
{
    public static class AsyncHelper
    {
        private static int _threadNo;

        public static void DoAsync(IList dataCollection, int threadCn, WaitCallback processItemMethod)
        {
            AsyncHelper.DoAsync(dataCollection, threadCn, processItemMethod, true);
        }

        public static void DoAsync(IList dataCollection, int threadCn, DoGetObjTask processItemMethod, bool needWaitAll, out Hashtable processResult)
        {
            AsyncHelper.DoAsyncPrivate(dataCollection, threadCn, (WaitCallback)null, processItemMethod, needWaitAll, true, out processResult);
        }

        public static void DoAsync(IList dataCollection, int threadCn, DoGetObjTask processItemMethod, out Hashtable processResult)
        {
            AsyncHelper.DoAsyncPrivate(dataCollection, threadCn, (WaitCallback)null, processItemMethod, true, true, out processResult);
        }

        public static void DoAsync(IList dataCollection, int threadCn, WaitCallback processItemMethod, bool needWaitAll)
        {
            Hashtable processResult;
            AsyncHelper.DoAsyncPrivate(dataCollection, threadCn, processItemMethod, (DoGetObjTask)null, needWaitAll, false, out processResult);
        }

        private static void DoAsyncPrivate(IList dataCollection, int threadCn, WaitCallback processItemMethod, DoGetObjTask getObjMethod, bool needWaitAll, bool hasReturnValue, out Hashtable processResult)
        {
            if (dataCollection == null)
                throw new ArgumentNullException("dataCollection");
            if (threadCn >= 64 || threadCn < 2)
                throw new ArgumentOutOfRangeException("threadCn", "threadCn 参数必须在2和64之间");
            if (threadCn > dataCollection.Count)
                threadCn = dataCollection.Count;
            IList[] listArray = (IList[])new ArrayList[threadCn];
            AsyncHelper.DataWithStateList dataWithStates = new AsyncHelper.DataWithStateList();
            AutoResetEvent[] autoResetEventArray = new AutoResetEvent[threadCn];
            for (int index = 0; index < threadCn; ++index)
            {
                listArray[index] = (IList)new ArrayList();
                autoResetEventArray[index] = new AutoResetEvent(false);
            }
            for (int index1 = 0; index1 < dataCollection.Count; ++index1)
            {
                object data = dataCollection[index1];
                int index2 = index1 % threadCn;
                listArray[index2].Add(data);
                dataWithStates.Add(new AsyncHelper.DataWithState(data, AsyncHelper.ProcessState.WaitForProcess));
            }
            AsyncHelper.AsyncContext context = AsyncHelper.AsyncContext.GetContext(threadCn, dataWithStates, needWaitAll, hasReturnValue, processItemMethod, getObjMethod);
            for (int index = 0; index < threadCn; ++index)
                ThreadPool.QueueUserWorkItem(new WaitCallback(AsyncHelper.DoPrivate), (object)new object[3]
        {
          (object) listArray[index],
          (object) context,
          (object) autoResetEventArray[index]
        });
            if (needWaitAll)
            {
                WaitHandle.WaitAll((WaitHandle[])autoResetEventArray);
            }
            else
            {
                WaitHandle.WaitAny((WaitHandle[])autoResetEventArray);
                context.SetBreakSignal();
            }
            processResult = context.ProcessResult;
        }

        private static void DoPrivate(object state)
        {
            object[] objArray = state as object[];
            IList list = objArray[0] as IList;
            AsyncHelper.AsyncContext asyncContext = objArray[1] as AsyncHelper.AsyncContext;
            AutoResetEvent autoResetEvent = objArray[2] as AutoResetEvent;
            AsyncHelper.DataWithStateList dataWithStates = asyncContext.DataWithStates;
            Thread.CurrentThread.Name = "Thread " + (object)AsyncHelper._threadNo;
            Interlocked.Increment(ref AsyncHelper._threadNo);
            string str = Thread.CurrentThread.Name + "[" + (object)Thread.CurrentThread.ManagedThreadId + "]";
            Trace.WriteLine("线程ID:" + str);
            if (list != null)
            {
                for (int index = 0; index < list.Count; ++index)
                {
                    if (asyncContext.NeedBreak)
                    {
                        Trace.WriteLine("线程" + str + "未执行完跳出");
                        break;
                    }
                    object obj = list[index];
                    if (dataWithStates.IsWaitForData(obj, true))
                    {
                        if (asyncContext.NeedBreak)
                        {
                            Trace.WriteLine("线程" + str + "未执行完跳出");
                            break;
                        }
                        asyncContext.Exec(obj);
                        Trace.WriteLine(string.Format("线程{0}处理{1}", (object)str, obj));
                    }
                }
            }
            if (asyncContext.NeedWaitAll)
            {
                while (dataWithStates.WaitForDataCount > asyncContext.ThreadCount && !asyncContext.NeedBreak)
                {
                    object waitForObject = dataWithStates.GetWaitForObject();
                    if (waitForObject != null && dataWithStates.IsWaitForData(waitForObject, false))
                    {
                        if (asyncContext.NeedBreak)
                        {
                            Trace.WriteLine("线程" + str + "未执行完跳出");
                            break;
                        }
                        asyncContext.Exec(waitForObject);
                        Trace.WriteLine(string.Format("线程{0}执行另一个进程的数据{1}", (object)str, waitForObject));
                    }
                }
            }
            autoResetEvent.Set();
        }

        private class AsyncContext
        {
            internal int ThreadCount;
            internal AsyncHelper.DataWithStateList DataWithStates;
            internal bool NeedWaitAll;
            internal bool HasReturnValue;
            internal WaitCallback VoidMethod;
            internal DoGetObjTask HasReturnValueMethod;
            private bool _breakSignal;
            private Hashtable _processResult;

            public static AsyncHelper.AsyncContext GetContext(int threadCn, AsyncHelper.DataWithStateList dataWithStates, bool needWaitAll, bool hasReturnValue, WaitCallback processItemMethod, DoGetObjTask hasReturnValueMethod)
            {
                AsyncHelper.AsyncContext asyncContext = new AsyncHelper.AsyncContext();
                asyncContext.ThreadCount = threadCn;
                asyncContext.DataWithStates = dataWithStates;
                asyncContext.NeedWaitAll = needWaitAll;
                if (hasReturnValue)
                {
                    Hashtable hashtable = Hashtable.Synchronized(new Hashtable());
                    asyncContext.ProcessResult = hashtable;
                    asyncContext.HasReturnValueMethod = hasReturnValueMethod;
                }
                else
                    asyncContext.VoidMethod = processItemMethod;
                asyncContext.HasReturnValue = hasReturnValue;
                return asyncContext;
            }

            internal Hashtable ProcessResult
            {
                get
                {
                    return this._processResult;
                }
                set
                {
                    this._processResult = value;
                }
            }

            internal void SetReturnValue(object obj, object result)
            {
                lock (this._processResult.SyncRoot)
                    this._processResult[obj] = result;
            }

            internal void SetBreakSignal()
            {
                if (this.NeedWaitAll)
                    throw new NotSupportedException("设定为NeedWaitAll时不可设置BreakSignal");
                this._breakSignal = true;
            }

            internal bool NeedBreak
            {
                get
                {
                    if (!this.NeedWaitAll)
                        return this._breakSignal;
                    return false;
                }
            }

            internal void Exec(object obj)
            {
                if (this.HasReturnValue)
                    this.SetReturnValue(obj, this.HasReturnValueMethod(obj));
                else
                    this.VoidMethod(obj);
                this.DataWithStates.SetState(obj, AsyncHelper.ProcessState.Processed);
            }
        }

        private enum ProcessState : byte
        {
            WaitForProcess,
            Processing,
            Processed,
        }

        private class DataWithStateList : List<AsyncHelper.DataWithState>
        {
            public void SetState(object obj, AsyncHelper.ProcessState state)
            {
                lock (((ICollection)this).SyncRoot)
                {
                    AsyncHelper.DataWithState dataWithState = this.Find((Predicate<AsyncHelper.DataWithState>)(i => object.Equals(i.Data, obj)));
                    if (dataWithState == null)
                        return;
                    dataWithState.State = state;
                }
            }

            public AsyncHelper.ProcessState GetState(object obj)
            {
                lock (((ICollection)this).SyncRoot)
                    return this.Find((Predicate<AsyncHelper.DataWithState>)(i => object.Equals(i.Data, obj))).State;
            }

            private int GetCount(AsyncHelper.ProcessState state)
            {
                List<AsyncHelper.DataWithState> all = this.FindAll((Predicate<AsyncHelper.DataWithState>)(i => i.State == state));
                if (all == null)
                    return 0;
                return all.Count;
            }

            public int WaitForDataCount
            {
                get
                {
                    return this.GetCount(AsyncHelper.ProcessState.WaitForProcess);
                }
            }

            internal object GetWaitForObject()
            {
                lock (((ICollection)this).SyncRoot)
                {
                    AsyncHelper.DataWithState dataWithState = this.Find((Predicate<AsyncHelper.DataWithState>)(i => i.State == AsyncHelper.ProcessState.WaitForProcess));
                    if (dataWithState == null)
                        return (object)null;
                    dataWithState.State = AsyncHelper.ProcessState.Processing;
                    return dataWithState.Data;
                }
            }

            internal bool IsWaitForData(object obj, bool setState)
            {
                lock (((ICollection)this).SyncRoot)
                {
                    AsyncHelper.DataWithState dataWithState = this.Find((Predicate<AsyncHelper.DataWithState>)(i => i.State == AsyncHelper.ProcessState.WaitForProcess));
                    if (setState && dataWithState != null)
                        dataWithState.State = AsyncHelper.ProcessState.Processing;
                    return dataWithState != null;
                }
            }
        }

        private class DataWithState
        {
            public readonly object Data;
            public AsyncHelper.ProcessState State;

            public DataWithState(object data, AsyncHelper.ProcessState state)
            {
                this.Data = data;
                this.State = state;
            }
        }
    }
}
