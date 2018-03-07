#include "socket_tcp.h"

#define CONNECT_NUM_MAX 10

SocketTcp::SocketTcp() {
    this->isPause = false;
    this->socketServer = NULL;
}

SocketTcp::SocketTcp(unsigned short port) {
    SocketTcp();
    this->InitServerSocket(port);
}

SocketTcp::~SocketTcp() {
    this->DestroySocket();
}

void SocketTcp::StartSocket() {
    this->isPause = false;

    // 循环接收数据
    SOCKET socketClient;
    sockaddr_in remoteAddr;
    int remoteAddrlen = sizeof(remoteAddr);
    char recvData[256];
    while (true) {
        if (this->isPause) {
            break;
        }
        // debug模式
#define DEBUG

#ifdef DEBUG
        cout << "wating connect ..." << endl;
#endif
        socketClient = accept(socketServer, (SOCKADDR *)&remoteAddr, &remoteAddrlen);
        if (socketClient == INVALID_SOCKET) {
#ifdef DEBUG
            cout << "accept error" << endl;
#endif
            continue;
        }
#ifdef DEBUG
        string ip = GetIp(remoteAddr);
        cout << "receive a connect [" << ip.c_str() << "]" << endl;
#endif
        int ret = recv(socketClient, recvData, 256, 0);
        if (ret > 0) {
            recvData[ret] = 0x00;
#ifdef DEBUG
            cout << this->UTF8ToGB2312(recvData) << endl;
#endif
        }

        // 发送数据
        const char *sendData = this->GB2312ToUTF8("服务端发送的数据");
        send(socketClient, sendData, strlen(sendData), 0);
        closesocket(socketClient);
    }
}

void SocketTcp::PauseSocket() {
    this->isPause = true;
}

void SocketTcp::DestroySocket() {
    closesocket(this->socketServer);
    WSACleanup();

    this->socketServer = NULL;
}

void SocketTcp::InitServerSocket(unsigned short port) {
    // 初始化 WSA
    WORD sockVersion = MAKEWORD(2, 2);
    WSADATA wsaData;
    if (WSAStartup(sockVersion, &wsaData) != 0) {
        cout << "WSAStartup error!" << endl;
        return;
    }

    // 创建套接字
    this->socketServer = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (this->socketServer == INVALID_SOCKET) {
        cout << "create socket error!" << endl;
        return;
    }

    // 绑定IP和端口
    sockaddr_in sin;
    sin.sin_family = AF_INET;
    sin.sin_port = htons(port);
    sin.sin_addr.S_un.S_addr = INADDR_ANY;
    if (bind(this->socketServer, (LPSOCKADDR)&sin, sizeof(sin)) == SOCKET_ERROR) {
        cout << "bind error" << endl;
        return;
    }

    // 监听
    if (listen(this->socketServer, 5) == SOCKET_ERROR) {
        cout << "listen error" << endl;
        return;
    }
}

string SocketTcp::GetIp(sockaddr_in sin) {
    char ip[15];

    sin.sin_addr.S_un.S_un_b.s_b1;

    sprintf_s(ip, 15, "%d.%d.%d.%d", sin.sin_addr.S_un.S_un_b.s_b1, sin.sin_addr.S_un.S_un_b.s_b2, sin.sin_addr.S_un.S_un_b.s_b3, sin.sin_addr.S_un.S_un_b.s_b4);

    return string(ip);
}
