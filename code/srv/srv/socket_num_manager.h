#pragma once

#include "world.h"
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

    // 初始化游戏世界的处理
    std::string _10001_InitMap(std::string data);
    // 保存游戏
    std::string _10002_Save(std::string data);
    // 获取游戏存档列表
    std::string _10003_GetSavesList(std::string data);
    // 载入游戏存档
    std::string _10004_LoadGame(std::string data);
};

