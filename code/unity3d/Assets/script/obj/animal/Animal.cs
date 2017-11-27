using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有动物的基类
/// </summary>
public abstract class Animal : BaseRole {
    // lps 和 fps 显示同步所需变量
    private bool isNeedMove; // 是否需要移动
    private Vector3 vec3BeforeMovePosition; // 移动前的位置
    private Vector3 vec3TargetPosition; // 移动的目标位置
    private Vector3 vec3PerFpsMove; // fps移动的相对距离
    private short thisMaxNeedFps; // 距离下一个 lps 需要的 fps 最大数目

    // 基本属性
    protected uint speed; // 速度
    protected uint health; // 当前
    protected uint maxHealth; // 最大生命值
    
    protected float angle;  // 角度值(0-360)


    protected static GameObject CreateAnimal(float x, float y, float z, string prefab, uint speed, uint health, uint max_health) {
        GameObject gameObject = CreateBaseRole(x, y, z, prefab);
        Animal animalScript = gameObject.GetComponent<Animal>();

        animalScript.speed = speed;
        animalScript.health = health;
        animalScript.maxHealth = max_health;
        animalScript.angle = 0;

        return gameObject;
    }

    // Start 的初始化方法
    protected override void StartInit() {
        isNeedMove = false;
        vec3BeforeMovePosition = vec3TargetPosition = transform.position;
        vec3PerFpsMove = new Vector3(0, 0, 0);
    }

    // 每个渲染帧执行的方法
    protected override void FPS() {
        FpsMove();
    }

    // 每个逻辑帧执行的方法
    public override void UPS() {
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






    protected void Move(Vector3 vec3Move) {
        vec3BeforeMovePosition = vec3TargetPosition;
        vec3TargetPosition += vec3Move;
        thisMaxNeedFps = MainUICtrl.NextUpsNeedFps;

        if (thisMaxNeedFps > 0) {
            vec3PerFpsMove = vec3Move / thisMaxNeedFps;
        } else {
            vec3PerFpsMove = vec3Move;
        }
        isNeedMove = true;
    }


    private void FpsMove() {

        if (this.isNeedMove) {
            short nextLps = MainUICtrl.NextUpsNeedFps;
            this.transform.position = vec3BeforeMovePosition + vec3PerFpsMove * ( thisMaxNeedFps - nextLps );
        }
    }

}
