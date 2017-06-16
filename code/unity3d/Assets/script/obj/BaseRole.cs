using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 2017年6月13日 23:16:10
/// 所有游戏对象的基类，具有公共的属性
/// </summary>
public abstract class BaseRole : MonoBehaviour 
{
	public ulong createTime;
	public new GameObject gameObject;

	public BaseRole()
	{
		this.createTime = Global.gameRunTime;
	}

	public abstract void Do();
}
