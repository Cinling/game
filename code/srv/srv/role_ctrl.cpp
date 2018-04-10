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

bool RoleCtrl::Load() {
    bool retBool = true;

    RoleDB * roleDB = RoleDB::GetInstance();
    std::list<std::map<std::string, std::string>> roleInfoList = roleDB->SelectAll();
    if (this->roleMap != nullptr) {
        delete this->roleMap;
        this->roleMap = nullptr;
    }
    this->roleMap = new std::map<int, BaseRole *>();

    for (std::list<std::map<std::string, std::string>>::iterator it = roleInfoList.begin(); it != roleInfoList.end(); ++it) {
        std::map<std::string, std::string> roleRowData = *it;

        int id = std::stoi(roleRowData[RoleDB::FIELD_ID]);
        int roleType = std::stoi(roleRowData[RoleDB::FIELD_TYPE]);
        float x = std::stof(roleRowData[RoleDB::FIELD_X]);
        float y = std::stof(roleRowData[RoleDB::FIELD_Y]);
        float z = std::stof(roleRowData[RoleDB::FIELD_Z]);
        float rotation = std::stof(roleRowData[RoleDB::FIELD_ROTATION]);
        std::string infoStr = roleRowData[RoleDB::FIELD_INFO];

        Tool::Struct::Vector3 * position = new Tool::Struct::Vector3(x, y, z);
        std::map<std::string, std::string> info = Tool::JaonStrToMap(infoStr);

        if (!this->AddRoleByType(id, roleType, position, rotation, info)) {
            retBool = false;
        }
    }

    return retBool;
}

bool RoleCtrl::AddRoleByType(int id, int roleType, Tool::Struct::Vector3 * position, float rotation, std::map<std::string, std::string> &info) {
    BaseRole * baseRole = nullptr;

    switch (roleType) {
        case RoleType::Animal:
            baseRole = this->AddRoleWithLoad<Animal>(id, roleType, position, rotation, info);
            break;
        case RoleType::Plant:
            baseRole = this->AddRoleWithLoad<Plant>(id, roleType, position, rotation, info);
            break;
        case RoleType::Tree:
            baseRole = this->AddRoleWithLoad<Tree>(id, roleType, position, rotation, info);
            break;
    }

    if (baseRole != nullptr) {
        return true;
    }
    return false;
}

void RoleCtrl::PrintRoleMap() {
    for (std::map<int, BaseRole *>::iterator it = this->roleMap->begin(); it != roleMap->end(); ++it) {
        BaseRole * role = it->second;
        Tool::Struct::Vector3 position = role->GetPosition();
        printf_s("[id:%s position:(%s, %s, %s)]\n", std::to_string(it->first).c_str(),
            std::to_string(position.x).c_str(), std::to_string(position.y).c_str(), std::to_string(position.z).c_str());
    }
}
