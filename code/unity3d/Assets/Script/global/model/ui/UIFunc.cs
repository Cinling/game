using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 纯方法帮助空间
/// </summary>
namespace UIFunc {
    class Panel {
        /// <summary>
        /// 创建一个pannel GameObject
        /// </summary>
        /// <param name="parent">父级GameObject对象</param>
        /// <param name="prefab">prefab路径</param>
        /// <returns></returns>
        public static GameObject CreatePanel(GameObject parent, string prefab) {
            Rect rectCanvas = parent.GetComponent<RectTransform>().rect;
            float width = rectCanvas.width;
            float height = rectCanvas.height;

            GameObject goPanel = GameObject.Instantiate(Resources.Load<GameObject>(prefab));
            goPanel.transform.SetParent(parent.transform);
            goPanel.transform.position = parent.transform.position;
            UnityEngine.UI.Image pannel = goPanel.GetComponent<UnityEngine.UI.Image>();
            SetSize(pannel, width, height);

            return goPanel;
        }

        /// <summary>
        /// 设置pannel的大小
        /// </summary>
        /// <param name="pannel"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void SetSize(UnityEngine.UI.Image pannel, float width, float height) {
            Vector2 oldSize = pannel.rectTransform.rect.size;
            Vector2 deltaSize = new Vector2(width, height) - oldSize;
            pannel.rectTransform.offsetMin = pannel.rectTransform.offsetMin - new Vector2(deltaSize.x * pannel.rectTransform.pivot.x, deltaSize.y * pannel.rectTransform.pivot.y);
            pannel.rectTransform.offsetMax = pannel.rectTransform.offsetMax + new Vector2(deltaSize.x * ( 1f - pannel.rectTransform.pivot.x ), deltaSize.y * ( 1f - pannel.rectTransform.pivot.y ));
        }

        /// <summary>
        /// 设置pannel的大小
        /// </summary>
        /// <param name="pannel"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void SetSize(GameObject goPannel, float width, float height) {
            UnityEngine.UI.Image pannel = goPannel.GetComponent<UnityEngine.UI.Image>();
            SetSize(pannel, width, height);
        }
    }

    class Button {
        /// <summary>
        /// 添加一个按钮
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="perfab"></param>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="call"></param>
        public static GameObject CreateButton(GameObject parent, string perfab, string text, float x, float y, UnityEngine.Events.UnityAction call) {
            GameObject goButton = GameObject.Instantiate(Resources.Load<GameObject>(perfab));
            goButton.transform.SetParent(parent.transform);
            goButton.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
            UnityEngine.UI.Button button = goButton.GetComponent<UnityEngine.UI.Button>();
            button.transform.position = new Vector3(x, y, 0);
            button.onClick.AddListener(call);

            return goButton;
        }
    }

    class InputField {
        /// <summary>
        /// 创建一个输入框
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="perfab"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static GameObject CreateInputField(GameObject parent, string perfab, float x, float y, float width) {
            GameObject goInputField = GameObject.Instantiate(Resources.Load<GameObject>(perfab));
            goInputField.transform.SetParent(parent.transform);
            goInputField.transform.position = new Vector3(x, y, 0);

            RectTransform rectTransform = goInputField.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(width, rectTransform.rect.height);

            return goInputField;
        }

        /// <summary>
        /// 获取输入框的值
        /// </summary>
        /// <param name="goInputField"></param>
        /// <returns></returns>
        public static string GetValue(GameObject goInputField) {
            UnityEngine.UI.InputField inputField = goInputField.GetComponent<UnityEngine.UI.InputField>();
            return inputField.text;
        }
    }


    class ScrollView {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="perfab"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static GameObject CreateScrollView(GameObject parent, string perfab, float x, float y, float width, float height) {
            GameObject goScrollView = GameObject.Instantiate(Resources.Load<GameObject>(perfab));
            goScrollView.transform.SetParent(parent.transform);
            goScrollView.transform.position = new Vector3(x, y, 0);

            RectTransform scrollRect = goScrollView.GetComponent<RectTransform>();
            scrollRect.sizeDelta = new Vector2(width, height);

            return goScrollView;
        }
    }
}


