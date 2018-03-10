#pragma once

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
//          ��Ի:    
//                  д��¥��д�ּ䣬д�ּ������Ա��    
//                  ������Աд�������ó��򻻾�Ǯ��    
//                  ����ֻ���������������������ߣ�    
//                  ��������ո��գ����������긴�ꡣ    
//                  ��Ը�������Լ䣬��Ը�Ϲ��ϰ�ǰ��    
//                  ���۱������Ȥ���������г���Ա��    
//                  ����Ц��߯��񲣬��Ц�Լ���̫����    
//                  ��������Ư���ã��ĸ���ó���Ա��

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
    const std::string TABLE_NAME = "db_version_manager";
    // unsigned int ����id
    const std::string FIELD_ID = "id";
    // unsigned int �������ݿ���µİ汾��
    const std::string FIELD_VERSION = "version";
    // text ���θ���ִ�е�sql
    const std::string FIELD_EXEC_SQL = "exec_sql";
    // unsigned int ���θ���ִ�е�ʱ���
    const std::string FIELD_RUN_TIME = "run_time";

public:

    // ���������ݿ⡿
    // dbName ���ݿ���
    // return true [���³ɹ�]��false [����ʧ��] 
    bool DBUpdate();

private:
    // ��ȡ��ǰ���ݿ�汾
    unsigned int GetDBVersion();

    // ������һ�����ݡ�
    bool Insert(int version, std::string execSql);

    // ��ִ��һ�����ݿ�汾�ĸ��¡�
    // version ��ǰ�İ汾��
    // updateSqlLambda ��Ҫִ�е�lambda����
    // return �Ƿ���³ɹ�
    bool UpdateOneVersion(int version, std::function<std::string()> updateSqlLambda);

public:
    // sql���ݱ�ṹ�汾���汾��0
    std::string DB_000000_CreateManagerTable(SqliteTool * sqliteTool);
};

