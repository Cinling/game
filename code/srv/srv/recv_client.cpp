#include "recv_client.h"



RecvClient * RecvClient::shareInstance = nullptr;

RecvClient::RecvClient() {
    
}

RecvClient * RecvClient::GetInstance() {
    if (RecvClient::shareInstance == nullptr) {
        RecvClient::shareInstance = new RecvClient();
    }
    return RecvClient::shareInstance;
}

RecvClient::~RecvClient() {
}

void RecvClient::StartLintener(unsigned short port) {
    SocketListener::AcceptCallBack socketCallback = [](SOCKET clientSocket) {
        RecvClient * recvClient = RecvClient::GetInstance();

        char recvData[1024];
        int ret = recv(clientSocket, recvData, 1024, 0);

        std::string retData = "没有收到客户端的数据";

        if (ret > 0) {
            recvData[ret] = 0x00;

            retData = recvClient->DoBySocketAction(std::string(Tool::Func::UTF8ToGB2312(recvData)));
        }

        // 发送数据
        const char *sendData = Tool::Func::GB2312ToUTF8(retData.c_str());
        int sendLen = static_cast<int>(strlen(sendData));

        send(clientSocket, std::to_string(sendLen).c_str(), 7, 0);
        send(clientSocket, sendData, sendLen, 0);

#ifdef DEBUG
        long long time = Tool::GetTimeSecond();
        std::cout << "[recv " << time << "]:" << std::string(recvData) << std::endl;
        std::cout << "[send " << time << "]:" << std::to_string(sendLen) + std::string(sendData) << std::endl;
#endif

        closesocket(clientSocket);

        return false;
    };

    SocketListener::GetInstance()->NewThreadListenPort(port, socketCallback);
}

std::string RecvClient::DoBySocketAction(std::string clientData) {
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
        case 10006:
            retData = socketNumManager->_10006_ExitServerProcess(data);
            break;
        case 10007:
            retData = socketNumManager->_10007_CreateRole(data);
            break;

        case 20001:
            retData = socketNumManager->_20001_GetMapData(data);
            break;
        case 20002:
            retData = socketNumManager->_20002_GetStartGameObjectData(data);
    }

    return retData;
}
