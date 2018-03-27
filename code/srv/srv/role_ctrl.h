#pragma once

#include "tool.h"
#include "base_role.h"
#include "animal.h"

class RoleCtrl {
private:
    static RoleCtrl * shareInstance;
    RoleCtrl();

    std::map<int, BaseRole *> roleList;

public:
    static RoleCtrl * GetInstance();

    // 创建一个对象，这个对象必须继承 BaseRole
    template<typename T, typename std::enable_if < std::is_base_of<BaseRole, T>{}, int > ::type = 0 >
    T * CreateRole(Tool::Struct::Vector3 * vector3);
};

template<typename T, typename std::enable_if < std::is_base_of<BaseRole, T>{}, int > ::type = 0 >
inline T * RoleCtrl::CreateRole(Tool::Struct::Vector3 * vector3) {
    T  * role = new T(vector3);
    this->roleList.push_back(role);
    return role;
}
