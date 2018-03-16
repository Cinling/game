#include "saves_manager.h"



SavesManager * SavesManager::shareInstance = nullptr;


SavesManager::SavesManager() {
}


SavesManager * SavesManager::GetInstance() {
    if (shareInstance == nullptr) {
        shareInstance = new SavesManager();
    }
    return shareInstance;
}

SavesManager::~SavesManager() {
}

bool SavesManager::Save(std::string savesName) {
    SqliteTool * sqlite = SqliteTool::GetInstance();
    sqlite->UseDB(("saves/" + savesName).c_str());

    DBManager * db = DBManager::GetInstance();
    if (db->DBUpdate()) {
        return false;
    }

    // ±£´æÊÀ½ç
    if (!this->SaveWorld()) {
        return false;
    }

    return false;
}

std::list<std::string> SavesManager::GetSavesList() {
    return std::list<std::string>();
}

bool SavesManager::Load(std::string savesName) {
    return false;
}

bool SavesManager::SaveWorld() {
    World * world = World::GetInstance();
    return false;
}
