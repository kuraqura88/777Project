using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="RangedAttackSO", menuName = "AttackSO/RangedAttackInfo", order =1)]
public class RangedAttackSO : AttackSO
{
    [Header("Projectile Property")]

    [Range(1, 64)]
    public int numberOfProjectilesPerShot = 1;
   
    [Range(1f, 90f)]
    public float projectilesAngleSpace = 15f;

    [Range(0f, 10f)]
    public float spreadAngle = 10f;

}