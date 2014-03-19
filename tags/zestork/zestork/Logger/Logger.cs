using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;

namespace SunPower.Common.Infrastructure.Logger
{
    public class Logger : ILogger
    {
        private string _currentClassName;
        private string _userName;
        ILog logger = null;
        public Logger(string currentClassName)
        {
            this._currentClassName = currentClassName;
            logger = LogManager.GetLogger(_currentClassName);
            BasicConfigurator.Configure();
            log4net.Config.XmlConfigurator.Configure();
        }       

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Error(string message, Exception ex)
        {
            logger.Error(message, ex);
        }

        public void Debug(string message, Exception ex)
        {
            
            logger.Debug(message, ex);
        }
       
        public void Fatal(string message, Exception ex)
        {
            logger.Fatal(message, ex);
        }
       

    }
}
