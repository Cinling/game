#pragma once

#include <iostream>
#include <string>
#include <time.h>

namespace Tool {
    // 获取系统时间戳（秒）
    long long GetTimeSecond();

    // 文件工具
    namespace File {
        // 重命名
        // oldDir 原来文件的路径
        // newDir 新的文件路径
        // return [true 处理成功]， [false 处理失败]
        bool Rename(std::string oldDir, std::string newDir);
    }
}