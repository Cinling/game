#include "sqlite_tool.h"


SqliteTool * SqliteTool::shareInstance = NULL;


SqliteTool::SqliteTool(const char * db) {
    size_t len = strlen(db) + 1;
    this->db = new char[len];
    strcpy_s(this->db, len, db);

    const char * dbName = (std::string(this->db) + ".sqlite").c_str();
    sqlite3_open(dbName, &this->sqlite);
}


SqliteTool * SqliteTool::GetInstance(const char * db) {

    if (shareInstance == NULL) {
        shareInstance = new SqliteTool(db);
    } else if (shareInstance->db != db) {
        shareInstance->~SqliteTool();
        shareInstance = new SqliteTool(db);
    }

    return shareInstance;
}

SqliteTool::~SqliteTool() {
    sqlite3_close(this->sqlite);
    delete this->db;
    this->db = NULL;

    if (shareInstance != NULL) {
        delete shareInstance;
        shareInstance = NULL;
    }
}

// 执行sql查询时触发的方法
static int QueryCallback(void *data, int argc, char **argv, char **azColName) {
    std::map<std::string, std::string> map;

    for (int i = 0; i < argc; ++i) {
        map.insert(std::pair<std::string, std::string>(std::string(azColName[i]), std::string(argv[i] ? argv[i] : "NULL")));
    }

    std::list<std::map<std::string, std::string>> *list = (std::list<std::map<std::string, std::string>> *) data;
    list->push_back(map);

    return 0;
}

std::list<std::map<std::string, std::string>> SqliteTool::Query(const char * sql) {

    std::list<std::map<std::string, std::string>> *data = new std::list<std::map<std::string, std::string>>;
    char * errorMsg = NULL;
    sqlite3_exec(this->sqlite, sql, QueryCallback, (void *)data, &errorMsg);

    if (errorMsg == NULL) {
        return *data;
    }

    return std::list<std::map<std::string, std::string>>();
}

