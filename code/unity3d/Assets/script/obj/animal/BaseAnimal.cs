using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有动物的基类
/// </summary>
public abstract class BaseAnimal : MonoBehaviour
{
	public uint speed;		// 速度
	public uint health;		// 当前
	public uint max_health;	// 最大生命值

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="speed"></param>
	/// <param name="max_health"></param>
	public BaseAnimal(uint speed, uint max_health)
	{
		this.speed = speed;
		this.max_health = this.health = max_health;
	}

	/// <summary>
	/// 运行AI的方法
	/// </summary>
	private void aiRun()
	{
		if (this.health > 0)
		{
			this.ai();
		}
		else
		{
			this.death();
		}
	}

	/// <summary>
	/// 子类必须实现的主要的AI方法
	/// </summary>
	protected abstract void ai();

	/// <summary>
	/// 死亡方法 生命值>=0时触发的方法
	/// </summary>
	protected abstract void death();

	/// <summary>
	/// 移动方法
	/// </summary>
	/// <param name="gameObject"></param>
	/// <param name="speed"></param>
	/// <param name="direction"></param>
	public void move(uint speed, byte direction)
	{
		float f_speed = (float)(speed / 10000.00);

		Vector3 vector3 = new Vector3( 0, 0, 0 );
		switch (direction)
		{
			case BaseAnimal.DIRECTION_LEFT:
				vector3.x = -f_speed;
			break;

			case BaseAnimal.DIRECTION_RIGHT:
				vector3.x = f_speed;
			break;

			case BaseAnimal.DIRECTION_BACK:
				vector3.z = -f_speed;
			break;

			case BaseAnimal.DIRECTION_FORWARD:
			default:
				vector3.z = f_speed;
			break;
		}

		this.gameObject.transform.Translate( vector3 );
	}

	/// <summary>
	/// 旋转方法
	/// </summary>
	/// <param name="gameObject"></param>
	/// <param name="angle"></param>
	/// <param name="clock"></param>
	public void turn(float angle, bool clock)
	{
		if (clock)
		{
			angle = -angle;
		}

		this.gameObject.transform.Rotate( Vector3.up, angle );
	}

	public void Start()
	{

	}


	public void Update()
	{
		this.aiRun();
	}


	// 旋转方向
	public const bool CLOCK = false;        // 顺时针
	public const bool UNCLOCK = true;       // 逆时针

	// 移动朝向
	public const byte DIRECTION_FORWARD = 0;    // 向前
	public const byte DIRECTION_LEFT = 1;       // 向左
	public const byte DIRECTION_RIGHT = 2;      // 向右
	public const byte DIRECTION_BACK = 3;       // 向后
}
