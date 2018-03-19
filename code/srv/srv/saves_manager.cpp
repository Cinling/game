#include "saves_manager.h"



SavesManager * SavesManager::shareInstance = nullptr;


const std::string SavesManager::SAVES_PATH = "saves";
const std::string SavesManager::TEMPORARY_SAVES = "tmp_save";


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
    sqlite->UseDB((SavesManager::SAVES_PATH + "/" + savesName).c_str());

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

    std::list<std::string> tmpData;

    tmpData.push_back("aaa");
    tmpData.push_back("bbb");

    return tmpData;
}

bool SavesManager::Load(std::string savesName) {
    return false;
}

bool SavesManager::SaveWorld() {
    World * world = World::GetInstance();
    return false;
}

bool SavesManager::BackupTemporarySaves(std::string savesName) {
    std::string oldPath = SavesManager::SAVES_PATH + "/" + savesName;
    std::string newPath = SavesManager::SAVES_PATH + "/" + SavesManager::TEMPORARY_SAVES;

    return Tool::File::Rename(oldPath, newPath);
}

bool SavesManager::DeleteTemporarySaves() {
    return false;
}

bool SavesManager::RecoveryTemporarySaves(std::string savesName) {
    return false;
}
