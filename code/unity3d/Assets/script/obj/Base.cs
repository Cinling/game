using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 2017年6月13日 23:16:10
/// 所有游戏对象的基类，具有公共的属性
/// </summary>
public class Base : MonoBehaviour 
{
	public ulong createTime;

	public Base()
	{
		this.createTime = Global.gameRunTime;
	}
}
