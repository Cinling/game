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

    // ���������ݿ⡿
    // dbName ���ݿ���
    // return true [���³ɹ�]��false [����ʧ��] 
    bool CreateDBTable();
};