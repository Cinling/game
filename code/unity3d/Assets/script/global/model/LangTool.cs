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
    /// 默認語言（最全的語言）
    /// </summary>
    const LANG DEFAULT_LANG = LANG.ENGLISH;

    /// <summary>
    /// 構造函數
    /// </summary>
    private LangTool() {
        settingLang = GetLangStringByLangEnum(DEFAULT_LANG);
    }

    /// <summary>
    /// 語言枚舉
    /// </summary>
    public enum LANG {
        ENGLISH, CHINESE_SIMPLIFIED, CHINESE_TRADITIONAL
    };

    /// <summary>
    /// 當前的語言
    /// </summary>
    private string settingLang;

    /// <summary>
    /// 設置語言
    /// </summary>
    /// <param name="lang"></param>
    public void SetLanguage(LANG lang) {
        settingLang = GetLangStringByLangEnum(lang);

        // 重置語言
        langFileDict = null;
        langFileDict = new Dictionary<string, Dictionary<string, string>>();
    }

    /// <summary>
    /// 根據枚舉獲取對應的語言
    /// </summary>
    /// <param name="langEnum"></param>
    /// <returns></returns>
    private string GetLangStringByLangEnum(LANG langEnum) {
        string langKey = "";

        switch (langEnum) {
            case LANG.ENGLISH:
                langKey = "en";
                break;
            case LANG.CHINESE_SIMPLIFIED:
                langKey = "chs";
                break;
            case LANG.CHINESE_TRADITIONAL:
                langKey = "cht";
                break;
            default:
                Debug.LogError("Unknow language key");
                break;
        }
        return langKey;
    }

    /// <summary>
    /// 記錄單個文件的字典（一個文件一個字典）
    /// </summary>
    private Dictionary<string, Dictionary<string, string>> langFileDict = new Dictionary<string, Dictionary<string, string>>();


    /// <summary>
    /// 獲取對應的語言
    /// </summary>
    /// <param name="langConfig">menu.item,  獲取menu中item對應的值</param>
    /// <returns>對應的語言字符</returns>
    public string Get(string langConfig) {
        int lastIndex = langConfig.LastIndexOf(".");

        if (lastIndex == 0) {
            return "";
        }

        string filePath = langConfig.Substring(0, lastIndex).Replace(".", "/");
        string langKey = langConfig.Substring(lastIndex + 1);

        if (!langFileDict.ContainsKey(filePath)) {
            langFileDict[filePath] = LoadByFile(settingLang, filePath);
        }

        if (!langFileDict[filePath].ContainsKey(langKey)) {
            LoadAddByDefaultLang(langFileDict[filePath], filePath);
        }

        if (!langFileDict[filePath].ContainsKey(langKey)) {
            langFileDict[filePath][langKey] = "NULL";
            Debug.LogError("Unknow filePath:[" + filePath + "] langKey:[" + langKey + "]");
        }
        return langFileDict[filePath][langKey];
    }

    /// <summary>
    /// 從文件中加載數據，並轉換成字典
    /// </summary>
    /// <param name="lang">語言，如：en，cht</param>
    /// <param name="filePath"></param>
    /// <returns>語言key對應的語言文字的字典</returns>
    private Dictionary<string, string> LoadByFile(string lang, string filePath) {
        object tmpObject = Resources.Load("lang/" + settingLang + "/" + filePath);
        Dictionary<string, string> keyLangDict = new Dictionary<string, string>();

        if (tmpObject != null) {
            string text = tmpObject.ToString().Replace("\r", "");
            tmpObject = null;

            string[] lines = text.Split('\n');

            for (int i = 0; i < lines.Length; ++i) {
                int equalIndex = lines[i].IndexOf("=");
                string key = lines[i].Substring(0, equalIndex);
                string value = lines[i].Substring(equalIndex + 1);
                keyLangDict[key] = value;
            }
        }

        return keyLangDict;
    }

    /// <summary>
    /// 從默認（最全）語言中加入目前語言沒有的鍵
    /// </summary>
    /// <param name="changeKeyLangDict"></param>
    /// <param name="filePath"></param>
    private void LoadAddByDefaultLang(Dictionary<string, string> changeKeyLangDict, string filePath) {
        string defaultLang = GetLangStringByLangEnum(DEFAULT_LANG);
        Dictionary<string, string> defaultKeyLangDich = LoadByFile(defaultLang, filePath);

        foreach (KeyValuePair<string, string> kv in defaultKeyLangDich) {

            if (!changeKeyLangDict.ContainsKey(kv.Key)) {
                changeKeyLangDict[kv.Key] = kv.Value;
            }
        }
    }
}
