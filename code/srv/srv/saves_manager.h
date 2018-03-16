#pragma once

#include "db_manager.h"
#include "world.h"

class SavesManager {
private:
    static SavesManager * shareInstance;
    SavesManager();
public:
    static SavesManager * GetInstance();
    ~SavesManager();

public:
    // ������Ϸ
    // savesName �浵����
    // return �Ƿ���ɹ�
    bool Save(std::string savesName);

    // ��ȡ�浵�����б�
    // ��ȡ�浵�б�
    // �浵���Ƶ��б�
    std::list<std::string> GetSavesList();

    // ������Ϸ
    // savesName �浵����
    // return �Ƿ���ɹ�
    bool Load(std::string savesName);


private:
    /*�浵�����ϸ�ڴ���*/
    bool SaveWorld();
};

