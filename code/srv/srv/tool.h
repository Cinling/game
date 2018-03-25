#pragma once

#include <io.h>  
#include <direct.h>  

#include <iostream>
#include <list>
#include <vector>
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

        // �������ļ��С�
        // folder �ļ����·��
        bool CreateDir(std::string folder);
        // ��ɾ���ļ���
        // fileName �ļ����·��
        // return [true ����ɹ�]�� [false ����ʧ��]
        bool DeleteFile(std::string fileName);
        // ����������
        // oldDir ԭ���ļ���·��
        // newDir �µ��ļ�·��
        // return [true ����ɹ�]�� [false ����ʧ��]
        bool Rename(std::string oldDir, std::string newDir);
        // ���ж��ļ����Ƿ���ڡ�
        // folder �ļ����·��
        // return [true �ļ�����] [false �ļ�������]
        bool IsDirExists(std::string folder);
        // ����ȡĿ¼�������ļ����ļ�����
        // folder �ļ����·�����磺saves/   ��ע�⣺������"/"��β��
        // return [true �ļ�����] [false �ļ�������]
        std::vector<std::string> GetChildFiles(std::string folder);
    }

    namespace Struct {
        class Vector3 {
        public:
            float x;
            float y;
            float z;
            
            Vector3(float x, float y, float z);
        };
    }
}