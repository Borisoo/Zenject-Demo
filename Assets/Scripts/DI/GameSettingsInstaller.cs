using UnityEngine;
using Zenject;


[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public Asteroids.AsteroidSpawnerSettings asteroidSpawnerSettings;
    public Asteroids.UFOSpawnerSettings uFOSpawnerSettings;
    public Asteroids.UFOSettings uFOSettings;
    public Asteroids.BulletSettings BulletSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(asteroidSpawnerSettings).IfNotBound();
        Container.BindInstance(uFOSpawnerSettings).IfNotBound();
        Container.BindInstance(uFOSettings).IfNotBound();
        Container.BindInstance(BulletSettings).IfNotBound();
    }
}