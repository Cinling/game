#pragma once

#include "tool.h"
#include "role_db.h"

#include "base_role.h"
#include "animal.h"
#include "plant.h"
#include "tree.h"

class RoleCtrl {
private:
    static RoleCtrl * shareInstance;
    RoleCtrl();
    ~RoleCtrl();

    int roleMapId = 0;
    std::map<int, BaseRole *> * roleMap;

public:
    static RoleCtrl * GetInstance();

    // 创建一个对象，这个对象必须继承 BaseRole
    template<typename T>
    T * CreateRole(Tool::Struct::Vector3 * position);

    // 加载角色数据
    template<typename T>
    T * AddRoleWithLoad(int id, int roleType, Tool::Struct::Vector3 * position, float rotation, std::map<std::string, std::string> &info);

    // 获取所有角色列表的map
    std::map<int, BaseRole *> * GetRoleMap();

    // 保存所有角色的数据
    bool Save();
    // 载入所有角色
    bool Load();
    // 清除当前单例对象
    bool Clear();

private:
    // 添加一个角色数据
    bool AddRoleByType(int id, int roleType, Tool::Struct::Vector3 * position, float rotation, std::map<std::string, std::string> &info);

    // 释放所有角色对象，以及释放 roleMap
    bool FreeRoleMap();

public: //调试的方法
    void PrintRoleMap();
};


template<typename T>
inline T * RoleCtrl::CreateRole(Tool::Struct::Vector3 * position) {

    T  * role = new T(position);
    (*this->roleMap)[this->roleMapId] = role;
    ++this->roleMapId;
    return role;
}

template<typename T>
inline T * RoleCtrl::AddRoleWithLoad(int id, int roleType, Tool::Struct::Vector3 * position, float rotation, std::map<std::string, std::string> &info) {

    T  * role = new T(position);
    role->SetType(roleType);
    role->SetRotation(rotation);
    role->SetInfo(info);

    (*this->roleMap)[id] = role;
    if (this->roleMapId < id) {
        this->roleMapId = id;
    }

    return role;
}
