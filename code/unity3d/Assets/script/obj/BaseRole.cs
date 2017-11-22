
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 2017年6月13日 23:16:10
/// 所有动态游戏对象的基类，具有公共的属性
/// </summary>
public abstract class BaseRole : MonoBehaviour {
    protected bool isNeedMove;
    private Vector3 vec3NowPosition;
    private Vector3 vec3TargetPosition;
    private Vector3 vec3PerFpsMove;
    private short thisMaxNeedFps;

    protected ulong createTime;                // 创建时的游戏帧数


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
        isNeedMove = false;
        vec3NowPosition = vec3TargetPosition = transform.position;
        vec3PerFpsMove = new Vector3(0, 0, 0);
    }

    protected void Update() {
        FpsMove();
    }
    
    // 每个渲染帧执行
    protected abstract void UpdatePerFps();
    // 每个逻辑帧执行的动作，这个方法不能包含Unity3D的方法
    public abstract void UpdatePerLps();


    protected void Move(Vector3 vec3Move) {
        vec3NowPosition = vec3TargetPosition;
        vec3TargetPosition += vec3Move;
        thisMaxNeedFps = MainUICtrl.NextLpsNeedFps;

        if (thisMaxNeedFps > 0) {
            vec3PerFpsMove = vec3Move / thisMaxNeedFps;
        } else {
            vec3PerFpsMove = vec3Move;
        }
        isNeedMove = true;
    }


    private void FpsMove() {

        if (this.isNeedMove) {

            short nextLps = MainUICtrl.NextLpsNeedFps;
            try {
                this.transform.position = vec3NowPosition + vec3PerFpsMove * ( thisMaxNeedFps - nextLps );
            } catch(System.Exception e) {
                Debug.Log("[position:" + ( vec3NowPosition + vec3PerFpsMove * ( thisMaxNeedFps - nextLps ) ) + ", vec3PerFpsMove:" + vec3PerFpsMove + "]");
            }

        }
    }
}
