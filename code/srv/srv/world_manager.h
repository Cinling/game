#pragma once

#include "sqlite_tool.h"
#include "db_manager.h"
#include "role_ctrl.h"
#include "json.h"

class World {
private:
    static World *shareInstance;
    World();

    // 当前地图
    Json::Map * map;

public:
    static World * GetInstance();
    ~World();

    // 初始化世界（首次进入游戏，创建世界）
    bool InitMap(Json::Map * map);

    // 开始游戏
    bool Start();

    // 暂停游戏
    bool Pause();

    // 继续被暂停的游戏
    bool Resume();

    // 退出游戏（关闭服务端）
    bool Exit();

    // 保存游戏数据
    bool Save();

    // 加载游戏数据
    bool Load();

    // 获取地图数据
    Json::Map GetMapInfo();

private:
    // 创建树木
    bool CreateTree();

private:
    // 保存世界数据
    void SaveWorld();

    // 创建地图
    // width 地图宽度
    // length 地图长度
    // return 当天地图的id
    //int CreateMap(int width, int length);
};