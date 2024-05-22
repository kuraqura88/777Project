using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class DataManager
{
    #region ========== Caching Data ==========

    private Dictionary<string, Projectile> projectileDict = new Dictionary<string, Projectile>();

    private Dictionary<Define.CharacterType, SpriteLibraryAsset> playerDict = new Dictionary<Define.CharacterType, SpriteLibraryAsset>();

    #endregion

    public void Init()
    {
        try
        {
            CreateProjectileData();

            CreatePlayerData();
        }
        catch(Exception ex)
        {
            Debug.Log(ex);
        }
    }

    private void CreatePlayerData()
    {
        SpriteLibraryAsset[] asset = LoadAll<SpriteLibraryAsset>("", Define.Prefabs.Player);

        foreach(Define.CharacterType type in Enum.GetValues(typeof(Define.CharacterType)))
        {
            for(int i = 0; i < asset.Length; i++)
            {
                if(Enum.GetName(typeof(Define.CharacterType), type).Equals(asset[i].name))
                {
                    playerDict.Add(type, asset[i]);
                    break;
                }
            }
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

    public SpriteLibraryAsset GetPlayerData(Define.CharacterType type)
    {
        if(playerDict.TryGetValue(type, out var playerData))
        {
            return playerData;
        }
        return null;
    }

    #endregion

}