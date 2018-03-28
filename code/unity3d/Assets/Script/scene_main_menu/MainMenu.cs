using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu {
    private static MainMenu share_instance = null;

    public static MainMenu GetInstance() {
        if (share_instance == null) {
            share_instance = new MainMenu();
        }
        return share_instance;
    }

    GameObject canvas = null;

    private MainMenu() {

    }

    /// <summary>
    /// 初始化菜单
    /// </summary>
    public void Init() {
        if (GameObject.Find("Canvas") == null) {
            canvas = Object.Instantiate(Resources.Load<GameObject>(PrefabPath._2D.Canvas));
            canvas.name = "Canvas";
        }
        ChangeToMenuPanel();
    }

    /// <summary>
    /// 删除最上层的Canvas
    /// </summary>
    private void DeleteRootPanel() {
        GameObject goPanel = GameObject.Find("Canvas/Panel");
        if (goPanel != null) {
            Object.Destroy(goPanel);
        }
    }



    /// <summary>
    /// 创建菜单
    /// </summary>
    private void ChangeToMenuPanel() {
        DeleteRootPanel();

        Rect rectCanvas = canvas.GetComponent<RectTransform>().rect;
        float width = rectCanvas.width;
        float height = rectCanvas.height;

        GameObject goPanel = UIFunc.Panel.CreatePanel(canvas, PrefabPath._2D.Panel);
        goPanel.name = "Panel";

        // 开始游戏的按钮
        UIFunc.Button.CreateButton(goPanel, PrefabPath._2D.Button, Lang.Get("main.menu.start_game"), width - 200, height - 200, ChangeToStartGameMenuPanel);

        // 切换到UI调试界面的按钮
        UIFunc.Button.CreateButton(goPanel, PrefabPath._2D.Button, "UI Testing Page", width - 200, height - 160, ChangeToUIMakeScene);
    }

    /// <summary>
    /// 初始化 【新建游戏】或【载入游戏】的界面
    /// </summary>
    private void ChangeToStartGameMenuPanel() {
        DeleteRootPanel();

        Rect rectCanvas = canvas.GetComponent<RectTransform>().rect;
        float width = rectCanvas.width;
        float height = rectCanvas.height;

        // 创建 Panel
        GameObject goPanel = UIFunc.Panel.CreatePanel(canvas, PrefabPath._2D.Panel);
        goPanel.name = "Panel";

        // 新游戏的按钮
        UIFunc.Button.CreateButton(goPanel, PrefabPath._2D.Button, Lang.Get("main.menu.new_game"), width - 200, height - 200, CreateNewGame);

        // 载入游戏的按钮
        UIFunc.Button.CreateButton(goPanel, PrefabPath._2D.Button, Lang.Get("main.menu.load_game"), width - 200, height - 250, OpenLoadGamePanel);

        // 返回的按钮
        UIFunc.Button.CreateButton(goPanel, PrefabPath._2D.Button, Lang.Get("main.menu.back"), width - 200, height - 300, ChangeToMenuPanel);
    }

    /// <summary>
    /// 切换到UI编辑的界面
    /// </summary>
    private void ChangeToUIMakeScene() {
        SceneCtrl.GetInstance().SwitchToUIMakeScene();
    }

    /// <summary>
    /// 打开载入游戏存档的面板
    /// </summary>
    private void OpenLoadGamePanel() {

        // 防止多次打开载入存档的面板
        if (GameObject.Find("Canvas/Panel/LoadSaveScrollView") != null) {
            return;
        }


        // 获取父级元素
        GameObject goPanel = GameObject.Find("Canvas/Panel");
        if (goPanel == null) {
            return;
        }

        float panelX = goPanel.transform.position.x;
        float panelY = goPanel.transform.position.y;

        // 创建 ScrollView
        UIClass.ScrollView scrollView = new UIClass.ScrollView("LoadSaveScrollView");
        scrollView.transform.SetParent(goPanel.transform);
        scrollView.SetPosition(panelX, panelY);
        scrollView.SetSize(400, 200);

        // 获取存档列表
        List<Json.Saves> savesList = SocketNum._10003_GetSavesList();

        foreach (Json.Saves saves in savesList) {
            scrollView.AddItem(saves.savesName);
        }

        // 添加功能性按钮
        scrollView.AddButton("load", this.LoadGame);
    }

    /// <summary>
    /// 载入存档
    /// </summary>
    /// <param name="scrollView"></param>
    /// <returns></returns>
    private bool LoadGame(UIClass.ScrollView scrollView) {
        UIClass.ScrollView_Item item = scrollView.GetSelectItem();
        if (item != null) {
            string savesName = item.GetText();
            if (SocketNum._10004_LoadGame(savesName)) {
                SceneCtrl.GetInstance().SwitchToWorld();
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// 创建新游戏
    /// </summary>
    private void CreateNewGame() {
        string ret = SocketNum._10001_InitMap(1000f, 500f, 100f);
        if (ret == "true") {
            SceneCtrl.GetInstance().SwitchToWorld();
        }

    }

    /// <summary>
    /// 关闭载入游戏存档的面板
    /// </summary>
    private void CloseLoadGamePanel() {
        GameObject goLoadGamePanel = GameObject.Find("Canvas/Panel/PanelLoadGame");
        if (goLoadGamePanel != null) {
            Object.Destroy(goLoadGamePanel);
        }

        GameObject goLoadGameScrollView = GameObject.Find("Canvas/Panel/ScrollViewLoadGame");
        if (goLoadGameScrollView != null) {
            Object.Destroy(goLoadGameScrollView);
        }
    }
}



