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
    static const std::string TABLE_NAME;
    // INTEGER 主键id
    static const std::string FIELD_ID;
    // INTEGER 本次数据库更新的版本号
    static const std::string FIELD_VERSION;
    // TEXT 本次更新执行的sql
    static const std::string FIELD_EXEC_SQL;
    // INTEGER 本次更新执行的时间戳
    static const std::string FIELD_RUN_TIME;

public:

    // 【更新数据库】
    // dbName 数据库名
    // return true [更新成功]，false [更新失败] 
    bool DBUpdate();

private:
    // 获取下一个DB版本号
    unsigned int GetNextDBVersion();

    // 【插入一条更新数据库信息的数据】
    // return [true 处理成功], [false 处理失败]
    bool Insert(int version, std::string execSql);

    // 【执行一个数据库版本的更新】
    // version 当前的版本号
    // updateSqlLambda 需要执行的lambda方法
    // return [true 更新成功] [false 更新失败]
    bool UpdateOneVersion(int version, std::function<std::string()> updateSqlLambda);

    // 【更新数据库时获取返回值】
    // 减少代码量
    // return 执行成功返回 sql语句，执行失败返回空字符串
    std::string GetReturnByUpdateDB(std::string sql);

public:
    // sql数据表结构版本，版本号0
    std::string _000000_CreateDBVersionManagerTable();
    // 创建地图数据表
    std::string _000001_CreateMapTable();
};