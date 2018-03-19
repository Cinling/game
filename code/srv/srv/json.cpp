#include "json.h"



Json::Exception::Exception(std::string exceptionMsg) {
    this->exceptionMsg = exceptionMsg;
}

std::string Json::Exception::GetExceptionMsg() {
    return this->exceptionMsg;
}

Json::Saves::Saves(std::string savesName) {
    this->savesName = savesName;
}

Json::Saves::Saves(const char * jsonStr) {
    rapidjson::Document document;
    document.Parse(jsonStr);
    this->savesName = Json::GetStdString(document, NameToStr(savesName), "");

    if (this->savesName == "") {
        throw Json::Exception("传入的json非所需格式，或数据为空，[传入json:" + std::string(jsonStr) + "]");
    }
}

Json::Saves::~Saves() {
}

std::string Json::Saves::ToJsonStr() {
    using namespace rapidjson;

    Document document;
    Document::AllocatorType& allocator = document.GetAllocator();
    document.SetObject();

    document.AddMember(Value(NameToStr(savesName), allocator), Value(this->savesName.c_str(), allocator), allocator);

    return Json::Encode(document);
}

std::string Json::Saves::GetSavesName() {
    return this->savesName;
}

std::string Json::Encode(rapidjson::Document & document) {
    rapidjson::StringBuffer sb;
    rapidjson::Writer<rapidjson::StringBuffer> writer(sb);
    document.Accept(writer);

    return std::string(sb.GetString());
}

std::string Json::Encode(rapidjson::Value & value) {
    rapidjson::StringBuffer sb;
    rapidjson::Writer<rapidjson::StringBuffer> writer(sb);
    value.Accept(writer);

    return std::string(sb.GetString());
}

int Json::GetInt(rapidjson::Document & document, std::string key, int defaultValue) {
    int retInt = defaultValue;
    rapidjson::Value::MemberIterator mt = document.FindMember(key.c_str());
    if (mt->value.IsInt()) {
        retInt = mt->value.GetInt();
    }

    return retInt;
}

std::string Json::GetStdString(rapidjson::Document & document, std::string key, std::string defaultValue) {
    std::string retString = defaultValue;
    rapidjson::Value::MemberIterator mt = document.FindMember(key.c_str());
    if (mt->value.IsString()) {
        retString = std::string(mt->value.GetString());
    }

    return retString;
}
