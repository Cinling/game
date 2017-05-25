using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有动物的基类
/// </summary>
public class BaseAnimal : MonoBehaviour
{
	public float speed;
	public uint health;
	public uint max_health;
	public Intent intent;

	public class Intent
	{
		public const byte BORING = 0;   // 无聊、无意图
		public const byte MOVE_TO = 1;  // 移动到某坐标点


		/// <summary>
		/// 对象的基本属性
		/// </summary>
		private byte intent;
		private Vector3 vector3_target;         // 目标点
		private GameObject gameObject_target;   // 意图目标


		/// <summary>
		/// 初始化意图
		/// </summary>
		public Intent()
		{
			this.intent = 0;
		}


		/// <summary>
		/// 设置移动意图
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public void setMoveTo(float x, float y, float z)
		{
			this.vector3_target = new Vector3( x, y, z );
		}


		/// <summary>
		/// 设置移动意图
		/// </summary>
		/// <param name="vector3"></param>
		public void setMoveTo(Vector3 vector3)
		{
			this.vector3_target = vector3;
		}


		/// <summary>
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

	
	public BaseAnimal(float speed, int health)
	{
		this.speed = speed;
		this.intent = null;
	}


	public void Update()
	{
		if (this.health > 0)
		{
			// 有意图
			if (this.intent != null)
			{
				this.intent.doIntent(this.gameObject);
			}
			// 没有意图，根据概率随机产生意图
			else
			{

			}
		}
		else
		{
			this.Death();
		}
	}



	public void Death()
	{

	}
}
