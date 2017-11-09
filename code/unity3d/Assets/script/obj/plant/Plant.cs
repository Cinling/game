using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 2017-11-09 23:55:24
/// 植物抽象父类
/// 
/// 说明：
///     生长周期：发芽 - 出芽 - 成熟 - 结果 - 死亡
/// </summary>
public abstract class Plant : BaseRole
{
    public const byte LIFE_CYCLE_SPROUT = 0;    // 发芽（未出土）
    public const byte LIFE_CYCLE_BUD = 1;       // 出芽
    public const byte LIFE_CYCLE_MATURE = 2;    // 成熟
    public const byte LIFE_CYCLE_BEAR = 3;      // 结果
    public const byte LIFE_CYCLE_DEATH = 4;     // 死亡

    protected int life_time;                    // 已生存的时间

    public override void Do()
	{
		
	}

	protected override void InitBaseRole()
	{
		InitPlant();
	}


    /// <summary>
    /// 2017-11-09 23:54:19
    /// 初始化植物
    /// 默认是未出土
    /// </summary>
	protected abstract void InitPlant();


    // 当生命状态改变
    protected abstract void OnLifeCycleChange(byte life_cycle);
}
