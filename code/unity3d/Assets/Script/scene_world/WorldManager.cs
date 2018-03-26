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

    private Map map;

    /// <summary>
    /// 创建地图
    /// </summary>
    /// <param name="width"></param>
    /// <param name="length"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public bool InitMap() {
        Json.Map map = SocketNum._20001_GetMapData();
        this.map = new Map(map);

        // 设置镜头移动的限制区域
        WorldCamera.LimitFoucusArrea(this.map.width, this.map.length);

        return true;
    }
}
