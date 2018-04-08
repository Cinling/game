#include "base_role.h"

BaseRole::BaseRole(Tool::Struct::Vector3 * vector3) {
    this->position = vector3;
    this->rotation = 0.0f;
    this->type = RoleType::BaseRole;
}

BaseRole::~BaseRole() {
    if (this->position != nullptr) {
        delete this->position;
        this->position = nullptr;
    }
}

void BaseRole::SetType(int type) {
}

int BaseRole::GetType() {
    return this->type;
}

Tool::Struct::Vector3 BaseRole::GetPosition() {
    return (*this->position);
}

void BaseRole::SetRotation(float rotation) {
}

float BaseRole::GetRotation() {
    return this->rotation;
}
