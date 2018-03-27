#include "base_role.h"

BaseRole::BaseRole(Tool::Struct::Vector3 * vector3) {
    this->position = vector3;
    this->type = RoleType::BaseRole;
}

BaseRole::~BaseRole() {
    if (this->position != nullptr) {
        delete this->position;
        this->position = nullptr;
    }
}

int BaseRole::GetType() {
    return this->type;
}

Tool::Struct::Vector3 BaseRole::GetPosition() {
    return (*this->position);
}
