using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2017年5月18日 23:23:28
/// 角色移动、转向的静态类
/// </summary>
public class ObjectMoveFunc : MonoBehaviour
{
	public const bool CLOCK = false;		// 顺时针
	public const bool UNCLOCK = true;       // 逆时针


	/// <summary>
	/// 2017年5月18日 23:23:52
	/// 橘色移动方法
	/// 【未完善，只有移动方向，和速度两个可选参数】
	/// </summary>
	/// <param name="gameObject">游戏对象</param>
	/// <param name="vector3">移动的方向</param>
	public static void move(GameObject gameObject, Vector3 vector3)
	{
		gameObject.transform.Translate( vector3 );
	}



	/// <summary>
	/// 2017年5月18日 23:19:20
	/// 角色转向方法
	/// </summary>
	/// <param name="gameObject">游戏对象</param>
	/// <param name="angle">旋转角度大小</param>
	/// <param name="clock">俯视选装方向</param>
	public static void turn(GameObject gameObject, float angle, bool clock)
	{
		if (clock)
		{
			angle = -angle;
		}

		gameObject.transform.Rotate(Vector3.forward, angle);
	}
}
