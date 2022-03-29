using System.Collections;
using UnityEngine;

public interface IDamagable
{
    void TakeDamage(Transform attackTranform, float damageValue);
}