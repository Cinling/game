#pragma once

#include <io.h>  
#include <direct.h>  

#include <iostream>
#include <string>
#include <time.h>

// �ѱ�����תΪ�ַ���
#define NameToStr(x) #x
#define PATH_DELIMITER '\\' 

namespace Tool {
    // ��ȡϵͳʱ������룩
    long long GetTimeSecond();

    // �ļ�����
    namespace File {
        // ����������
        // oldDir ԭ���ļ���·��
        // newDir �µ��ļ�·��
        // return [true ����ɹ�]�� [false ����ʧ��]
        bool Rename(std::string oldDir, std::string newDir);

        // �������ļ��С�
        // folder �ļ����·��
        bool CreateDir(std::string folder);

        // �ж��ļ����Ƿ����
        // folder �ļ����·��
        // return [true �ļ�����] [false �ļ�������]
        bool IsDirExists(std::string folder);
    }
}