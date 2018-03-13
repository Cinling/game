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
        // 创建 Canvas
        canvas = GameObject.Instantiate(Resources.Load<GameObject>("ui/Canvas"));
    }

    /// <summary>
    /// 初始化菜单
    /// </summary>
    public void Init() {
        CreateMenu();
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
    /// 添加一个按钮
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="perfab"></param>
    /// <param name="text"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="call"></param>
    private void AddButton(GameObject parent, string perfab, string text, float x, float y, UnityEngine.Events.UnityAction call) {
        GameObject gameObject = GameObject.Instantiate(Resources.Load<GameObject>(perfab));
        gameObject.transform.SetParent(parent.transform);
        gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
        UnityEngine.UI.Button button = gameObject.GetComponent<UnityEngine.UI.Button>();
        button.transform.position = new Vector3(x, y, 0);
        button.onClick.AddListener(call);
    }

    /// <summary>
    /// 创建菜单
    /// </summary>
    private void CreateMenu() {
        DeleteRootPannel();

        Rect rectCanvas = canvas.GetComponent<RectTransform>().rect;
        float width = rectCanvas.width;
        float height = rectCanvas.height;

        // 创建 Pannel
        GameObject goPannel = GameObject.Instantiate(Resources.Load<GameObject>("ui/Panel"));
        goPannel.transform.SetParent(canvas.transform);
        UnityEngine.UI.Image pannel = goPannel.GetComponent<UnityEngine.UI.Image>();
        pannel.rectTransform.sizeDelta = new Vector2(width, height);

        this.AddButton(goPannel, "ui/Button", Lang.Get("main.menu.start_game"), width - 200, height - 200, OnStartGameClick);
    }

    /// <summary>
    /// 初始化 【新建游戏】或【载入游戏】的界面
    /// </summary>
    private void CreateStartGameMenu() {
        DeleteRootPannel();

        Rect rectCanvas = canvas.GetComponent<RectTransform>().rect;
        float width = rectCanvas.width;
        float height = rectCanvas.height;

        // 创建 Pannel
        GameObject goPannel = GameObject.Instantiate(Resources.Load<GameObject>("ui/Panel"));
        goPannel.transform.SetParent(canvas.transform);
        UnityEngine.UI.Image pannel = goPannel.GetComponent<UnityEngine.UI.Image>();
        pannel.rectTransform.sizeDelta = new Vector2(width, height);

        // 创建新游戏的按钮
        this.AddButton(goPannel, "ui/Button", Lang.Get("main.menu.new_game"), width - 200, height - 200, OnNewGameClick);

        // 创建载入游戏的按钮
        this.AddButton(goPannel, "ui/Button", Lang.Get("main.menu.load_game"), width - 200, height - 250, OnLoadGameClick);

        // 创建返回的按钮
        this.AddButton(goPannel, "ui/Button", Lang.Get("main.menu.back"), width - 200, height - 300, OnBackClick);
    }

    /// <summary>
    /// 开始游戏按钮 事件
    /// </summary>
    private void OnStartGameClick() {
        //SceneCtrl.GetInstance().SwitchToWorld();
        this.CreateStartGameMenu();
    }

    /// <summary>
    /// 新游戏按钮 事件
    /// </summary>
    private void OnNewGameClick() {
        
    }

    /// <summary>
    /// 载入游戏
    /// </summary>
    private void OnLoadGameClick() {
        
    }

    /// <summary>
    /// 返回按钮
    /// </summary>
    private void OnBackClick() {
        CreateMenu();
    }
}