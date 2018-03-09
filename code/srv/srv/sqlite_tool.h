#pragma once

#include "../lib/sqlite3.h"

#include <list>
#include <map>
#include <iostream>

class SqliteTool {
private:
    // ��������
    static SqliteTool * shareInstance;
    SqliteTool(const char * db);

    sqlite3 * sqlite;
    char * db;

public:
    /*
    ��ȡ��������
    db ���ݿ��ļ���
    */
    static SqliteTool * GetInstance(const char * db);
    ~SqliteTool();

public:
    // ��ѯ����
    std::list<std::map<std::string, std::string>> Query(const char * sql);
};
