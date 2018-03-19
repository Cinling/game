#pragma once

#include "map_db.h"
#include "sqlite_tool.h"
#include "tool.h"

#include <iostream>
#include <string>
#include <functional>

/*
                   _ooOoo_
                  o8888888o
                  88" . "88
                  (| -_- |)
                  O\  =  /O
               ____/`---'\____
             .'  \\|     |//  `.
            /  \\|||  :  |||//  \
           /  _||||| -:- |||||-  \
           |   | \\\  -  /// |   |
           | \_|  ''\---/''  |   |
           \  .-\__  `-`  ___/-. /
         ___`. .'  /--.--\  `. . __
      ."" '<  `.___\_<|>_/___.'  >'"".
     | | :  `- \`.;`\ _ /`;.`/ - ` : | |
     \  \ `-.   \_ __\ /__ _/   .-` /  /
======`-.____`-.___\_____/___.-`____.-'======
                   `=---='
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
         ���汣��       ����BUG
*/

class DBManager {
private:
    // ��������
    static DBManager * shareInstance;
    DBManager();
public:
    // ��ȡ��������
    static DBManager * GetInstance();
    ~DBManager();

public:
    // ���ݱ�����
    static const std::string TABLE_NAME;
    // INTEGER ����id
    static const std::string FIELD_ID;
    // INTEGER �������ݿ���µİ汾��
    static const std::string FIELD_VERSION;
    // TEXT ���θ���ִ�е�sql
    static const std::string FIELD_EXEC_SQL;
    // INTEGER ���θ���ִ�е�ʱ���
    static const std::string FIELD_RUN_TIME;

public:

    // ���������ݿ⡿
    // dbName ���ݿ���
    // return true [���³ɹ�]��false [����ʧ��] 
    bool DBUpdate();

private:
    // ��ȡ��һ��DB�汾��
    unsigned int GetNextDBVersion();

    // ������һ���������ݿ���Ϣ�����ݡ�
    // return [true ����ɹ�], [false ����ʧ��]
    bool Insert(int version, std::string execSql);

    // ��ִ��һ�����ݿ�汾�ĸ��¡�
    // version ��ǰ�İ汾��
    // updateSqlLambda ��Ҫִ�е�lambda����
    // return [true ���³ɹ�] [false ����ʧ��]
    bool UpdateOneVersion(int version, std::function<std::string()> updateSqlLambda);

    // ���������ݿ�ʱ��ȡ����ֵ��
    // ���ٴ�����
    // return ִ�гɹ����� sql��䣬ִ��ʧ�ܷ��ؿ��ַ���
    std::string GetReturnByUpdateDB(std::string sql);

public:
    // sql���ݱ�ṹ�汾���汾��0
    std::string _000000_CreateDBVersionManagerTable();
    // ������ͼ���ݱ�
    std::string _000001_CreateMapTable();
};