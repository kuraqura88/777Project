using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamagable
{
    EnemyPattern enemyPattern;

    public Define.EnemyType enemyType;

    public int currentHP;

    public int maxHP = 5;

    private void Start()
    {
        currentHP = maxHP;
    }

    public bool Damage(int damage)
    {
        currentHP += damage;

        if(currentHP <= 0)
        {
            Debug.Log("»ç¸Á");
            return true;
        }
        
        if(damage > 0)
        {
            // TODO : Èú
            return false;
        }

        return true;
    }
}