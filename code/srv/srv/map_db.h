#pragma once

#include "db_interface.h"
#include "../lib/rapidjson/document.h"
#include "../lib/rapidjson/prettywriter.h"

#include <iostream>
#include <string>
#include <list>
#include <map>

class MapDB : public DBInterface {
private:
    static MapDB * shareInstance;
    MapDB();

public:
    static MapDB * GetInstance();
    ~MapDB();

    // ͨ�� DBInterface �̳�
    virtual bool CreateTable() override;
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
    // [���Ƽ�ʹ��]�������������
    // return [true �����ɹ�] [false ����ʧ��]
    bool Insert(int id, int worldWidth, int worldLength);
    bool Insert(int id, std::string config, std::string info);
    // ��ȡ���ݿ�������Idֵ
    int GetMaxId();
    // ��ȡ��������
    std::list<std::map<std::string, std::string>> SelectAll();

private:
    // ����������תΪjson�ַ���
    std::string GetConfigJsonStr(int worldWidth, int worldLength);
};


