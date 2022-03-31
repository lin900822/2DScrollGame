using CoreComponents;
using System.Collections;
using UnityEngine;
using Zenject;

public class EnemyFacade : MonoBehaviour, IDamagable
{
    EnemyModel _enemyModel = null;
    EnemyStateManager _stateManager = null;
    HealthPoint _healthPoint = null;

    [Inject]
    public void Construct(EnemyModel playerModel, EnemyStateManager stateManager, HealthPoint healthPoint)
    {
        _enemyModel = playerModel;
        _stateManager = stateManager;
        _healthPoint = healthPoint;
    }

    private void Update()
    {
        if (transform.position.y <= -10f) Die();
    }

    public void TakeDamage(Transform attackTranform, float damageValue)
    {
        _healthPoint.TakeDamage(attackTranform, damageValue);

        if(_healthPoint.HP <= 0) Die();

        int damageDirectionX = CalculateDamageDirectionX(attackTranform, transform);

        Hurt(damageDirectionX);
    }

    int CalculateDamageDirectionX(Transform attackTranform, Transform selfTransform)
    {
        Vector2 atkDirection = attackTranform.position - selfTransform.position;

        int damageDirectionX = atkDirection.x > 0 ? 1 : -1;

        return damageDirectionX;
    }

    void Hurt(int damageDirectionX)
    {
        _stateManager.ChangeState(EnemyStateManager.States.Hurt);
        _enemyModel.AddForce(new Vector2(-damageDirectionX, 1) * 10f, ForceMode2D.Impulse);
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((_enemyModel.SettingsConfiguration.TargetLayer & (1 << collision.gameObject.layer)) > 0)
        {
            IDamagable damagable = null;

            if (collision.gameObject.TryGetComponent<IDamagable>(out damagable))
            {
                damagable.TakeDamage(this.transform, _enemyModel.SettingsConfiguration.AttackDamage);
            }
        }
    }
}