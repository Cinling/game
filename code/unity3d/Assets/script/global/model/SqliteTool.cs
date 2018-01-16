using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class SqliteTool {

    private SqliteConnection dbConnection;

    /// <summary>
    /// 创建数据库连接
    /// </summary>
    /// <param name="fileUrl">文件相对位置，如："save/myworld/sqlite.db"</param>
    public SqliteTool(string fileUrl) {
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
            dbConnection = new SqliteConnection("URI=file://" + fileUrl);
            dbConnection.Open();
        } catch (Exception e) {
            Log.PrintLog("SqliteTool", "SqliteTool", e.ToString(), Log.LOG_LEVEL.ERROR);
        }
    }
}
