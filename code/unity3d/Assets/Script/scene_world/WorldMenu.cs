
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System.Threading;
using System.IO;

public class WorldMenu {
    private static WorldMenu share_instance = null;

    public static WorldMenu GetInstance() {
        if (share_instance == null) {
            share_instance = new WorldMenu();
        }
        return share_instance;
    }

    private GameObject canvas = null;

    private WorldMenu() {
        
    }

    /// <summary>
    /// 初始化世界場景所需的按鈕事件 以及設置語言
    /// </summary>
    public void InitWorldButtonEvent() {
        canvas = GameObject.Find("CanvasGame");

        GameObject goBtnMenu = UIHelper.Button.CreateButton(canvas, "ui/Button", Lang.Get("world.menu.menu"), 90, 25, ShowMenuPannel);
        goBtnMenu.name = "BtnMenu";
    }

    /// <summary>
    /// 打开菜单面板
    /// </summary>
    private void ShowMenuPannel() {
        GameObject goPanel = GameObject.Find("CanvasGame/PanelMenu");
        if (goPanel != null) {
            return;
        }

        goPanel = UIHelper.Panel.CreatePanel(canvas, "ui/Panel");
        goPanel.name = "PanelMenu";
        UIHelper.Panel.SetSize(goPanel, 400, 600);
        float pannelX = goPanel.transform.position.x;
        float pannelY = goPanel.transform.position.y;

        UIHelper.Button.CreateButton(goPanel, "ui/Button", Lang.Get("world.menu.save"), pannelX, pannelY + 200, OpenSavePannel);

        UIHelper.Button.CreateButton(goPanel, "ui/Button", Lang.Get("world.menu.back_to_menu"), pannelX, pannelY - 100, BackTuMenu);
        UIHelper.Button.CreateButton(goPanel, "ui/Button", Lang.Get("world.menu.exit"), pannelX, pannelY - 140, ExitGame);
        UIHelper.Button.CreateButton(goPanel, "ui/Button", Lang.Get("world.menu.close"), pannelX, pannelY - 200, CloseMenuPannel);
    }

    /// <summary>
    /// 打开保存存档的面板
    /// </summary>
    private void OpenSavePannel() {
        GameObject goPanel = GameObject.Find("CanvasGame/PanelSaveGame");
        if (goPanel != null) {
            return;
        }

        // 创建 Pannel
        GameObject goLoadGamePannel = UIHelper.Panel.CreatePanel(canvas, "ui/Panel");
        goLoadGamePannel.name = "PanelSaveGame";
        UIHelper.Panel.SetSize(goLoadGamePannel, 400, 500);

        float panelX = goLoadGamePannel.transform.position.x;
        float panelY = goLoadGamePannel.transform.position.y;

        // 输入框
        GameObject goInputField =  UIHelper.InputField.CreateInputField(goLoadGamePannel, "ui/InputField", panelX, panelY - 180, 338);
        goInputField.name = "InputFieldSavesName";

        // 保存按钮
        UIHelper.Button.CreateButton(goLoadGamePannel, "ui/Button", Lang.Get("world.menu.save"), panelX - 90, panelY - 220, Save);

        // 关闭按钮
        UIHelper.Button.CreateButton(goLoadGamePannel, "ui/Button", Lang.Get("world.menu.close"), panelX + 90, panelY - 220, CloseSaveGamePanel);
    }

    /// <summary>
    /// 关闭保存的面板
    /// </summary>
    private void CloseSaveGamePanel() {
        GameObject goSaveGamePanel = GameObject.Find("CanvasGame/PanelSaveGame");
        if (goSaveGamePanel != null) {
            GameObject.Destroy(goSaveGamePanel);
        }
    }

    /// <summary>
    /// 保存游戏（存档）
    /// </summary>
    private void Save() {
        GameObject goInputField = GameObject.Find("CanvasGame/PanelSaveGame/InputFieldSavesName");
        string savesName = UIHelper.InputField.GetValue(goInputField);

        if (savesName == "") {
            Debug.LogError("存档名不能为空");
            return;
        }

        string res = SocketNum._10002_Save(savesName);
    }

    /// <summary>
    /// 关闭菜单面板
    /// </summary>
    private void CloseMenuPannel() {
        GameObject goPanel = GameObject.Find("CanvasGame/PanelMenu");
        if (goPanel != null) {
            GameObject.Destroy(goPanel);
        }
    }

    /// <summary>
    /// 返回到主页面(切换场景)
    /// </summary>
    private void BackTuMenu() {
        SceneCtrl.GetInstance().SwitchToMain();
    }

    /// <summary>
    /// 关闭游戏
    /// </summary>
    private void ExitGame() {

    }
}
