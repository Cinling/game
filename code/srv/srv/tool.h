#pragma once

#include <iostream>
#include <string>
#include <time.h>

namespace Tool {
    // ��ȡϵͳʱ������룩
    long long GetTimeSecond();

    // �ļ�����
    namespace File {
        // ������
        // oldDir ԭ���ļ���·��
        // newDir �µ��ļ�·��
        // return [true ����ɹ�]�� [false ����ʧ��]
        bool Rename(std::string oldDir, std::string newDir);
    }
}