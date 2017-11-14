using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有动物的基类
/// </summary>
public abstract class Animal : BaseRole {
    // 旋转方向
    public const bool CLOCK = false;        // 顺时针
    public const bool UNCLOCK = true;       // 逆时针

    // 移动朝向
    public const byte DIRECTION_FORWARD = 0;    // 向前
    public const byte DIRECTION_LEFT = 1;       // 向左
    public const byte DIRECTION_RIGHT = 2;      // 向右
    public const byte DIRECTION_BACK = 3;       // 向后


    // 基本属性
    public uint speed;         // 速度
    public uint health;        // 当前
    public uint maxHealth;    // 最大生命值

    // 逻辑帧的目的地
    private Vector3 target_vec3;

    protected static GameObject CreateAnimal(float x, float y, float z, string prefab, uint speed, uint health, uint max_health) {
        GameObject gameObject = CreateBaseRole(x, y, z, prefab);
        Animal animal_script = gameObject.GetComponent<Animal>();

        animal_script.speed = speed;
        animal_script.health = health;
        animal_script.maxHealth = max_health;

        return gameObject;
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
    /// 2017-07-03 23:16:25
    /// 初始化方法
    /// </summary>
    protected override void InitBaseRole() {

    }






    /// <summary>
    /// 运行AI的方法
    /// </summary>
    public override void Do() {
        if (this.health > 0) {
            this.AI();
        } else {
            this.Death();
        }
    }

    /// <summary>
    /// 移动方法
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="direction"></param>
    public void Move(byte direction) {
        float f_speed = (float)( this.speed / 300.0 );

        Vector3 vector3 = new Vector3(0, 0, 0);
        switch (direction) {
            case Animal.DIRECTION_LEFT:
                vector3.x = -f_speed;
                break;

            case Animal.DIRECTION_RIGHT:
                vector3.x = f_speed;
                break;

            case Animal.DIRECTION_BACK:
                vector3.z = -f_speed;
                break;

            case Animal.DIRECTION_FORWARD:
            default:
                vector3.z = f_speed;
                break;
        }
        this.gameObject.transform.Translate(vector3);
    }

    /// <summary>
    /// 旋转方法
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="angle"></param>
    /// <param name="clock"></param>
    public void Turn(float angle, bool clock) {
        if (clock) {
            angle = -angle;
        }

        this.gameObject.transform.Rotate(Vector3.up, angle);
    }
}
