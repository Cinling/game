using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log {
    public enum LOG_LEVEL {
        INFO = 1,
        DEBUG,
        ERROR,
    }

    /// <summary>
    /// 2017-09-17 10:19:11 
    /// 日志管理类
    /// </summary>
    /// <param name="cls_name"></param>
    /// <param name="func_name"></param>
    /// <param name="info"></param>
    /// <param name="log_level"></param>
    public static void PrintLog(string cls_name, string func_name, string info, LOG_LEVEL log_level) {
        Debug.Log(string.Format("{0:s} {1:s} [[{2:s}.{3:s}] {4:s}]", System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), GetLogLevelStr(log_level), cls_name, func_name, info));
    }

    public static void WriteLog() {

    }


    private static string GetLogLevelStr(LOG_LEVEL log_level) {
        switch (log_level) {
            case LOG_LEVEL.INFO:
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
