using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class PlayerMoveHandler : IFixedTickable
{
    readonly PlayerModel _playerModel;
    readonly PlayerInputState _inputState;
    readonly Settings _settings;

    public PlayerMoveHandler(
        PlayerModel playerModel,
        PlayerInputState inputState,
        Settings settings)
    {
        _playerModel = playerModel;
        _inputState = inputState;
        _settings = settings;
    }

    public void FixedTick()
    {
        Move();
        SetAnimation();
        SetFaceDirection();
    }

    void Move()
    {
        if(!_playerModel.IsHurt)
            _playerModel.Velocity = new Vector2(_inputState.HorizontalAxis * _settings.MoveSpeed, _playerModel.Velocity.y);
    }

    void SetAnimation()
    {
        if (_playerModel.Velocity.x != .0f)
            _playerModel.Animator.SetBool("isWalk", true);
        else
            _playerModel.Animator.SetBool("isWalk", false);
    }

    void SetFaceDirection()
    {
        if (_playerModel.IsHurt) return;

        if (_playerModel.Velocity.x < -0.1f)
            _playerModel.LocalScale = new Vector3(-1f, 1f, 1f);
        else if (_playerModel.Velocity.x > 0.1f)
            _playerModel.LocalScale = new Vector3(1f, 1f, 1f);
    }

    [Serializable]
    public class Settings
    {
        public float MoveSpeed;
    }
}
