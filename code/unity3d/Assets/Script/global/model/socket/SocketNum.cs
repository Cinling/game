using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SocketNum {
    /// <summary>
    /// 初始化地图
    /// </summary>
    /// <param name="worldWidth">地图宽度</param>
    /// <param name="worldLength">地图长度</param>
    /// <returns></returns>
    public static string _10001_InitMap(int worldWidth, int worldLength) {

        string data = JsonUtility.ToJson(new Json.Map(worldWidth, worldLength));
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
        string sendStr = "10003|";
        string jsonData = SocketTcp.Send(sendStr);

        string[] savesJsonStrArray = jsonData.Split('|');
        List<Json.Saves> savesList = new List<Json.Saves>();

        for (int i = 0; i < savesJsonStrArray.Length; ++i) {
            Json.Saves saves = JsonUtility.FromJson<Json.Saves>(savesJsonStrArray[i]);
            savesList.Add(saves);
        }

        return savesList;

    }
}
