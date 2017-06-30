using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
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
    public interface IWrapper
    {
        void Write(Exception ex, string message, LogLevels level);
    }
}
