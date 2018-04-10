#pragma once

#include "tool.h"

#include "../lib/rapidjson/document.h"
#include "../lib/rapidjson/prettywriter.h"
#include <string>

namespace Json {
    // 抛出的异常类
    class Exception {
    private:
        std::string exceptionMsg;

    public:
        Exception(std::string exceptionMsg);
        std::string GetExceptionMsg();
    };

    // 所有需要json包装的数据类型的基类
    class Base {
    public:
        virtual std::string ToJsonStr() = 0;
    };

    // 存档数据封装
    class Saves : public Json::Base {
    public:
        // 存档名称
        std::string savesName;

    public:
        // 使用存档名字创建对象
        Saves(std::string savesName);
        // 使用json串初始化对象
        Saves(const char * jsonStr);

        ~Saves();

        // 通过 Base 继承
        // return json串
        virtual std::string ToJsonStr() override;

        // return 存档名字
        std::string GetSavesName();
    };

    // 地图数据
    class Map : public Json::Base {
    public:
        float width;
        float length;
        float height;

        Map(float width, float length, float height);
        // 通过特定的json初始化
        Map(std::string json);

        // 通过 Base 继承
        virtual std::string ToJsonStr() override;
    };

    // 所有角色通用的传输数据
    class BaseRole : public Base {
    public:
        int id;
        int type;
        float x;
        float y;
        float z;
        // 扩展json数据，用于根据不同类型的物体进行的特殊显示处理
        std::string specialShow;

        BaseRole(int id, int type, float x, float y, float z, std::string specialShow);

        // 通过 Base 继承
        virtual std::string ToJsonStr() override;
    };



    /*##############################################################
        json解析，json格式化，获取json对象中的值相关的方法
    ##############################################################*/

    // 把 rapidjson::Document 转为json串
    // return json串
    std::string Encode(rapidjson::Document & document);
    // 把 rapidjson::Value 转为json串
    // return json串
    std::string Encode(rapidjson::Value & value);

    // 获取对象中的int值
    int GetInt(rapidjson::Document &document, std::string key, int defaultValue);
    // 获取对象中的float值
    float GetFloat(rapidjson::Document &document, std::string key, float defaultValue);
    // 获取对象中的string值
    std::string GetStdString(rapidjson::Document &document, std::string key, std::string defaultValue);
}
