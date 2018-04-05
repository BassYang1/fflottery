using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Lottery.Core
{
    /// <summary>
    /// 通用处理方法
    /// </summary>
    public class Commonn
    {
        /// <summary>
        /// JSON序列化
        /// </summary>
        public static string JsonSerializer<T>(T t)
        {
            var ser = new DataContractJsonSerializer(typeof (T));
            var ms = new MemoryStream();
            ser.WriteObject(ms, t);
            var jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            var ser = new DataContractJsonSerializer(typeof (T));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            var obj = (T) ser.ReadObject(ms);
            return obj;
        }

        /// <summary>
        /// 生成随机数字，默认6位
        /// </summary>
        /// <returns></returns>
        public static String GetRandomNums(int count = 6)
        {
            var ran = new Random();
            var ranNums = string.Empty;

            for (int i = 0; i < count; i++)
            {
                while (true)
                {
                    var num = ran.Next(0, 10);

                    //单个字符不连续出现
                    if (!ranNums.EndsWith(num.ToString("G")))
                    {
                        ranNums += num;
                        break;
                    }
                }
            }

            return ranNums;
        }
        
        /// <summary>
        /// 生成并格式化GUID
        /// </summary>
        /// <returns></returns>
        public static string NewGuid()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}