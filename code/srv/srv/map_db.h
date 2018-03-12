#pragma once

#include "db_interface.h"

class MapDB : public DBInterface {
private:
    static MapDB * shareInstance;
    MapDB();

public:
    static MapDB * GetInstance();
    ~MapDB();
public:
    // ���ݱ���
    static const std::string TABLE_NAME;
    // INTEGER ����
    static const std::string FIELD_ID;
    // TEXT ��ͼ��Ϣ����̬�������
    static const std::string FIELD_INFO;
    // TEXT ���ã����ͼ��С�����������ӵȣ������޸ĵ���Ϣ
    static const std::string FIELD_CONFIG;

public:

    // �洢����
    virtual std::list<std::map<std::string, std::string>> Save(std::list<std::map<std::string, std::string>>) override;
    // ��ȡ����
    virtual std::list<std::map<std::string, std::string>> Load(std::list<std::map<std::string, std::string>>) override;
};


