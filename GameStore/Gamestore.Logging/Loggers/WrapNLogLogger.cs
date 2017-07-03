using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace GameStore.Logging.Loggers
{
    public class WrapNLogLogger : ILogWrapper
    {
        private ILogger _logger;

        public void Write(string name, Exception ex, string message, LogLevels level)
        {
            if(name != null)
            {
                _logger = LogManager.GetLogger(name);
            }
            else
            {
                _logger = LogManager.GetCurrentClassLogger();
            }

            switch (level)
            {
                case LogLevels.Trace:
                    _logger.Trace(ex, message);
                    break;
                case LogLevels.Debug:
                    _logger.Debug(ex, message);
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
