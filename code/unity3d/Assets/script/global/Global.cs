using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 2017年6月13日 22:51:33
/// 全局单例类，负责全局的数据控制
/// </summary>
public static class Global
{
	private static bool lastRunEnd = true;		// 查看上次运行是否结束，确保数据不会错乱。这个属性不能乱用，只是暂时的解决方案
	
	public static int logicalFrame_ms = 100;    // 逻辑帧的时间间隔，单位毫秒
	public static System.Timers.Timer timer;    // 游戏定时器

	public static ulong gameRunTime;            // 游戏运行时间，每个逻辑帧自增一次

	public static Dictionary<uint, BaseRole> baseRoleMap;		// 存储游戏对象


	
	
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
	/// 游戏全局控制类初始化方法
	/// </summary>
	static Global()
	{
		Global.InitData();

		// 设置定时任务
		timer = new System.Timers.Timer();
		timer.Elapsed += TimersLoop;
		timer.Interval = Global.logicalFrame_ms;
		timer.Start();
	}


	/// <summary>
	/// 数据初始化
	/// </summary>
	private static void InitData()
	{
		Global.gameRunTime = 0;
		baseRoleMap = new Dictionary<uint, BaseRole>();

		//BaseRole guy = new Guy(1, 1, 1);
		//Global.AddBaseRole(1, guy);
	}


	/// <summary>
	/// 2017-06-14 23:31:18
	/// 游戏每个逻辑帧执行的方法，主要处理保持数据统一的问题
	/// </summary>
	private static void TimersLoop(object sender, System.Timers.ElapsedEventArgs e)
	{
		if (Global.lastRunEnd)
		{
			Global.lastRunEnd = false;

			Global.MainLoop();

			++Global.gameRunTime;
			Global.lastRunEnd = true;
		}
	}


	/// <summary>
	/// 2017-06-16 23:27:47
	/// 游戏主循环方法
	/// </summary>
	private static void MainLoop()
	{
		// 所有动态物体执行一次方法
		foreach (BaseRole baseRole in Global.baseRoleMap.Values)
		{
			baseRole.Do();
		}
	}


	/// <summary>
	/// 2017年6月13日 22:52:16
	/// 存储游戏对象
	/// </summary>
	/// <param name="key"></param>
	/// <param name="gameObject"></param>
	public static void AddBaseRole(uint key, BaseRole baseRole)
	{
		baseRoleMap[key] = baseRole;
	}


	/// <summary>
	/// 2017年6月13日 22:58:37
	/// 获取游戏对象
	/// </summary>
	/// <param name="key"></param>
	/// <returns></returns>
	public static BaseRole GetBaseRole(uint key)
	{
		return baseRoleMap[key];
	}
}
