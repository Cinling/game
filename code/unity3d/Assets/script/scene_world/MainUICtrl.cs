using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂在到GameCanvas中的类
/// 主要控制 界面UI 和 渲染帧、逻辑帧
/// </summary>
public class MainUICtrl : MonoBehaviour {

    /// <summary>
    /// 正常速度下的ups
    /// </summary>
    public const short CNF_REAL_UPS = 3;

    // 渲染帧和逻辑帧相关的变量
    private static short nextUpsNeedFps;    // 距离下一个逻辑帧需要的经过的渲染帧
    private static short reflushNum;        // 渲染帧刷新次数，用于计算渲染帧
    private static short ups;   // 逻辑帧
    private static short fps;   // 渲染帧

    /// <summary>
    /// 游戏运行时，实际的fps（不太实时）
    /// </summary>
    public static short Fps {
        get { return fps; }
    }
    /// <summary>
    /// 当前游戏的ups
    /// </summary>
    public static short Ups {
        get { return ups; }
    }
    /// <summary>
    /// 距离下一个逻辑帧需要的经过的渲染帧
    /// </summary>
    public static short NextUpsNeedFps {
        get {
            return nextUpsNeedFps;
        }
    }

    /// <summary>
    /// UPS 需要调用
    /// </summary>
    public static void ReSetNextUpsNeedFps() {
        nextUpsNeedFps = (short)( fps / ups );
    }

    void Start() {
        InitData(); // 初始化数据
        StartFpsReflush();  // 刷新fps
        RoleCtrl.GetInstence().StartRoleThread(ups);
    }

    void Update() {
        ++reflushNum;   // 刷新帧数，用于计算fps

        // 刷新相对距离下一个逻辑帧需要的渲染帧
        if (nextUpsNeedFps > 0) {
            --nextUpsNeedFps;
        }

        // 执行子线程中需要在主线程中执行的方法（UI修改）
        while (ThreadCtrl.GetInstance().MainThread_RunMainThreadLambda()) {

        }

        FpsInputEvent();
    }








    // 初始化数据
    private void InitData() {
        // 渲染帧和逻辑帧
        //nextLpsNeedFps = 0;
        reflushNum = 0;
        ups = CNF_REAL_UPS;
        fps = 60;
    }

    // 渲染帧输入事件
    private void FpsInputEvent() {

        if (Input.GetKeyDown("n")) {
            RoleCtrl.GetInstence().Test_CreateGuy();
        }

        // 测试方法
        if (Input.GetKeyDown("t")) {
        }

        if (Input.GetKeyDown("i")) {
            WorldUI.GetInstance().InitWorldButtonEvent();
        }

        // 游戏退出、结束的方法
        if (Input.GetKeyDown("1")) {
            ThreadCtrl.GetInstance().StopThread();
        }

        if (Input.GetKeyDown("2")) {
            ThreadCtrl.GetInstance().ReStartThread();
        }
    }






    // 显示fps
    private void StartFpsReflush() {
        InvokeRepeating("ReflushFpsInSecond", 0f, 1f);
    }

    // 每秒刷新一次当前帧数
    private void ReflushFpsInSecond() {
        fps = reflushNum;
        reflushNum = 0;

        UnityEngine.UI.Text text = GameObject.Find("TextLeftTop").GetComponent<UnityEngine.UI.Text>();

        if (text != null) {
            text.text = "FPS:" + Fps;
        }
    }
}
