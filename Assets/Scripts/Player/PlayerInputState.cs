using UnityEditor;
using UnityEngine;

public class PlayerInputState
{
    public float HorizontalAxis
    {
        get;
        set;
    }

    public bool IsDashing
    {
        get;
        set;
    }

    public bool IsJumping
    {
        get;
        set;
    }

    public bool IsAttacking
    {
        get;
        set;
    }
}