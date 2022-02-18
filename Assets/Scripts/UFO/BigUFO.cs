using UnityEngine;


namespace Asteroids
{
    public class BigUFO : UFOBase
    {
        public override UFOType MyType => UFOType.largeUFO;
        public override int Points { get => GetScore(); }
        public override UFOData MyData { get => base.GetData(ResourcesPath.BigUFODataPath); }
        private int GetScore()
        {
            UFOData data = Resources.Load(ResourcesPath.BigUFODataPath) as UFOData;
            return data.scorePoints;
        }
    }
}