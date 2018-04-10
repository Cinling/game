#include "db_manager.h"

DBManager * DBManager::shareInstance = nullptr;


DBManager::DBManager() {
}


DBManager * DBManager::GetInstance() {
    if (shareInstance == nullptr) {
        shareInstance = new DBManager();
    }
    return shareInstance;
}

DBManager::~DBManager() {
}

bool DBManager::CreateDBTable() {

    SqliteTool * sqliteTool = SqliteTool::GetInstance();
    bool retBool = true;

    // 创建地图的数据表
    if (!MapDB::GetInstance()->CreateTable()) {
        retBool = false;
    }

    // 创建角色的数据表
    if (!RoleDB::GetInstance()->CreateTable()) {
        retBool = false;
    }

    return retBool;
}

