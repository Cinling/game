#pragma once

#include "world_manager.h"
#include "saves_manager.h"
#include "json.h"

#include <string>
#include <list>

class SocketNumManager {
private:
    static SocketNumManager * shareInstance;
    SocketNumManager();
    ~SocketNumManager();
public:
    static SocketNumManager * GetInstance();

    /*客户端操作服务端，服务端做出相应的处理*/
    // 初始化游戏世界的处理
    std::string _10001_InitMap(std::string data);
    // 保存游戏
    std::string _10002_Save(std::string data);
    // 获取游戏存档列表
    std::string _10003_GetSavesList(std::string data);
    // 载入游戏存档
    std::string _10004_LoadGame(std::string data);
    // 【未实现】加载配置
    std::string _10005_InitConfig(std::string data);
    // 结束服务端进程
    std::string _10006_ExitServerProcess(std::string data);
    // 创建一个角色
    std::string _10007_CreateRole(std::string data);

    /*客户端请求服务端数据*/
    // 获取游戏的地图的数据
    std::string _20001_GetMapData(std::string data);
    // 刚刚进入游戏时，获取场景所有对象数据
    std::string _20002_GetStartGameObjectData(std::string data);
    // 【未实现】获取配置加载的进度
    std::string _20003_GetInitConfigProgress(std::string data);
};

