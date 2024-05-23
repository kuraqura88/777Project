using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyPattern enemyPattern; 
    private float timer = 0.0f;
    private float attackRate = 1.0f;
    private float nextAttackTime = 0.0f;

    public Define.EnemyAttack atkType;

    private void Awake()
    {
        enemyPattern = GetComponent<EnemyPattern>();
    }

    private void Update()
    {
        switch (atkType)
        {
            case Define.EnemyAttack.Straight:
                {
                    Attack1();
                }
                break;
            case Define.EnemyAttack.Sector:
                {
                    Attack2();
                }
                break;
            case Define.EnemyAttack.Targetting:
                timer += Time.deltaTime;
                if (timer > 3.0f && Time.time > nextAttackTime)
                {
                    Attack3();
                    nextAttackTime = Time.time + attackRate;
                }
                break;
            case Define.EnemyAttack.Suicide:
                Attack4();
                break;

            default:

                break;
        }        
    }
    private void Attack1()
    {
        Debug.Log("세갈래 공격");
    }
    private void Attack2()
    {
        Debug.Log("공격없음");
    }
    private void Attack3()
    {
        Debug.Log("일직선 공격");
    }

    private void Attack4()
    {
        Debug.Log("저격 공격");
    }
}
