using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu {
    private static MainMenu share_instance = null;

    public static MainMenu GetInstance() {
        if (share_instance == null) {
            share_instance = new MainMenu();
        }
        return share_instance;
    }

    private MainMenu() {
        CreateMenu();
    }

    private Canvas canvas = null;

    /// <summary>
    /// 
    /// </summary>
    public void CreateMenu() {
        canvas = Canvas.Instantiate(Resources.Load<Canvas>("ui/Canvas"));
        //canvas.
    }
}
