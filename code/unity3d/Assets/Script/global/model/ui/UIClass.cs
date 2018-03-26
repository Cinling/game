using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自定义控件
/// </summary>
namespace UIClass {
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
            this.itemList = new List<ScrollView_Item>();
            this.selectItem = null;
            this.gameObject = Object.Instantiate(Resources.Load<GameObject>(PrefabPath._2D.PanelScrollView));

            // 设置关闭按钮的事件
            GameObject goCloseButton = GameObject.Find(this.gameObject.name + "/ButtonClose");
            UnityEngine.UI.Button btn = goCloseButton.GetComponent<UnityEngine.UI.Button>();
            btn.onClick.AddListener(ClosePanel);
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
            float height = this.GetItemNum() * 40 + 5;
            RectTransform rectTransformContent = this.GetContentGameObject().GetComponent<RectTransform>();
            rectTransformContent.sizeDelta = new Vector2(rectTransformContent.rect.width, height);
        }
        /// <summary>
        /// 自动重置子项的位置
        /// </summary>
        private void AutoReSetItemPosition() {

            for (int i = 0; i < this.itemList.Count; ++i) {
                ScrollView_Item item = this.itemList[i];
                item.SetPosition(0, -20 - i * 40);
            }
        }
        /// <summary>
        /// 获取子项的数目
        /// </summary>
        /// <returns></returns>
        private int GetItemNum() {
            return this.itemList.Count;
        }


        /// <summary>
        /// 所有子项的列表
        /// </summary>
        private List<ScrollView_Item> itemList;
        /// <summary>
        /// 当前被选中的对象
        /// </summary>
        private ScrollView_Item selectItem;

        /// <summary>
        /// 添加一个子项
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
            float positionY = -20 - this.GetItemNum() * 40;
            item.SetPosition(0, positionY);

            // 把对象添加到子项列表中
            this.itemList.Add(item);

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
                if (!this.selectItem.Equals(item)) {
                    this.selectItem.Unchecked();
                }
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
        /// 删除当前被选中的子项
        /// </summary>
        /// <returns></returns>
        public bool DeleteItem() {
            if (this.selectItem != null) {
                return this.DeleteItem(this.selectItem);
            }
            return true;
        }
        /// <summary>
        /// 删除一个子项
        /// </summary>
        /// <returns></returns>
        public bool DeleteItem(ScrollView_Item item) {
            if (item.transform.IsChildOf(this.GetContentGameObject().transform)) {
                this.itemList.Remove(item);
                Object.Destroy(this.gameObject);
                this.AutoReSetItemPosition();
                this.AutoSetContentHeight();
            }
            return true;
        }


        /// <summary>
        /// 功能按钮被点击时执行的代理方法
        /// </summary>
        /// <param name="scrollView"></param>
        /// <returns></returns>
        public delegate bool ButtonAction(ScrollView scrollView);
        /// <summary>
        /// 记录功能按钮的列表
        /// </summary>
        private List<ScrollView_FuncButton> funcButtonList = new List<ScrollView_FuncButton>();

        /// <summary>
        /// 添加一个额外的按钮
        /// </summary>
        /// <param name="name"></param>
        /// <param name="buttonEvent"></param>
        public void AddButton(string name, ButtonAction buttonEvent) {
            ScrollView_FuncButton button = new ScrollView_FuncButton(name);
            button.SetOnClickEvent(buttonEvent, this);
            button.transform.SetParent(this.transform);

            this.funcButtonList.Add(button);
            this.AutoReSetFuncButtonPositionAndSize();
        }
        /// <summary>
        /// 根据按钮的数量，自动设置按钮的位置
        /// </summary>
        private void AutoReSetFuncButtonPositionAndSize() {
            RectTransform scrollViewRectTransform = this.gameObject.GetComponent<RectTransform>();
            float scrollViewWidth = scrollViewRectTransform.rect.width;
            float scrollViewHeight = scrollViewRectTransform.rect.height;

            int buttonNum = this.funcButtonList.Count;
            float buttonWidth = scrollViewWidth / buttonNum;

            float zoreX = 0 - scrollViewWidth / 2;
            float y = 0 - scrollViewHeight / 2 + 20;

            for (int i = 0; i < buttonNum; ++i) {
                ScrollView_FuncButton button = this.funcButtonList[i];
                button.SetPosition(zoreX + buttonWidth * i + buttonWidth / 2, y);
                button.SetSize(buttonWidth - 5, 30);
            }
        }


        /// <summary>
        /// 关闭交叉按钮触发的事件[关闭窗口]
        /// </summary>
        private void ClosePanel() {
            Object.Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 滚动窗口（UIClass.ScrollView）的子项
    /// </summary>
    public class ScrollView_Item : Base {

        /// <summary>
        /// 父级 ScrollView 对象
        /// </summary>
        private ScrollView parentScrollView;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentScrollView"></param>
        /// <param name="text"></param>
        public ScrollView_Item(ScrollView parentScrollView, string text) {
            Init(parentScrollView);
            this.SetText(text);
        }

        /// <summary>
        /// 初始化的一些操作（每个构造函数都必须执行该方法）
        /// </summary>
        private void Init(ScrollView parentScrollView) {
            this.gameObject = Object.Instantiate(Resources.Load<GameObject>(PrefabPath._2D.PanelScrollView_Item));
            this.parentScrollView = parentScrollView;
            this.SetOnClickAction(this.OnClick);
        }
        /// <summary>
        /// 设置被点击时触发的时间
        /// </summary>
        private void SetOnClickAction(UnityEngine.Events.UnityAction call) {
            UnityEngine.UI.Button button = this.gameObject.GetComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(call);
        }
        /// <summary>
        /// 被点击时执行的方法
        /// </summary>
        private void OnClick() {
            this.parentScrollView.SelectItem(this);
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
        /// 获取文字内容
        /// </summary>
        /// <returns></returns>
        public string GetText() {
            string retStr = "";
            UnityEngine.UI.Text uiText = this.gameObject.GetComponentInChildren<UnityEngine.UI.Text>();
            if (uiText != null) {
                retStr = uiText.text;
            }
            return retStr;
        }

        /// <summary>
        /// 选中的动作
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

    /// <summary>
    /// 按钮
    /// </summary>
    public class ScrollView_FuncButton : Base {

        /// <summary>
        /// 
        /// </summary>
        public ScrollView_FuncButton(string name) {
            this.Init(name);
        }

        /// <summary>
        /// 初始化方法
        /// </summary>
        private void Init(string name) {
            this.gameObject = Object.Instantiate(Resources.Load<GameObject>(PrefabPath._2D.Button));
            this.gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = name;
        }

        /// <summary>
        /// 设置按钮的大小
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetSize(float width, float height) {
            RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(width, rectTransform.rect.height);
        }

        /// <summary>
        /// 设置按钮的位置
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(float x, float y) {
            RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(x, y);
        }

        /// <summary>
        /// 父级的 ScrollView 对象
        /// </summary>
        private ScrollView scrollView = null;
        /// <summary>
        /// 需要执行的动作
        /// </summary>
        private ScrollView.ButtonAction scrollViewAction = null;
        /// <summary>
        /// 设置被点击的事件监听
        /// </summary>
        /// <param name="action"></param>
        public void SetOnClickEvent(ScrollView.ButtonAction scrollViewAction, ScrollView scrollView) {
            this.scrollView = scrollView;
            this.scrollViewAction = scrollViewAction;

            UnityEngine.UI.Button button = this.gameObject.GetComponentInChildren<UnityEngine.UI.Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(this.Clicked);
        }
        /// <summary>
        /// 被点击时执行的方法
        /// </summary>
        private void Clicked() {
            scrollViewAction(this.scrollView);

            return;
        }
    }
}
