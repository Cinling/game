using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUI {
    private static WorldUI share_instance = null;

    public static WorldUI GetInstance() {
        if (share_instance == null) {
            share_instance = new WorldUI();
        }
        return share_instance;
    }

    private bool isWorldInit = false;
    public void InitWorldButtonEvent() {

        if (!isWorldInit) {
            AddOnClickListenerWithBtnName("CanvasGame/ButtonMenu");
        }
    }

    private bool isPannelInit = false;
    public void InitPannelButtonEvent() {

        if (!isPannelInit) {
            AddOnClickListenerWithBtnName("CanvasGame/PanelMainMenu/ButtonClose");
            AddOnClickListenerWithBtnName("CanvasGame/PanelMainMenu/ButtonTest1");
            isPannelInit = true;
        }
    }

    /// <summary>
    /// 2017-09-03 16:54:49
    /// </summary>
    /// <param name="btnName"></param>
    public void AddOnClickListenerWithBtnName(string btnName) {
        // 获取按钮 GameObject
        GameObject btnGameObject = GameObject.Find(btnName);

        if (btnGameObject == null) {
            Log.PrintLog("MainMenu", "AddOnClickListenerWithBtnName", "not find button with name:[" + btnName + "]", Log.LOG_LEVEL.ERROR);
            return;
        }

        // 获取按钮脚本组件
        UnityEngine.UI.Button btnComponent = btnGameObject.GetComponent<UnityEngine.UI.Button>();
        // 添加点击监听
        btnComponent.onClick.AddListener(delegate () {
            OnClick(btnName);
        });
    }


    /// <summary>
    /// 2017-09-03 16:54:57
    /// 监听到按钮的功能实现
    /// </summary>
    /// <param name="btnName"></param>
    void OnClick(string btnName) {
        switch (btnName) {
            // 打开主菜单
            case "CanvasGame/ButtonMenu":
                MainMenuCtrl.Show();
                GetInstance().InitPannelButtonEvent();
                break;

            // 关闭主菜单
            case "CanvasGame/PanelMainMenu/ButtonClose":
                Log.PrintLog("MainMenu", "OnClick", "MainMenuHide", Log.LOG_LEVEL.DEBUG);
                MainMenuCtrl.Hide();
                break;

            // 测试按钮一
            case "CanvasGame/PanelMainMenu/ButtonTest1":
                Test1();
                break;

            default:
                Debug.Log("Unkonw button event.");
                break;
        }
    }


    public void Test1() {
        DbHelper sqlite = new DbHelper("URI=file://t1");
        string[] cols = { "name", "age" };
        string[] colsType = { "varchar(20)", "integer" };
        sqlite.CreateTable("t1", cols, colsType);
    }
}
