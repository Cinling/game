using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGame_Menu : MonoBehaviour {

    private static GameObject panelGameObject = null;

    void Start() {
        panelGameObject = gameObject;
        gameObject.SetActive(false);
    }

    public static void Show() {
        panelGameObject.SetActive(true);
    }

    public static void Hide() {
        panelGameObject.SetActive(false);
    }
}
