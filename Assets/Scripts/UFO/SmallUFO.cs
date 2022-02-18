using UnityEngine;
using System.Collections;

namespace Asteroids
{
    public class SmallUFO : UFOBase
    {
        public override UFOType MyType => UFOType.smallUFO;
        public override int Points { get => GetScore(); }
        public override UFOData MyData { get => base.GetData(ResourcesPath.SmallUFODataPath); }

        private int GetScore()
        {
            UFOData data = Resources.Load(ResourcesPath.SmallUFODataPath) as UFOData;
            return data.scorePoints;
        }
    }
}