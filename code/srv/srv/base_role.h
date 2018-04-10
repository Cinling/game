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
    BaseRole(Tool::Struct::Vector3 * position);
    ~BaseRole();

    // ���ý�ɫ����
    void SetType(int type);
    // ��ȡ��ɫ����
    int GetType();

    // ���ý�ɫλ��
    void SetPosition(Tool::Struct::Vector3 * position);
    // ��ȡ��ɫ��λ����Ϣ
    Tool::Struct::Vector3 & GetPosition();

    // ���ý�ɫ����
    void SetRotation(float rotation);
    // ��ȡ��ɫ��������
    float GetRotation();


    // ������ϸ���ݣ����ڴ����ݿ��ж�ȡ���ݣ�
    virtual void SetInfo(std::map<std::string, std::string> &info) = 0;
    // ��ȡ��ɫ����ϸ���ݣ����ڿͻ���չʾ
    virtual const std::map<std::string, std::string> GetInfo() = 0;

public:
    // ÿ���߼�֡��Ҫˢ�µ�����
    virtual void UPSDo(void * voidRoleCtrl) = 0;

    // ��ȡ��ʾ��������ݣ��磺������״̬�ȣ������ɲ�ͬ�����͸�ʽ�᲻ͬ
    virtual const std::string GetSpecialShowData() = 0;
};



