#pragma once

#include "tool.h"

#include "../lib/rapidjson/document.h"
#include "../lib/rapidjson/prettywriter.h"
#include <string>

namespace Json {
    // ������Ҫjson��װ���������͵Ļ���
    class Base {
    public:
        virtual std::string ToJsonStr() = 0;
    };

    // �׳����쳣��
    class Exception {
    private:
        std::string exceptionMsg;

    public:
        Exception(std::string exceptionMsg);
        std::string GetExceptionMsg();
    };

    // �浵���ݷ�װ
    class Saves : public Json::Base {
    public:
        // �浵����
        std::string savesName;

    public:
        // ʹ�ô浵���ִ�������
        Saves(std::string savesName);
        // ʹ��json����ʼ������
        Saves(const char * jsonStr);

        ~Saves();

    public:
        // ͨ�� Base �̳�
        // return json��
        virtual std::string ToJsonStr() override;

    public:
        // return �浵����
        std::string GetSavesName();
    };

    class Map : public Json::Base {
    public:
        float width;
        float length;
        float height;
    public:
        Map(float width, float length, float height);

    public:
        // ͨ�� Base �̳�
        virtual std::string ToJsonStr() override;
    };


    /*##############################################################
        json������json��ʽ������ȡjson�����е�ֵ��صķ���
    ##############################################################*/

    // �� rapidjson::Document תΪjson��
    // return json��
    std::string Encode(rapidjson::Document & document);
    // �� rapidjson::Value תΪjson��
    // return json��
    std::string Encode(rapidjson::Value & value);

    // ��ȡ�����е�intֵ
    int GetInt(rapidjson::Document &document, std::string key, int defaultValue);
    // ��ȡ�����е�floatֵ
    float GetFloat(rapidjson::Document &document, std::string key, float defaultValue);
    // ��ȡ�����е�stringֵ
    std::string GetStdString(rapidjson::Document &document, std::string key, std::string defaultValue);
}
