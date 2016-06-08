using System;
using NLog;
using ILogger = Memoirs.Common.ILogger;

namespace Memoirs.Logging
{
    public class NLogLogger : ILogger
    {
        private Logger _logger;
        public NLogLogger(string currentClassName)
        {
            _logger = LogManager.GetLogger(currentClassName);
        }
        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Debug(string messageTemplate, params object[] paramsStrings)
        {
            _logger.Debug(messageTemplate, paramsStrings);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Info(string messageTemplate, params object[] paramsStrings)
        {
            _logger.Info(messageTemplate, paramsStrings);
        }

        public void Error(Exception ex)
        {
            _logger.Error(ex);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception ex, string message)
        {
            _logger.Error(ex, message);
        }

        public void Error(Exception ex, string messageTemplate, params object[] paramsStrings)
        {
            _logger.Error(ex, messageTemplate, paramsStrings);
        }

        public void Fatal(Exception ex)
        {
            _logger.Fatal(ex);
        }

        public void Fatal(Exception ex, string message)
        {
            _logger.Fatal(ex, message);
        }

        public void Fatal(Exception ex, string messageTemplate, params object[] paramsStrings)
        {
            _logger.Fatal(ex, messageTemplate, paramsStrings);
        }
    }
}
