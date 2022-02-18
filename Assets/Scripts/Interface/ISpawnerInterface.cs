using UnityEngine;

namespace Asteroids
{

    public interface ISpawnerInterface<T>
    {
        void Start();
        void SpawnAtPosition(T type, Vector3 position);
        void Stop();
    }
}