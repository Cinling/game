﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 2017年6月13日 23:16:10
/// 所有动态游戏对象的基类，具有公共的属性
/// </summary>
public abstract class BaseRole : MonoBehaviour 
{
	public ulong createTime;				// 创建时的游戏帧数
	public static float runSecond = 0.05f;	// 每个逻辑帧的秒数


	public void Awake()
	{
		this.InitBaseRoleChild();
		InvokeRepeating( "Do", 0, runSecond );
	}


	public void Start()
	{
		
	}


	/// <summary>
	/// 2017-07-03 23:13:18
	/// 初始化继承此类的对象
	/// </summary>
	protected abstract void InitBaseRoleChild();

	// 游戏开始方法
	public void InvokeStart()
	{
		CancelInvoke();
		InvokeRepeating( "Do", 0, runSecond );
	}

	// 游戏暂停方法
	public void InvokeStop()
	{
		CancelInvoke();
	}

	// 游戏变速
	public void InvokeChangeRunSecond(float second)
	{
		BaseRole.runSecond = second;
		InvokeStop();
		InvokeStart();
	}


	// 每个逻辑帧执行的动作
	public abstract void Do();
	
}