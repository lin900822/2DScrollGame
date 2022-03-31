using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingInstaller", menuName = "Installers/GameSettingInstaller")]
public class GameSettingInstaller : ScriptableObjectInstaller<GameSettingInstaller>
{
    public GamePlayInstaller.Settings GamePlaySettings;
    public PlayerModel.Settings PlayerModelSettings;
    public PlayerMoveHandler.Settings PlayerMovementSettings;
    public PlayerJumpHandler.Settings PlayerJumpSettings;
    public PlayerAttackHandler.Settings PlayerAttackSettings;
    public EnemyModel.Settings EnemySettings;

    public override void InstallBindings()
    {
        Container.BindInstance(GamePlaySettings).IfNotBound();
        Container.BindInstance(PlayerModelSettings).IfNotBound();
        Container.BindInstance(PlayerMovementSettings).IfNotBound();
        Container.BindInstance(PlayerJumpSettings).IfNotBound();
        Container.BindInstance(PlayerAttackSettings).IfNotBound();
        Container.BindInstance(EnemySettings).IfNotBound();
    }
}