using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 2017-11-09 23:55:24
/// 植物抽象父类
/// </summary>
public abstract class Plant : BaseRole {

    // 配置
    protected int CNF_matureValue;
    protected int CNF_harvestValue;
    protected int CNF_harvestNum;

    protected int matureValue; // 成熟值
    protected int harvestValue; // 成熟后 或 收获后 所需多少时间可收获
    protected short harvestNum; // 收获次数

    protected static GameObject CreatePlant(float x, float y, float z, string prebPlant, string prebFruit) {
        GameObject gameObject = CreateBaseRole(x, y, z, prebPlant);
       
        return gameObject;
    }

    protected override void StartInit() {
    }


    protected override void FPS() {

    }

    public override void UPS() {

        // 未成熟
        if (matureValue > 0) {
            --matureValue;
            // 已成熟
        } else {
            // 在结果
            if (harvestValue > 0) {
                --harvestValue;
            }
        }

        this.Grow();
    }

    // 每个生长期所做的更变
    protected abstract void Grow();


    /// <summary>
    /// 每天调用的方法
    /// </summary>
    /// <returns></returns>
    public abstract void DailyGrow();



    // 判断是否可收获
    public bool IsHarvest() {
        return ( harvestValue <= 0 );
    }

    // 收获
    public void Harvest() {

        if (!IsHarvest()) {
            return;
        }
    }






    // 枯萎
    protected void Withered() {

    }

    /// <summary>
    /// 修改 prefab 的方法
    /// </summary>
    /// <param name="prefab">prefab路径</param>
    public void ChangePreb(string prefab) {

        ThreadTool.GetInstance().RunOnWorldSceneMainThread(() => {
            MainThread_ChangePreb(prefab);
            return 1;
        });
    }

    private void MainThread_ChangePreb(string prefab) {
        // 记录之前的位置
        Vector3 currentPosition = transform.position;
        // 记录之前的脚本
        UnityEngine.Object plant = gameObject.GetComponent(this.GetType());

        // 销毁当前 gameObject
        Destroy(gameObject);

        // 创建一个新的 GameObject, 并使用新的 资源
        UnityEngine.Object spherePreb = Resources.Load(prefab, typeof(GameObject));
        GameObject newGameObject = Instantiate(spherePreb) as GameObject;
        newGameObject.transform.position = currentPosition;

        // 添加
        newGameObject.AddComponent(this.GetType());
        UnityEngine.Object currentPlant = newGameObject.GetComponent(this.GetType());
        currentPlant = plant;
    }

    /// <summary>
    /// 被鼠標點擊需要執行噶方法
    /// </summary>
    override protected void OnClick() {

    }
}
