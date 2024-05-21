using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyPattern enemyPattern;
    public Define.EnemyType type;

    //public Define.EnemyType enemyType;

    private void Awake()
    {
        enemyPattern = GetComponent<EnemyPattern>();
    }

    private void Start()
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

}
