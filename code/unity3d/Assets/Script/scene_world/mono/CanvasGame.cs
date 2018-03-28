using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂在到GameCanvas中的类
/// 主要控制 界面UI 和 渲染帧、逻辑帧
/// </summary>
public class CanvasGame : MonoBehaviour {

    /// <summary>
    /// 正常速度下的ups
    /// </summary>
    public const short CNF_REAL_UPS = 3;
    private static short reflushNum;        // 渲染帧刷新次数，用于计算渲染帧

    /// <summary>
    /// 游戏运行时，实际的fps（不太实时）
    /// </summary>
    public static short Fps { get; private set; }
    /// <summary>
    /// 当前游戏的ups
    /// </summary>
    public static short Ups { get; private set; }
    /// <summary>
    /// 距离下一个逻辑帧需要的经过的渲染帧
    /// </summary>
    public static short NextUpsNeedFps {
        get; private set; }

    /// <summary>
    /// UPS 需要调用
    /// </summary>
    public static void ReSetNextUpsNeedFps() {
        NextUpsNeedFps = (short)( Fps / Ups );
    }

    void Start() {
        InitData(); // 初始化数据
        StartFpsReflush();  // 刷新fps
        RoleCtrl.GetInstence().StartRoleThread(Ups);
    }

    void Update() {
        ++reflushNum;   // 刷新帧数，用于计算fps

        // 刷新相对距离下一个逻辑帧需要的渲染帧
        if (NextUpsNeedFps > 0) {
            --NextUpsNeedFps;
        }

        FpsInputEvent();
    }

    // 初始化数据
    private void InitData() {
        // 渲染帧和逻辑帧
        //nextLpsNeedFps = 0;
        reflushNum = 0;
        Ups = CNF_REAL_UPS;
        Fps = 60;
    }

    // 渲染帧输入事件
    private void FpsInputEvent() {

        if (Input.GetKeyDown("n")) {
            RoleCtrl roleCtrl = RoleCtrl.GetInstence();
            roleCtrl.AddRole(new Json.BaseRole(1, 21000, 1, 0, 1, ""));

            int i = 1;
        }

        // 测试方法
        if (Input.GetKeyDown("t")) {
        }

        if (Input.GetKeyDown("i")) {
            WorldMenu.GetInstance().InitWorldButtonEvent();
        }

        // 游戏退出、结束的方法
        if (Input.GetKeyDown("1")) {
            ThreadTool.GetInstance().StopThread();
        }

        if (Input.GetKeyDown("2")) {
            ThreadTool.GetInstance().ReStartThread();
        }
    }

    // 显示fps
    private void StartFpsReflush() {
        InvokeRepeating("ReflushFpsInSecond", 0f, 1f);
    }

    // 每秒刷新一次当前帧数
    private void ReflushFpsInSecond() {
        Fps = reflushNum;
        reflushNum = 0;

        UnityEngine.UI.Text text = GameObject.Find("TextLeftTop").GetComponent<UnityEngine.UI.Text>();

        if (text != null) {
            text.text = "FPS:" + Fps;
        }
    }
}
