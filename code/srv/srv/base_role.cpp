#include "base_role.h"




BaseRole::BaseRole(Tool::Struct::Vector3 * vector3) {
    this->position = vector3;
}

BaseRole::~BaseRole() {
    if (this->position != nullptr) {
        delete this->position;
        this->position = nullptr;
    }
}
