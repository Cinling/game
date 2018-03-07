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

    private MainMenu() {
        
    }

    /// <summary>
    /// 初始化菜单
    /// </summary>
    public void Init() {
        CreateMenu();
    }

    /// <summary>
    /// 创建菜单
    /// </summary>
    private void CreateMenu() {
        // 创建 Canvas
        GameObject goCanvas = GameObject.Instantiate(Resources.Load<GameObject>("ui/Canvas"));
        Rect rectCanvas = goCanvas.GetComponent<RectTransform>().rect;
        float width = rectCanvas.width;
        float height = rectCanvas.height;

        // 创建 Pannel
        GameObject goPannel = GameObject.Instantiate(Resources.Load<GameObject>("ui/Panel"));
        goPannel.transform.SetParent(goCanvas.transform);
        UnityEngine.UI.Image pannel = goPannel.GetComponent<UnityEngine.UI.Image>();
        pannel.rectTransform.sizeDelta = new Vector2(width, height);
        Debug.Log(width + ", " + height);

        // 创建开始游戏的按钮
        GameObject goStartGame = GameObject.Instantiate(Resources.Load<GameObject>("ui/Button"));
        goStartGame.transform.SetParent(goPannel.transform);
        goStartGame.GetComponentInChildren<UnityEngine.UI.Text>().text = Lang.Get("main.menu.start_game");
        UnityEngine.UI.Button btnStartGame = goStartGame.GetComponent<UnityEngine.UI.Button>();
        btnStartGame.transform.position = new Vector3(width - 200, height - 200, 0);
        btnStartGame.onClick.AddListener(OnStartGameClick);
    }

    /// <summary>
    /// 开始游戏时执行的方法
    /// </summary>
    private void OnStartGameClick() {
        SceneCtrl.GetInstance().SwitchToWorld();
    }
}
