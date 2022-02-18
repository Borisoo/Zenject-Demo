using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "UfoSettingsInstaller", menuName = "Installers/UfoSettingsInstaller")]
public class UfoSettingsInstaller : ScriptableObjectInstaller<UfoSettingsInstaller>
{
    public Asteroids.UFOSettings uFOSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(uFOSettings).IfNotBound();
    }
}
