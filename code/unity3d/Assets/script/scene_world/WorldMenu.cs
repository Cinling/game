using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMenu {
    private static WorldMenu share_instance = null;

    public static WorldMenu GetInstance() {
        if (share_instance == null) {
            share_instance = new WorldMenu();
        }
        return share_instance;
    }

    /// <summary>
    /// 初始化世界場景所需的按鈕
    /// </summary>
    public void InitWorldButtonEvent() {
        AddOnClickListenerWithBtnName("CanvasGame/ButtonMenu");
    }

    public void InitPannelButtonEvent() {
        AddOnClickListenerWithBtnName("CanvasGame/PanelMainMenu/ButtonClose");
        AddOnClickListenerWithBtnName("CanvasGame/PanelMainMenu/ButtonTest1");
        AddOnClickListenerWithBtnName("CanvasGame/PanelMainMenu/ButtonBackToMenu");
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

            // 返回主菜單
            case "CanvasGame/PanelMainMenu/ButtonBackToMenu":
                SceneCtrl.GetInstance().SwitchToMain();
                break;

            default:
                Debug.Log("Unkonw button event.");
                break;
        }
    }


    public void Test1() {
        //SqliteTool sqlite = SqliteTool.GetInstance("save/myworld/sqlite.db");
    }
}
