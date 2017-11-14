using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2017-11-14 21:26:12
/// 挂在到GameCanvas中的类
/// </summary>
public class MainUICtrl : MonoBehaviour {

    // 游戏对象列表
    private static Dictionary<int, List<GameObject>> baseRoleDict;
    private const byte BASE_ROLE_DICT_KEY = 0;
    private const int BASE_ROLE_DICT_COMMON = BASE_ROLE_DICT_KEY + 1;

    // 渲染帧和逻辑帧相关的变量
    private static short nextLpsNeedFps;    // 距离下一个逻辑帧需要的经过的渲染帧
    private static short reflushNum;        // 渲染帧刷新次数，用于计算渲染帧
    private static short lps;   // 逻辑帧
    private static short fps;   // 渲染帧

    public static short NextLpsNeedFps {
        get {
            return nextLpsNeedFps;
        }
    }

    void Start () {
        InitData(); // 初始化数据
        StartLps(); // 开启逻辑帧

        StartFpsReflush();  // 刷新fps
    }
    
    void Update () {
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
        nextLpsNeedFps = 0;
        reflushNum = 0;
        lps = 16;
        fps = 60;

        // 基本角色对象字典
        baseRoleDict = new Dictionary<int, List<GameObject>>();
        baseRoleDict[BASE_ROLE_DICT_COMMON] = new List<GameObject>();
    }





    // 开启逻辑帧
    public void StartLps() {
        InvokeRepeating("LpsReflush", 0f, 1f / lps);
    }

    // 停止逻辑帧
    public void StopLps() {
        CancelInvoke("LpsReflush");
    }

    // 改变逻辑帧
    public void ChangeLps(short lps) {

    }

    // 逻辑帧刷新
    private void LpsReflush() {
        LpsInputEvent();

        // 刷新相对于fps的帧数
        nextLpsNeedFps = (short) (fps / lps);

        // 刷新普通列表中的对象
        if (baseRoleDict[BASE_ROLE_DICT_COMMON] != null) {

            foreach (GameObject baseRole in baseRoleDict[BASE_ROLE_DICT_COMMON]) {

                BaseRole baseRole_cls = baseRole.GetComponent<BaseRole>();
                baseRole_cls.Do();
            }
        }
    }




    // 逻辑帧输入事件
    private void LpsInputEvent() {

    }


    // 渲染帧输入事件
    private void FpsInputEvent() {

        if (Input.GetKeyDown("n")) {
            GameObject gameObject = Guy.CreateGuy(10f, 0f, 0f, 100, 10, 10);
            baseRoleDict[BASE_ROLE_DICT_COMMON].Add(gameObject);
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
            text.text = "FPS:" + fps;
        }
    }
}
