using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager
{
    #region ========== Caching Data ==========

    // Projectiles Data
    private Dictionary<string, Projectile> projectileDict = new Dictionary<string, Projectile>();

    private Dictionary<Define.EnemyType, EnemyController[]> enemyDict = new Dictionary<Define.EnemyType, EnemyController[]>();
    
    #endregion

    public void Init()
    {
        try
        {
            CreateProjectileData();

            CreateEnemyData();
        }
        catch(Exception ex)
        {
            Debug.Log(ex);
        }
    }

    private void CreateEnemyData()
    {
        EnemyController[] enemies = LoadAll<EnemyController>("", Define.Prefabs.Enemy);
        foreach (Define.EnemyType type in Enum.GetValues(typeof(Define.EnemyType)))
        {
            EnemyController[] controller = enemies.Where(x => x.enemyType == type).ToArray();

            enemyDict.Add(type, controller);
        }
    }

    private void CreateProjectileData()
    {
        Projectile[] projectiles = LoadAll<Projectile>("", Define.Prefabs.Projectiles);

        if (projectiles != null)
        {
            foreach (var projectile in projectiles)
            {
                projectileDict.Add(projectile.name, projectile);
                PoolManager.Instance.CreatePool<Projectile>(projectile.gameObject, 300);
            }
        }
    }


    /*
     * Resources의 Prefabs에 있는 데이터를 가져오는 기능
     */
    public T Load<T>(string name, Define.Prefabs prefabType = Define.Prefabs.Projectiles) where T : UnityEngine.Object
    {
        string path = "Prefabs/";

        if (prefabType == Define.Prefabs.None)
            path = "";
        else
            path += $"{Enum.GetName(typeof(Define.Prefabs), (int)prefabType)}/{name}";


        return Resources.Load<T>(path);
    }

    /*
     * Resources의 Prefabs에 있는 모든 데이터를 가져오는 기능
     */
    public T[] LoadAll<T>(string name = "", Define.Prefabs prefabType = Define.Prefabs.Projectiles) where T : UnityEngine.Object
    {
        string path = "Prefabs/";

        if (prefabType == Define.Prefabs.None)
            path = "";
        else
            path += $"{Enum.GetName(typeof(Define.Prefabs), (int)prefabType)}/{name}";

        return Resources.LoadAll<T>(path);
    }

    #region ========== Get Data Method ===========
    public Projectile GetProjectile(string name)
    {
        if(projectileDict.TryGetValue(name, out Projectile projectile))
        {
            return projectile;
        }
        return null;
    }

    public EnemyController[] GetEnemyData(Define.EnemyType enemyType)
    {
        if(enemyDict.TryGetValue(enemyType, out var enemies))
        {
            return enemies;
        }
        return null;
    }
    #endregion

}