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

float Json::GetFloat(rapidjson::Document & document, std::string key, float defaultValue) {
    float retFloat = defaultValue;
    rapidjson::Value::MemberIterator mt = document.FindMember(key.c_str());
    if (mt->value.IsFloat()) {
        retFloat = mt->value.GetFloat();
    }

    return retFloat;
}

std::string Json::GetStdString(rapidjson::Document & document, std::string key, std::string defaultValue) {
    std::string retString = defaultValue;
    rapidjson::Value::MemberIterator mt = document.FindMember(key.c_str());
    if (mt->value.IsString()) {
        retString = std::string(mt->value.GetString());
    }

    return retString;
}

Json::Map::Map(float width, float length, float height) {
    this->width = width;
    this->length = length;
    this->height = height;
}

std::string Json::Map::ToJsonStr() {
    using namespace rapidjson;

    Document document;
    Document::AllocatorType& allocator = document.GetAllocator();
    document.SetObject();

    document.AddMember(Value(NameToStr(width), allocator), Value(this->width), allocator);
    document.AddMember(Value(NameToStr(length), allocator), Value(this->length), allocator);
    document.AddMember(Value(NameToStr(height), allocator), Value(this->height), allocator);

    return Json::Encode(document);
}

Json::BaseRole::BaseRole(int id, int type, float x, float y, float z) {
    this->id = id;
    this->type = type;
    this->x = x;
    this->y = y;
    this->z = z;
}

std::string Json::BaseRole::ToJsonStr() {
    using namespace rapidjson;

    Document document;
    Document::AllocatorType& allocator = document.GetAllocator();
    document.SetObject();

    document.AddMember(Value(NameToStr(id), allocator), Value(this->id), allocator);
    document.AddMember(Value(NameToStr(type), allocator), Value(this->type), allocator);
    document.AddMember(Value(NameToStr(x), allocator), Value(this->x), allocator);
    document.AddMember(Value(NameToStr(y), allocator), Value(this->y), allocator);
    document.AddMember(Value(NameToStr(z), allocator), Value(this->z), allocator);

    return Json::Encode(document);
}
