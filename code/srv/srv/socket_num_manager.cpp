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

    // 解析错误
    if (document.HasParseError()) {
        return "false";
    }

    // 数据错误
    if (!document.IsObject()) {
        return "false";
    }

    float width = Json::GetFloat(document, "width", 0);
    float length = Json::GetFloat(document, "length", 0);

    // 初始化游戏世界的数据
    World * world = World::GetInstance();
    if (!world->InitMap(width, length)) {
        return "false";
    }

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
    std::vector<std::string> savesList = savesManager->GetSavesList();

    std::string retStr = "";

    for (std::vector<std::string>::iterator it = savesList.begin(); it != savesList.end(); ++it) {
        if (retStr != "") {
            retStr += "|";
        }
        std::string json = Json::Saves(*it).ToJsonStr();
        retStr += json;
    }

    return retStr;
}

std::string SocketNumManager::_10004_LoadGame(std::string data) {
    std::string send = "true";
    return send;
}

std::string SocketNumManager::_20001_GetMapData() {
    World * world = World::GetInstance();
    std::string retStr = world->GetMapInfo().ToJsonStr();
    return retStr;
}

