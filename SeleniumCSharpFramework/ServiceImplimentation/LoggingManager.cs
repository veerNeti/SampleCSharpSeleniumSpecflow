using CSharpUISelenium.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CSharpUISelenium.ServiceImplimentation
{
    public class LoggingManager : ILoggerService
    {
        /*   public void Log(TraceEventType logType, string message, string objectInfo)
           {
               try
               {
                   var log = new LogEntry
                   {
                       EventId = 300,
                       Message = message,
                       Severity = logType
                   };
                   log.Categories.Add("Exception");
                   if (string.IsNullOrWhiteSpace(objectInfo) == false)
                   {
                       var dictionary = new Dictionary<string, object> { { "Object Info:", objectInfo } };
                       log.ExtendedProperties = dictionary;
                   }
                   Logger.Write(log);
               }
               catch
               {
                   // eating the error do not let logging the exception cause a problem
               }
           }*/
    }
}
