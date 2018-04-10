#include "base_role.h"

BaseRole::BaseRole(Tool::Struct::Vector3 * position) {
    this->position = position;
    this->rotation = 0.0f;
    this->type = RoleType::BaseRole;
}

BaseRole::~BaseRole() {
    if (this->position != nullptr) {
        delete this->position;
    }
    return;
}

void BaseRole::SetType(int type) {
}

int BaseRole::GetType() {
    return this->type;
}

void BaseRole::SetPosition(Tool::Struct::Vector3 * position) {
    this->position = position;
}

Tool::Struct::Vector3 & BaseRole::GetPosition() {
    return (*this->position);
}

void BaseRole::SetRotation(float rotation) {
}

float BaseRole::GetRotation() {
    return this->rotation;
}
