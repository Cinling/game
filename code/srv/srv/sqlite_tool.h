#pragma once

#include "../lib/sqlite3.h"

#include <list>
#include <map>
#include <iostream>

class SqliteTool {
private:
    // 单例对象
    static SqliteTool * shareInstance;
    SqliteTool(const char * db);

    sqlite3 * sqlite;
    char * db;

public:
    /*
    获取单例对象
    db 数据库文件名
    */
    static SqliteTool * GetInstance(const char * db);
    ~SqliteTool();

public:
    // 查询数据
    std::list<std::map<std::string, std::string>> Query(const char * sql);
};
