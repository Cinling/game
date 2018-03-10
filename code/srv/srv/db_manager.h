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
         佛祖保佑       永无BUG
*/
//          佛曰:    
//                  写字楼里写字间，写字间里程序员；    
//                  程序人员写程序，又拿程序换酒钱。    
//                  酒醒只在网上坐，酒醉还来网下眠；    
//                  酒醉酒醒日复日，网上网下年复年。    
//                  但愿老死电脑间，不愿鞠躬老板前；    
//                  奔驰宝马贵者趣，公交自行程序员。    
//                  别人笑我忒疯癫，我笑自己命太贱；    
//                  不见满街漂亮妹，哪个归得程序员？

class DBManager {
private:
    // 单例对象
    static DBManager * shareInstance;
    DBManager();
public:
    // 获取单例对象
    static DBManager * GetInstance();
    ~DBManager();

public:
    // 数据表名称
    const std::string TABLE_NAME = "db_version_manager";
    // unsigned int 主键id
    const std::string FIELD_ID = "id";
    // unsigned int 本次数据库更新的版本号
    const std::string FIELD_VERSION = "version";
    // text 本次更新执行的sql
    const std::string FIELD_EXEC_SQL = "exec_sql";
    // unsigned int 本次更新执行的时间戳
    const std::string FIELD_RUN_TIME = "run_time";

public:

    // 【更新数据库】
    // dbName 数据库名
    // return true [更新成功]，false [更新失败] 
    bool DBUpdate();

private:
    // 获取当前数据库版本
    unsigned int GetDBVersion();

    // 【插入一条数据】
    bool Insert(int version, std::string execSql);

    // 【执行一个数据库版本的更新】
    // version 当前的版本号
    // updateSqlLambda 需要执行的lambda方法
    // return 是否更新成功
    bool UpdateOneVersion(int version, std::function<std::string()> updateSqlLambda);

public:
    // sql数据表结构版本，版本号0
    std::string DB_000000_CreateManagerTable(SqliteTool * sqliteTool);
};

