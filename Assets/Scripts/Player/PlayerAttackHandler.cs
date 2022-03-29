using System.Collections;
using UnityEngine;
using Zenject;
using System;
using CoreComponents;

public class PlayerAttackHandler : ITickable
{
    readonly PlayerModel _playerModel;
    readonly PlayerInputState _inputState;
    readonly Settings _settings;
    readonly CinemechineShake _cinemechineShake;

    readonly Transform _attackPoint;

    public PlayerAttackHandler(
        PlayerModel playerModel,
        PlayerInputState inputState,
        Settings settings,
        CinemechineShake cinemechineShake,
        Transform attackPoint)
    {
        _playerModel = playerModel;
        _inputState = inputState;
        _settings = settings;
        _cinemechineShake = cinemechineShake;
        _attackPoint = attackPoint;
    }

    public void Tick()
    {
        if (_inputState.IsAttacking)
            Attack();
    }

    float firstAtkTime = -999f;
    float secondAtkTime = -999f;

    void Attack()
    {
        if (Time.time - secondAtkTime <= 1f)
        {
            SpecialAttack();
            firstAtkTime = -999f;
            secondAtkTime = -999f;
        }
        else
        {
            if (Time.time - firstAtkTime <= 1f)
            {
                secondAtkTime = Time.time;
                NormalAttack();
            }
            else
            {
                firstAtkTime = Time.time;
                NormalAttack();
            }
        }
    }

    void NormalAttack()
    {
        _playerModel.Animator.SetTrigger("Attack");
        bool hasGivenDamage = GiveDamage(_settings.NormalDamage);

        if (hasGivenDamage)
            _cinemechineShake.Shake(CinemechineShakeType.Normal);
    }

    void SpecialAttack()
    {
        _playerModel.Animator.SetTrigger("Strike");
        bool hasGivenDamage = GiveDamage(_settings.SpecialDamage);

        if (hasGivenDamage)
            _cinemechineShake.Shake(CinemechineShakeType.Hard);
    }
    
    bool GiveDamage(float damage)
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(_attackPoint.position, _settings.AttackRange, _settings.EnemyLayer);

        for (int i = 0; i < enemyColliders.Length; i++)
        {
            IDamagable damagable = null;

            if (enemyColliders[i].gameObject.TryGetComponent<IDamagable>(out damagable))
            {
                damagable.TakeDamage(_playerModel.Transform, damage);
            }
        }

        if (enemyColliders.Length >= 1)
            return true;

        return false;
    }

    [Serializable]
    public class Settings
    {
        public float NormalDamage = 10f;
        public float SpecialDamage = 20f;

        public LayerMask EnemyLayer = default;
        public float AttackRange = .5f;
    }
}