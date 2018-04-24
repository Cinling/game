#pragma once

#include <string>
#include <map>

class Config {
private:
    static Config * shareInstance;
    Config();

public:
    // 环境类型的配置，如：CPU核数、内存大小等
    static const short TYPE_ENV = 1;
    // 游戏系统配置，如：逻辑帧、游戏时间快慢等设置
    static const short TYPE_GAME_SYS = 2;

public:
    // 获取单例对象，如果没有对象存在，则创建一个新的对象
    static Config * GetInstance();
    // 【性能方法】快速获取单例对象，使用前请确保 单例对象已经初始化
    static Config * UseShareInstance();
    ~Config();
public:
    // 初始化配置，把所有配置接在到类中
    void InitConfig();

    // 引用设置配置（std::string）
    void Set(const short type, const std::string key, std::string & value);
    // 引用设置配置（int）
    void Set(const short type, const std::string key, int & value);
    // 引用设置配置（float）
    void Set(const short type, const std::string key, float & value);

private:
    // 环境配置存放的列表
    std::map<std::string, std::string> envConfig;
    // 初始化系统配置
    void InitEnvConfig();

    std::map<std::string, std::string> gameSysConfig;
    // 初始化游戏系统配置
    void InitGameSysConfig();
};

