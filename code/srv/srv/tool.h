#pragma once

#include <io.h>  
#include <direct.h>  

#include <iostream>
#include <string>
#include <time.h>

// 把变量名转为字符串
#define NameToStr(x) #x
#define PATH_DELIMITER '\\' 

namespace Tool {
    // 获取系统时间戳（秒）
    long long GetTimeSecond();

    // 文件工具
    namespace File {
        // 【重命名】
        // oldDir 原来文件的路径
        // newDir 新的文件路径
        // return [true 处理成功]， [false 处理失败]
        bool Rename(std::string oldDir, std::string newDir);

        // 【创建文件夹】
        // folder 文件相对路径
        bool CreateDir(std::string folder);

        // 判断文件夹是否存在
        // folder 文件相对路径
        // return [true 文件存在] [false 文件不存在]
        bool IsDirExists(std::string folder);
    }
}