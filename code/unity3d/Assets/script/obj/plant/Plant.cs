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
public abstract class Plant : BaseRole {

    protected long bronTime;

    protected static GameObject CreatePlant(float x, float y, float z, string prefab) {
        GameObject gameObject = CreateBaseRole(x, y, z, prefab);
 
        return gameObject;
    }

    protected override void UpdatePerFps() {

    }

    public override void UpdatePerLps() {

    }

    // 每个生长期所做的更变
    protected abstract void Grow();
}
