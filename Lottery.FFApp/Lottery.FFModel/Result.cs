using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.FFModel
{
    /// <summary>
    /// 请求响应
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Description("请求响应")]
    public class Result<T>
    {
        /// <summary>
        /// 处理结果
        /// </summary>
        [Description("处理结果")]
        public T Data { get; set; }

        private string _statusCode = string.Empty;

        [Description("请求状态码")]
        public string Code
        {
            get
            {
                if (string.IsNullOrEmpty(_statusCode))
                {
                    _statusCode = "200";
                }

                return _statusCode;
            }
            set
            {
                _statusCode = value;
            }
        }

        /// <summary>
        /// 异常提示信息
        /// </summary>
        [Description("异常提示信息")]
        public string Message { get; set; }
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    [Description("分页查询")]
    public class PageData<T>
    {
        public PageData(int pageSize, int pageIndex)
        {
            Result = new List<T>();
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPage = 0;
            TotalCount = 0;
        }

        public PageData(List<T> data, int pageSize, int pageIndex, int totalCount)
        {
            if (data != null && data.Count > 0)
            {
                Result = data;
                PageIndex = pageIndex;
                PageSize = pageSize;
                TotalCount = totalCount;
                TotalPage = totalCount / pageSize + (totalCount % pageSize > 0 ? 1 : 0);
            }
        }

        /// <summary>
        /// 处理结果
        /// </summary>
        [Description("分页数据")]
        public List<T> Result { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        [Description("记录总数")]
        public int? TotalCount { get; set; }

        /// <summary>
        /// 分页总数
        /// </summary>
        [Description("分页总数")]
        public int? TotalPage { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        [Description("分页大小")]
        public int? PageSize { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        [Description("当前页")]
        public int? PageIndex { get; set; }
    }
}
