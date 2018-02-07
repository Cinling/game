using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class SqliteTool {

    private static Dictionary<string, SqliteTool> shareInstanceDict = new Dictionary<string, SqliteTool>();

    /// <summary>
    /// 获取多单例对象，由于不同存档需要分开文件储存，所以需要携带一个参数作为标记
    /// </summary>
    /// <param name="fileUrl">文件路径</param>
    /// <returns></returns>
    public static SqliteTool GetInstance(string fileUrl) {

        if (!shareInstanceDict.ContainsKey(fileUrl)) {
            shareInstanceDict[fileUrl] = new SqliteTool(fileUrl);
        }

        return shareInstanceDict[fileUrl];
    }

    private SqliteConnection db;

    /// <summary>
    /// 创建数据库连接
    /// </summary>
    /// <param name="fileUrl">文件相对位置，如："save/myworld/sqlite.db"</param>
    private SqliteTool(string fileUrl) {
        string[] dirs = fileUrl.Split('/');


        string dir = "";
        int dirLen = dirs.Length - 1;
        for (byte i = 0; i < dirLen; ++i) {
            dir += dirs[i] + "/";

            if (!System.IO.Directory.Exists(dir)) {
                System.IO.Directory.CreateDirectory(dir);
            }
        }

        string absDir = System.IO.Directory.GetCurrentDirectory() + "/" + fileUrl;

        try {
            db = new SqliteConnection("URI=file://" + fileUrl);
            db.Open();
        } catch (Exception e) {
            Log.PrintLog("SqliteTool", "SqliteTool", e.ToString(), Log.LOG_LEVEL.ERROR);
        }
    }

    /// <summary>
    /// 执行SQL命令
    /// </summary>
    /// <param name="sql">SQL语句</param>
    /// <returns></returns>
    private SqliteDataReader ExecuteSql(string sql) {
        SqliteCommand cmd = db.CreateCommand();
        cmd.CommandText = sql;
        return cmd.ExecuteReader();
    }

    /// <summary>
    /// 把参数转为安全的sql参数，防止数据库被注入
    /// </summary>
    /// <param name="sqlParam">sql参数，可以是表名、字段 和 数值</param>
    /// <returns>安全的sql字符</returns>
    private string ToSafeSqlParma(string sqlParam) {
        // sqlite 字符串中，[''] 代表 [']，只要所有内容都是字符串，就能防止注入
        sqlParam = sqlParam.Replace("'", "''");

        return sqlParam;
    }

    /// <summary>
    /// 一次只能插入一条数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="data">入库数据的键值对</param>
    /// <returns></returns>
    public SqliteDataReader InsertOnce(string tableName, Dictionary<string, object> data) {
        string field = "";
        string value = "";

        foreach (KeyValuePair<string, object> kv in data) {
            string tmpKey = ToSafeSqlParma(kv.Key);
            string tmpValue = ToSafeSqlParma(kv.Value.ToString());

            if (field != "") {
                field += ",";
                value += ",";
            }
            field += "`" + tmpKey + "`";
            value += "'" + tmpValue + "'";
        }

        string sql = "INSERT INTO " + tableName + "(" + field + ") VALUES(" + value + ")";
        return this.ExecuteSql(sql);
    }
}
