using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCUserInput : IUserInput
{
    public bool PressAttack()
    {
        return Input.GetMouseButtonDown(0);
    }

    public float GetAxis(string axisName)
    {
        return Input.GetAxis(axisName);
    }

    public bool PressDash()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public bool PressJump()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
