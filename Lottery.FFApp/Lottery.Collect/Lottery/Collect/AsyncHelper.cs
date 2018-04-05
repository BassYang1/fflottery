using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Lottery.Collect
{
	public static class AsyncHelper
	{
		public static void DoAsync(IList dataCollection, int threadCn, WaitCallback processItemMethod)
		{
			AsyncHelper.DoAsync(dataCollection, threadCn, processItemMethod, true);
		}

		public static void DoAsync(IList dataCollection, int threadCn, DoGetObjTask processItemMethod, bool needWaitAll, out Hashtable processResult)
		{
			AsyncHelper.DoAsyncPrivate(dataCollection, threadCn, null, processItemMethod, needWaitAll, true, out processResult);
		}

		public static void DoAsync(IList dataCollection, int threadCn, DoGetObjTask processItemMethod, out Hashtable processResult)
		{
			AsyncHelper.DoAsyncPrivate(dataCollection, threadCn, null, processItemMethod, true, true, out processResult);
		}

		public static void DoAsync(IList dataCollection, int threadCn, WaitCallback processItemMethod, bool needWaitAll)
		{
			Hashtable hashtable;
			AsyncHelper.DoAsyncPrivate(dataCollection, threadCn, processItemMethod, null, needWaitAll, false, out hashtable);
		}

		private static void DoAsyncPrivate(IList dataCollection, int threadCn, WaitCallback processItemMethod, DoGetObjTask getObjMethod, bool needWaitAll, bool hasReturnValue, out Hashtable processResult)
		{
			if (dataCollection == null)
			{
				throw new ArgumentNullException("dataCollection");
			}
			if (threadCn >= 64 || threadCn < 2)
			{
				throw new ArgumentOutOfRangeException("threadCn", "threadCn 参数必须在2和64之间");
			}
			if (threadCn > dataCollection.Count)
			{
				threadCn = dataCollection.Count;
			}
			IList[] array = new ArrayList[threadCn];
			AsyncHelper.DataWithStateList dataWithStateList = new AsyncHelper.DataWithStateList();
			AutoResetEvent[] array2 = new AutoResetEvent[threadCn];
			for (int i = 0; i < threadCn; i++)
			{
				array[i] = new ArrayList();
				array2[i] = new AutoResetEvent(false);
			}
			for (int i = 0; i < dataCollection.Count; i++)
			{
				object obj = dataCollection[i];
				int num = i % threadCn;
				array[num].Add(obj);
				dataWithStateList.Add(new AsyncHelper.DataWithState(obj, AsyncHelper.ProcessState.WaitForProcess));
			}
			AsyncHelper.AsyncContext context = AsyncHelper.AsyncContext.GetContext(threadCn, dataWithStateList, needWaitAll, hasReturnValue, processItemMethod, getObjMethod);
			for (int i = 0; i < threadCn; i++)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(AsyncHelper.DoPrivate), new object[]
				{
					array[i],
					context,
					array2[i]
				});
			}
			if (needWaitAll)
			{
				WaitHandle.WaitAll(array2);
			}
			else
			{
				WaitHandle.WaitAny(array2);
				context.SetBreakSignal();
			}
			processResult = context.ProcessResult;
		}

		private static void DoPrivate(object state)
		{
			object[] array = state as object[];
			IList list = array[0] as IList;
			AsyncHelper.AsyncContext asyncContext = array[1] as AsyncHelper.AsyncContext;
			AutoResetEvent autoResetEvent = array[2] as AutoResetEvent;
			AsyncHelper.DataWithStateList dataWithStates = asyncContext.DataWithStates;
			Thread.CurrentThread.Name = "Thread " + AsyncHelper._threadNo;
			Interlocked.Increment(ref AsyncHelper._threadNo);
			string text = string.Concat(new object[]
			{
				Thread.CurrentThread.Name,
				"[",
				Thread.CurrentThread.ManagedThreadId,
				"]"
			});
			Trace.WriteLine("线程ID:" + text);
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (asyncContext.NeedBreak)
					{
						Trace.WriteLine("线程" + text + "未执行完跳出");
						break;
					}
					object obj = list[i];
					if (dataWithStates.IsWaitForData(obj, true))
					{
						if (asyncContext.NeedBreak)
						{
							Trace.WriteLine("线程" + text + "未执行完跳出");
							break;
						}
						asyncContext.Exec(obj);
						Trace.WriteLine(string.Format("线程{0}处理{1}", text, obj));
					}
				}
			}
			if (asyncContext.NeedWaitAll)
			{
				while (dataWithStates.WaitForDataCount > asyncContext.ThreadCount)
				{
					if (asyncContext.NeedBreak)
					{
						break;
					}
					object obj = dataWithStates.GetWaitForObject();
					if (obj != null && dataWithStates.IsWaitForData(obj, false))
					{
						if (asyncContext.NeedBreak)
						{
							Trace.WriteLine("线程" + text + "未执行完跳出");
							break;
						}
						asyncContext.Exec(obj);
						Trace.WriteLine(string.Format("线程{0}执行另一个进程的数据{1}", text, obj));
					}
				}
			}
			autoResetEvent.Set();
		}

		private static int _threadNo = 0;

		private class AsyncContext
		{
			public static AsyncHelper.AsyncContext GetContext(int threadCn, AsyncHelper.DataWithStateList dataWithStates, bool needWaitAll, bool hasReturnValue, WaitCallback processItemMethod, DoGetObjTask hasReturnValueMethod)
			{
				AsyncHelper.AsyncContext asyncContext = new AsyncHelper.AsyncContext();
				asyncContext.ThreadCount = threadCn;
				asyncContext.DataWithStates = dataWithStates;
				asyncContext.NeedWaitAll = needWaitAll;
				if (hasReturnValue)
				{
					Hashtable processResult = Hashtable.Synchronized(new Hashtable());
					asyncContext.ProcessResult = processResult;
					asyncContext.HasReturnValueMethod = hasReturnValueMethod;
				}
				else
				{
					asyncContext.VoidMethod = processItemMethod;
				}
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
				{
					this._processResult[obj] = result;
				}
			}

			internal void SetBreakSignal()
			{
				if (this.NeedWaitAll)
				{
					throw new NotSupportedException("设定为NeedWaitAll时不可设置BreakSignal");
				}
				this._breakSignal = true;
			}

			internal bool NeedBreak
			{
				get
				{
					return !this.NeedWaitAll && this._breakSignal;
				}
			}

			internal void Exec(object obj)
			{
				if (this.HasReturnValue)
				{
					this.SetReturnValue(obj, this.HasReturnValueMethod(obj));
				}
				else
				{
					this.VoidMethod(obj);
				}
				this.DataWithStates.SetState(obj, AsyncHelper.ProcessState.Processed);
			}

			internal int ThreadCount;

			internal AsyncHelper.DataWithStateList DataWithStates;

			internal bool NeedWaitAll;

			internal bool HasReturnValue;

			internal WaitCallback VoidMethod;

			internal DoGetObjTask HasReturnValueMethod;

			private bool _breakSignal;

			private Hashtable _processResult;
		}

		private enum ProcessState : byte
		{
			WaitForProcess,
			Processing,
			Processed
		}

		private class DataWithStateList : List<AsyncHelper.DataWithState>
		{
			public void SetState(object obj, AsyncHelper.ProcessState state)
			{
				lock (((ICollection)this).SyncRoot)
				{
					AsyncHelper.DataWithState dataWithState = base.Find((AsyncHelper.DataWithState i) => object.Equals(i.Data, obj));
					if (dataWithState != null)
					{
						dataWithState.State = state;
					}
				}
			}

			public AsyncHelper.ProcessState GetState(object obj)
			{
				AsyncHelper.ProcessState state;
				lock (((ICollection)this).SyncRoot)
				{
					AsyncHelper.DataWithState dataWithState = base.Find((AsyncHelper.DataWithState i) => object.Equals(i.Data, obj));
					state = dataWithState.State;
				}
				return state;
			}

			private int GetCount(AsyncHelper.ProcessState state)
			{
				List<AsyncHelper.DataWithState> list = base.FindAll((AsyncHelper.DataWithState i) => i.State == state);
				int result;
				if (list == null)
				{
					result = 0;
				}
				else
				{
					result = list.Count;
				}
				return result;
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
				object result;
				lock (((ICollection)this).SyncRoot)
				{
					AsyncHelper.DataWithState dataWithState = base.Find((AsyncHelper.DataWithState i) => i.State == AsyncHelper.ProcessState.WaitForProcess);
					if (dataWithState == null)
					{
						result = null;
					}
					else
					{
						dataWithState.State = AsyncHelper.ProcessState.Processing;
						result = dataWithState.Data;
					}
				}
				return result;
			}

			internal bool IsWaitForData(object obj, bool setState)
			{
				bool result;
				lock (((ICollection)this).SyncRoot)
				{
					AsyncHelper.DataWithState dataWithState = base.Find((AsyncHelper.DataWithState i) => i.State == AsyncHelper.ProcessState.WaitForProcess);
					if (setState && dataWithState != null)
					{
						dataWithState.State = AsyncHelper.ProcessState.Processing;
					}
					result = (dataWithState != null);
				}
				return result;
			}
		}

		private class DataWithState
		{
			public DataWithState(object data, AsyncHelper.ProcessState state)
			{
				this.Data = data;
				this.State = state;
			}

			public readonly object Data;

			public AsyncHelper.ProcessState State;
		}
	}
}
