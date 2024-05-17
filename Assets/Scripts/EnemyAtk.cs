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
        Debug.Log("세갈래 공격");
    }

    private void Attack2()
    {
        Debug.Log("캐릭터를 향해 일직선 공격");
    }

    private void Attack3()
    {
        Debug.Log("일직선 공격");
    }
}
