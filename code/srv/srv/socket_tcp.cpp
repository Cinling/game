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
    while (true) {
        if (this->isPause) {
            break;
        }

#ifdef DEBUG
        std::cout << "wating connect ..." << std::endl;
#endif
        socketClient = accept(socketServer, (SOCKADDR *)&remoteAddr, &remoteAddrlen);
        if (socketClient == INVALID_SOCKET) {
#ifdef DEBUG
            std::cout << "accept error" << std::endl;
#endif
            continue;
        }

        std::thread th(Client, this, socketClient, remoteAddr);
        th.detach();
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
        std::cout << "WSAStartup error!" << std::endl;
        return;
    }

    // 创建套接字
    this->socketServer = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (this->socketServer == INVALID_SOCKET) {
        std::cout << "create socket error!" << std::endl;
        return;
    }

    // 绑定IP和端口
    sockaddr_in sin;
    sin.sin_family = AF_INET;
    sin.sin_port = htons(port);
    sin.sin_addr.S_un.S_addr = INADDR_ANY;
    if (bind(this->socketServer, (LPSOCKADDR)&sin, sizeof(sin)) == SOCKET_ERROR) {
        std::cout << "bind error" << std::endl;
        return;
    }

    // 监听
    if (listen(this->socketServer, 5) == SOCKET_ERROR) {
        std::cout << "listen error" << std::endl;
        return;
    }
}

std::string SocketTcp::GetIp(sockaddr_in sin) {
    char ip[15];

    sin.sin_addr.S_un.S_un_b.s_b1;

    sprintf_s(ip, 15, "%d.%d.%d.%d", sin.sin_addr.S_un.S_un_b.s_b1, sin.sin_addr.S_un.S_un_b.s_b2, sin.sin_addr.S_un.S_un_b.s_b3, sin.sin_addr.S_un.S_un_b.s_b4);

    return std::string(ip);
}

std::string SocketTcp::DoBySocketAction(std::string clientData) {

    size_t cutIndex = clientData.find("|");
    int tcpNum = std::stoi(clientData.substr(0, cutIndex));
    std::string data = clientData.substr(cutIndex + 1);

    std::string retData = "";

    SocketNumManager * socketNumManager = SocketNumManager::GetInstance();

    switch (tcpNum) {
        case 10001:
            retData = socketNumManager->_10001_InitMap(data);
            break;
        case 10002:
            retData = socketNumManager->_10002_Save(data);
            break;
        case 10003:
            retData = socketNumManager->_10003_GetSavesList(data);
            break;
        case 10004:
            retData = socketNumManager->_10004_LoadGame(data);
            break;
        case 20001:
            retData = socketNumManager->_20001_GetMapData(data);
            break;
    }

    return retData;
}

void Client(SocketTcp * socketTcp, SOCKET client, sockaddr_in remoteAddr) {
    //std::string ip = socketTcp->GetIp(remoteAddr);
    //std::cout << "receive a connect [" << ip.c_str() << "]" << std::endl;

    // 接收客户端发送过来的数据
    char recvData[1024];
    int ret = recv(client, recvData, 1024, 0);

    std::string retData = "没有收到客户端的数据";

    if (ret > 0) {
        recvData[ret] = 0x00;

        retData = socketTcp->DoBySocketAction(std::string(socketTcp->UTF8ToGB2312(recvData)));

    }

    // 发送数据
    const char *sendData = socketTcp->GB2312ToUTF8(retData.c_str());
    int sendLen = static_cast<int>(strlen(sendData));

    send(client, std::to_string(sendLen).c_str(), 6, 0);
    send(client, sendData, sendLen, 0);

#ifdef DEBUG
    long long time = Tool::GetTimeSecond();
    std::cout << "[recv " << time << "]:" << std::string(recvData) << std::endl;
    std::cout << "[send " << time << "]:" << std::to_string(sendLen) + std::string(sendData) << std::endl;
#endif

    closesocket(client);
}
