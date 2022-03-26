using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreComponents
{
    public class HealthPoint : MonoBehaviour
    {
        public event Action<Transform> OnDamaged = null;

        public float HP => hp;

        [SerializeField] float hp = 0f;
        [SerializeField] float maxHP = 100f;

        private void Start()
        {
            hp = maxHP;
        }

        public void TakeDamage(Transform atkTrans, float damage)
        {
            hp -= damage;
            OnDamaged?.Invoke(atkTrans);
        }
    }
}

