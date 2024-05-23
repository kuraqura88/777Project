using System;
using System.Net;
using UnityEngine;

using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class ProjectileController : MonoBehaviour
{
    public StatusHandler statusHandler;
    private AudioSource aSource = null;
    private AudioClip shootSound = null;

    [SerializeField]
    private RangedAttackSO attackSO;

    #region 기본 공격 속성

    [Header("공격 속성")]

    [SerializeField]
    private float delay = 0.2f;

    [SerializeField]
    private float speed = 10f;

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


    private float currentTime = 0;

    private bool isAttack = false;

    private Collider2D hit;

    public float radius = 10f;

    private bool isStart = false;

    private void Awake()
    {
        statusHandler = GetComponent<StatusHandler>();
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

        shootSound = attackSO.attackSound;
    }

    private void OnEnable()
    {
        EnemyRespawn.OnSpawn += EnemyCanAttack;
        GameManager.Instance.OnGameStart += CanAttack;
        GameManager.Instance.OnAppearBoss += StopAttack;
        GameManager.Instance.OnFightBoss += CanAttack;
        GameManager.Instance.OnGameOver += StopAttack;
        GameManager.Instance.OnGameClear += StopAttack;
        statusHandler.OnHit += ControlAttack;
    }
    private void OnDisable()
    {
        EnemyRespawn.OnSpawn -= EnemyCanAttack;

        GameManager.Instance.OnGameStart -= CanAttack;
        GameManager.Instance.OnAppearBoss -= StopAttack;
        GameManager.Instance.OnFightBoss -= CanAttack;
        GameManager.Instance.OnGameClear -= StopAttack;
        statusHandler.OnHit -= ControlAttack;

    }

    private void Update()
    {
        if(isStart)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= attackSO.delay)
            {
                currentTime = 0;
                Shoot();
            }
        }
    }

    private void StopAttack() => isStart = false;
    private void ControlAttack(bool active)
    {
        if (statusHandler.type == Define.EntityType.Player)
        {
            isStart = false;
            Invoke(nameof(CanAttack), 2.5f);
        }

    }

    private void EnemyCanAttack()
    {
        isStart = true;
    }

    private void CanAttack() => isStart = true;

    private void CanAttack(Define.Scene scene)
    {
        if (scene != Define.Scene.Start || scene != Define.Scene.ClearStage || scene != Define.Scene.GameoverStage)
        {
            isStart = true;
        }
    }

    public void SetNumberOfProjectilesPerShot(int perShot)
    {
        numberOfProjectilesPerShot += perShot;

        if(perShot <= 0)
        {
            return;
        }
        if (numberOfProjectilesPerShot <= 7 && numberOfProjectilesPerShot > 0)
        {
            projectilesAngleSpace = 180 / (float)(numberOfProjectilesPerShot);
        }
        else if(numberOfProjectilesPerShot >= 8)
        {
            projectilesAngleSpace = 360 / (float)numberOfProjectilesPerShot;
        }
    }

    public void SetSpeed(float speed)
    {
        if(speed > 0)
            this.speed += speed;
    }
    private void Shoot()
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
                hit = Physics2D.OverlapCircle(transform.position, radius, attackSO.target);
                
                if (hit == null) 
                    isAttack = false;
                else
                {
                    if(projectileRoot == null)
                    {
                        aimDirection = (hit.transform.position - transform.position).normalized;

                    }
                    else
                    {
                        aimDirection = (hit.transform.position - projectileRoot.position).normalized;

                    }

                }

                break;
        }

        if(isAttack)
        {
            // TODO : 공격 사운드 재생
            if (aSource.isPlaying)
            {
                aSource.Stop();
            }
            aSource.PlayOneShot(shootSound, 0.5f);

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
        if(projectileRoot == null)
        {
            Projectile projectile = PoolManager.Instance.Pop<Projectile>(GameManager.Instance.Data.GetProjectile(name).gameObject);

            if (projectile != null)
            {
                projectile.transform.position = transform.position;
                projectile.Init(RotateVector2(aimDirection, angle), speed, targetMask);
            }
        }
        else
        {
            Projectile projectile = PoolManager.Instance.Pop<Projectile>(GameManager.Instance.Data.GetProjectile(name).gameObject, projectileRoot);

            if (projectile != null)
            {
                projectile.transform.position = projectileRoot.position;
                projectile.Init(RotateVector2(aimDirection, angle), speed, targetMask);
            }
        }


    }

    private Vector2 RotateVector2(Vector2 v, float angle)
    {
        return Quaternion.Euler(0f, 0f, angle) * v;
    }
}