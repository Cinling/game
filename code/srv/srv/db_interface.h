#pragma once

#include "sqlite_tool.h"

class DBInterface {
public:
    // 创建这个数据管理类所需的数据表
    virtual bool CreateTable() = 0;
    virtual ~DBInterface() {}
};

