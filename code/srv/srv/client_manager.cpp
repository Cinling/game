#include "client_manager.h"


ClientManager * ClientManager::shareInstance = nullptr;


ClientManager::ClientManager() {
    this->clientThreadMap = std::map<Client *, std::thread *>();
}


ClientManager * ClientManager::GetInstance() {
    if (ClientManager::shareInstance == nullptr) {
        ClientManager::shareInstance = new ClientManager();
    }
    return ClientManager::shareInstance;
}

ClientManager::~ClientManager() {

}

void ClientManager::AddClient(Client * client, std::thread * sendDataThread) {
    this->clientThreadMap[client] = sendDataThread;
}

std::map<Client *, std::thread *> ClientManager::GetClientList() {
    return this->clientThreadMap;
}

void ClientManager::StartClientConnectListener(unsigned short port) {
    SocketListener::AcceptCallBack callback = [](SOCKET clientSocket) {
        ClientManager * clientManager = ClientManager::GetInstance();

        Client * client = new Client(clientSocket);
        std::thread * sendDataThread = new std::thread(LoopSendDataToClient, client);
        clientManager->AddClient(client, sendDataThread);
        sendDataThread->detach();

        return false;
    };

    SocketListener::GetInstance()->NewThreadListenPort(port, callback);
}

void LoopSendDataToClient(Client * client) {
    ClientManager * clientManager = ClientManager::GetInstance();

    while (true) {
        try {
            client->Send(3);

            std::this_thread::sleep_for(std::chrono::milliseconds(1));
        } catch (const std::exception&) {
            std::this_thread::sleep_for(std::chrono::milliseconds(500));
        }
    }
}
