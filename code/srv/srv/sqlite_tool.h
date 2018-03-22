#pragma once

#include "tool.h"
#include "../lib/sqlite3/sqlite3.h"

#include <list>
#include <map>
#include <iostream>

class SqliteTool {
private:
    // 单例对象
    static SqliteTool * shareInstance;
    static char * db;
    // db 数据库路径，如：saves/abc
    SqliteTool(const char * db);

    sqlite3 * sqlite;

public:
    /*
    获取单例对象
    db 数据库文件名
    */
    static SqliteTool * GetInstance();
    // 切换数据库
    static void UseDB(const char * dbName);
    ~SqliteTool();

public:
    // 【查询数据，并返回结果集】
    // sql 执行的sql语句
    // return 查询得到的数据列表
    std::list<std::map<std::string, std::string>> Query(const char * sql);

    // 【执行sql语句，判断是否顺利执行】
    // return [true 执行成功] [false 执行失败]
    bool ExecSql(const char * sql);

    // 【查询表是否存在】
    // tableName 表明
    // return 是否存在（true 存在）
    bool IsTableExists(const char * tableName);

private:
    // 【根据db名称创建对应的文件夹】
    // dbName 数据库名称
    bool CreateDirWithDBName(const char * dbName);
};
