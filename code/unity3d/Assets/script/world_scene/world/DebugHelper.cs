using System;
using UnityEngine;
using UnityEngine.UI;

public class DebugHelper : MonoBehaviour
{
    private static int reflushNum;
    
    void Start ()
    {
        MathComputer.fps = 60;
        reflushNum = 0;
        InvokeRepeating("FpsReflush", 0, 1f);
    }
    
    void Update ()
    {
        ++reflushNum;
    }

    // 刷新帧数
    private void FpsReflush()
    {
        MathComputer.fps = reflushNum;
        reflushNum = 0;

        // 设置帧数的显示
        Text text = GameObject.Find("Text_LeftTop").GetComponent<Text>();
        if (text != null)
        {
            text.text = "FPS:" + MathComputer.fps;
        }
    }
}
