#include "client.h"





Client::Client(SOCKET socket) {
    this->socket = socket;
    this->sendDataQueue = std::queue<std::string *>();
}

Client::~Client() {
    closesocket(this->socket);
}

void Client::AddSendData(std::string * data) {
    this->sendDataQueue.push(data);
}

void Client::Send(unsigned short maxNum) {
    try {
        while (maxNum-- > 0) {
            if (this->sendDataQueue.size() != 0) {
                std::string * data = this->sendDataQueue.front();

                const char * buff = data->c_str();
                int len = strlen(buff);

                if (send(this->socket, buff, len, 0) != SOCKET_ERROR) {
                    char recvBuff[5];

                    // 发送数据后，等待客户端接收完毕，再重新发送数据，确保每条数据都单独让客户端接收
                    if (recv(this->socket, recvBuff, 4, 0) != SOCKET_ERROR) {
                        recvBuff[4] = 0x00;
                        std::cout << Tool::Func::UTF8ToGB2312(recvBuff) << std::endl;

                        this->sendDataQueue.pop();
                        delete data;
                        data = nullptr;
                    }
                } else {
                    // 发送数据出错
                }
            }
        }
    } catch (std::exception &e) {
        std::cout << e.what() << std::endl;
    }
}
