#pragma once

#include "db_interface.h"

class MapDB : public DBInterface {
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

    // 存储数据
    virtual std::list<std::map<std::string, std::string>> Save(std::list<std::map<std::string, std::string>>) override;
    // 读取数据
    virtual std::list<std::map<std::string, std::string>> Load(std::list<std::map<std::string, std::string>>) override;
};


