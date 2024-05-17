using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileController : MonoBehaviour
{
    string localPath = "Prefabs/";

    [SerializeField]
    private RangedAttackSO attackSO;

    [SerializeField]
    private Transform projectileRoot;

    // 캐싱 작업
    private Dictionary<string, Projectile> projectileDict = new Dictionary<string, Projectile>();

    private Vector2 aimDirection = Vector2.right;

    public float t = 1;

    private float currentTime = 0;

    public bool isAttack = false;

    private Collider2D target;

    public float radius = 10f;
    private void Awake()
    {
        Init();
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 start = transform.position;
        int segmentCount = 64;
        for (int i = 0; i <= segmentCount; i++)
        {
            float angle = i * Mathf.PI * 2 / segmentCount;
            Vector3 point = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0) + start;
            
            Vector3 nextPoint = new Vector3(Mathf.Cos((i + 1) * Mathf.PI * 2 / segmentCount) * radius, Mathf.Sin((i + 1) * Mathf.PI * 2 / segmentCount) * radius, 0) + start;

            Gizmos.DrawLine(point, nextPoint);
        }
    }

    private void Init()
    {
        Projectile[] projectiles = Resources.LoadAll<Projectile>(localPath);

        if (projectiles != null)
        {
            foreach(var projectile in projectiles)
            {
                projectileDict.Add(projectile.name, projectile);
                PoolManager.Instance.CreatePool(projectile.gameObject);
            }
        }
    }
    
    private void Attack()
    {
        Shoot();
    }

    public void Shoot()
    {
        float minAngle = -(attackSO.numberOfProjectilesPerShot / 2f) * attackSO.projectilesAngleSpace + 0.5f * attackSO.projectilesAngleSpace;

        isAttack = true;
        switch (attackSO.direction)
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
            for (int i = 0; i < attackSO.numberOfProjectilesPerShot; i++)
            {
                float angle = minAngle + attackSO.projectilesAngleSpace * i;
                float randomSpread = Random.Range(-attackSO.spreadAngle, attackSO.spreadAngle);
                angle += randomSpread;
                CreateProjectile(angle);
            }
        }

    }


    // 발사체를 생성하는 메서드
    public void CreateProjectile(float angle)
    {
        string name = Enum.GetName(typeof(Define.Projectile), (int)attackSO.projectile);

        if (projectileDict.TryGetValue(name, out Projectile bullet))
        {
            GameObject go = PoolManager.Instance.Pop(bullet.gameObject, projectileRoot);
            Projectile projectile = go.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
                projectile.Init(RotateVector2(aimDirection, angle));
            }
        }
    }


    private Vector2 RotateVector2(Vector2 v, float angle)
    {
        return Quaternion.Euler(0f, 0f, angle) * v;
    }


}