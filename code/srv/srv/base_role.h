#pragma once

#include "role_type.h"
#include "tool.h"

class BaseRole {
protected:
    // 角色类型
    int type;
protected:
    // 角色的位置
    Tool::Struct::Vector3 * position;
    // 角色的面向方向(360°)
    float rotation;
public:
    // 所有子类必须实现的构造函数，用于初始化一个角色对象
    BaseRole(Tool::Struct::Vector3 * position);
    ~BaseRole();

    // 设置角色类型
    void SetType(int type);
    // 获取角色类型
    int GetType();

    // 设置角色位置
    void SetPosition(Tool::Struct::Vector3 * position);
    // 获取角色的位置信息
    Tool::Struct::Vector3 & GetPosition();

    // 设置角色方向
    void SetRotation(float rotation);
    // 获取角色的面向方向
    float GetRotation();


    // 设置详细数据（用于从数据库中读取数据）
    virtual void SetInfo(std::map<std::string, std::string> &info) = 0;
    // 获取角色的详细数据，用于客户端展示
    virtual const std::map<std::string, std::string> GetInfo() = 0;

public:
    // 每个逻辑帧需要刷新的数据
    virtual void UPSDo(void * voidRoleCtrl) = 0;

    // 获取显示特殊的数据，如：动作、状态等，具体由不同的类型格式会不同
    virtual const std::string GetSpecialShowData() = 0;
};



