using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtk : MonoBehaviour
{
    public EnemyPattern enemyPattern;

    private void Update()
    {
        if (enemyPattern.isStop)
        {
            Attack1();
        }
    }

    private void Attack1()
    {
        Debug.Log("������ ����");
    }

    private void Attack2()
    {
        Debug.Log("ĳ���͸� ���� ������ ����");
    }

    private void Attack3()
    {
        Debug.Log("������ ����");
    }
}
