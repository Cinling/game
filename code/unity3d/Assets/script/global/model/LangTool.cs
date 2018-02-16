using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LangTool {
    /// <summary>
    /// 單例對象
    /// </summary>
    private static LangTool shareInstance = null;

    /// <summary>
    /// 獲取單例對象
    /// </summary>
    /// <returns></returns>
    public static LangTool GetInstance() {
        if (shareInstance == null) {
            shareInstance = new LangTool();
        }

        return shareInstance;
    }

    /// <summary>
    /// 構造函數
    /// </summary>
    private LangTool() {
        langKey = "en";
    }

    /// <summary>
    /// 語言枚舉
    /// </summary>
    public enum LANG {
        ENGLISH, CHINESE_SIMPLIFIED, CHINESE_TRADITIONAL
    };

    /// <summary>
    /// 當前的語言key
    /// </summary>
    private string langKey;

    /// <summary>
    /// 設置語言
    /// </summary>
    /// <param name="lang"></param>
    public void SetLanguage(LANG lang) {
        switch (lang) {
            case LANG.ENGLISH:
                langKey = "en";
                break;
            case LANG.CHINESE_SIMPLIFIED:
                langKey = "chs";
                break;
            case LANG.CHINESE_TRADITIONAL:
                langKey = "cht";
                break;
        }
    }

    /// <summary>
    /// 獲取對應的語言
    /// </summary>
    /// <param name="keyPath">menu.item,  獲取menu中item對應的值</param>
    /// <returns></returns>
    public string Get(string keyPath) {
        object text = Resources.Load("lang/en/menu");

        return "";
    }
}
