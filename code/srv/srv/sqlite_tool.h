#pragma once

#include "tool.h"
#include "../lib/sqlite3/sqlite3.h"

#include <list>
#include <map>
#include <iostream>

class SqliteTool {
private:
    // ��������
    static SqliteTool * shareInstance;
    static char * db;
    // db ���ݿ�·�����磺saves/abc
    SqliteTool(const char * db);

    sqlite3 * sqlite;

public:
    /*
    ��ȡ��������
    db ���ݿ��ļ���
    */
    static SqliteTool * GetInstance();
    // �л����ݿ�
    static void UseDB(const char * dbName);
    ~SqliteTool();

public:
    // ����ѯ���ݣ������ؽ������
    // sql ִ�е�sql���
    // return ��ѯ�õ��������б�
    std::list<std::map<std::string, std::string>> Query(const char * sql);

    // ��ִ��sql��䣬�ж��Ƿ�˳��ִ�С�
    // return [true ִ�гɹ�] [false ִ��ʧ��]
    bool ExecSql(const char * sql);

    // ����ѯ���Ƿ���ڡ�
    // tableName ����
    // return �Ƿ���ڣ�true ���ڣ�
    bool IsTableExists(const char * tableName);

private:
    // ������db���ƴ�����Ӧ���ļ��С�
    // dbName ���ݿ�����
    bool CreateDirWithDBName(const char * dbName);
};
