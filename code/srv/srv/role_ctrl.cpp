#include "role_ctrl.h"


RoleCtrl * RoleCtrl::shareInstance = nullptr;


RoleCtrl::RoleCtrl() {
    this->roleMap = std::map<int, BaseRole *>();
}

RoleCtrl::~RoleCtrl() {
    
    if (this->roleMap.size() > 0) {
        // 释放所有角色
        for (std::map<int, BaseRole *>::iterator it = this->roleMap.begin(); it != roleMap.end(); ++it) {
            BaseRole * role = it->second;
            delete role;
            role = nullptr;
        }

        this->roleMap.clear();
    }
}

RoleCtrl * RoleCtrl::GetInstance() {
    if (shareInstance == nullptr) {
        shareInstance = new RoleCtrl();
    }
    return shareInstance;
}

void RoleCtrl::PrintRoleMap() {
    for (std::map<int, BaseRole *>::iterator it = this->roleMap.begin(); it != roleMap.end(); ++it) {
        BaseRole * role = it->second;
        Tool::Struct::Vector3 position = role->GetPosition();
        printf_s("[id:%s position:(%s, %s, %s)]\n", std::to_string(it->first).c_str(),
            std::to_string(position.x).c_str(), std::to_string(position.y).c_str(), std::to_string(position.z).c_str());
    }
}
