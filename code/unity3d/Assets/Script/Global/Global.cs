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

	public static Dictionary<uint, GameObject> baseRoleMap;		// 存储游戏对象
	public static uint nextKey = 0;								// 用于记录 baseRoleMap 的下一个 key


	
	
	/// <summary>
	/// 2017-06-15 00:09:46
	/// 暂停后重新开始
	/// </summary>
	public static void Start()
	{
		foreach (GameObject gameObject in baseRoleMap.Values)
		{
			BaseRole baseRole = gameObject.GetComponent<BaseRole>();
			baseRole.InvokeStart();
		}
	}

	
	/// <summary>
	/// 2017-06-15 00:03:00
	/// 暂停
	/// </summary>
	public static void Pause()
	{
		foreach (GameObject gameObject in baseRoleMap.Values)
		{
			BaseRole baseRole = gameObject.GetComponent<BaseRole>();
			baseRole.InvokeStop();
		}
	}


	/// <summary>
	/// 2017年6月13日 23:35:24
	/// 游戏全局控制类初始化方法
	/// </summary>
	static Global()
	{
		Global.InitData();
	}


	/// <summary>
	/// 数据初始化
	/// </summary>
	private static void InitData()
	{
		Global.gameRunTime = 0;
		baseRoleMap = new Dictionary<uint, GameObject>();

		//BaseRole guy = new Guy(1, 1, 1);
		//Global.AddBaseRole(1, guy);
	}



	/// <summary>
	/// 2017年6月13日 22:52:16
	/// 存储游戏对象
	/// </summary>
	/// <param name="key"></param>
	/// <param name="gameObject"></param>
	public static void AddBaseRole(uint key, GameObject baseRole)
	{
		if (key == 0)
		{
			key = ++nextKey;
		}
		baseRoleMap[key] = baseRole;
	}


	/// <summary>
	/// 2017年6月13日 22:58:37
	/// 获取游戏对象
	/// </summary>
	/// <param name="key"></param>
	/// <returns></returns>
	public static GameObject GetBaseRole(uint key)
	{
		return baseRoleMap[key];
	}


	public static GameObject CreateBaseRole(string preb, float x, float y, float z)
	{
		Object spherePreb = Resources.Load( preb, typeof( GameObject ) );
		GameObject sphere = MonoBehaviour.Instantiate( spherePreb ) as GameObject;
		sphere.transform.position = new Vector3( x, y, z );

		return sphere;
	}
}
