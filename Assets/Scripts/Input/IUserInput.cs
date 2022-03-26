using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserInput
{
    public float GetAxis(string axisName);

    public bool PressJump();

    public bool PressDash();

    public bool PressAttack();
}
