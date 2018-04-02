#include "role_ctrl.h"


RoleCtrl * RoleCtrl::shareInstance = nullptr;


RoleCtrl::RoleCtrl() {
    this->roleMap = new std::map<int, BaseRole *>();
}

RoleCtrl::~RoleCtrl() {

    if (this->roleMap != nullptr) {

        if (this->roleMap->size() > 0) {
            // 释放所有角色
            for (std::map<int, BaseRole *>::iterator it = this->roleMap->begin(); it != roleMap->end(); ++it) {
                BaseRole * role = it->second;
                delete role;
                role = nullptr;
            }
            this->roleMap->clear();
        }

        delete this->roleMap;
        this->roleMap = nullptr;
    }
}

RoleCtrl * RoleCtrl::GetInstance() {
    if (shareInstance == nullptr) {
        shareInstance = new RoleCtrl();
    }
    return shareInstance;
}

std::map<int, BaseRole *> * RoleCtrl::GetRoleMap() {
    return this->roleMap;
}

bool RoleCtrl::Save() {
    bool retBool = true;

    if (this->roleMap != nullptr) {

        RoleDB * roleDB = RoleDB::GetInstance();

        for (std::map<int, BaseRole *>::iterator it = this->roleMap->begin(); it != this->roleMap->end(); ++it) {
            int roleId = it->first;
            BaseRole * baseRole = it->second;
            std::string info = Tool::MapToJsonStr(baseRole->GetInfo());

            if (!roleDB->InsertOnce(roleId, baseRole->GetType(), baseRole->GetPosition(), baseRole->GetRotation(), info)) {
                retBool = false;
                break;
            }
        }
    }

    return retBool;
}

void RoleCtrl::PrintRoleMap() {
    for (std::map<int, BaseRole *>::iterator it = this->roleMap->begin(); it != roleMap->end(); ++it) {
        BaseRole * role = it->second;
        Tool::Struct::Vector3 position = role->GetPosition();
        printf_s("[id:%s position:(%s, %s, %s)]\n", std::to_string(it->first).c_str(),
            std::to_string(position.x).c_str(), std::to_string(position.y).c_str(), std::to_string(position.z).c_str());
    }
}
