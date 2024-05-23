using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyPattern enemyPattern;

    public StatusHandler statusHandler { get; private set; }

    protected Collider2D col = null;
    public Define.EnemyType type;

    public Animator animator;

    public GameObject Root;

    private readonly int hashHit = Animator.StringToHash("isHit");
    private readonly int hashDead = Animator.StringToHash("isDead");
    //public Define.EnemyType enemyType;

    protected virtual void Awake()
    {
        enemyPattern = GetComponent<EnemyPattern>();
        statusHandler = GetComponent<StatusHandler>();
        animator = GetComponentInChildren<Animator>();
        col = GetComponent<Collider2D>();

    }

    protected virtual void OnEnable()
    {

        statusHandler.OnHit += OnHit;
        statusHandler.OnDead += OnDead;

    }
    protected virtual void OnDisable()
    {

        statusHandler.OnHit -= OnHit;
        statusHandler.OnDead -= OnDead;
    }
    protected virtual void Start()
    {
        if (enemyPattern != null)
        {
            enemyPattern.canMove = true;
        }
        else
        {
            Debug.LogWarning("EnemyPattern component is missing on " + gameObject.name);
        }
    }
    public void CanMove()
    {
        enemyPattern.CanMove();
    }

    protected void OnHit(bool active)
    {
        animator.SetTrigger(hashHit);
    }

    protected virtual void OnDead()
    {
        col.enabled = false;
        animator.SetBool(hashDead, true);
        Invoke(nameof(Dead), 0.3f);
    }

    private void Dead()
    {
        EnemyRespawn.Clear(this);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        statusHandler.OnHit -= OnHit;
        statusHandler.OnDead -= OnDead;

    }
}
