
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 2017年6月13日 23:16:10
/// 所有动态游戏对象的基类，具有公共的属性
/// </summary>
public abstract class BaseRole : MonoBehaviour {
    
    protected ulong createTime; // 角色创建时的游戏时间


    /// <summary>
    /// 在场景中新建一个UI对象
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="perfab">prefab 文件相对 Resourse 资源路径</param>
    protected static GameObject CreateBaseRole(float x, float y, float z, string prefab) {
        Object spherePreb = Resources.Load(prefab, typeof(GameObject));
        GameObject gameObject = Instantiate(spherePreb) as GameObject;
        gameObject.transform.position = new Vector3(x, y, z);

        return gameObject;
    }


    protected void Awake() {
        
    }


    protected void Start() {
        StartInit();
    }

    protected void Update() {
        FPS();
    }

    // 初始化方法
    protected abstract void StartInit();
    // 每个渲染帧执行
    protected abstract void FPS();
    // 每个逻辑帧执行的动作，这个方法不能包含Unity3D的方法
    public abstract void UPS();

}
