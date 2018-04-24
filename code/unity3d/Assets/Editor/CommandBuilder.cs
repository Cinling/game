using UnityEngine;
using UnityEditor;

public class Build : Editor {

    public static void Test() {
        string[] levels = { "Assets/Scenes/Main.unity", "Assets/Scenes/World.unity", "Assets/Scenes/UIMakeScene.unity" };
        string path = "E:\\u3d_package\\test\\test.exe";
        BuildPipeline.BuildPlayer(levels, path, BuildTarget.StandaloneWindows64, BuildOptions.None);
    }
}