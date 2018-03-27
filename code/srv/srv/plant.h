#pragma once
#include "base_role.h"

class Plant : public BaseRole {
public:
    Plant(Tool::Struct::Vector3 * vector3);
    ~Plant();

    // Í¨¹ý BaseRole ¼Ì³Ð
    virtual void UPSDo(void * voidRoleCtrl) override;
    virtual const std::map<std::string, std::string> GetInfo() override;
};

