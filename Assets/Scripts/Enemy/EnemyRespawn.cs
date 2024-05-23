using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyRespawn : MonoBehaviour
{
    public EnemyController[] enemyprefabs;

    public GameObject[] boss;

    public static event Action OnSpawn;

    private static List<EnemyController> spawnedEnemy = new List<EnemyController>();

    private bool canSpawn = false;

    public float delay = 2.5f;
    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += CreateEnemy;
        GameManager.Instance.OnGameClear += StopSpawn;
        GameManager.Instance.OnGameOver += StopSpawn;
        GameManager.Instance.OnAppearBoss += StopSpawn;
        GameManager.Instance.OnAppearBoss += BossSpawn;
        GameManager.Instance.OnFightBoss += OnFightSpawn;
    }

    private void OnFightSpawn()
    {
        delay = 20.0f;
        CreateEnemy();
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= CreateEnemy;
        GameManager.Instance.OnGameClear -= StopSpawn;
        GameManager.Instance.OnGameOver -= StopSpawn;
        GameManager.Instance.OnAppearBoss -= StopSpawn;
        GameManager.Instance.OnAppearBoss -= BossSpawn;
        GameManager.Instance.OnFightBoss -= OnFightSpawn;

    }

    public static void Spawn()
    {
        OnSpawn?.Invoke();
    }

    public static void Clear(EnemyController enemy)
    {
        spawnedEnemy.Remove(enemy);
    }

    private void StopSpawn()
    {
        // 스폰 중단!
        canSpawn = false;

        // 모든 생성된 적 파괴
        ClearAllEnemy();
    }

    // 보스 스폰
    private void CreateEnemy()
    {
        delay = 2.5f;
        canSpawn = true;
        StartCoroutine(MakeEnemy(delay));
    }
    // 일반 스폰
    private void CreateEnemy(Define.Scene scene)
    {
        canSpawn = true;
        StartCoroutine(MakeEnemy(delay));
    }

    private void ClearAllEnemy()
    {
        foreach (var enemy in spawnedEnemy)
        {
            // TODO : 뭐 더 없앨거 있으면 추가 삭제
            Destroy(enemy.gameObject);
        }
        spawnedEnemy.Clear();
    }

    private IEnumerator MakeEnemy(float delay)
    {
        while (canSpawn)
        {
            if(spawnedEnemy.Count < 5)
            {
                int rand = Random.Range(0, enemyprefabs.Length);
                EnemyController newone = Instantiate(enemyprefabs[rand]);
                newone.transform.position = GetSpawnPosition(newone);
                newone.CanMove();
                spawnedEnemy.Add(newone);
                Spawn();
                yield return new WaitForSeconds(delay);
            }

            yield return new WaitUntil(() => spawnedEnemy.Count < 5);

        }

        yield break;
    }

    private void BossSpawn()
    {
        switch(GameManager.Instance.scene)
        {
            case Define.Scene.BasicBossStage:
                GameObject basic = Instantiate(boss[0]);
                Boss bsBoss = basic.GetComponent<Boss>();
                bsBoss.Appear();
                break;
            case Define.Scene.StandardBossStage:
                GameObject standard = Instantiate(boss[1]);
                Boss sdBoss = standard.GetComponent<Boss>();
                sdBoss.Appear();
                break;
            case Define.Scene.ChallangeBossStage:
                GameObject challange = Instantiate(boss[2]);
                Boss challBoss = challange.GetComponent<Boss>();
                challBoss.Appear();
                break;
        }
    }

    private Vector2 GetSpawnPosition(EnemyController enemy)
    {
        switch ((int)enemy.type)
        {

            case (int)Define.EnemyName.Switch:
                int positionChoice = Random.Range(0, 2);
                if (positionChoice == 0)
                {
                    return new Vector2(7.5f, 6);
                }
                else
                {
                    return new Vector2(7.5f, -6);
                }
            case (int)Define.EnemyName.Public:
                positionChoice = Random.Range(0, 2);
                if (positionChoice == 0)
                {
                    return new Vector2(-6, 7.5f);
                }
                else
                {
                    return new Vector2(-6, -7.5f);
                }

            default:
                float RespawnY = Random.Range(4, -4);
                return new Vector2(7.5f, RespawnY);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStart -= CreateEnemy;
        GameManager.Instance.OnGameClear -= StopSpawn;
        GameManager.Instance.OnGameOver -= StopSpawn;
        GameManager.Instance.OnAppearBoss -= StopSpawn;
        GameManager.Instance.OnAppearBoss -= BossSpawn;
        GameManager.Instance.OnFightBoss -= OnFightSpawn;
    }
}
