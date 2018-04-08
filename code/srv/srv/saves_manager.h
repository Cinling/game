#pragma once

#include "db_manager.h"
#include "world_manager.h"
#include "tool.h"

class SavesManager {
private:
    static SavesManager * shareInstance;
    SavesManager();

    // �浵·��
    static const std::string SAVES_PATH;
    // ��ʱ�浵����
    static const std::string TEMPORARY_SAVES;
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
    std::vector<std::string> GetSavesList();

    // ������Ϸ
    // savesName �浵����
    // return �Ƿ���ɹ�
    bool Load(std::string savesName);

private:
    /*�浵�����ϸ�ڴ���*/
    bool SaveWorld();
    bool LoadWorld();
    bool SaveRole();
    bool LoadRole();
    
    // ��������ʱ�浵��
    // ʵ���ϵĸ���
    // savesName �浵����
    // return true [���ݳɹ�]��false [����ʧ��] 
    bool BackupTemporarySaves(std::string savesName);
    // ��ɾ����ʱ�浵��
    // return true [ɾ���ɹ�]��false [ɾ��ʧ��] 
    bool DeleteTemporarySaves();
    // ���ָ��浵���浵ʧ��ʱ���á�
    // ʵ�����ǰ����ָĻ�ԭ��������
    // savesName �浵����
    // return true [�ָ��ɹ�]��false [�ָ�ʧ��] 
    bool RecoveryTemporarySaves(std::string savesName);

    // �жϴ浵�Ƿ����
    bool IsSavesExists(std::string savesName);
};

