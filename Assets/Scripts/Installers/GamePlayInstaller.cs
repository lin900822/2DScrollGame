using UnityEngine;
using Zenject;
using System;

public class GamePlayInstaller : MonoInstaller
{
    [Inject]
    Settings _settings = null;

    public override void InstallBindings()
    {
        Container.Bind<IUserInput>().To<PCUserInput>().AsCached().NonLazy();

        Container.BindFactory<DeathEffect, DeathEffect.Factory>()
                // We could just use FromMonoPoolableMemoryPool here instead, but
                // for IL2CPP to work we need our pool class to be used explicitly here
                .FromPoolableMemoryPool<DeathEffect, DeathEffectPool>(poolBinder => poolBinder
                    // Spawn 4 right off the bat so that we don't incur spikes at runtime
                    .WithInitialSize(4)
                    .FromComponentInNewPrefab(_settings.DeathEffectPrefab)
                    .UnderTransformGroup("DeathEffects"));

        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<PlayerHPChangedSignal>();
        Container.DeclareSignal<PlayerDiedSignal>();
    }

    [Serializable]
    public class Settings
    {
        public GameObject DeathEffectPrefab = null;
    }
}