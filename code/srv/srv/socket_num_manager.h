#pragma once

#include "world.h"
#include "../lib/rapidjson/document.h"
#include "../lib/rapidjson/prettywriter.h"

#include <string>

class SocketNumManager {
public:
    // 初始化游戏世界的处理
    static std::string _10001_InitMap(std::string data);
    // 保存游戏
    static std::string _10002_Save(std::string data);

private:
    static int GetInt(rapidjson::Document &document, std::string key, int defaultValue);
};

