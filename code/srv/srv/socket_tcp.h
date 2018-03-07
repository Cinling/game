#pragma once

#include <winsock2.h>
#include <iostream>

#pragma comment(lib, "ws2_32.lib")

using namespace std;

// tcp ͨ����
class SocketTcp {
private:
    // ������׽���
    SOCKET socketServer;
    // �ж��Ƿ���ͣsocket��״̬
    bool isPause;

private:
    SocketTcp();

public:
    SocketTcp(unsigned short port);
    ~SocketTcp();

    // ����socket����
    void StartSocket();
    // ��ͣsocket����
    void PauseSocket();
    // �ر�socket
    void DestroySocket();

private:
    // ��ʼ��������׽���
    void InitServerSocket(unsigned short port);

    // �� int32 ��IP����תΪ�ɶ���ʽ��ip
    string GetIp(sockaddr_in socketAddrIn);

    //UTF-8��GB2312��ת��  
    char* UTF8ToGB2312(const char* utf8) {
        int len = MultiByteToWideChar(CP_UTF8, 0, utf8, -1, NULL, 0);
        wchar_t* wstr = new wchar_t[len + 1];
        memset(wstr, 0, len + 1);
        MultiByteToWideChar(CP_UTF8, 0, utf8, -1, wstr, len);
        len = WideCharToMultiByte(CP_ACP, 0, wstr, -1, NULL, 0, NULL, NULL);
        char* str = new char[len + 1];
        memset(str, 0, len + 1);
        WideCharToMultiByte(CP_ACP, 0, wstr, -1, str, len, NULL, NULL);
        if (wstr) delete[] wstr;
        return str;
    }
    //GB2312��UTF-8��ת��  
    char* GB2312ToUTF8(const char* gb2312) {
        int len = MultiByteToWideChar(CP_ACP, 0, gb2312, -1, NULL, 0);
        wchar_t* wstr = new wchar_t[len + 1];
        memset(wstr, 0, len + 1);
        MultiByteToWideChar(CP_ACP, 0, gb2312, -1, wstr, len);
        len = WideCharToMultiByte(CP_UTF8, 0, wstr, -1, NULL, 0, NULL, NULL);
        char* str = new char[len + 1];
        memset(str, 0, len + 1);
        WideCharToMultiByte(CP_UTF8, 0, wstr, -1, str, len, NULL, NULL);
        if (wstr) delete[] wstr;
        return str;
    }
};