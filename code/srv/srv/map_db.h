#pragma once

#include "sqlite_tool.h"
#include "../lib/rapidjson/document.h"
#include "../lib/rapidjson/prettywriter.h"

#include <iostream>
#include <string>
#include <list>
#include <map>

class MapDB {
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
    // �������������
    // return [true �����ɹ�] [false ����ʧ��]
    bool Insert(int id, int worldWidth, int worldLength);
    // ��ȡ�ϴβ����id
    int GetMaxId();

private:
    // ����������תΪjson�ַ���
    std::string GetConfigJsonStr(int worldWidth, int worldLength);
};


