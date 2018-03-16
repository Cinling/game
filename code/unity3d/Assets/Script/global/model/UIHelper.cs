using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 纯方法帮助空间
/// </summary>
namespace UIHelper {
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

/// <summary>
/// 自定义控件
/// </summary>
namespace UISelfCreate {
    class ScrollView {
        public GameObject gameObject;
        public Transform transform {
            get { return this.gameObject.transform; }
        }

        /// <summary>
        /// 初始化 ScrollView
        /// </summary>
        public ScrollView() {
            this.gameObject = GameObject.Instantiate(Resources.Load<GameObject>("ui/PanelScrollView"));
        }

        /// <summary>
        /// 初始化 ScrollView 并设置在场景中的名字
        /// </summary>
        /// <param name="name">场景中控件的名字，用于GameObject.Find()时查找</param>
        public ScrollView(string name) {
            this.gameObject = GameObject.Instantiate(Resources.Load<GameObject>("ui/PanelScrollView"));
            this.gameObject.name = name;
        }

        /// <summary>
        /// 设置大小
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetSize(float width, float height) {
            // 设置外部panel
            RectTransform panelRect = this.gameObject.GetComponent<RectTransform>();
            panelRect.sizeDelta = new Vector2(width, height + 80);

            // 设置内部 ScrollView
            GameObject goScrollView = GameObject.Find(this.gameObject.name + "/ScrollView");
            RectTransform scrollRect = goScrollView.GetComponent<RectTransform>();
            scrollRect.sizeDelta = new Vector2(width, height);

            // 设置关闭按钮的位置
            float panelX = this.transform.position.x;
            float panelY = this.transform.position.y;
            GameObject goButtonClose = GameObject.Find(this.gameObject.name + "/ButtonClose");
            goButtonClose.transform.position = new Vector3(panelX + width / 2 - 20, panelY + height / 2 + 40 - 20, 0);
        }

        /// <summary>
        /// 设置位置（以中心点为坐标点）
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(float x, float y) {
            this.transform.position = new Vector3(x, y, 0);
        }
    }
}
