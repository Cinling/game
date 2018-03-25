#pragma once

#include "tool.h"
#include "base_role.h"

class BaseRole {
protected:
    Tool::Struct::Vector3 * position;
public:
    // 所有子类必须实现的构造函数，用于初始化一个角色对象
    BaseRole(Tool::Struct::Vector3 * vector3);
    ~BaseRole();
};



