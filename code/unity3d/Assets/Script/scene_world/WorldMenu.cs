
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

    /// <summary>
    /// 初始化世界場景所需的按鈕事件 以及設置語言
    /// </summary>
    public void InitWorldButtonEvent() {
        GameObject.Find("CanvasGame/ButtonMenu/Text").GetComponent<UnityEngine.UI.Text>().text = Lang.Get("world.menu.menu");
        AddOnClickListenerWithBtnName("CanvasGame/ButtonMenu");
    }

    /// <summary>
    /// 初始化面板的按鈕事件 以及設置語言
    /// </summary>
    public void InitPannelButtonEvent() {
        GameObject.Find("CanvasGame/PanelMainMenu/ButtonBackToMenu/Text").GetComponent<UnityEngine.UI.Text>().text = Lang.Get("world.menu.back_to_menu");
        GameObject.Find("CanvasGame/PanelMainMenu/ButtonExit/Text").GetComponent<UnityEngine.UI.Text>().text = Lang.Get("world.menu.exit");
        GameObject.Find("CanvasGame/PanelMainMenu/ButtonClose/Text").GetComponent<UnityEngine.UI.Text>().text = Lang.Get("world.menu.close");
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
                CanvasGame_Menu.Show();
                GetInstance().InitPannelButtonEvent();
                break;

            // 关闭主菜单
            case "CanvasGame/PanelMainMenu/ButtonClose":
                Log.PrintLog("MainMenu", "OnClick", "MainMenuHide", Log.LOG_LEVEL.DEBUG);
                CanvasGame_Menu.Hide();
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
        SocketClient.Program.Run();
    }
}


namespace SocketClient {
    class Program {
        public static void Run() {
            TcpClient tcpClient;
            tcpClient = new TcpClient();  //创建一个TcpClient对象，自动分配主机IP地址和端口号  
            tcpClient.Connect("127.0.0.1", 6000);   //连接服务器，其IP和端口号为127.0.0.1和51888  
            if (tcpClient != null) {
                Debug.Log("连接服务器成功");
                NetworkStream networkStream = tcpClient.GetStream();
                BinaryReader br = new BinaryReader(networkStream);
                BinaryWriter bw = new BinaryWriter(networkStream);
                bw.Write(System.Text.Encoding.UTF8.GetBytes("Client = 中文"));  //向服务器发送字符串  
                while (true) {
                    try {
                        //string brString = br.ReadString();     //接收服务器发送的数据  
                        string brString = System.Text.Encoding.UTF8.GetString(br.ReadBytes(100));
                        if (brString != null) {
                            Debug.Log(brString);
                            break;
                        }
                    } catch (Exception e) {
                        Debug.Log(e.StackTrace);
                        break;        //接收过程中如果出现异常，将推出循环  
                    }
                }
            }
        }
    }
}
