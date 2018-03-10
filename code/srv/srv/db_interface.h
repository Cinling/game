#pragma once

#include <list>
#include <map>
#include <iostream>


class DBInterface {
public:
    virtual std::list<std::map<std::string, std::string>> Save(std::list<std::map<std::string, std::string>>) = 0;
    virtual std::list<std::map<std::string, std::string>> Load(std::list<std::map<std::string, std::string>>) = 0;
};

