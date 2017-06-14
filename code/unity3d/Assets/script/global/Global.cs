using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 2017年6月13日 22:51:33
/// 全局单例类，负责全局的数据控制
/// </summary>
public static class Global
{
	
	public static int logicalFrame_ms = 100;    // 逻辑帧的时间间隔，单位毫秒
	public static System.Timers.Timer timer;    // 游戏定时器

	public static ulong gameRunTime;            // 游戏运行时间，每个逻辑帧自增一次

	public static Dictionary<string, GameObject> gameObjectMap;		// 存储游戏对象


	
	
	/// <summary>
	/// 2017-06-15 00:09:46
	/// 暂停后重新开始
	/// </summary>
	public static void Start()
	{
		Global.timer.Enabled = true;
	}

	
	/// <summary>
	/// 2017-06-15 00:03:00
	/// 暂停
	/// </summary>
	public static void Pause()
	{
		Global.timer.Enabled = false;
	}


	/// <summary>
	/// 2017年6月13日 23:35:24
	/// 类的主入口
	/// </summary>
	static Global()
	{
		Global.InitData();
		Debug.Log( "init" );

		// 设置定时任务
		timer = new System.Timers.Timer();
		timer.Elapsed += MainLoop;
		timer.Interval = Global.logicalFrame_ms;
		timer.Start();
	}


	/// <summary>
	/// 数据初始化
	/// </summary>
	private static void InitData()
	{
		Global.gameRunTime = 0;
		gameObjectMap = new Dictionary<string, GameObject>();
	}


	/// <summary>
	/// 2017-06-14 23:31:18
	/// 游戏每个逻辑帧执行的方法
	/// </summary>
	private static void MainLoop(object sender, System.Timers.ElapsedEventArgs e)
	{
		Debug.Log("print something");


		++Global.gameRunTime;
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
