#pragma once

#include "db_manager.h"
#include "world.h"

class SavesManager {
private:
    static SavesManager * shareInstance;
    SavesManager();
public:
    static SavesManager * GetInstance();
    ~SavesManager();

public:
    // 保存游戏
    // savesName 存档名称
    // return 是否处理成功
    bool Save(std::string savesName);

    // 获取存档名字列表
    // 获取存档列表
    // 存档名称的列表
    std::list<std::string> GetSavesList();

    // 载入游戏
    // savesName 存档名称
    // return 是否处理成功
    bool Load(std::string savesName);


private:
    /*存档具体的细节处理*/
    bool SaveWorld();
};

