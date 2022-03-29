using CoreComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerFacade : MonoBehaviour, IDamagable
{
    PlayerModel _playerModel = null;
    PlayerMoveHandler _playerMoveHandler = null;
    HealthPoint _healthPoint = null;
    CinemechineShake _cinemechineShake = null;

    [Inject]
    public void Construct(PlayerModel playerModel, PlayerMoveHandler playerMoveHandler, HealthPoint healthPoint, CinemechineShake cinemechineShake)
    {
        _playerModel = playerModel;
        _playerMoveHandler = playerMoveHandler;
        _healthPoint = healthPoint;
        _cinemechineShake = cinemechineShake;
    }

    public void TakeDamage(Transform attackTranform, float damageValue)
    {
        _healthPoint.TakeDamage(attackTranform, damageValue);

        Vector2 atkDirection = attackTranform.position - transform.position;

        int damageDirection = atkDirection.x > 0 ? 1 : -1;

        _playerModel.AddForce(new Vector2(-damageDirection, 1) * 8f, ForceMode2D.Impulse);

        _cinemechineShake.Shake(CinemechineShakeType.Normal);

        _playerMoveHandler.IsHurt = true;
        Invoke(nameof(ResetHurt), .5f);
    }

    void ResetHurt()
    {
        _playerMoveHandler.IsHurt = false;
    }
}
