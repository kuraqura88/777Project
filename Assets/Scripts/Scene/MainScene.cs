using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    public Sprite[] sprites;

    public TMP_Text clearTitleTxt;
    public Image clearImage;

    public PlayableDirector director;

    public PlayableAsset[] scenes;

    public GameObject gameoverUI;
    public GameObject clearUI;

    public GameObject[] backgrounds;

    private void Start()
    {
        GameManager.Instance.Player.Enter();
    }

    private void OnEnable()
    {
        ChangeBackground();
        GameManager.Instance.OnAppearBoss += AppearBoss;
        GameManager.Instance.OnFightBoss += FightBoss;
        GameManager.Instance.OnGameOver += OnGameOver;
        GameManager.Instance.OnGameClear += GameClear;

        if(GameManager.Instance.stage == Define.Scene.BasicStage)
        {
            clearImage.sprite = sprites[0];
            clearTitleTxt.text = "베이직 강의를\n클리어했습니다.";
        }
        else if(GameManager.Instance.stage == Define.Scene.StandardStage)
        {
            clearImage.sprite = sprites[1];
            clearTitleTxt.text = "스탠더드 강의를\n클리어했습니다.";

        }
        else if(GameManager.Instance.stage == Define.Scene.ChallangeStage)
        {
            clearImage.sprite = sprites[2];
            clearTitleTxt.text = "챌린지 강의를\n클리어했습니다.";

        }
    }

    private void OnDisable()
    {
        GameManager.Instance.OnAppearBoss -= AppearBoss;
        GameManager.Instance.OnFightBoss -= FightBoss;
        GameManager.Instance.OnGameOver -= OnGameOver;
        GameManager.Instance.OnGameClear -= GameClear;
    }

    private void Update()
    {
        if(director.gameObject.activeSelf)
        {
            if(director.time >= 6.9f)
            {
                GameManager.Instance.FightBoss();
            }
        }
    }

    private void ChangeBackground()
    {
        if(GameManager.Instance.stage == Define.Scene.BasicStage)
        {
            backgrounds[0].SetActive(true);
            backgrounds[1].SetActive(false);
            backgrounds[2].SetActive(false);
        }
        else if (GameManager.Instance.stage == Define.Scene.StandardStage)
        {
            backgrounds[0].SetActive(false);
            backgrounds[1].SetActive(true);
            backgrounds[2].SetActive(false);
        }
        else if (GameManager.Instance.stage == Define.Scene.ChallangeStage)
        {
            backgrounds[0].SetActive(false);
            backgrounds[1].SetActive(false);
            backgrounds[2].SetActive(true);
        }
    }
    private void OnGameOver()
    {
        GameManager.Instance.SoundManager.PlayBGM(Define.Scene.GameoverStage);
        gameoverUI.SetActive(true);
    }

    private void GameClear()
    {
        GameManager.Instance.SoundManager.PlayBGM(Define.Scene.ClearStage);
        clearUI.SetActive(true);
    }

    public void AppearBoss()
    {
        GameManager.Instance.SoundManager.StopBGM();

        director.gameObject.SetActive(true);
        if (GameManager.Instance.scene == Define.Scene.BasicBossStage)
        {
            director.playableAsset = scenes[0];
            director.Play();
        }
        else if (GameManager.Instance.scene == Define.Scene.StandardBossStage)
        {
            director.playableAsset = scenes[1];
            director.Play();
        }
        else if (GameManager.Instance.scene == Define.Scene.ChallangeBossStage)
        {
            director.playableAsset = scenes[2];
            director.Play();
        }
    }

    private void FightBoss()
    {
        director.gameObject.SetActive(false);
        GameManager.Instance.SoundManager.StopBGM();

        if (GameManager.Instance.scene == Define.Scene.BasicBossStage)
        {
            GameManager.Instance.SoundManager.PlayBGM(Define.Scene.BasicBossStage);
        }
        else if (GameManager.Instance.scene == Define.Scene.StandardBossStage)
        {
            GameManager.Instance.SoundManager.PlayBGM(Define.Scene.StandardBossStage);
        }
        else if (GameManager.Instance.scene == Define.Scene.ChallangeBossStage)
        {
            GameManager.Instance.SoundManager.PlayBGM(Define.Scene.ChallangeBossStage);
        }
    }
}
