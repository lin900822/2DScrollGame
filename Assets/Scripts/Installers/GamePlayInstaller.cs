using UnityEngine;
using Zenject;

public class GamePlayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IUserInput>().To<PCUserInput>().AsCached().NonLazy();

        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<PlayerHPChangedSignal>();
        Container.DeclareSignal<PlayerDiedSignal>();
    }
}