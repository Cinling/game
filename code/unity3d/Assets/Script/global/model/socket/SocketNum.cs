using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SocketNum {
    /// <summary>
    /// 初始化地图
    /// </summary>
    /// <param name="width">地图宽度</param>
    /// <param name="length">地图长度</param>
    /// <returns></returns>
    public static string _10001_InitMap(float width, float length, float height) {

        string data = JsonUtility.ToJson(new Json.Map(width, length, height));
        string sendStr = "10001|" + data;

        return SocketTcp.Send(sendStr);
    }

    /// <summary>
    /// 保存存档
    /// </summary>
    /// <param name="savesName">存档名称</param>
    /// <returns></returns>
    public static string _10002_Save(string savesName) {
        string sendStr = "10002|" + savesName;
        return SocketTcp.Send(sendStr);
    }

    /// <summary>
    /// 获取所有存档的列表
    /// </summary>
    /// <returns></returns>
    public static List<Json.Saves> _10003_GetSavesList() {
        string send = "10003|";
        string recv = SocketTcp.Send(send);

        string[] savesJsonArray = recv.Split('|');
        List<Json.Saves> savesList = new List<Json.Saves>();

        for (int i = 0; i < savesJsonArray.Length; ++i) {
            Json.Saves saves = JsonUtility.FromJson<Json.Saves>(savesJsonArray[i]);
            savesList.Add(saves);
        }

        return savesList;

    }

    /// <summary>
    /// 载入游戏
    /// </summary>
    /// <param name="savesName"></param>
    /// <returns></returns>
    public static bool _10004_LoadGame(string savesName) {
        string send = "10004|" + savesName;
        string recv = SocketTcp.Send(send);
        string[] retStrArr = recv.Split('|');

        if (retStrArr[0] == "true") {
            return true;
        }
        Debug.LogError("载入存档[savesName:" + savesName + "]错误，返回数据：" + recv);
        return false;
    }

    /// <summary>
    /// 获取地图的数据
    /// </summary>
    /// <returns></returns>
    public static Json.Map _20001_GetMapData() {
        string sendData = "20001|";
        string recvData = SocketTcp.Send(sendData);
        Json.Map map = JsonUtility.FromJson<Json.Map>(recvData);
        return map;
    }
}
