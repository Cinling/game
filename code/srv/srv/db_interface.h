#pragma once

#include "sqlite_tool.h"

class DBInterface {
public:
    // ����������ݹ�������������ݱ�
    virtual bool CreateTable() = 0;
    virtual ~DBInterface() {}
};

