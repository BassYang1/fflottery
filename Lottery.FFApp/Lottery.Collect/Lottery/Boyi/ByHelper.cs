using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace Lottery.Collect.Boyi
{
    public static class ByHelper
    {
        /// <summary>
        /// Log instance.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(ByHelper));

        /// <summary>
        /// 获取开奖数据
        /// </summary>
        /// <param name="apiUrl">开奖API</param>
        /// <returns></returns>
        public static IList<ByLottery> GetLotteryData(string apiUrl)
        {
            if (string.IsNullOrEmpty(apiUrl))
            {
                throw new Exception("无效的API地址");
            }

            string response = HtmlOperate2.HttpGet(apiUrl, Encoding.UTF8);

            if (response.IndexOf("data") < 0 && response.IndexOf("opentime") < 0 && response.IndexOf("expect") < 0 && response.IndexOf("opencode") < 0)
            {
                throw new Exception("无效的开奖数据:" + response);
            }

            ByResponse obj = JsonDeserialize<ByResponse>(response);

            if (obj == null || obj.data == null || obj.data.Count <= 0)
            {
                throw new Exception("开奖数据转换异常");
            }

            IEnumerable<ByLottery> lts = from lt in obj.data where string.IsNullOrEmpty(lt.opentime) || string.IsNullOrEmpty(lt.opencode) || string.IsNullOrEmpty(lt.expect) select lt;

            if (lts.Count() > 0)
            {
                throw new Exception("开奖数据不能为空");
            }

            Log.DebugFormat("访问API: {0} {1} {1} 开奖数据: {2}", apiUrl, Environment.NewLine, response);

            return obj.data;
        }

        /// <summary>
        /// JSON序列化
        /// </summary>
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
    }
}