#pragma once

#include "tool.h"
#include "base_role.h"

class BaseRole {
protected:
    Tool::Struct::Vector3 * position;
public:
    // �����������ʵ�ֵĹ��캯�������ڳ�ʼ��һ����ɫ����
    BaseRole(Tool::Struct::Vector3 * vector3);
    ~BaseRole();
};



