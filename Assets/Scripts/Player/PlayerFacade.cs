using CoreComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerFacade : MonoBehaviour, IDamagable
{
    PlayerModel _playerModel = null;
    HealthPoint _healthPoint = null;
    CinemechineShake _cinemechineShake = null;
    SignalBus _signalBus = null;
    DeathEffect.Factory _deathEffectFactory = null;

    [Inject]
    public void Construct(
        PlayerModel playerModel,
        HealthPoint healthPoint,
        CinemechineShake cinemechineShake,
        SignalBus signalBus,
        DeathEffect.Factory deathEffectFactory)
    {
        _playerModel = playerModel;
        _healthPoint = healthPoint;
        _cinemechineShake = cinemechineShake;
        _signalBus = signalBus;
        _deathEffectFactory = deathEffectFactory;
    }

    private void Update()
    {
        if (transform.position.y <= -10f) Die();
    }

    public void TakeDamage(Transform attackTranform, float damageValue)
    {
        _healthPoint.TakeDamage(attackTranform, damageValue);
        _signalBus.Fire(new PlayerHPChangedSignal() { HP = _healthPoint.HP, MaxHP = _healthPoint.MaxHP });

        if (_healthPoint.HP <= 0) Die();

        int damageDirectionX = CalculateDamageDirectionX(attackTranform, transform);
        Hurt(damageDirectionX);

        _playerModel.IsHurt = true;
        Invoke(nameof(ResetHurt), .5f);
    }

    void Die()
    {
        _signalBus.Fire<PlayerDiedSignal>();

        DeathEffect deathEffect = _deathEffectFactory.Create();
        deathEffect.transform.position = transform.position;

        this.gameObject.SetActive(false);
    }

    int CalculateDamageDirectionX(Transform attackTranform, Transform selfTransform)
    {
        Vector2 atkDirection = attackTranform.position - selfTransform.position;

        int damageDirectionX = atkDirection.x > 0 ? 1 : -1;

        return damageDirectionX;
    }

    void Hurt(int damageDirection)
    {
        _playerModel.Animator.SetTrigger("Hurt");
        _playerModel.AddForce(new Vector2(-damageDirection, 0.1f) * 3f, ForceMode2D.Impulse);

        _cinemechineShake.Shake(CinemechineShakeType.Normal);
    }

    void ResetHurt()
    {
        _playerModel.IsHurt = false;
    }
}
