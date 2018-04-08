#pragma once

#include "db_interface.h"
#include "tool.h"

class RoleDB : public DBInterface {
private:
    static RoleDB * shareInstance;
    RoleDB();
public:
    static RoleDB * GetInstance();
    ~RoleDB();

    // ͨ�� DBInterface �̳�
    virtual bool CreateTable() override;
public:
    // ����
    static const std::string TABLE_NAME;
    // INTEGER ��������ɫid
    static const std::string FIELD_ID;
    // INTEGER ��ɫ����
    static const std::string FIELD_TYPE;
    // FLOAT ��ɫλ��x
    static const std::string FIELD_X;
    // FLOAT ��ɫλ��y
    static const std::string FIELD_Y;
    // FLOAT ��ɫλ��z
    static const std::string FIELD_Z;
    // FLOAT ��ɫ����Ƕ�
    static const std::string FIELD_ROTATION;
    // TEXT ������������Ϣ
    static const std::string FIELD_INFO;

public:
    // ���洢һ����ɫ���ݡ�
    // id ��ɫid
    // type ��ɫ����
    // position ��ɫλ��
    // rotation ��ɫ����
    // info �����Ե���Ϣ
    bool InsertOnce(int id, int type, Tool::Struct::Vector3 position, float rotation, std::string info);

    // ��ѯ��������
    std::list<std::map<std::string, std::string>> SelectAll();
};

