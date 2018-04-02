#pragma once

#include "role_type.h"
#include "tool.h"

class BaseRole {
protected:
    // ��ɫ����
    int type;
protected:
    // ��ɫ��λ��
    Tool::Struct::Vector3 * position;
    // ��ɫ��������(360��)
    float rotation;
public:
    // �����������ʵ�ֵĹ��캯�������ڳ�ʼ��һ����ɫ����
    BaseRole(Tool::Struct::Vector3 * vector3);
    ~BaseRole();

    // ��ȡ��ɫ����
    int GetType();
    // ��ȡ��ɫ��λ����Ϣ
    Tool::Struct::Vector3 GetPosition();
    // ��ȡ��ɫ��������
    float GetRotation();

public:
    // ÿ���߼�֡��Ҫˢ�µ�����
    virtual void UPSDo(void * voidRoleCtrl) = 0;
    // ��ȡ��ɫ����ϸ���ݣ����ڿͻ���չʾ
    virtual const std::map<std::string, std::string> GetInfo() = 0;
    // ��ȡ��ʾ��������ݣ��磺������״̬�ȣ������ɲ�ͬ�����͸�ʽ�᲻ͬ
    virtual const std::string GetSpecialShowData() = 0;
};



