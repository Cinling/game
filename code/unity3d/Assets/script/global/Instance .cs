using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instance : MonoBehaviour {
    [RuntimeInitializeOnLoadMethod]
    static void Initialize() {
        GameObject.DontDestroyOnLoad(new GameObject("Instance", typeof(Instance)) {
            hideFlags = HideFlags.HideInHierarchy
        });

        // 跳轉到主場景
        SceneCtrl.GetInstance().SwitchToMain();
    }
}
