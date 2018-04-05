using System;
using System.Threading;
using System.Timers;
using Lottery.DAL;

namespace Lottery.Collect
{
	public class TaskData
	{
		public static void Run()
		{
			TaskData.timerOne.Elapsed += new ElapsedEventHandler(TaskData.timerOne_Elapsed);
			ThreadPool.QueueUserWorkItem(new WaitCallback(TaskData.ThOne_Fun));
		}

		private static void ThOne_Fun(object stateInfo)
		{
			TaskData.timerOne_Elapsed(null, null);
			TaskData.timerOne.Start();
		}

		private static void timerOne_Elapsed(object sender, ElapsedEventArgs e)
		{
			try
			{
				lock (TaskData.obj_locOne)
				{
					int hour = DateTime.Now.Hour;
					if (hour >= 5 && hour <= 6)
					{
						new TiskDAL().TiskOper();
						new LogSysDAL().Save("系统自动", "自动执行了定时任务！");
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception:" + ex.Message);
			}
		}

		private static System.Timers.Timer timerOne = new System.Timers.Timer(600000.0);

		private static object obj_locOne = new object();
	}
}
