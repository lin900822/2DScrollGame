using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreComponents;

public class EnemyController : MonoBehaviour
{
    //
    [Header("Main Components")]
    public Rigidbody2D enemyRigbody = null;
    public Animator animator = null;

    public HealthPoint healthPoint = null;

    //
    [Space(10)]
    [Header("Death Effect")]
    [SerializeField] GameObject dieEffectPrefab = null;

    //
    [Space(10)]
    [Header("Target")]
    public Transform Target = null;
    
    [SerializeField] LayerMask targetLayer = default;
    [SerializeField] float damage = 10f;
    [SerializeField] float detectTargetRadius = 5f;

    //
    [Space(10)]
    [Header("Basic Variables")]
    public float walkSpeed = 2f;
    public float runSpeed = 3f;

    //
    EnemyState state = null;

    private void Start()
    {
        healthPoint.OnDamaged += hurt;

        SetState(new EnemyIdleState(this));
    }

    private void OnDestroy()
    {
        healthPoint.OnDamaged -= hurt;
    }

    private void Update()
    {
        state.Tick();

        Target = DetectTarget();

        if (Target != null)
        {
            if (!(state is EnemyRunState || state is EnemyHurtState))
            {
                SetState(new EnemyRunState(this));
            }
        }
    }

    //
    void hurt(Transform atkTrans)
    {
        SetState(new EnemyHurtState(this));

        Vector2 atkDirection = atkTrans.position - transform.position;

        enemyRigbody.AddForce(new Vector2(-atkDirection.normalized.x, 1) * 8f, ForceMode2D.Impulse);

        if (healthPoint.HP <= 0) dead();
    }

    void dead()
    {
        Instantiate(dieEffectPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    //
    Transform DetectTarget()
    {
        Collider2D targetCollider = Physics2D.OverlapCircle(transform.position, detectTargetRadius, targetLayer);

        if (targetCollider != null)
            return targetCollider.transform;
        else
            return null;
    }

    //
    public void SetState(EnemyState state)
    {
        if(this.state != null)
            this.state.ExitState();

        this.state = state;
    }

    public EnemyState GetState() => state;

    //
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((targetLayer & (1 << collision.gameObject.layer)) > 0 && !(GetState() is EnemyHurtState))
        {
            HealthPoint targetHP = null;

            if (collision.gameObject.TryGetComponent<HealthPoint>(out targetHP))
            {
                targetHP.TakeDamage(this.transform, damage);
            }
        }
    }
}
