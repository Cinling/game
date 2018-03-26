using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : Json.Map {
    public new float width {
        get { return base.width; }
    }
    public new float length {
        get { return base.length; }
    }
    public new float height {
        get { return base.height; }
    }

    public Map(Json.Map map) : base(map.width, map.length, map.height) {
        GameObject goCube = Object.Instantiate(Resources.Load<GameObject>(PrefabPath._3D.Cube));
        goCube.transform.position = new Vector3(map.width / 2, map.height / -2, map.length / 2);
        goCube.transform.localScale = new Vector3(map.width, map.height, map.length);
    }
}
