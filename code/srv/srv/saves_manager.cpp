#include "saves_manager.h"



SavesManager * SavesManager::shareInstance = nullptr;


const std::string SavesManager::SAVES_PATH = "saves/";
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
    bool retBool = true;

    SqliteTool::UseDB((SavesManager::SAVES_PATH + savesName).c_str());
    DBManager * db = DBManager::GetInstance();
    if (!db->CreateDBTable()) {
        retBool = false;
    }

    // 保存世界
    if (!this->SaveWorld()) {
        retBool = false;
    }

    // 保存角色
    if (!this->SaveRole()) {
        retBool = false;
    }

    return retBool;
}

std::vector<std::string> SavesManager::GetSavesList() {

    std::vector<std::string> filesNameVec = Tool::File::GetChildFiles(SavesManager::SAVES_PATH);
    std::vector<std::string> retVec;

    for (std::vector<std::string>::iterator it = filesNameVec.begin(); it != filesNameVec.end(); ++it) {
        size_t lastIndex = it->find_last_of(".");
        retVec.push_back(it->substr(0, lastIndex));
    }

    return retVec;
}

bool SavesManager::Load(std::string savesName) {
    SqliteTool::UseDB((SavesManager::SAVES_PATH + savesName).c_str());

    return false;
}

bool SavesManager::SaveWorld() {
    WorldManager * worldManager = WorldManager::GetInstance();
    return worldManager->Save();
}

bool SavesManager::LoadWorld() {
    WorldManager * worldManager = WorldManager::GetInstance();
    return worldManager->Load();
}

bool SavesManager::SaveRole() {
    RoleCtrl * roleCtrl = RoleCtrl::GetInstance();
    return roleCtrl->Save();
}

bool SavesManager::LoadRole() {
    RoleCtrl * roleCtrl = RoleCtrl::GetInstance();
    return roleCtrl->Load();
}

bool SavesManager::BackupTemporarySaves(std::string savesName) {
    std::string oldPath = SavesManager::SAVES_PATH + savesName;
    std::string newPath = SavesManager::SAVES_PATH + SavesManager::TEMPORARY_SAVES;

    return Tool::File::Rename(oldPath, newPath);
}

bool SavesManager::DeleteTemporarySaves() {
    return false;
}

bool SavesManager::RecoveryTemporarySaves(std::string savesName) {
    return false;
}

bool SavesManager::IsSavesExists(std::string savesName) {
    std::vector<std::string> savesNameVec = SavesManager::GetSavesList();

    return false;
}
