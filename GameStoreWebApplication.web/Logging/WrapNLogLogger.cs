using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Logging
{
    public class WrapNLogLogger : IWrapper
    {
        ILogger _logger = LogManager.GetCurrentClassLogger();
        //public WrapNLogLogger(string logger)
        //{

        //}
        public void Write(Exception ex, string message, LogLevels level)
        {
            switch (level)
            {
                case LogLevels.Trace:
                    _logger.Trace(ex, message);
                    break;
                case LogLevels.Debug:
                    _logger.Trace(ex, message);
                    break;
                case LogLevels.Info:
                    _logger.Info(ex, message);
                    break;
                case LogLevels.Warm:
                    _logger.Warn(ex, message);
                    break;
                case LogLevels.Error:
                    _logger.Error(ex, message);
                    break;
                case LogLevels.Fatal:
                    _logger.Fatal(ex, message);
                    break;
            }
        }

    }
}
