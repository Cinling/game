#pragma once

#include "json.h"

#include <io.h>  
#include <direct.h>  

#include <iostream>
#include <list>
#include <vector>
#include <map>
#include <string>
#include <time.h>

// 把变量名转为字符串
#define NameToStr(x) #x
#define PATH_DELIMITER '\\' 

// 调试模式
#define DEBUG

namespace Tool {
    // 获取系统时间戳（秒）
    long long GetTimeSecond();

    // 把 std::map<std::string, std::string> 转为 std::string 的json字符串
    std::string MapToJsonStr(std::map<std::string, std::string> jsonMap);
    // 把 std::string 的json字符串 转为 std::map<std::string, std::string>
    std::map<std::string, std::string> JaonStrToMap(std::string jsonStr);

    // 文件工具
    namespace File {

        // 【创建文件夹】
        // folder 文件相对路径
        bool CreateDir(std::string folder);
        // 【删除文件】【未实现】
        // fileName 文件相对路径
        // return [true 处理成功]， [false 处理失败]
        bool DeleteFile(std::string fileName);
        // 【重命名】【未实现】
        // oldDir 原来文件的路径
        // newDir 新的文件路径
        // return [true 处理成功]， [false 处理失败]
        bool Rename(std::string oldDir, std::string newDir);
        // 【判断文件夹是否存在】
        // folder 文件相对路径
        // return [true 文件存在] [false 文件不存在]
        bool IsDirExists(std::string folder);
        // 【获取目录下所有文件的文件名】
        // folder 文件相对路径，如：saves/   【注意：必须以"/"结尾】
        // return [true 文件存在] [false 文件不存在]
        std::vector<std::string> GetChildFiles(std::string folder);
    }

    // 简单类 类型，用于封装部分数据结构，减少变量的数量
    namespace Struct {
        class Vector3 {
        public:
            float x;
            float y;
            float z;

            Vector3();
            Vector3(float x, float y, float z);
            ~Vector3();
        };
    }

    // 常用的数学方法
    namespace Math {
        // 获取随机数
        int Random(int min, int max);
    }
}