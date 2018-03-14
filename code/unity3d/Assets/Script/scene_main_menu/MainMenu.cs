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
        if (GameObject.Find("Canvas(Clone)") == null) {
            canvas = GameObject.Instantiate(Resources.Load<GameObject>("ui/Canvas"));
        }
        ChangeToMenuPannel();
    }

    /// <summary>
    /// 删除最上层的Canvas
    /// </summary>
    private void DeleteRootPannel() {
        GameObject goPannel = GameObject.Find("Canvas(Clone)/Panel(Clone)");
        if (goPannel != null) {
            GameObject.Destroy(goPannel);
        }
    }



    /// <summary>
    /// 创建菜单
    /// </summary>
    private void ChangeToMenuPannel() {
        DeleteRootPannel();

        Rect rectCanvas = canvas.GetComponent<RectTransform>().rect;
        float width = rectCanvas.width;
        float height = rectCanvas.height;

        // 创建 Pannel
        GameObject goPannel = GameObject.Instantiate(Resources.Load<GameObject>("ui/Panel"));
        goPannel.transform.SetParent(canvas.transform);
        goPannel.transform.position = canvas.transform.position;
        UnityEngine.UI.Image pannel = goPannel.GetComponent<UnityEngine.UI.Image>();
        UIHelper.Pannel.SetSize(pannel, width, height);

        UIHelper.Button.CreateButton(goPannel, "ui/Button", Lang.Get("main.menu.start_game"), width - 200, height - 200, ChangeToStartGameMenuPannel);
    }

    /// <summary>
    /// 初始化 【新建游戏】或【载入游戏】的界面
    /// </summary>
    private void ChangeToStartGameMenuPannel() {
        DeleteRootPannel();

        Rect rectCanvas = canvas.GetComponent<RectTransform>().rect;
        float width = rectCanvas.width;
        float height = rectCanvas.height;

        // 创建 Pannel
        GameObject goPannel = GameObject.Instantiate(Resources.Load<GameObject>("ui/Panel"));
        goPannel.transform.SetParent(canvas.transform);
        goPannel.transform.position = canvas.transform.position;
        UnityEngine.UI.Image pannel = goPannel.GetComponent<UnityEngine.UI.Image>();
        UIHelper.Pannel.SetSize(pannel, width, height);

        // 新游戏的按钮
        UIHelper.Button.CreateButton(goPannel, "ui/Button", Lang.Get("main.menu.new_game"), width - 200, height - 200, CreateNewGame);

        // 载入游戏的按钮
        UIHelper.Button.CreateButton(goPannel, "ui/Button", Lang.Get("main.menu.load_game"), width - 200, height - 250, OpenLoadGamePannel);

        // 返回的按钮
        UIHelper.Button.CreateButton(goPannel, "ui/Button", Lang.Get("main.menu.back"), width - 200, height - 300, ChangeToMenuPannel);
    }

    /// <summary>
    /// 打开载入游戏存档的面板
    /// </summary>
    private void OpenLoadGamePannel() {

        // 防止多次打开载入存档的面板
        if (GameObject.Find("Canvas(Clone)/Panel(Clone)/Panel(Clone)") != null) {
            return;
        }

        // 获取父级元素
        GameObject goPannel = GameObject.Find("Canvas(Clone)/Panel(Clone)");
        if (goPannel == null) {
            return;
        }

        // 创建 Pannel
        GameObject goLoadGamePannel = GameObject.Instantiate(Resources.Load<GameObject>("ui/Panel"));
        goLoadGamePannel.transform.SetParent(goPannel.transform);
        goLoadGamePannel.transform.position = goPannel.transform.position;
        UnityEngine.UI.Image pannel = goLoadGamePannel.GetComponent<UnityEngine.UI.Image>();
        UIHelper.Pannel.SetSize(pannel, 400, 500);


        float btnX = goLoadGamePannel.transform.position.x;
        float btnY = goLoadGamePannel.transform.position.y;

        // 载入按钮
        UIHelper.Button.CreateButton(goLoadGamePannel, "ui/Button", Lang.Get("main.menu.ok"), btnX, btnY - 180, CloseLoadGamePannel);

        // 关闭按钮
        UIHelper.Button.CreateButton(goLoadGamePannel, "ui/Button", Lang.Get("main.menu.close"), btnX, btnY - 220, CloseLoadGamePannel);
    }

    /// <summary>
    /// 创建新游戏
    /// </summary>
    private void CreateNewGame() {
        string ret = TcpTool._10001_InitMap(500, 500);
        if (ret == "true") {
            SceneCtrl.GetInstance().SwitchToWorld();
        }
    }

    /// <summary>
    /// 载入存档
    /// </summary>
    private void LoadGame() {

    }

    /// <summary>
    /// 关闭载入游戏存档的面板
    /// </summary>
    private void CloseLoadGamePannel() {
        GameObject goLoadGamePannel = GameObject.Find("Canvas(Clone)/Panel(Clone)/Panel(Clone)");
        if (goLoadGamePannel != null) {
            GameObject.Destroy(goLoadGamePannel);
        }
    }
}



