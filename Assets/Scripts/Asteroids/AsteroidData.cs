using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{

    [CreateAssetMenu(menuName = "AsteroidData", order = 51)]
    public class AsteroidData : ScriptableObject
    {
        public Vector3 scale;
        public int numberOfFragments;
        public int scorePoints;
        public AsteroidType fragmentType;
    }

}