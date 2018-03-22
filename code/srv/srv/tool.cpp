#include "tool.h"

long long Tool::GetTimeSecond() {

    time_t t = time(NULL);
    tm *ptm = new tm;
    localtime_s(ptm, &t);

    time_t time = mktime(ptm);
    delete ptm;
    ptm = nullptr;

    return time;
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
bool Tool::File::IsDirExists(std::string folder) {
    if (_access(folder.c_str(), 0) == -1) {
        return false;
    }
    return true;
}
