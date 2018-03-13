using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIHelper {
    class Pannel {
        public static void SetSize(UnityEngine.UI.Image pannel, float width, float height) {
            Vector2 oldSize = pannel.rectTransform.rect.size;
            Vector2 deltaSize = new Vector2(width, height) - oldSize;
            pannel.rectTransform.offsetMin = pannel.rectTransform.offsetMin - new Vector2(deltaSize.x * pannel.rectTransform.pivot.x, deltaSize.y * pannel.rectTransform.pivot.y);
            pannel.rectTransform.offsetMax = pannel.rectTransform.offsetMax + new Vector2(deltaSize.x * ( 1f - pannel.rectTransform.pivot.x ), deltaSize.y * ( 1f - pannel.rectTransform.pivot.y ));
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
        public static void AddButton(GameObject parent, string perfab, string text, float x, float y, UnityEngine.Events.UnityAction call) {
            GameObject gameObject = GameObject.Instantiate(Resources.Load<GameObject>(perfab));
            gameObject.transform.SetParent(parent.transform);
            gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
            UnityEngine.UI.Button button = gameObject.GetComponent<UnityEngine.UI.Button>();
            button.transform.position = new Vector3(x, y, 0);
            button.onClick.AddListener(call);
        }
    }
}
