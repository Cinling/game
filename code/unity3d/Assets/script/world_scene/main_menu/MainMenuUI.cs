using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI {
    private static MainMenuUI share_instance = null;

    public static MainMenuUI GetInstance() {
        if (share_instance == null) {
            share_instance = new MainMenuUI();
        }
        return share_instance;
    }

    public void InitMainMenuButtonEvent() {
        AddOnClickListenerWithBtnName("MainCanvas/BtnStartGame");
    }

    /// <summary>
    /// 2017-09-03 16:54:49
    /// </summary>
    /// <param name="btnName"></param>
    void AddOnClickListenerWithBtnName(string btnName) {
        // 获取按钮 GameObject
        GameObject btnGameObject = GameObject.Find(btnName);

        if (btnGameObject == null) {
            Log.PrintLog("MainMenuUI", "AddOnClickListenerWithBtnName", "not find button with name:[" + btnName + "]", Log.LOG_LEVEL.ERROR);
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
            // 进入主界面
            case "MainCanvas/BtnStartGame":
                SceneCtrl.GetInstance().SwitchToWorld();
                break;

            default:
                Debug.Log("Unkonw button event.");
                break;
        }
    }
}
