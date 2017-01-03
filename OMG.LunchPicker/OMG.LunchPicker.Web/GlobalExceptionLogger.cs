using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace OMG.LunchPicker.Web
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        private static readonly ILog Log4Net = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override void Log(ExceptionLoggerContext context)
        {
            Log4Net.Error($"Unhandled exception thrown in {context.Request.Method}\n" +
                          $"\nUser: {HttpContext.Current.User?.Identity?.Name}" +
                          $"\nUri: {context.Request.RequestUri}" +
                          $"\nVersion: {Assembly.GetExecutingAssembly().GetName().Version}\n" +
                          $"\nHeaders: {context.Request.Headers}" +
                          $"\nException: {context.Exception}");
        }
    }
}