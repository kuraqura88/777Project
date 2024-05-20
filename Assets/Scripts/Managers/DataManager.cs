using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

public class DataManager
{
    #region ========== Caching Data ==========

    // Projectiles Data
    private Dictionary<string, Projectile> projectileDict = new Dictionary<string, Projectile>();
    
    #endregion


    public void Init()
    {
        try
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
        catch(Exception ex)
        {
            Debug.Log(ex);
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

    public Projectile GetProjectile(string name)
    {
        if(projectileDict.TryGetValue(name, out Projectile projectile))
        {
            return projectile;
        }
        return null;
    }
}