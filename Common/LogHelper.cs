using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4Net.config", Watch = true)]
namespace RenJiCaoZuo
{
    public class LogHelper
    {
        #region static void Debug(Type t, Exception ex)

        public static void Debug(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Warn("Debug", ex);
        }

        #endregion

        #region static void Warn(Type t, string msg)

        public static void Debug(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Debug(msg);
        }

        #endregion

        #region static void Fatal(Type t, Exception ex)

        public static void Fatal(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Warn("Fatal", ex);
        }

        #endregion

        #region static void Fatal(Type t, string msg)

        public static void Fatal(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Fatal(msg);
        }

        #endregion

        #region static void Warn(Type t, Exception ex)

        public static void Warn(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Warn("Warn", ex);
        }

        #endregion

        #region static void Warn(Type t, string msg)

        public static void Warn(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Warn(msg);
        }

        #endregion

        #region static void Info(Type t, Exception ex)

        public static void Info(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Info("Info", ex);
        }

        #endregion

        #region static void Info(Type t, string msg)

        public static void Info(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Info(msg);
        }

        #endregion
        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        #region static void Error(Type t, Exception ex)

        public static void Error(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region static void Error(Type t, string msg)

        public static void Error(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(msg);
        }

        #endregion
    }
}