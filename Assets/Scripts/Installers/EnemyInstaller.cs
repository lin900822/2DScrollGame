using UnityEngine;
using Zenject;
using System;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] Settings _settings = null;

    public override void InstallBindings()
    {
        Container.Bind<EnemyModel>().AsSingle().WithArguments(_settings.Rigidbody2D, _settings.Animator);
        Container.BindInterfacesAndSelfTo<EnemyStateManager>().AsSingle();
        Container.BindInterfacesTo<EnemyTargetDetecter>().AsSingle();

        Container.Bind<EnemyStateIdle>().AsSingle();
        Container.Bind<EnemyStateWalk>().AsSingle();
        Container.Bind<EnemyStateRun>().AsSingle();
        Container.Bind<EnemyStateHurt>().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public Rigidbody2D Rigidbody2D = null;
        public Animator Animator = null;
    }
}