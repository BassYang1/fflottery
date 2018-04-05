using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Lottery.FFData;

namespace Lottery.Service
{
    /// <summary>
    /// 服务父类型
    /// </summary>
    public class BaseService
    {
        /// <summary>
        /// Log instance.
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(typeof(BaseService));

        /// <summary>
        /// 记录DbEntityValidationException异常到日志
        /// </summary>
        /// <param name="dbEx"></param>
        protected virtual void LogDatabaseError(DbEntityValidationException dbEx)
        {
            if (dbEx != null)
            {
                var entityValidationError = dbEx.EntityValidationErrors.FirstOrDefault();

                if (entityValidationError != null)
                {
                    var validationError = entityValidationError.ValidationErrors.FirstOrDefault();

                    if (validationError != null)
                    {
                        Log.Error(string.Format("保存字段{0}出错：{1}", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }
            }
        }

        /// <summary>
        /// 提交数据库变更
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <returns></returns>
        protected virtual int SaveDbChanges(TicketEntities dbContext)
        {
            try
            {
                return dbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                LogDatabaseError(ex);
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取会页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        protected List<T> GetPagingData<T>(List<T> source, int pageSize, int pageIndex, out int totalCount)
        {
            if (source == null || source.Count < 1)
            {
                totalCount = 0;
                return new List<T>();
            }

            totalCount = source.Count;

            return source.Skip(pageIndex*pageSize).Take(pageSize).ToList();
        }
    }
}
