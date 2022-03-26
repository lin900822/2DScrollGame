using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingInstaller", menuName = "Installers/GameSettingInstaller")]
public class GameSettingInstaller : ScriptableObjectInstaller<GameSettingInstaller>
{
    public PlayerMoveHandler.Settings PlayerMovementSettings;
    public PlayerJumpHandler.Settings PlayerJumpSettings;
    public PlayerAttackHandler.Settings PlayerAttackSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(PlayerMovementSettings).IfNotBound();
        Container.BindInstance(PlayerJumpSettings).IfNotBound();
        Container.BindInstance(PlayerAttackSettings).IfNotBound();
    }
}