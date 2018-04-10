#pragma once

#include "socket_num_manager.h"
#include "tool.h"

#include <winsock2.h>
#include <thread>
#include <iostream>
#include <string>

#pragma comment(lib, "ws2_32.lib")


// tcp 通信类
class SocketTcp {
private:
    // 服务端套接字
    SOCKET socketServer;
    // 判断是否暂停socket的状态
    bool isPause;

private:
    SocketTcp();

public:
    SocketTcp(unsigned short port);
    ~SocketTcp();

    // 开启socket监听
    void StartSocket();
    // 暂停socket监听
    void PauseSocket();
    // 关闭socket
    void DestroySocket();

    // 用于客户端连接时，穿件新线程使用
    friend void Client(SocketTcp * socketTcp, SOCKET client, sockaddr_in remoteAddr);

private:
    // 初始化服务端套接字
    void InitServerSocket(unsigned short port);

    // 将 int32 的IP数据转为可读形式的ip
    std::string GetIp(sockaddr_in socketAddrIn);

private:
    // 根据客户端返回的数据，执行相应的处理
    // clientData 客户端发送过来的数据
    // return 返回给客户端的数据
    std::string DoBySocketAction(std::string clientData);

private:
    //UTF-8到GB2312的转换  
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
    //GB2312到UTF-8的转换  
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


// 用于客户端连接时，穿件新线程使用
void Client(SocketTcp * socketTcp, SOCKET client, sockaddr_in remoteAddr);