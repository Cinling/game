#pragma once

#include "map_db.h"
#include "role_db.h"
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

    // 【更新数据库】
    // dbName 数据库名
    // return true [更新成功]，false [更新失败] 
    bool CreateDBTable();
};