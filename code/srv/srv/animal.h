#pragma once

#include "base_role.h"

class Animal : public BaseRole {
public:
    Animal(Tool::Struct::Vector3 * vector3);
    ~Animal();
};

