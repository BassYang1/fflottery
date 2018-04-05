using log4net;
using Lottery.Collect.Sys;
//using Lottery.DAL;
using Lottery.DAL.Flex;
using Lottery.WinService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            Log.Debug("开始");


            TestSrv srv = new TestSrv();
            srv.TestStart();

            Console.ReadKey();
        }


        /// <summary>
        /// 生成随机数字, 不允许重复
        /// </summary>
        /// <returns></returns>
        public static string[] GetRandomNums(string[] source, int count, bool repeatable)
        {
            if (source == null || source.Length <= 0 || count <= 0 || (!repeatable && source.Length < count))
            {
                return null;
            }

            string[] ranNums = new string[count];
            var ran = new Random(GetRandomSeed());

            for (int i = 0; i < count; i++)
            {
                while (true)
                {
                    var index = ran.Next(0, source.Length);
                    var temp = source[index];

                    if (repeatable || !ranNums.Contains(temp))
                    {
                        ranNums[i] = temp;
                        break;
                    }
                }
            }

            return ranNums;
        }

        private static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

    }
}
