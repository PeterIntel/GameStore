using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Logging.Loggers
{
    public enum LogLevels
    {
        Trace,
        Debug,
        Info,
        Warm,
        Error,
        Fatal
    }
    public interface ILogWrapper
    {
        void Write(string name, Exception ex, string message, LogLevels level);
    }
}
