using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自定义控件
/// </summary>
namespace UISelfCreate {
    public class Base {
        /// <summary>
        /// gameObject 对象
        /// </summary>
        public GameObject gameObject;
        /// <summary>
        /// gameObject 的 transform 对象
        /// </summary>
        public Transform transform {
            get { return this.gameObject.transform; }
        }
    }

    /// <summary>
    /// 滚动窗口
    /// </summary>
    public class ScrollView : Base {

        /// <summary>
        /// 子项目列表
        /// </summary>
        private List<ScrollView_Item> items;
        private ScrollView_Item selectItem;

        /// <summary>
        /// 初始化 ScrollView
        /// </summary>
        public ScrollView() {
            this.Init();
        }

        /// <summary>
        /// 初始化 ScrollView 并设置在场景中的名字
        /// </summary>
        /// <param name="name">场景中控件的名字，用于GameObject.Find()时查找</param>
        public ScrollView(string name) {
            this.Init();
            this.gameObject.name = name;
        }

        /// <summary>
        /// 初始化成员变量
        /// </summary>
        private void Init() {
            // 初始化参数
            this.items = new List<ScrollView_Item>();
            this.selectItem = null;
            this.gameObject = Object.Instantiate(Resources.Load<GameObject>("ui/PanelScrollView"));

            // 设置关闭按钮的事件
            GameObject goCloseButton = GameObject.Find(this.gameObject.name + "/ButtonClose");
            UnityEngine.UI.Button btn = goCloseButton.GetComponent<UnityEngine.UI.Button>();
            btn.onClick.AddListener(ClosePanel);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClosePanel() {
            GameObject.Destroy(this.gameObject);
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

        /// <summary>
        /// 获取存放子项的 GameObject
        /// </summary>
        /// <returns></returns>
        private GameObject GetContentGameObject() {
            return GameObject.Find(gameObject.name + "/ScrollView/Viewport/Content");
        }

        /// <summary>
        /// 自动设置 Content 的高度
        /// </summary>
        private void AutoSetContentHeight() {
            float height = this.items.Count * 40 + 5;
            RectTransform rectTransformContent = this.GetContentGameObject().GetComponent<RectTransform>();
            rectTransformContent.sizeDelta = new Vector2(rectTransformContent.rect.width, height); ;
        }

        /// <summary>
        /// 添加一条项目
        /// </summary>
        /// <param name="item"></param>
        public bool AddItem(string text) {
            // 设置父级 GameObject
            GameObject goContent = this.GetContentGameObject();
            ScrollView_Item item = new ScrollView_Item(this, text);
            item.transform.SetParent(goContent.transform);

            // 设置大小
            Rect rectContent = goContent.GetComponent<RectTransform>().rect;
            float contentWidth = rectContent.width;
            item.SetSize(contentWidth - 5, 39);

            // 设置位置
            float positionY = -20 - this.items.Count * 40;
            item.SetPosition(0, positionY);

            // 添加到列表中
            this.items.Add(item);

            // 自动设置 Content 的高度
            this.AutoSetContentHeight();

            return true;
        }

        /// <summary>
        /// 设置选中一个项目
        /// </summary>
        /// <param name="item"></param>
        public void SelectItem(ScrollView_Item item) {
            if (this.selectItem != null) {
                this.selectItem.Unchecked();
            }
            this.selectItem = item;
            this.selectItem.Checked();
        }

        /// <summary>
        /// 获取当前被选中的项目
        /// </summary>
        /// <returns></returns>
        public ScrollView_Item GetSelectItem() {
            return this.selectItem;
        }

        /// <summary>
        /// 删除一个项目
        /// </summary>
        /// <returns></returns>
        public bool DeleteItem(ScrollView_Item item) {
            return false;
        }
    }


    public class ScrollView_Item : Base {

        private ScrollView parentScrollView;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentScrollView"></param>
        /// <param name="text"></param>
        public ScrollView_Item(ScrollView parentScrollView, string text) {
            InitAction(parentScrollView);
            this.SetText(text);
        }

        /// <summary>
        /// 初始化的一些操作（每个构造函数都必须执行该方法）
        /// </summary>
        private void InitAction(ScrollView parentScrollView) {
            this.parentScrollView = parentScrollView;
            InitProp();
            this.SetOnClickAction(this.OnClick);
        }

        /// <summary>
        /// 初始化成员不许初始化的成员变量
        /// </summary>
        private void InitProp() {
            this.gameObject = GameObject.Instantiate(Resources.Load<GameObject>("ui/PanelScrollView_Item"));
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetOnClickAction(UnityEngine.Events.UnityAction call) {
            UnityEngine.UI.Button button = this.gameObject.GetComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(call);
        }

        /// <summary>
        /// 设置大小
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetSize(float width, float height) {
            // 设置大小
            RectTransform rectTransformItem = this.gameObject.GetComponent<RectTransform>();
            rectTransformItem.sizeDelta = new Vector2(width, height);
        }

        /// <summary>
        /// 设置位置
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(float x, float y) {
            Vector3 position = this.transform.position;
            RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(x, y);
        }

        /// <summary>
        /// 设置 Text 显示内容
        /// </summary>
        /// <param name="text"></param>
        public void SetText(string text) {
            UnityEngine.UI.Text uiText = this.gameObject.GetComponentInChildren<UnityEngine.UI.Text>();
            if (uiText != null) {
                uiText.text = text;
            }
        }

        /// <summary>
        /// 被点击时执行的方法
        /// </summary>
        private void OnClick() {
            this.parentScrollView.SelectItem(this);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Checked() {
            Debug.Log("选中：" + this.gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text);
        }

        /// <summary>
        /// 被取消选中的动作
        /// </summary>
        public void Unchecked() {
            Debug.Log("取消：" + this.gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text);
        }
    }
}
