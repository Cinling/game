#include "config.h"

Config * Config::shareInstance = nullptr;


Config::Config() {
    this->sysConfig = std::map<std::string, std::string>();
}

Config * Config::GetInstance() {
    if (shareInstance == nullptr) {
        shareInstance = new Config();
    }
    return shareInstance;
}

Config * Config::GetInstanceFast() {
    return shareInstance;
}

Config::~Config() {
}

void Config::GetFast(const short type, const std::string key, std::string & value) {

    switch (type) {
        case Config::TYPE_SYS:
            value = this->sysConfig[key];
            break;
    }
}

void Config::InitConfig() {
    this->InitSysConfig();
}

void Config::InitSysConfig() {
    this->sysConfig["CORE_NUM"] = "4";
}
