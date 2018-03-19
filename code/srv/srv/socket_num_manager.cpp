#include "socket_num_manager.h"

SocketNumManager * SocketNumManager::shareInstance = nullptr;

SocketNumManager::SocketNumManager() {

}

SocketNumManager::~SocketNumManager() {
}

SocketNumManager * SocketNumManager::GetInstance() {
    if (shareInstance == nullptr) {
        shareInstance = new SocketNumManager();
    }
    return shareInstance;
}

std::string SocketNumManager::_10001_InitMap(std::string data) {
    rapidjson::Document document;
    document.Parse(data.c_str());

    // ½âÎö´íÎó
    if (document.HasParseError()) {
        return "false";
    }

    // Êı¾İ´íÎó
    if (!document.IsObject()) {
        return "false";
    }

    int worldWidth = Json::GetInt(document, "worldWidth", 0);
    int worldLength = Json::GetInt(document, "worldLength", 0);

    return "true";
}

std::string SocketNumManager::_10002_Save(std::string data) {
    std::string savesName = data;

    std::string retStr = "false";

    SavesManager *savesManager = SavesManager::GetInstance();
    if (savesManager->Save(savesName)) {
        retStr = "true";
    }

    return retStr;
}

std::string SocketNumManager::_10003_GetSavesList(std::string data) {
    SavesManager *savesManager = SavesManager::GetInstance();
    std::list<std::string> savesList = savesManager->GetSavesList();

    std::string retStr = "";

    for (std::list<std::string>::iterator it = savesList.begin(); it != savesList.end(); ++it) {
        if (retStr != "") {
            retStr += "|";
        }
        std::string json = Json::Saves(*it).ToJsonStr();
        retStr += json;
    }

    return retStr;
}

std::string SocketNumManager::_10004_LoadGame(std::string data) {
    return std::string();
}

