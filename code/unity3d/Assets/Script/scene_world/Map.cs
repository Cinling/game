using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {
    private Json.Map map;

    public Map(Json.Map map) {
        this.map = map;

        GameObject goCube = Object.Instantiate(Resources.Load<GameObject>(PrefabPath._3D.Cube));
        goCube.transform.position = new Vector3(map.width / 2, map.height / -2, map.length / 2);
        goCube.transform.localScale = new Vector3(map.width, map.height, map.length);
    }
}
