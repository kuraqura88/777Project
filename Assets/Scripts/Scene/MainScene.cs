using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{

    public GameObject basicBoss;
    public GameObject standardBoss;
    public GameObject challangeBoss;

    private void Start()
    {
        GameManager.Instance.Player.Enter();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnAppearBoss += AppearBoss;
    }

    public void AppearBoss()
    {
        if(GameManager.Instance.scene == Define.Scene.BasicBossStage)
        {
            GameManager.Instance.SoundManager.PlayBGM(GameManager.Instance.scene);
        }
        else if(GameManager.Instance.scene == Define.Scene.StandardBossStage)
        {
            GameManager.Instance.SoundManager.PlayBGM(GameManager.Instance.scene);
        }
        else if(GameManager.Instance.scene == Define.Scene.ChallangeBossStage)
        {
            GameManager.Instance.SoundManager.PlayBGM(GameManager.Instance.scene);
        }
    }
}
