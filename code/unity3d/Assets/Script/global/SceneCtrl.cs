using UnityEngine.SceneManagement;

public class SceneCtrl {

    private static SceneCtrl share_instance = null;

    public static SceneCtrl GetInstance() {
        if (share_instance == null) {
            share_instance = new SceneCtrl();
        }
        return share_instance;
    }

    /// <summary>
    /// 跳轉到遊戲世界場景
    /// </summary>
    public void SwitchToWorld() {
        SceneManager.LoadScene("World");

        ThreadTool.GetInstance().RunOnWorldSceneMainThread(() => {
            WorldMenu.GetInstance().InitWorldButtonEvent();
            return 0;
        });
    }

    /// <summary>
    /// 跳轉到主菜單場景
    /// </summary>
    public void SwitchToMain() {
        SceneManager.LoadScene("Main");

        // 停止線程
        ThreadTool threadTool = ThreadTool.GetInstance();
        threadTool.StopThread();

        threadTool.RunOnMainSceneMainThread(() => {
            MainMenu.GetInstance().Init();
            return 0;
        });

    }
}
