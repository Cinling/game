using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    void Start() {

    }

    void Update() {
        ThreadTool threadTool = ThreadTool.GetInstance();
        while (threadTool.MainThread_RunOnMainSceneLambda()) { }
    }
}
