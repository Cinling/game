
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 2017年6月13日 23:16:10
/// 所有动态游戏对象的基类，具有公共的属性
/// </summary>
public abstract class BaseRole : MonoBehaviour {

    /// <summary>
    /// 角色創建時噶遊戲時間（當前邏輯幀幀數）
    /// </summary>
    protected ulong createTime;

    /// <summary>
    /// 系創景度創建一個基礎噶 GUI（prefab） 對象
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="perfab">prefab 文件在 Resources 下面噶位置。不需要後綴名，如：Animal/Persion/Guy</param>
    protected static GameObject CreateBaseRole(float x, float y, float z, string prefab) {
        Object spherePreb = Resources.Load(prefab, typeof(GameObject));
        GameObject gameObject = Instantiate(spherePreb) as GameObject;
        gameObject.transform.position = new Vector3(x, y, z);

        return gameObject;
    }

    /// <summary>
    /// 創建對象之前 執行咖方法
    /// </summary>
    private void Awake() {

    }

    /// <summary>
    /// 對象創建之後 執行咖方法
    /// </summary>
    private void Start() {
        StartInit();
    }

    /// <summary>
    /// 每個 FPS 進行噶操作
    /// </summary>
    private void Update() {
        FPS();
    }

    /// <summary>
    /// 當對象被鼠標點擊時觸發噶方法
    /// </summary>
    private void OnMouseUpAsButton() {
        OnClick();
    }

    /// <summary>
    /// 子類進行初始化噶方法
    /// </summary>
    protected abstract void StartInit();
    /// <summary>
    /// 每個渲染幀執行噶方法
    /// </summary>
    protected abstract void FPS();
    /// <summary>
    /// 每個邏輯幀執行噶方法，【注意：唔可以包含 Unity3D 引擎中噶方法】
    /// </summary>
    public abstract void UPS();

    /// <summary>
    /// 被點擊出發咖方法
    /// </summary>
    protected abstract void OnClick();
}
