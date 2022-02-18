using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "AsteroidSpawnerSettingsInstaller", menuName = "Installers/AsteroidSpawnerSettingsInstaller")]
public class AsteroidSpawnerSettingsInstaller : ScriptableObjectInstaller<AsteroidSpawnerSettingsInstaller>
{
    public Asteroids.AsteroidSpawnerSettings asteroidSpawnerSettings;
    public override void InstallBindings()
    {
        Container.BindInstance(asteroidSpawnerSettings).IfNotBound();
    }
}
