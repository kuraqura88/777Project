using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public EnemyPattern[] enemyprefabs;
    //public Dictionary<Define.EnemyType, GameObject> enemyDictionary;
    //public Dictionary<Define.EnemyName, GameObject> nameDictionary;
    private void Start()
    {
        enemies = new GameObject[] { enemyif, enemyfor, enemyswitch, enemypublic };
        //보스가 죽었거나 플레이어가 죽었거나 씬이 전환되는 경우에 빠져나가는 조건 적용 필수
        InvokeRepeating("MakeEnemy", 1f, 2f);      
    }
    private void MakeEnemy()
    {
        int rand = Random.Range(0, enemyprefabs.Length);
        EnemyPattern newone = Instantiate(enemyprefabs[rand]);
        newone.transform.position = GetSpawnPosition(newone);
        //Define.EnemyName[] enemyTypes = (Define.EnemyName[])System.Enum.GetValues(typeof(Define.EnemyName));
        //Define.EnemyName selectedType = enemyTypes[rand];
        //if (nameDictionary.TryGetValue(selectedType, out GameObject selectedEnemy))
        //{
        //Instantiate(selectedEnemy, spawnPosition, Quaternion.identity);
        //newone.name = (Define.EnemyName)rand;
        //}
    }
    private Vector2 GetSpawnPosition(EnemyPattern enemy)
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
                return new Vector2(10, RespawnY);
        }
    }     
}
