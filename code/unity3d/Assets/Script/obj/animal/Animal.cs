using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 動物噶基本類
/// </summary>
public abstract class Animal : BaseRole {
    // START lps 和 fps 显示同步所需变量
    /// <summary>
    /// 是否需要移动
    /// </summary>
    private bool isNeedMove;
    /// <summary>
    /// 移动前的位置
    /// </summary>
    private Vector3 vec3BeforeMovePosition;
    /// <summary>
    /// 移动的目标位置
    /// </summary>
    private Vector3 vec3TargetPosition;
    /// <summary>
    /// fps移动的相对距离
    /// </summary>
    private Vector3 vec3PerFpsMove;
    /// <summary>
    /// 距离下一个 lps 需要的 fps 最大数目
    /// </summary>
    private short thisMaxNeedFps;
    // END lps 和 fps 显示同步所需变量

    // START 動物基本屬性
    /// <summary>
    /// 速度
    /// </summary>
    protected uint speed;
    /// <summary>
    /// 當前生命值
    /// </summary>
    protected uint health;
    /// <summary>
    /// 最大生命值
    /// </summary>
    protected uint maxHealth;
    /// <summary>
    /// 角度
    /// </summary>
    protected float angle;
    // END 動物基本屬性

    /// <summary>
    /// 創建一個動物對象，并添加一滴基本屬性
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="prefab">prefab 文件相對 Resources 資料夾噶位置</param>
    /// <param name="speed">移動速度</param>
    /// <param name="health">當前生命值</param>
    /// <param name="max_health">最大生命值</param>
    /// <returns></returns>
    protected static GameObject CreateAnimal(float x, float y, float z, string prefab, uint speed, uint health, uint max_health) {
        GameObject gameObject = CreateBaseRole(x, y, z, prefab);
        Animal animalScript = gameObject.GetComponent<Animal>();

        animalScript.speed = speed;
        animalScript.health = health;
        animalScript.maxHealth = max_health;
        animalScript.angle = 0;

        return gameObject;
    }

    /// <summary>
    /// 父類中 Start 中執行噶方法
    /// </summary>
    protected override void StartInit() {
        isNeedMove = false;
        vec3BeforeMovePosition = vec3TargetPosition = transform.position;
        vec3PerFpsMove = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// 每個渲染幀執行噶方法
    /// </summary>
    protected override void FPS() {
        FpsMove();
    }

    /// <summary>
    /// 沒噶邏輯幀執行咖方法
    /// </summary>
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

    /// <summary>
    /// 提供俾所有動物使用咖方法
    /// </summary>
    /// <param name="vec3Move"></param>
    protected void Move(Vector3 vec3Move) {
        vec3BeforeMovePosition = vec3TargetPosition;
        vec3TargetPosition += vec3Move;
        thisMaxNeedFps = CanvasGame.NextUpsNeedFps;

        if (thisMaxNeedFps > 0) {
            vec3PerFpsMove = vec3Move / thisMaxNeedFps;
        } else {
            vec3PerFpsMove = vec3Move;
        }
        isNeedMove = true;
    }

    /// <summary>
    /// 每個 FPS 移動執行咖方法
    /// </summary>
    private void FpsMove() {

        if (this.isNeedMove) {
            short nextLps = CanvasGame.NextUpsNeedFps;
            this.transform.position = vec3BeforeMovePosition + vec3PerFpsMove * ( thisMaxNeedFps - nextLps );
        }
    }

    /// <summary>
    /// 被鼠標點擊需要執行噶方法
    /// </summary>
    override protected void OnClick() {
        
    }

}
