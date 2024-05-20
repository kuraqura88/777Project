using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject enemyif;
    public GameObject enemyfor;
    public GameObject enemyswitch;
    public GameObject enemypublic;

    public Dictionary<Define.EnemyType, GameObject> enemyDictionary;
    public Dictionary<Define.EnemyType, GameObject> EnemyDictionary => enemyDictionary;
    private void Start()
    {
        enemyDictionary = new Dictionary<Define.EnemyType, GameObject>
        {
            { Define.EnemyType.If, enemyif },
            { Define.EnemyType.For, enemyfor },
            { Define.EnemyType.Switch, enemyswitch },
            { Define.EnemyType.Public, enemypublic }
        };

        //보스가 죽었거나 플레이어가 죽었거나 씬이 전환되는 경우에 빠져나가는 조건 적용 필수
        InvokeRepeating("MakeEnemy", 1f, 2f);      
    }
    private void MakeEnemy()
    {
        Define.EnemyType[] enemyTypes = (Define.EnemyType[])System.Enum.GetValues(typeof(Define.EnemyType));
        Define.EnemyType selectedType = enemyTypes[Random.Range(0, enemyTypes.Length)];

        if (enemyDictionary.TryGetValue(selectedType, out GameObject selectedEnemy))
        {
            Vector2 spawnPosition = GetSpawnPosition(selectedType);
            Instantiate(selectedEnemy, spawnPosition, Quaternion.identity);
        }
    }
    private Vector2 GetSpawnPosition(Define.EnemyType type)
    {
        switch (type)
        {
            case Define.EnemyType.Switch:
                int positionChoice = Random.Range(0, 2);
                if (positionChoice == 0)
                {
                    return new Vector2(7.5f, 6);
                }
                else
                {
                    return new Vector2(7.5f, -6);
                }
            case Define.EnemyType.Public:
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
                return new Vector2(10, RespawnY);
        }
    }     
}
