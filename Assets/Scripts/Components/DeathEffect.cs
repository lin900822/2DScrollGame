using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DeathEffect : MonoBehaviour, IPoolable<IMemoryPool>
{
    [SerializeField]
    float _lifeTime;

    [SerializeField]
    ParticleSystem _particleSystem;

    IMemoryPool _pool;

    public void OnDespawned()
    {
        
    }

    public void OnSpawned(IMemoryPool pool)
    {
        _particleSystem.Clear();
        _particleSystem.Play();

        _pool = pool;

        Invoke(nameof(Despawn), _lifeTime);
    }

    void Despawn()
    {
        _pool.Despawn(this);
    }

    public class Factory : PlaceholderFactory<DeathEffect>
    {
    }
}
