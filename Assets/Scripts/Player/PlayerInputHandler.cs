using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInputHandler : ITickable
{
    readonly PlayerInputState _inputState = null;
    readonly IUserInput _userInput = null;

    public PlayerInputHandler(PlayerInputState inputState, IUserInput userInput)
    {
        _inputState = inputState;
        _userInput = userInput;
    }

    public void Tick()
    {
        _inputState.HorizontalAxis = _userInput.GetAxis("Horizontal");

        _inputState.IsJumping = _userInput.PressJump();
        _inputState.IsDashing = _userInput.PressDash();
        _inputState.IsAttacking = _userInput.PressAttack();
    }
}
