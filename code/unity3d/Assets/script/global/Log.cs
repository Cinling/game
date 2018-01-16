using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log {
    public enum LOG_LEVEL {
        DEBUG,
        WARNING,
        ERROR,
    }

    /// <summary>
    /// 2017-09-17 10:19:11 
    /// 日志管理类
    /// </summary>
    /// <param name="cls_name">类名</param>
    /// <param name="func_name">方法名</param>
    /// <param name="info">需要输出的错误信息</param>
    /// <param name="log_level">错误等级</param>
    public static void PrintLog(string cls_name, string func_name, string info, LOG_LEVEL log_level) {
        
        switch (log_level) {
            case LOG_LEVEL.DEBUG:
                Debug.LogFormat("{0:s} DEBUG [[{1:s}.{2:s}] {3:s}]", System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), cls_name, func_name, info);
                break;
            case LOG_LEVEL.WARNING:
                Debug.LogWarningFormat("{0:s} WARNING [[{1:s}.{2:s}] {3:s}]", System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), cls_name, func_name, info);
                break;
            case LOG_LEVEL.ERROR:
                Debug.LogErrorFormat("{0:s} ERROR [[{1:s}.{2:s}] {3:s}]", System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), cls_name, func_name, info);
                break;
        }
    }

    public static void WriteLog() {

    }


    private static string GetLogLevelStr(LOG_LEVEL log_level) {
        switch (log_level) {
            case LOG_LEVEL.WARNING:
                return "Info";

            case LOG_LEVEL.DEBUG:
                return "Debug";

            case LOG_LEVEL.ERROR:
                return "Error";
            default:
                return "Unknown";
        }
    }
}
