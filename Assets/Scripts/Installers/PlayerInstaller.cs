using UnityEngine;
using Zenject;
using System;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField]
    Settings _settings = null;

    [Space(10)]
    [SerializeField]
    GameSettingInstaller gameSetting = null;

    public override void InstallBindings()
    {
        Container.Bind<PlayerModel>().AsSingle()
                .WithArguments(_settings.Rigidbody2D, _settings.Collider2D, _settings.Animator);

        Container.BindInterfacesTo<PlayerInputHandler>().AsSingle();
        Container.Bind<PlayerInputState>().AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerJumpHandler>().AsSingle().WithArguments(_settings.FootTrans);
        Container.BindInterfacesAndSelfTo<PlayerMoveHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerDashHandler>().AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerAttackHandler>().AsSingle().WithArguments(_settings.AttackPoint, _settings.CinemechineShake);
    }

    [Serializable]
    public class Settings
    {
        public Rigidbody2D Rigidbody2D = null;
        public CapsuleCollider2D Collider2D = null;
        public Animator Animator = null;
        public CinemechineShake CinemechineShake = null;

        [Space(10)]
        public Transform FootTrans = null;

        [Space(10)]
        public Transform AttackPoint = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_settings.FootTrans.position, gameSetting.PlayerJumpSettings.DetectFloorRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_settings.AttackPoint.position, gameSetting.PlayerAttackSettings.AttackRange);
    }
}