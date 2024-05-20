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
                // 'enemypublic'�� ���ؼ��� ������ Ÿ�̸� ������ ���
                timer += Time.deltaTime;
                if (timer > 3.0f && Time.time > nextAttackTime)
                {
                    Attack3();
                    nextAttackTime = Time.time + attackRate; // ���� ���� �ð� ����
                }
            }
            else if (Time.time > nextAttackTime)
            {
                // ������ �� Ÿ�Կ� ���� ���� ����
                if (gameObject.tag == "enemyif" && enemyPattern.isStop)
                {
                    Attack1();
                }
                else if (gameObject.tag == "enemyswitch")
                {
                    Attack2();
                }

                nextAttackTime = Time.time + attackRate; // ���� ���� �ð� ����
            }
        }
    }
    private void Attack1()
    {
        Debug.Log("������ ����");
    }
    private void Attack2()
    {
        Debug.Log("������ ����");
    }
    private void Attack3()
    {
        Debug.Log("ĳ���͸� ���� ������ ����");
    }
}
