using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2017-11-14 21:26:12
/// 挂在到GameCanvas中的类
/// 主要控制 界面UI 和 渲染帧、逻辑帧
/// </summary>
public class MainUICtrl : MonoBehaviour {

    //// 游戏对象列表
    //private static Dictionary<int, List<BaseRole>> baseRoleDict;
    //private const byte BASE_ROLE_DICT_KEY = 0;
    //private const int BASE_ROLE_DICT_COMMON = BASE_ROLE_DICT_KEY + 1;

    // 渲染帧和逻辑帧相关的变量
    private static short nextLpsNeedFps;    // 距离下一个逻辑帧需要的经过的渲染帧
    private static short reflushNum;        // 渲染帧刷新次数，用于计算渲染帧
    private static short lps;   // 逻辑帧
    private static short fps;   // 渲染帧

    public static short Fps {
        get {return fps; }
    }
    public static short NextLpsNeedFps {
        get {
            return nextLpsNeedFps;
        }
    }
    public static void ReSetNextLpsNeedFps() {
        nextLpsNeedFps = (short)(fps / lps);
    }

    void Start() {
        InitData(); // 初始化数据
        StartFpsReflush();  // 刷新fps
        RoleCtrl.GetInstence().StartRoleThread(lps);
    }

    void Update() {
        ++reflushNum;   // 刷新帧数，用于计算fps

        // 刷新相对距离下一个逻辑帧需要的渲染帧
        if (nextLpsNeedFps > 0) {
            --nextLpsNeedFps;
        }

        FpsInputEvent();
    }








    // 初始化数据
    private void InitData() {
        // 渲染帧和逻辑帧
        //nextLpsNeedFps = 0;
        reflushNum = 0;
        lps = 6;
        fps = 60;
    }

    // 渲染帧输入事件
    private void FpsInputEvent() {

        if (Input.GetKeyDown("n")) {
            RoleCtrl.GetInstence().CreateTestGuy();
        }

        // 测试方法
        if (Input.GetKeyDown("t")) {
            //RoleCtrl.GetInstence().StartNewThread();            
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

        UnityEngine.UI.Text text = GameObject.Find("Text_LeftTop").GetComponent<UnityEngine.UI.Text>();

        if (text != null) {
            text.text = "FPS:" + Fps;
        }
    }
}
