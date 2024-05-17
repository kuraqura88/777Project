using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject enemyif;
    public GameObject enemyfor;
    public GameObject enemyswitch;
    public GameObject enemypublic;
    private GameObject[] enemies;
    private void Start()
    {
        enemies = new GameObject[] { enemyif, enemyfor, enemyswitch, enemypublic };
        //보스가 죽었거나 플레이어가 죽었거나 씬이 전환되는 경우에 빠져나가는 조건 적용 필수
        InvokeRepeating("MakeEnemy", 1f, 2f);      
    }
    private void MakeEnemy()
    {
        int index = Random.Range(0, enemies.Length);
        GameObject selectedEnemy = enemies[index];

        Vector2 spawnPosition = GetSpawnPosition(selectedEnemy);

        Instantiate(selectedEnemy, spawnPosition, Quaternion.identity);
    }
    private Vector2 GetSpawnPosition(GameObject enemy)
    {
        if (enemy == enemyswitch)
        {
            int positionChoice = Random.Range(0, 2);
            if (positionChoice == 0)
            {
                return new Vector2(7.5f, 6);
            }
            else
            {
                return new Vector2(7.5f, -6);
            }
        }
        else if (enemy == enemypublic)
        {
            int positionChoice = Random.Range(0, 2);
            if (positionChoice == 0)
            {
                return new Vector2(-6, 7.5f);
            }
            else
            {
                return new Vector2(-6, -7.5f);
            }
        }
        else
        {
            float RespawnY = Random.Range(4, -4);
            return new Vector2(10, RespawnY);
        }
    }
}
