#include "plant.h"




Plant::Plant(Tool::Struct::Vector3 * vector3) : BaseRole(vector3) {
    this->type = RoleType::Plant;
}

Plant::~Plant() {
}

void Plant::UPSDo(void * voidRoleCtrl) {
}

const std::map<std::string, std::string> Plant::GetInfo() {
    return std::map<std::string, std::string>();
}

const std::string Plant::GetSpecialShowData() {
    return std::string();
}

void Plant::SetInfo(std::map<std::string, std::string>) {
}
