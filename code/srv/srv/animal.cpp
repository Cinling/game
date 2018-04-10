#include "animal.h"



Animal::Animal(Tool::Struct::Vector3 * vector3) : BaseRole(vector3) {
    this->type = RoleType::Animal;
}

Animal::~Animal() {
}

void Animal::UPSDo(void * voidRoleCtrl) {
   
}

const std::map<std::string, std::string> Animal::GetInfo() {
    return std::map<std::string, std::string>();
}

void Animal::SetInfo(std::map<std::string, std::string>& info) {
}

const std::string Animal::GetSpecialShowData() {
    return std::string();
}
