#include "config.h"

Config * Config::shareInstance = nullptr;


Config::Config() {
}

Config * Config::GetInstance() {
    if (shareInstance == nullptr) {
        shareInstance = new Config();
    }
    return shareInstance;
}

Config * Config::UseShareInstance() {
    return shareInstance;
}

Config::~Config() {
}

void Config::Set(const short type, const std::string key, std::string & value) {

    switch (type) {
        case Config::TYPE_ENV:
            value = this->envConfig[key];
            break;
    }
}

void Config::Set(const short type, const std::string key, int & value) {
    std::string valueStr = "0";
    this->Set(type, key, valueStr);
    value = std::stoi(valueStr);
}

void Config::Set(const short type, const std::string key, float & value) {
    std::string valueStr = "0";
    this->Set(type, key, valueStr);
    value = std::stof(valueStr);
}

void Config::InitConfig() {
    this->InitEnvConfig();
    this->InitGameSysConfig();
}

void Config::InitEnvConfig() {
    this->envConfig = std::map<std::string, std::string>();
    this->envConfig["CORE_NUM"] = "3";
}

void Config::InitGameSysConfig() {
    this->gameSysConfig = std::map<std::string, std::string>();
    this->gameSysConfig["LPS"] = "3";
}
