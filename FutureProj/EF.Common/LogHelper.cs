using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 
        /// </summary>
        static LogHelper()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void ConfigWithFile(string file)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(file));
        }

        /// <summary>
        /// 输出错误信息
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="ex"></param>
        public static void Error<T>(Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(T));
            log.Error("Error", ex);
        }

        /// <summary>
        /// 输出错误信息
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="msg">消息</param>
        public static void Error<T>(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(T));
            log.Error(msg);
        }

        /// <summary>
        /// 输出错误日志
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="formart">消息</param>
        /// <param name="args">参数</param>
        public static void Error<T>(string formart, params object[] args)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(T));
            log.ErrorFormat(formart, args);
        }

        /// <summary>
        /// 输出Info信息
        /// </summary>
        /// <typeparam name="T">调用者类型</typeparam>
        /// <param name="msg">消息</param>
        public static void Info<T>(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(T));
            log.Info(msg);
        }

        /// <summary>
        /// 输出Info日志
        /// </summary>
        /// <typeparam name="T">调用者类型</typeparam>
        /// <param name="formart">消息</param>
        /// <param name="args">参数</param>
        public static void Info<T>(string formart, params object[] args)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(T));
            log.InfoFormat(formart, args);
        }

        /// <summary>
        /// 输出Debug信息
        /// </summary>
        /// <typeparam name="T">调用者类型</typeparam>
        /// <param name="msg">消息</param>
        public static void Debug<T>(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(T));
            log.Debug(msg);
        }

        /// <summary>
        /// 输出Debug日志
        /// </summary>
        /// <typeparam name="T">调用者类型</typeparam>
        /// <param name="formart">消息</param>
        /// <param name="args">参数</param>
        public static void Debug<T>(string formart, params object[] args)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(T));
            log.DebugFormat(formart, args);
        }

        /// <summary>
        /// 输出Warning信息
        /// </summary>
        /// <typeparam name="T">调用者类型</typeparam>
        /// <param name="msg">消息</param>
        public static void Warn<T>(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(T));
            log.Warn(msg);
        }

        /// <summary>
        /// 输出Warning日志
        /// </summary>
        /// <typeparam name="T">调用者类型</typeparam>
        /// <param name="formart">消息</param>
        /// <param name="args">参数</param>
        public static void Warn<T>(string formart, params object[] args)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(T));
            log.WarnFormat(formart, args);
        }

        /// <summary>  
        /// 日志类型  
        /// </summary>  
        public enum LogMessageType
        {
            /// <summary>  
            /// 调试  
            /// </summary>  
            Debug,
            /// <summary>  
            /// 信息  
            /// </summary>  
            Info,
            /// <summary>  
            /// 警告  
            /// </summary>  
            Warn,
            /// <summary>  
            /// 错误  
            /// </summary>  
            Error,
            /// <summary>  
            /// 致命错误  
            /// </summary>  
            Fatal
        }
    }
}
