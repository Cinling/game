using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager {
    private static WorldManager shareInstance = null;
    private WorldManager() {

    }
    public static WorldManager GetInstance() {
        if (shareInstance == null) {
            shareInstance = new WorldManager();
        }
        return shareInstance;
    }

    /// <summary>
    /// 地图
    /// </summary>
    private Map map;

    /// <summary>
    /// 世界场景数据初始化
    /// </summary>
    /// <returns></returns>
    public bool Init() {
        InitMap();
        InitGameObject();
        return true;
    }

    /// <summary>
    /// 创建地图
    /// </summary>
    /// <returns></returns>
    private bool InitMap() {
        Json.Map map = SocketNum._20001_GetMapData();
        this.map = new Map(map);

        // 设置镜头移动的限制区域
        WorldCamera.LimitFoucusArrea(this.map.width, this.map.length);

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private bool InitGameObject() {
        List<Json.BaseRole> baseRoleList = SocketNum._20002_GetStartGameObjectData();
        RoleCtrl roleCtrl = RoleCtrl.GetInstence();

        foreach (Json.BaseRole baseRole in baseRoleList) {
            roleCtrl.AddRole(baseRole);
        }
        return true;
    }
}
