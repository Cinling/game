#include "socket_num_manager.h"

std::string SocketNumManager::_10001(std::string data) {

    rapidjson::Document document;
    document.Parse(data.c_str());

    // ½âÎö´íÎó
    if (document.HasParseError()) {
        return "false";
    }

    // Êý¾Ý´íÎó
    if (!document.IsObject()) {
        return false;
    }

    int worldWidth = SocketNumManager::GetInt(document, "worldWidth", 0);
    int worldLength = SocketNumManager::GetInt(document, "worldLength", 0);

    World::GetInstance()->Init(worldWidth, worldLength);
    return std::string();
}

int SocketNumManager::GetInt(rapidjson::Document &document, std::string key, int defaultValue) {
    int retInt = defaultValue;
    rapidjson::Value::ConstMemberIterator itr = document.FindMember(key.c_str());
    if (itr != document.MemberEnd()) {
        retInt = document.GetInt();
    }

    return retInt;
}
