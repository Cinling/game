using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Json {
    /// <summary>
    /// 地图结构化
    /// </summary>
    public class Map {
        public int worldWidth;
        public int worldLength;

        public Map(int width, int height) {
            this.worldWidth = width;
            this.worldLength = height;
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
