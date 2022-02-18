using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BulletSettingsInstaller", menuName = "Installers/BulletSettingsInstaller")]
public class BulletSettingsInstaller : ScriptableObjectInstaller<BulletSettingsInstaller>
{
    public Asteroids.BulletSettings BulletSettings;
    public override void InstallBindings()
    {
        Container.BindInstance(BulletSettings).IfNotBound();
    }
}
