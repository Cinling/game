#pragma once

#include "world.h"
#include "../lib/rapidjson/document.h"
#include "../lib/rapidjson/prettywriter.h"

#include <string>

class SocketNumManager {
public:
    // ��ʼ����Ϸ����Ĵ���
    static std::string _10001(std::string data);

private:
    static int GetInt(rapidjson::Document &document, std::string key, int defaultValue);
};

