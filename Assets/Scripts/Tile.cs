using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Tile
    {
        public GameObject tileGameObject;
        public float creationTime;

        public Tile(GameObject gameObject, float creationTime)
        {
            tileGameObject = gameObject;
            this.creationTime = creationTime;
        }
    }
}