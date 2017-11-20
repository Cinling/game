using System.Threading;
using System.Collections.Generic;

public class ThreadCtrl {
    // 线程状态
    public enum THREAD_STATUS {
        START,
        STOP,
        END
    }
    public static Dictionary<short, THREAD_STATUS> threadCtrlDict = new Dictionary<short, THREAD_STATUS>();

    public const byte THREAD_STATUS_START = 1;
    public const byte THREAD_STATUS_STOP = 2;
    public const byte THREAD_STATUS_END = 3;

    /// <summary>
    /// 2017-11-20 10:45:11
    /// 开启一个线程
    /// </summary>
    /// <param name="tag">线程号</param>
    /// <param name="lambda">每个逻辑帧执行的方法</param>
    /// <returns></returns>
    public static bool AddThead(short tag, System.Func<short> lambda) {

        threadCtrlDict[tag] = THREAD_STATUS.START;

        new Thread(() => {

            while (true) {
                THREAD_STATUS status = threadCtrlDict[tag];

                if (status == THREAD_STATUS.START) {
                    lambda();
                    Thread.Sleep(10);
                    continue;
                } else if (status == THREAD_STATUS.STOP) {
                    Thread.Sleep(500);
                    continue;
                } else if (status == THREAD_STATUS.END) {
                    break;
                }

                Log.PrintLog("ThreadCtrl", "StartThead", "unknow value: THREAD_STATUS", Log.LOG_LEVEL.ERROR);
                break;
            }
        }).Start(tag);

        return true;
    }


    public static bool ReStartThread() {
        foreach (KeyValuePair<short, THREAD_STATUS> kv in threadCtrlDict) {
            threadCtrlDict[kv.Key] = THREAD_STATUS.START;
        }

        return true;
    }


    /// <summary>
    /// 2017-11-20 10:54:11
    /// 停止（暂停）所有逻辑线程
    /// </summary>
    /// <returns></returns>
    public static bool StopThread() {

        foreach (KeyValuePair<short, THREAD_STATUS> kv in threadCtrlDict) {
            threadCtrlDict[kv.Key] = THREAD_STATUS.STOP;
        }

        return true;
    }
}
