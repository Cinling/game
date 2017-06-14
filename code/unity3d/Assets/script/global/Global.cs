using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 2017年6月13日 22:51:33
/// 全局单例类，负责全局的数据控制
/// </summary>
public static class Global
{
	// 逻辑帧的时间间隔，单位毫秒
	public static int logicalFrame_ms = 100;
	public static Dictionary<string, GameObject> gameObjectMap;
	public static ulong gameRunTime;


	/// <summary>
	/// 2017-06-14 23:51:44
	/// 提供给外部启动 Global 的方法
	/// 因为C#脚本在没有被调用的情况下不会启动，所以只要有任何地方调用此方法，整个 Global 类就会被激活
	/// </summary>
	public static void StartGlobal()
	{

	}


	/// <summary>
	/// 2017年6月13日 23:35:24
	/// 程序主入口
	/// </summary>
	static Global()
	{
		Global.Init();
		Debug.Log( "init" );

		// 设置定时任务
		System.Timers.Timer timer = new System.Timers.Timer();
		timer.Elapsed += MainLoop;
		timer.Interval = Global.logicalFrame_ms;
		timer.Start();
	}


	/// <summary>
	/// 数据初始化
	/// </summary>
	private static void Init()
	{
		Global.gameRunTime = 0;
	}


	/// <summary>
	/// 2017-06-14 23:31:18
	/// 游戏每个逻辑帧执行的方法
	/// </summary>
	private static void MainLoop(object sender, System.Timers.ElapsedEventArgs e)
	{
		Debug.Log("print something");
	}


	/// <summary>
	/// 2017年6月13日 22:52:16
	/// 存储游戏对象
	/// </summary>
	/// <param name="key"></param>
	/// <param name="gameObject"></param>
	public static void AddGameObject(string key, GameObject gameObject)
	{
		gameObjectMap[key] = gameObject;
	}


	/// <summary>
	/// 2017年6月13日 22:58:37
	/// 获取游戏对象
	/// </summary>
	/// <param name="key"></param>
	/// <returns></returns>
	public static GameObject GetGameObject(string key)
	{
		return gameObjectMap[key];
	}
}
