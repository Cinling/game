#pragma once

#include "role_type.h"
#include "tool.h"

class BaseRole {
protected:
    // 角色类型
    int type;
protected:
    Tool::Struct::Vector3 * position;
public:
    // 所有子类必须实现的构造函数，用于初始化一个角色对象
    BaseRole(Tool::Struct::Vector3 * vector3);
    ~BaseRole();

    // 获取角色类型
    int GetType();
    // 获取角色的位置信息
    Tool::Struct::Vector3 GetPosition();

public:
    // 每个逻辑帧需要刷新的数据
    virtual void UPSDo(void * voidRoleCtrl) = 0;
    // 获取角色的详细数据，用于客户端展示
    virtual const std::map<std::string, std::string> GetInfo() = 0;
};



