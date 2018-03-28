#pragma once

#include "tool.h"
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

    // ����һ����������������̳� BaseRole
    template<class T,
        typename std::enable_if < std::is_base_of<BaseRole, T>{}, int > ::type = 0 >
        T * CreateRole(Tool::Struct::Vector3 * vector3);

    // ��ȡ���н�ɫ�б��map
    std::map<int, BaseRole *> * GetRoleMap();

public: //���Եķ���
    void PrintRoleMap();
};

template<class T,
    typename std::enable_if < std::is_base_of<BaseRole, T>{}, int > ::type = 0 >
    inline T * RoleCtrl::CreateRole(Tool::Struct::Vector3 * vector3) {

    T  * role = new T(vector3);
    //this->roleMap->insert(std::pair<int, BaseRole *>(this->roleMapId, role));
    (*this->roleMap)[this->roleMapId] = role;
    ++this->roleMapId;
    return role;
}
