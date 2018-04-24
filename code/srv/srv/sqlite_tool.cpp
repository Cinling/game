#include "sqlite_tool.h"


SqliteTool * SqliteTool::shareInstance = nullptr;
char * SqliteTool::db = nullptr;


SqliteTool::SqliteTool(const char * db) {
    if (Tool::File::IsDirExists(db)) {
        this->CreateDirWithDBName(db);
    }

    // 设置内部使用的数据库名称
    size_t tmpLen = strlen(db) + 1;
    SqliteTool::db = new char[tmpLen];
    strcpy_s(SqliteTool::db, tmpLen, db);

    std::string tmpStr = std::string(SqliteTool::db) + std::string(".sqlite");
    const char * dbPath = tmpStr.c_str();
    size_t len = strlen(dbPath) + 1;

    char * dbName = new char[len];
    strcpy_s(dbName, len, dbPath);
    sqlite3_open(dbName, &this->sqlite);

    delete[] dbName;

}


SqliteTool * SqliteTool::GetInstance() {

    if (SqliteTool::shareInstance == nullptr) {

        if (SqliteTool::db == nullptr) {
            SqliteTool::db = new char[9];
            strcpy_s(SqliteTool::db, 9, "autosave");
        }

        SqliteTool::shareInstance = new SqliteTool(SqliteTool::db);
    }

    return shareInstance;
}

void SqliteTool::UseDB(const char * dbName) {

    if (SqliteTool::db != nullptr) {
        delete[] SqliteTool::db;
        SqliteTool::db = nullptr;
    }

    size_t len = strlen(dbName) + 1;
    SqliteTool::db = new char[len];
    strcpy_s(SqliteTool::db, len, dbName);

    if (SqliteTool::shareInstance != nullptr) {
        SqliteTool::shareInstance->~SqliteTool();
        SqliteTool::shareInstance = nullptr;
    }
}

SqliteTool::~SqliteTool() {
    if (this->sqlite != nullptr) {
        sqlite3_close(this->sqlite);
        this->sqlite = nullptr;
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

    std::list<std::map<std::string, std::string>> data;
    char * errorMsg = NULL;
    sqlite3_exec(this->sqlite, sql, QueryCallback, (void *)&data, &errorMsg);

    if (errorMsg == NULL) {
        return data;
    }

    return std::list<std::map<std::string, std::string>>();
}

bool SqliteTool::ExecSql(const char * sql) {
    std::list<std::map<std::string, std::string>> data;
    char * errorMsg = NULL;
    sqlite3_exec(this->sqlite, sql, QueryCallback, (void *)&data, &errorMsg);

    if (errorMsg == NULL) {
        return true;
    }

    return false;
}

bool SqliteTool::IsTableExists(const char * tableName) {
    std::string sql = "SELECT `name` FROM `sqlite_master` WHERE type='table' AND `name` = '" + std::string(tableName) + "'";
    std::list<std::map<std::string, std::string>> res = this->Query(sql.c_str());

    if (res.size() == 0) {
        return false;
    }
    return true;
}

bool SqliteTool::CreateDirWithDBName(const char * dbName) {
    std::string dbStr = std::string(dbName);
    size_t lastIndex = dbStr.find_last_of("/");
    std::string dir = dbStr.substr(0, lastIndex);
    Tool::File::CreateDir(dir);
    return true;
}

