using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScrollViewCtrl : MonoBehaviour {

    void Start() {
        GameObject goCloseButton = GameObject.Find(gameObject.name + "/ButtonClose");
        UnityEngine.UI.Button btn = goCloseButton.GetComponent<UnityEngine.UI.Button>();
        btn.onClick.AddListener(ClosePanel);
    }

    private void ClosePanel() {
        GameObject.Destroy(gameObject);
    }
}
