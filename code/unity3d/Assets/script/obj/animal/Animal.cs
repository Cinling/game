using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有动物的基类
/// </summary>
public abstract class Animal : BaseRole {

    // 基本属性
    protected uint speed; // 速度
    protected uint health; // 当前
    protected uint maxHealth; // 最大生命值
    
    protected float angle;  // 角度值(0-360)


    protected static GameObject CreateAnimal(float x, float y, float z, string prefab, uint speed, uint health, uint max_health) {
        GameObject gameObject = CreateBaseRole(x, y, z, prefab);
        Animal animal_script = gameObject.GetComponent<Animal>();

        animal_script.speed = speed;
        animal_script.health = health;
        animal_script.maxHealth = max_health;
        animal_script.angle = 0;

        return gameObject;
    }

    // 每个渲染帧执行的方法
    protected override void UpdatePerFps() {
    }

    // 每个逻辑帧执行的方法
    public override void UpdatePerLps() {
        if (this.health > 0) {
            this.AI();
        } else {
            this.Death();
        }
    }


    /// <summary>
    /// 子类必须实现的主要的AI方法
    /// </summary>
    protected abstract void AI();

    /// <summary>
    /// 死亡方法 生命值>=0时触发的方法
    /// </summary>
    protected abstract void Death();

}
