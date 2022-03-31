using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreComponents
{
    public class HealthPoint : MonoBehaviour
    {
        public float HP = 0f;
        public float MaxHP = 100f;

        private void Start()
        {
            HP = MaxHP;
        }

        public void TakeDamage(Transform atkTrans, float damage)
        {
            HP -= damage;

            if (HP <= 0) HP = 0;
        }
    }
}

