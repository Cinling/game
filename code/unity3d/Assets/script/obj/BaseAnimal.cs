using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有动物的基类
/// </summary>
public class BaseAnimal : MonoBehaviour
{
	public uint speed;
	public uint health;
	public uint max_health;

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

	/// <summary>
	/// 移动朝向
	/// </summary>
	public const byte DIRECTION_FORWARD = 0;    // 向前
	public const byte DIRECTION_LEFT = 1;       // 向左
	public const byte DIRECTION_RIGHT = 2;
	public const byte DIRECTION_BACK = 3;


	/// <summary>
	/// 运行AI的方法
	/// </summary>
	public void aiRun()
	{
		if (this.health > 0)
		{
			this.move(this.speed, BaseAnimal.DIRECTION_FORWARD);
		}
		else
		{
			this.death();
		}
	}

	/// <summary>
	/// 死亡方法
	/// </summary>
	public void death()
	{

	}


	/// <summary>
	/// 移动方法
	/// </summary>
	/// <param name="gameObject"></param>
	/// <param name="speed"></param>
	/// <param name="direction"></param>
	public void move(int speed, byte direction)
	{
		float f_speed = (float)(speed / 10000.00);

		Vector3 vector3 = new Vector3( 0, 0, 0 );
		switch (direction)
		{
			case ObjectMoveFunc.DIRECTION_LEFT:
				vector3.x = -f_speed;
			break;

			case ObjectMoveFunc.DIRECTION_RIGHT:
				vector3.x = f_speed;
			break;

			case ObjectMoveFunc.DIRECTION_BACK:
				vector3.z = -f_speed;
			break;

			case ObjectMoveFunc.DIRECTION_FORWARD:
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
}
