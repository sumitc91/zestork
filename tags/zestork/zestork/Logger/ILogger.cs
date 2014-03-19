using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPower.Common.Infrastructure.Logger
{
   public interface ILogger
    {
         void Info(string message);
         void Error(string message, Exception ex);
         void Debug(string message, Exception ex);
         void Fatal(string message, Exception ex);         
    }
}
