#pragma once

#include "sqlite_tool.h"
#include "../lib/rapidjson/document.h"
#include "../lib/rapidjson/prettywriter.h"

#include <iostream>
#include <string>
#include <list>
#include <map>

class MapDB {
private:
    static MapDB * shareInstance;
    MapDB();

public:
    static MapDB * GetInstance();
    ~MapDB();
public:
    // 数据表名
    static const std::string TABLE_NAME;
    // INTEGER 主键
    static const std::string FIELD_ID;
    // TEXT 地图信息（静态的物件）
    static const std::string FIELD_INFO;
    // TEXT 配置，如地图大小、环境、种子等，不可修改的信息
    static const std::string FIELD_CONFIG;

public:
    // 插入世界的数据
    // return [true 操作成功] [false 操作失败]
    bool Insert(int worldWidth, int worldHeight);
    // 获取上次插入的id
    int GetLastInsertId();

private:
    // 把配置数据转为json字符串
    std::string GetConfigJsonStr(int worldWidth, int worldHeight);
};


