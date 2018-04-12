using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色控制器
/// </summary>
public class RoleCtrl {

    /// <summary>
    /// 单例对象
    /// </summary>
    private static RoleCtrl shareInstance = null;
    private RoleCtrl() {
        roleParent = Object.Instantiate(Resources.Load<GameObject>(PrefabPath._3D.EmptyGamObject));
        roleParent.name = "roleContainer";
    }
    public static RoleCtrl GetInstence() {

        if (shareInstance == null) {
            shareInstance = new RoleCtrl();
        }
        return shareInstance;
    }

    private GameObject roleParent = null;

    /// <summary>
    /// 位置经过变化的对象列表
    /// </summary>
    private List<BaseRole> changeRoleDict = new List<BaseRole>();

    // 开启角色进程
    public void StartRoleThread(short ups) {
        ThreadTool.GetInstance().AddThead(ThreadTool.THREAD_NUM.LOGIC_ROLE, () => {

            try {
                CanvasGame.ReSetNextUpsNeedFps();

            } catch (System.Exception e) {  // 防止线程死掉
                Log.PrintLog("RoleCtrl", "StartRoleThread", e.Message, Log.LOG_LEVEL.ERROR);
                return -1;
            }
            return 0;
        }, 1000 / ups + 1);
    }

    /// <summary>
    /// 往世界场景中添加一个角色
    /// </summary>
    /// <param name="baseRole"></param>
    /// <returns></returns>
    public GameObject AddRole(Json.BaseRole baseRole) {
        string prefab = RoleType.GetPrebPathByRoleType(baseRole.type);
        GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>(prefab));

        // 设置上层 GameObject
        gameObject.transform.SetParent(roleParent.transform);

        // 添加 MonoBehaviour 控制脚本
        gameObject.AddComponent<BaseRole>();

        // 设置位置
        gameObject.transform.position = new Vector3(baseRole.x, baseRole.y, baseRole.z);

        // 添加 GameObject 的 Tag ，用于搜索对象
        gameObject.name = baseRole.id.ToString();

        return gameObject;
    }

    /// <summary>
    /// 根据id获取 GameObject 对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GameObject GetObjectById(int id) {
        return GameObject.Find(this.roleParent.name + "/" + id);
    }

    /// <summary>
    /// 销毁所有的对象
    /// </summary>
    /// <returns></returns>
    public bool Clean() {
        shareInstance = null;
        return true;
    }
}
