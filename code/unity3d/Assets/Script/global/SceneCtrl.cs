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
            TcpTool.Init("127.0.0.1", 6000);
            MainMenu.GetInstance().Init();
            return 0;
        });

    }

    /// <summary>
    /// 切换到UI编辑的界面
    /// </summary>
    public void SwitchToUIMakeScene() {
        SceneManager.LoadScene("UIMakeScene");
    }
}
