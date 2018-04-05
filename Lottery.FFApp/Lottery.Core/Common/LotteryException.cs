using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Core
{
    class LotteryException : Exception
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(LotteryException));

        public LotteryException(string message, Exception ex)
            : base(message, ex)
        {
            if (ex != null)
            {
                _log.Error(ex.Message);
                _log.Error(ex);
            }
        }

        public LotteryException(ILog log, string message, Exception ex)
            : base(message, ex)
        {
            _log = log ?? _log;

            if (ex != null)
            {
                _log.Error(ex.Message);
                _log.Error(ex);
            }
        }

        public LotteryException(string message)
            : base(message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                _log.Error(message);
            }
        }

        public LotteryException(ILog log, string message) : base(message)
        {
            _log = log ?? _log;

            if (!string.IsNullOrEmpty(message))
            {
                _log.Error(message);
            }
        }
    }
}
