using UnityEngine;
using Zenject;


[CreateAssetMenu(fileName = "UfoSpawnerSettingsInstaller", menuName = "Installers/UfoSpawnerSettingsInstaller")]
public class UfoSpawnerSettingsInstaller : ScriptableObjectInstaller<UfoSpawnerSettingsInstaller>
{
    public Asteroids.UFOSpawnerSettings uFOSpawnerSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(uFOSpawnerSettings).IfNotBound();
    }
}