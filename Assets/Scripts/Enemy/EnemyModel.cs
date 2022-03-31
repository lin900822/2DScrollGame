using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyModel
{
    readonly Settings _settings = null;

    Rigidbody2D _rigidbody2D = null;
    Animator _animator = null;

    Transform _targetTrans = null;

    public EnemyModel(Settings settings, Rigidbody2D rigidbody2D, Animator animator)
    {
        _settings = settings;
        _rigidbody2D = rigidbody2D;
        _animator = animator;
    }

    public Settings SettingsConfiguration => _settings;

    public Transform Transform => _rigidbody2D.transform;

    public Transform TargetTransform
    {
        get => _targetTrans;
        set => _targetTrans = value;
    }

    public Vector2 LocalScale
    {
        get => _rigidbody2D.transform.localScale;
        set => _rigidbody2D.transform.localScale = value;
    }

    public Animator Animator => _animator;

    public Vector2 Velocity
    {
        get => _rigidbody2D.velocity;
        set => _rigidbody2D.velocity = value;
    }

    public void AddForce(Vector3 force, ForceMode2D forceMode2D) => _rigidbody2D.AddForce(force, forceMode2D);

    [Serializable]
    public class Settings
    {
        public float WalkSpeed = 3f;
        public float RunSpeed = 5f;

        public LayerMask TargetLayer = default;
        public float DetectTargetRadius = 5f;
        public float AttackDamage = 10f;

        public GameObject deathEffectPrefab = null;
    }
}
