using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Json {
    /// <summary>
    /// 地图结构化
    /// </summary>
    public class Map {
        public int width;
        public int length;

        public Map(int width, int height) {
            this.width = width;
            this.length = height;
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
