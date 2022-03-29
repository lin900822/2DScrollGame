using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerJumpHandler : ITickable
{
    readonly PlayerModel _playerModel;
    readonly PlayerInputState _inputState;
    readonly Settings _settings;

    readonly Transform _footTrans;

    public PlayerJumpHandler(
        PlayerModel playerModel,
        PlayerInputState inputState,
        Settings settings,
        Transform footTrans)
    {
        _playerModel = playerModel;
        _inputState = inputState;
        _settings = settings;

        _footTrans = footTrans;
    }

    public void Tick()
    {
        if (IsGround())
        {
            if (_inputState.IsJumping) Jump();

            _playerModel.Animator.SetBool("isJump", false);
        }
        else
        {
            _playerModel.Animator.SetBool("isJump", true);
        }
    }

    void Jump() => _playerModel.Velocity = (Vector2.up * _settings.JumpForce);

    bool IsGround() => Physics2D.OverlapCircle(_footTrans.position, _settings.DetectFloorRange, _settings.FloorLayer) != null;

    [Serializable]
    public class Settings
    {
        public float JumpForce;
        public LayerMask FloorLayer = default;
        public float DetectFloorRange = .1f;
    }
}