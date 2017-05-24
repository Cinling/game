using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2017年5月23日 23:53:48
/// AI对象的目的类，所有AI动作最终目的就是一个意图，AI会尝试完成这个意图
/// </summary>
public class Intent : MonoBehaviour
{
	public const byte BORING = 0;	// 无聊、无意图
	public const byte MOVE_TO = 1;	// 移动到某坐标点


	/// <summary>
	/// 对象的基本属性
	/// </summary>
	private byte intent;
	private Vector3 vector3_target;			// 目标点
	private GameObject gameObject_target;	// 意图目标


	/// <summary>
	/// 2017年5月23日 23:58:18
	/// 初始化意图
	/// </summary>
	public Intent()
	{
		this.intent = 0;
	}


	/// <summary>
	/// 2017年5月24日 00:04:05
	/// 设置移动意图
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="z"></param>
	public void setMoveTo(float x, float y, float z)
	{
		this.vector3_target = new Vector3(x, y, z);
	}


	/// <summary>
	/// 2017年5月24日 00:04:46
	/// 设置移动意图
	/// </summary>
	/// <param name="vector3"></param>
	public void setMoveTo(Vector3 vector3)
	{
		this.vector3_target = vector3;
	}


	/// <summary>
	/// 2017年5月24日 00:04:42
	/// </summary>
	/// <param name="gameObject">执行这个意图的游戏对象</param>
	public void doIntent(GameObject gameObject)
	{
		switch (this.intent)
		{
			case Intent.BORING:
			break;

			case Intent.MOVE_TO:
			break;
		}
	}
}
