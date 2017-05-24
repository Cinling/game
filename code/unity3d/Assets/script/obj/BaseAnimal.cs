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

	
	public BaseAnimal(float speed)
	{
		this.speed = speed;
	}
}
