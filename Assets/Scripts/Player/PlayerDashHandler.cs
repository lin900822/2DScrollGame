using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerDashHandler : ITickable
{
    readonly PlayerModel _playerModel;
    readonly PlayerInputState _inputState;

    public PlayerDashHandler(
        PlayerModel playerModel,
        PlayerInputState inputState
        )
    {
        _playerModel = playerModel;
        _inputState = inputState;
    }

    public void Tick()
    {
        if (_inputState.IsDashing)
            Dash();
        else
            EndDash();
    }

    void Dash()
    {
        _playerModel.AnimatorSetBool("isDash", true);
        _playerModel.Dash();
    }

    void EndDash()
    {
        _playerModel.AnimatorSetBool("isDash", false);
        _playerModel.EndDash();
    }
}