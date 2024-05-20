using System;

using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class ProjectileController : MonoBehaviour
{
    private AudioSource aSource = null;
    public AudioClip shootSound = null;

    [SerializeField]
    private RangedAttackSO attackSO;

    #region 기본 공격 속성

    [Header("공격 속성")]

    [SerializeField]
    private float delay = 0.2f;

    [SerializeField]
    private float speed = 100f;

    [SerializeField]
    private Define.AttackDirection attackDirection;

    [SerializeField]
    private Define.Projectile projectileType;

    // 타겟팅 공격 처리
    [SerializeField]
    private LayerMask targetMask;

    [Range(1, 64)]
    [SerializeField]
    private int numberOfProjectilesPerShot = 1;
    
    [Range(0, 30f)]
    [SerializeField]
    private float projectilesAngleSpace = 10f;

    [Range(0f, 5f)]
    [SerializeField]
    private float spreadAngle = 1f;

    #endregion

    [SerializeField]
    private Transform projectileRoot;


    private Vector2 aimDirection = Vector2.right;

    public float t = 1;

    private float currentTime = 0;

    public bool isAttack = false;

    private Collider2D target;

    public float radius = 10f;

    private void Awake()
    {
        aSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        delay = attackSO.delay;
        speed = attackSO.speed;
        attackDirection = attackSO.direction;
        projectileType = attackSO.projectile;

        numberOfProjectilesPerShot = attackSO.numberOfProjectilesPerShot;
        projectilesAngleSpace = attackSO.projectilesAngleSpace;
        spreadAngle = attackSO.spreadAngle;

        targetMask = attackSO.target;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= attackSO.delay)
        {
            currentTime = 0;
            Attack();
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Vector3 start = transform.position;
    //    int segmentCount = 64;
    //    for (int i = 0; i <= segmentCount; i++)
    //    {
    //        float angle = i * Mathf.PI * 2 / segmentCount;
    //        Vector3 point = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0) + start;
            
    //        Vector3 nextPoint = new Vector3(Mathf.Cos((i + 1) * Mathf.PI * 2 / segmentCount) * radius, Mathf.Sin((i + 1) * Mathf.PI * 2 / segmentCount) * radius, 0) + start;

    //        Gizmos.DrawLine(point, nextPoint);
    //    }
    //}

    
    private void Attack()
    {
        Shoot();

    }

    public void Shoot()
    {
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * projectilesAngleSpace;

        isAttack = true;
        switch (attackDirection)
        {
            case Define.AttackDirection.Up:
                aimDirection = transform.up;
                break;
            case Define.AttackDirection.Down:
                aimDirection = -transform.up;
                break;
            case Define.AttackDirection.Left:
                aimDirection = -transform.right;
                break;
            case Define.AttackDirection.Right:
                aimDirection = transform.right;
                break;
            case Define.AttackDirection.Target:
                target = Physics2D.OverlapCircle(transform.position, radius, attackSO.target);
                
                if (target == null) 
                    isAttack = false;
                else 
                    aimDirection = (target.transform.position - transform.position).normalized;

                break;
        }

        if(isAttack)
        {
            // TODO : 공격 사운드 재생
            if (aSource.isPlaying)
            {
                aSource.Stop();
            }
            aSource.PlayOneShot(shootSound);

            for (int i = 0; i < numberOfProjectilesPerShot; i++)
            {
                float angle = minAngle + projectilesAngleSpace * i;
                float randomSpread = Random.Range(-spreadAngle, spreadAngle);
                angle += randomSpread;
                CreateProjectile(angle);
            }
        }
    }


    // 발사체를 생성하는 메서드
    public void CreateProjectile(float angle)
    {
        string name = Enum.GetName(typeof(Define.Projectile), (int)projectileType);

        Projectile projectile = PoolManager.Instance.Pop<Projectile>(GameManager.Instance.Data.GetProjectile(name).gameObject, projectileRoot);

        if (projectile != null)
        {
            projectile.transform.position = transform.position;
            projectile.Init(RotateVector2(aimDirection, angle), speed);
        }
    }

    private Vector2 RotateVector2(Vector2 v, float angle)
    {
        return Quaternion.Euler(0f, 0f, angle) * v;
    }
}