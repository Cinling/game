#pragma once

#include <string>
#include <map>

class Config {
private:
    static Config * shareInstance;
    Config();

public:
    // 配置类型
    static const short TYPE_SYS = 1;

    static Config * GetInstance();
    // 【性能方法】快速获取单例对象，使用前请确保 单例对象已经初始化
    static Config * GetInstanceFast();
    ~Config();
public:
    // 【性能方法】快速获取配置，使用前请确保 key 一定存在
    void GetFast(const short type, const std::string key, std::string & value);

private:
    std::map<std::string, std::string> sysConfig;

private:
    // 初始化配置，把所有配置接在到类中
    void InitConfig();
    // 初始化系统配置
    void InitSysConfig();
};

