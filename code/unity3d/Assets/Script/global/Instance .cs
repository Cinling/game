using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Instance : MonoBehaviour {
    [RuntimeInitializeOnLoadMethod]
    static void Initialize() {
        GameObject.DontDestroyOnLoad(new GameObject("Instance", typeof(Instance)) {
            hideFlags = HideFlags.HideInHierarchy
        });

        // 启动服务端
        //StartServer();

        // 初始化幀數
        Application.targetFrameRate = 60;

        // 跳轉到主場景
        SceneCtrl.GetInstance().SwitchToMain();
    }

    /// <summary>
    /// 启动服务端
    /// </summary>
    public static void StartServer() {
        new Thread(() => {
            string text = SystemHandle.RunCmd("srv.exe");

            //声明字符集   
            System.Text.Encoding utf8, gb2312;
            //gb2312   
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            //utf8   
            utf8 = System.Text.Encoding.GetEncoding("utf-8");
            byte[] gb;
            gb = gb2312.GetBytes(text);
            gb = System.Text.Encoding.Convert(gb2312, utf8, gb);
            //返回转换后的字符   
            string utf8Text = utf8.GetString(gb);

            Debug.Log(utf8Text);
        }).Start();
    }
}
