using System;
using System.Threading;
using System.Collections.Generic;

public class ThreadCtrl {

    // 单例对象
    private static ThreadCtrl theadCtrl = null;
    public static ThreadCtrl GetInstance() {

        if (theadCtrl == null) {
            theadCtrl = new ThreadCtrl();
        }
        return theadCtrl;
    }

    // 线程号
    public static class THREAD_NUM{
        public const short LOGIC_ROLE = 101;  // 角色进程号
    }


    // 线程状态
    public enum THREAD_STATUS {
        START,STOP,END
    }
    private Dictionary<short, THREAD_STATUS> threadCtrlDict = new Dictionary<short, THREAD_STATUS>();
    private Dictionary<short, THREAD_STATUS> GetThreadCtrlDict() {
        return threadCtrlDict;
    }


    /// <summary>
    /// 2017-11-20 10:45:11
    /// 开启一个线程
    /// </summary>
    /// <param name="tag">线程号</param>
    /// <param name="lambda">每个逻辑帧执行的方法</param>
    /// <param name="repeatTimeMS">重复时间间隔（如果执行超过这个时间，则时间间隔变大）</param>
    /// <returns></returns>
    public bool AddThead(short tag, Func<short> lambda, int repeatTimeMS) {
        
        lock (threadCtrlDict) {

            // 当前线程号已经存在
            if (threadCtrlDict.ContainsKey(tag)) {
                Log.PrintLog("ThreadCtrl", "AddThead", "this thread is exists[tag:" + tag + "]", Log.LOG_LEVEL.ERROR);
                return false;
            }
            threadCtrlDict[tag] = THREAD_STATUS.START;
        }

        new Thread((object objTag) => {
            ThreadCtrl t_threadCtrl = ThreadCtrl.GetInstance();
            short t_tag = short.Parse(objTag.ToString());

            while (true) {
                long startMS = DateTime.Now.Millisecond; // 开始执行的时间
                THREAD_STATUS status = THREAD_STATUS.START;

                lock (threadCtrlDict) {
                    Dictionary<short, THREAD_STATUS> t_threadCtrlDict = t_threadCtrl.GetThreadCtrlDict();
                    status = t_threadCtrlDict[t_tag];
                }
                
                if (status == THREAD_STATUS.START) {
                    lambda();
                    int sleepMS = repeatTimeMS - (int)(DateTime.Now.Millisecond - startMS);
                    if (sleepMS > 0) {
                        Thread.Sleep(sleepMS);
                    }
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
        }).Start(""+tag);   // 把参数传入线程

        return true;
    }


    public bool ReStartThread() {

        lock (threadCtrlDict) {
            List<short> dictKeyList = new List<short>();

            // 不允许迭代修改dict的值
            foreach (KeyValuePair<short, THREAD_STATUS> kv in threadCtrlDict) {
                dictKeyList.Add(kv.Key);    // 记录所有的key
            }
            // 遍历所有的key
            foreach (short tag in dictKeyList) {
                threadCtrlDict[tag] = THREAD_STATUS.START;
            }
        }

        return true;
    }


    public bool StopThread() {

        lock (threadCtrlDict) {
            List<short> dictKeyList = new List<short>();

            // 不允许迭代修改dict的值
            foreach (KeyValuePair<short, THREAD_STATUS> kv in threadCtrlDict) {
                dictKeyList.Add(kv.Key);    // 记录所有的key
            }
            // 遍历所有的key
            foreach (short tag in dictKeyList) {
                threadCtrlDict[tag] = THREAD_STATUS.STOP;
            }
        }

        return true;
    }
}
