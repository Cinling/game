using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScrollViewCtrl : MonoBehaviour {

    void Start() {
        // 关闭按钮监听
        GameObject goCloseButton = GameObject.Find(gameObject.name + "/ButtonClose");
        UnityEngine.UI.Button btn = goCloseButton.GetComponent<UnityEngine.UI.Button>();
        btn.onClick.AddListener(ClosePanel);
    }

    private void ClosePanel() {
        Destroy(gameObject);
    }
}
