
using UnityEngine;

[CreateAssetMenu(fileName ="DefaultAttackSO", menuName = "AttackSO/AttackInfo", order = 0)]
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")]
    public float size;
    public float delay;
    public float speed;

    public Define.AttackDirection direction;
    public Define.Projectile projectile;
    public LayerMask target;

    public AudioClip attackSound;

}
