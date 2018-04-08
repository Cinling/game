#pragma once

#include "db_manager.h"
#include "world_manager.h"
#include "tool.h"

class SavesManager {
private:
    static SavesManager * shareInstance;
    SavesManager();

    // 存档路径
    static const std::string SAVES_PATH;
    // 临时存档名称
    static const std::string TEMPORARY_SAVES;
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
    std::vector<std::string> GetSavesList();

    // 载入游戏
    // savesName 存档名称
    // return 是否处理成功
    bool Load(std::string savesName);

private:
    /*存档具体的细节处理*/
    bool SaveWorld();
    bool LoadWorld();
    bool SaveRole();
    bool LoadRole();
    
    // 【备份临时存档】
    // 实际上的更名
    // savesName 存档名称
    // return true [备份成功]，false [备份失败] 
    bool BackupTemporarySaves(std::string savesName);
    // 【删除临时存档】
    // return true [删除成功]，false [删除失败] 
    bool DeleteTemporarySaves();
    // 【恢复存档，存档失败时调用】
    // 实际上是把名字改回原来的名字
    // savesName 存档名称
    // return true [恢复成功]，false [恢复失败] 
    bool RecoveryTemporarySaves(std::string savesName);

    // 判断存档是否存在
    bool IsSavesExists(std::string savesName);
};

