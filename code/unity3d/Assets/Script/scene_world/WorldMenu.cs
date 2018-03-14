
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
        GameObject goPanel = GameObject.Find("CanvasGame/Panel(Clone)");
        if (goPanel != null) {
            return;
        }

        goPanel = UIHelper.Pannel.CreatePanel(canvas, "ui/Panel");
        UIHelper.Pannel.SetSize(goPanel, 400, 600);
        float pannelX = goPanel.transform.position.x;
        float pannelY = goPanel.transform.position.y;

        UIHelper.Button.CreateButton(goPanel, "ui/Button", Lang.Get("world.menu.back_to_menu"), pannelX, pannelY - 100, BackTuMenu);
        UIHelper.Button.CreateButton(goPanel, "ui/Button", Lang.Get("world.menu.exit"), pannelX, pannelY - 140, ExitGame);
        UIHelper.Button.CreateButton(goPanel, "ui/Button", Lang.Get("world.menu.close"), pannelX, pannelY - 200, CloseMenuPannel);
    }

    /// <summary>
    /// 关闭菜单面板
    /// </summary>
    private void CloseMenuPannel() {
        GameObject goPanel = GameObject.Find("CanvasGame/Panel(Clone)");
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


    public void Test1() {
        new Thread(() => {
            TcpTool._10001_InitMap(700, 700);
        }).Start();
    }
}
