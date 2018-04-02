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

    // ������ͼ�����ݱ�
    if (!MapDB::GetInstance()->CreateTable()) {
        retBool = false;
    }

    // ������ɫ�����ݱ�
    if (!RoleDB::GetInstance()->CreateTable()) {
        retBool = false;
    }

    return retBool;
}

