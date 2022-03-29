using CoreComponents;
using System.Collections;
using UnityEngine;
using Zenject;

public class EnemyFacade : MonoBehaviour, IDamagable
{
    EnemyModel _enemyModel = null;
    EnemyStateManager _stateManager = null;
    [SerializeField] HealthPoint healthPoint = null;

    [Inject]
    public void Construct(EnemyModel playerModel, EnemyStateManager stateManager)
    {
        _enemyModel = playerModel;
        _stateManager = stateManager;
    }

    public void TakeDamage(Transform attackTranform, float damageValue)
    {
        healthPoint.TakeDamage(attackTranform, damageValue);

        _stateManager.ChangeState(EnemyStateManager.States.Hurt);

        Vector2 atkDirection = attackTranform.position - transform.position;

        int damageDirection = atkDirection.x > 0 ? 1 : -1;

        _enemyModel.AddForce(new Vector2(-damageDirection, 1) * 10f, ForceMode2D.Impulse);
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