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

    private void Awake()
    {
        enemyPattern = GetComponent<EnemyPattern>();
    }

    private void Update()
    {
        if (Time.time > nextAttackTime)
        {
            if (gameObject.tag == "enemypublic")
            {
                // 'enemypublic'에 대해서만 별도의 타이머 로직을 사용
                timer += Time.deltaTime;
                if (timer > 3.0f && Time.time > nextAttackTime)
                {
                    Attack3();
                    nextAttackTime = Time.time + attackRate; // 다음 공격 시간 설정
                }
            }
            else if (Time.time > nextAttackTime)
            {
                // 나머지 적 타입에 대한 공격 로직
                if (gameObject.tag == "enemyif" && enemyPattern.isStop)
                {
                    Attack1();
                }
                else if (gameObject.tag == "enemyswitch")
                {
                    Attack2();
                }

                nextAttackTime = Time.time + attackRate; // 다음 공격 시간 설정
            }
        }
    }
    private void Attack1()
    {
        Debug.Log("세갈래 공격");
    }
    private void Attack2()
    {
        Debug.Log("일직선 공격");
    }
    private void Attack3()
    {
        Debug.Log("캐릭터를 향해 일직선 공격");
    }
}
