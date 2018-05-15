﻿#include "socket_num_manager.h"

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
    float height = Json::GetFloat(document, "height", 0);

    // 初始化游戏世界的数据
    WorldManager * world = WorldManager::GetInstance();
    Json::Map *map = new Json::Map(width, length, height);
    if (!world->InitMap(map)) {
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
    
    SavesManager * savesManager = SavesManager::GetInstance();
    std::string send = "false";
    if (savesManager->Load(data)) {
        send = "true";
    }
    return send;
}

std::string SocketNumManager::_10005_InitConfig(std::string data) {
    return std::string();
}

std::string SocketNumManager::_10006_ExitServerProcess(std::string data) {
    exit(0);
    return std::string();
}

std::string SocketNumManager::_10007_CreateRole(std::string data) {
    using namespace rapidjson;

    Document document;
    document.Parse(data.c_str());

    float x = Json::GetFloat(document, "x", 0.0f);
    float y = Json::GetFloat(document, "y", 0.0f);
    float z = Json::GetFloat(document, "z", 0.0f);

    RoleCtrl::GetInstance()->CreateRole<Animal>(new Tool::Struct::Vector3(x, y, z));

    return std::string();
}

std::string SocketNumManager::_20001_GetMapData(std::string data) {
    WorldManager * world = WorldManager::GetInstance();
    std::string retStr = world->GetMapInfo().ToJsonStr();
    return retStr;
}

std::string SocketNumManager::_20002_GetStartGameObjectData(std::string data) {
    std::string retStr = "";

    RoleCtrl * roleCtrl = RoleCtrl::GetInstance();
    std::map<int, BaseRole *> * roleMap = roleCtrl->GetRoleMap();

    for (std::map<int, BaseRole *>::iterator it = roleMap->begin(); it != roleMap->end(); ++it) {
        if (retStr != "") {
            retStr += "|";
        }

        int id = it->first;
        int type = it->second->GetType();
        Tool::Struct::Vector3 position = it->second->GetPosition();
        std::string specialShow = it->second->GetSpecialShowData();

        retStr += Json::BaseRole(id, type, position.x, position.y, position.z, specialShow).ToJsonStr();
    }

    return retStr;
}

std::string SocketNumManager::_20003_GetInitConfigProgress(std::string data) {
    return std::string();
}

