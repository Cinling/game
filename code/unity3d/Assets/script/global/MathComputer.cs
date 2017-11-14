
/// <summary>
/// 2017-11-13 08:33:55
/// 用于计算和存储一些全局数据（###############逐渐弃用#############）
/// </summary>
public class MathComputer {
    public static int fps = 0;      // 渲染帧
    public static int lps = 16;     // 游戏逻辑帧

    public static int nextLpsNum = 0;   // 距离下一个逻辑帧，还差多少个渲染帧
}
