using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerModel
{
    private readonly Rigidbody2D _rigidbody2D = null;
    private readonly CapsuleCollider2D _collider2D = null;
    private readonly Animator _animator = null;

    public bool _isHurt = false;

    public PlayerModel(
        Rigidbody2D rigidbody2D,
        CapsuleCollider2D collider2D, 
        Animator animator)
    {
        _rigidbody2D = rigidbody2D;
        _collider2D = collider2D;
        _animator = animator;
    }

    public bool IsHurt
    {
        get => _isHurt;
        set => _isHurt = value;
    }

    public Animator Animator => _animator;

    public Transform Transform => _rigidbody2D.transform;

    public Vector2 Position
    {
        get => _rigidbody2D.transform.position;
        set => _rigidbody2D.transform.position = value;
    }

    public Vector3 LocalScale
    {
        get => _rigidbody2D.transform.localScale;
        set => _rigidbody2D.transform.localScale = value;
    }

    public Vector2 Velocity
    {
        get => _rigidbody2D.velocity;
        set => _rigidbody2D.velocity = value;
    }

    public void AddForce(Vector3 force, ForceMode2D forceMode2D) => _rigidbody2D.AddForce(force, forceMode2D);

    public void Dash()
    {
        _collider2D.offset = new Vector2(0.06f, -1.13f);
        _collider2D.size = new Vector2(2, 1);
        _collider2D.direction = CapsuleDirection2D.Horizontal;
    }

    public void EndDash()
    {
        _collider2D.offset = new Vector2(0.06f, -0.67f);
        _collider2D.size = new Vector2(1, 2);
        _collider2D.direction = CapsuleDirection2D.Vertical;
    }

    [Serializable]
    public class Settings
    {
        public float MaxHP = 100f;
    }
}
