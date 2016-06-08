using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memoirs.Common
{
    public interface ILogger
    {
        void Debug(string message);
        void Debug(string messageTemplate, params object[] paramsStrings);
        void Info(string message);
        void Info(string messageTemplate, params object[] paramsStrings);
        void Error(Exception ex);
        void Error(string message);
        void Error(Exception ex, string message);
        void Error(Exception ex, string messageTemplate, params object[] paramsStrings);
        void Fatal(Exception ex);
        void Fatal(Exception ex, string message);
        void Fatal(Exception ex, string messageTemplate, params object[] paramsStrings);
    }
}
