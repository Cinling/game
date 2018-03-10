#pragma once

#include "sqlite_tool.h"
#include "world_db.h"
#include "db_manager.h";

class World {
private:
    static World *shareInstance;
    World();

    // 世界宽度
    float worldWidth;
    // 世界长度
    float worldLength;

public:
    static World * GetInstance();
    ~World();

    // 初始化世界（首次进入游戏，创建世界）
    void Init(float width, float height);

    // 开始游戏
    void Start();

    // 暂停游戏
    void Pause();

    // 继续被暂停的游戏
    void Resume();

    // 退出游戏（关闭服务端）
    void Exit();

    // 保存游戏数据
    void Save();

    // 加载游戏数据
    void Load();

    // sqlite 测试方法
    void SqliteTest();

private:
    // 保存世界数据
    void SaveWorld();
};