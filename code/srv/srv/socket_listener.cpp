#include "socket_listener.h"


SocketListener * SocketListener::shareInstance = nullptr;

SocketListener::SocketListener() {
    this->portSocketMap = std::map<unsigned short, SOCKET>();
}

SocketListener * SocketListener::GetInstance() {
    if (SocketListener::shareInstance == nullptr) {
        SocketListener::shareInstance = new SocketListener();
    }
    return SocketListener::shareInstance;
}

SocketListener::~SocketListener() {
}

void SocketListener::NewThreadListenPort(unsigned short port, AcceptCallBack listenCallback) {
    std::thread * listenThread = new std::thread(ListenPort, this, port, listenCallback);
    listenThread->detach();
}

void ListenPort(SocketListener * socketListener, unsigned short port, SocketListener::AcceptCallBack listenCallback) {

    // 判断端口是否已经有 SOCKET 在监听，如果有，则关闭原来的 SOCKET 开启新的 SOCKET
    if (socketListener->portSocketMap.find(port) != socketListener->portSocketMap.end()) {
        SOCKET prevSocket = socketListener->portSocketMap.find(port)->second;
        closesocket(prevSocket);
    }

    // 初始化 WSA
    WORD sockVersion = MAKEWORD(2, 2);
    WSADATA wsaData;
    if (WSAStartup(sockVersion, &wsaData) != 0) {
        std::cout << "WSAStartup error!" << std::endl;
        return;
    }

    // 创建套接字
    SOCKET serverSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (serverSocket == INVALID_SOCKET) {
        std::cout << "create socket error!" << std::endl;
        return;
    }

    // 绑定IP和端口
    sockaddr_in sin;
    sin.sin_family = AF_INET;
    sin.sin_port = htons(port);
    sin.sin_addr.S_un.S_addr = INADDR_ANY;
    if (bind(serverSocket, (LPSOCKADDR) &sin, sizeof(sin)) == SOCKET_ERROR) {
        std::cout << "bind error" << std::endl;
        return;
    }

    // 监听
    if (listen(serverSocket, 5) == SOCKET_ERROR) {
        std::cout << "listen error" << std::endl;
        return;
    }

    // 保存监听该端口的 SOCKET
    socketListener->portSocketMap[port] = serverSocket;
    
    // 等待客户端连接
    SOCKET socketClient;
    sockaddr_in remoteAddr;
    int remoteAddrlen = sizeof(remoteAddr);
    while (true) {
        socketClient = accept(serverSocket, (SOCKADDR *) &remoteAddr, &remoteAddrlen);

        std::thread clientConnectThread = std::thread(listenCallback, socketClient);
        clientConnectThread.detach();
    }
}
