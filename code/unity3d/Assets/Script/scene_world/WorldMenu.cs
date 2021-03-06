﻿
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

        GameObject goBtnMenu = UIFunc.Button.CreateButton(canvas, PrefabPath._2D.Button, Lang.Get("world.menu.menu"), 90, 25, ShowMenuPannel);
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

        goPanel = UIFunc.Panel.CreatePanel(canvas, PrefabPath._2D.Panel);
        goPanel.name = "PanelMenu";
        UIFunc.Panel.SetSize(goPanel, 400, 600);
        float pannelX = goPanel.transform.position.x;
        float pannelY = goPanel.transform.position.y;

        UIFunc.Button.CreateButton(goPanel, PrefabPath._2D.Button, Lang.Get("world.menu.save"), pannelX, pannelY + 200, OpenSavePannel);
        UIFunc.Button.CreateButton(goPanel, PrefabPath._2D.Button, "createRole", pannelX, pannelY + 160, CreateRole);

        UIFunc.Button.CreateButton(goPanel, PrefabPath._2D.Button, Lang.Get("world.menu.back_to_menu"), pannelX, pannelY - 100, BackTuMenu);
        UIFunc.Button.CreateButton(goPanel, PrefabPath._2D.Button, Lang.Get("world.menu.exit"), pannelX, pannelY - 140, ExitGame);
        UIFunc.Button.CreateButton(goPanel, PrefabPath._2D.Button, Lang.Get("world.menu.close"), pannelX, pannelY - 200, CloseMenuPannel);
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
        GameObject goLoadGamePannel = UIFunc.Panel.CreatePanel(canvas, PrefabPath._2D.Panel);
        goLoadGamePannel.name = "PanelSaveGame";
        UIFunc.Panel.SetSize(goLoadGamePannel, 400, 500);

        float panelX = goLoadGamePannel.transform.position.x;
        float panelY = goLoadGamePannel.transform.position.y;

        // 输入框
        GameObject goInputField =  UIFunc.InputField.CreateInputField(goLoadGamePannel, PrefabPath._2D.InputField, panelX, panelY - 180, 338);
        goInputField.name = "InputFieldSavesName";

        // 保存按钮
        UIFunc.Button.CreateButton(goLoadGamePannel, PrefabPath._2D.Button, Lang.Get("world.menu.save"), panelX - 90, panelY - 220, Save);

        // 关闭按钮
        UIFunc.Button.CreateButton(goLoadGamePannel, PrefabPath._2D.Button, Lang.Get("world.menu.close"), panelX + 90, panelY - 220, CloseSaveGamePanel);
    }
    private void CreateRole() {
        SocketNum._10007_CreateRole(new Json.BaseRole(0, 10000, 2f, 0, 2f, ""));
    }
    /// <summary>
    /// 关闭保存的面板
    /// </summary>
    private void CloseSaveGamePanel() {
        GameObject goSaveGamePanel = GameObject.Find("CanvasGame/PanelSaveGame");
        if (goSaveGamePanel != null) {
            UnityEngine.Object.Destroy(goSaveGamePanel);
        }
    }
    /// <summary>
    /// 保存游戏（存档）
    /// </summary>
    private void Save() {
        GameObject goInputField = GameObject.Find("CanvasGame/PanelSaveGame/InputFieldSavesName");
        string savesName = UIFunc.InputField.GetValue(goInputField);

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
            UnityEngine.Object.Destroy(goPanel);
        }
    }
    /// <summary>
    /// 返回到主页面(切换场景)
    /// </summary>
    private void BackTuMenu() {
        SceneCtrl.GetInstance().SwitchToMain();
        RoleCtrl.GetInstence().Clean();
    }
    /// <summary>
    /// 关闭游戏
    /// </summary>
    private void ExitGame() {
        SocketNum._10006_ExitServerProcess();
        Application.Quit();
    }
}
