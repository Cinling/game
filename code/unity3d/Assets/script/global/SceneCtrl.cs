using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCtrl {

    private static SceneCtrl share_instance = null;

    public static SceneCtrl GetInstance() {
        if (share_instance == null) {
            share_instance = new SceneCtrl();
        }
        return share_instance;
    }

    /// <summary>
    /// 跳转到游戏世界场景
    /// </summary>
    public void SwitchToWorld() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("World");

        ThreadCtrl.GetInstance().RunOnMainThread(()=> {
            WorldUI.GetInstance().InitWorldButtonEvent();
            return 0;
        });
    }
}
