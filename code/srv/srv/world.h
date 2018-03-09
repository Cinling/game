#pragma once

#include "sqlite_tool.h"

class World {
private:
    static World *shareInstance;
    World();

public:
    static World * GetInstance();
    ~World();

    // 初始化世界（首次进入游戏，创建世界）
    void Init();

    // 开始游戏
    void Start();

    // 暂停游戏
    void Pause();

    // 继续被暂停的游戏
    void Resume();

    // 保存游戏数据
    void Save();

    // 退出游戏（关闭服务端）
    void Exit();



    // sqlite 测试方法
    void SqliteTest();
};