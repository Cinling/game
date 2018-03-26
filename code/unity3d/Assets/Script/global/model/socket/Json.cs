using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Json {
    /// <summary>
    /// 地图结构化
    /// </summary>
    public class Map {
        public float width;
        public float length;
        public float height;

        public Map(float width, float length, float height) {
            this.width = width;
            this.length = length;
            this.height = height;
        }
    }

    /// <summary>
    /// 存档结构化
    /// </summary>
    public class Saves {
        public string savesName;

        public Saves(string savesName) {
            this.savesName = savesName;
        }
    }
}
