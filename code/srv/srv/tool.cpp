﻿#include "tool.h"


#include <io.h>  
#include <direct.h>  

#include <time.h>

#include <winsock2.h>


long long Tool::GetTimeSecond() {

    time_t t = time(NULL);
    tm *ptm = new tm;
    localtime_s(ptm, &t);

    time_t time = mktime(ptm);
    delete ptm;
    ptm = nullptr;

    return time;
}

std::string Tool::MapToJsonStr(std::map<std::string, std::string> jsonMap) {
    using namespace rapidjson;

    Document document;
    Document::AllocatorType& allocator = document.GetAllocator();
    document.SetObject();

    for (std::map<std::string, std::string>::iterator it = jsonMap.begin(); it != jsonMap.end(); ++it) {
        document.AddMember(Value(it->first.c_str(), allocator), Value(it->second.c_str(), allocator), allocator);
    }

    return Json::Encode(document);
}

std::map<std::string, std::string> Tool::JaonStrToMap(std::string jsonStr) {
    using namespace rapidjson;

    Document document;
    document.Parse(jsonStr.c_str());
    std::map<std::string, std::string> map = std::map<std::string, std::string>();

    for (Value::ConstMemberIterator itr = document.MemberBegin(); itr != document.MemberEnd(); ++itr) {
        map[std::string(itr->name.GetString())] = std::string(itr->value.GetString());
    }

    return map;
}

bool Tool::File::Rename(std::string oldDir, std::string newDir) {
    return false;
}

bool Tool::File::CreateDir(std::string folder) {
    std::string folder_builder;
    std::string sub;
    sub.reserve(folder.size());
    for (std::string::iterator it = folder.begin(); it != folder.end(); ++it) {
        //cout << *(folder.end()-1) << endl;  
        const char c = *it;
        sub.push_back(c);
        if (c == PATH_DELIMITER || it == folder.end() - 1) {
            folder_builder.append(sub);
            if (0 != ::_access(folder_builder.c_str(), 0)) {
                // this folder not exist  
                if (0 != ::_mkdir(folder_builder.c_str())) {
                    // create failed  
                    return false;
                }
            }
            sub.clear();
        }
    }
    return true;
}

bool Tool::File::RemoveFile(std::string fileName) {
    return false;
}

bool Tool::File::IsDirExists(std::string folder) {
    if (_access(folder.c_str(), 0) == -1) {
        return false;
    }
    return true;
}

std::vector<std::string> Tool::File::GetChildFiles(std::string folder) {
    std::vector<std::string> childFiles;

    // 文件句柄
    long long hFile = 0;
    struct _finddata_t fileInfo;
    std::string pathName;

    // 判断文件是否
    hFile = _findfirst(pathName.assign(folder).append("*").c_str(), &fileInfo);
    if (hFile == -1) {
        return std::vector<std::string>();
    }

    // 遍历每个文件
    do {
        std::string fileName = fileInfo.name;
        if (fileName == "." || fileName == "..") {
            continue;
        }
        childFiles.push_back(fileName);
    } while (_findnext(hFile, &fileInfo) == 0);

    return childFiles;
}

Tool::Struct::Vector3::Vector3() {
    this->x = 0;
    this->y = 0;
    this->z = 0;
}

Tool::Struct::Vector3::Vector3(float x, float y, float z) {
    this->x = x;
    this->y = y;
    this->z = z;
}

Tool::Struct::Vector3::~Vector3() {
}

int Tool::Math::Random(int min, int max) {
    return (rand() % (max - min + 1) + min);
}

char * Tool::Func::UTF8ToGB2312(const char * utf8) {
    int len = MultiByteToWideChar(CP_UTF8, 0, utf8, -1, NULL, 0);
    wchar_t* wstr = new wchar_t[len + 1];
    memset(wstr, 0, len + 1);
    MultiByteToWideChar(CP_UTF8, 0, utf8, -1, wstr, len);
    len = WideCharToMultiByte(CP_ACP, 0, wstr, -1, NULL, 0, NULL, NULL);
    char* str = new char[len + 1];
    memset(str, 0, len + 1);
    WideCharToMultiByte(CP_ACP, 0, wstr, -1, str, len, NULL, NULL);
    if (wstr) delete[] wstr;
    return str;
}

char * Tool::Func::GB2312ToUTF8(const char * gb2312) {
    int len = MultiByteToWideChar(CP_ACP, 0, gb2312, -1, NULL, 0);
    wchar_t* wstr = new wchar_t[len + 1];
    memset(wstr, 0, len + 1);
    MultiByteToWideChar(CP_ACP, 0, gb2312, -1, wstr, len);
    len = WideCharToMultiByte(CP_UTF8, 0, wstr, -1, NULL, 0, NULL, NULL);
    char* str = new char[len + 1];
    memset(str, 0, len + 1);
    WideCharToMultiByte(CP_UTF8, 0, wstr, -1, str, len, NULL, NULL);
    if (wstr) delete[] wstr;
    return str;
}
