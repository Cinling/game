using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2017年5月24日 22:40:36
/// 所有动物的基类
/// </summary>
public class BaseAnimal : MonoBehaviour
{
	public float speed;
	public uint health;
	public uint max_health;

	
	public BaseAnimal(float speed, int health)
	{
		this.speed = speed;
	}


	public void Update()
	{
		if (this.health > 0)
		{

		}
		else
		{

		}
	}
}
