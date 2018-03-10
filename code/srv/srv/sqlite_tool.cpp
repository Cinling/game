#include "sqlite_tool.h"


SqliteTool * SqliteTool::shareInstance = nullptr;
char * SqliteTool::db = nullptr;


SqliteTool::SqliteTool(const char * db) {
    size_t len = strlen(db) + 1;
    this->db = new char[len];
    strcpy_s(this->db, len, db);

    char * dbName = new char[len + 8];
    strcpy_s(dbName, len + 8, std::string(std::string(this->db) + ".sqlite").c_str());
    sqlite3_open(dbName, &this->sqlite);
    delete dbName;
}


SqliteTool * SqliteTool::GetInstance() {

    if (shareInstance == nullptr) {

        if (SqliteTool::db == nullptr) {
            SqliteTool::db = new char[5];
            strcpy_s(SqliteTool::db, 5, "auto");
        }

        shareInstance = new SqliteTool(SqliteTool::db);
    }

    return shareInstance;
}

void SqliteTool::UseDB(const char * dbName) {
    size_t len = strlen(dbName) + 1;
    SqliteTool::db = new char[len];
    strcpy_s(SqliteTool::db, len, dbName);
}

SqliteTool::~SqliteTool() {
    sqlite3_close(this->sqlite);

    if (shareInstance != nullptr) {
        delete shareInstance;
        shareInstance = nullptr;
    }

    if (SqliteTool::db != nullptr) {
        delete SqliteTool::db;
        SqliteTool::db = nullptr;
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

