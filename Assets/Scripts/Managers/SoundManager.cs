using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region BGM

    private AudioSource bgm;

    public AudioClip startBgm;
    public AudioClip basicBgm;
    public AudioClip standardBgm;
    public AudioClip challangeBgm;
    public AudioClip clearBgm;
    public AudioClip gameoverBgm;

    public AudioClip basicBossBgm;
    public AudioClip standardBossBgm;
    public AudioClip challangeBossBgm;

    #endregion


    #region Effect

    private AudioSource effect;

    public AudioClip clearEffect;

    public AudioClip failedEffect;

    public AudioClip enemyHitEffect;

    public AudioClip playerHitEffect;

    public AudioClip deadEffect;


    #endregion
    private void Awake()
    {
        bgm = transform.AddComponent<AudioSource>();
        bgm.playOnAwake = false;
        effect = transform.AddComponent<AudioSource>();
        effect.playOnAwake = false;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += PlayBGM;
    }

    public void PlayBGM(Define.Scene stage)
    {
        if(bgm != null)
        {
            bgm.loop = true;

            bgm.Stop();

            switch (stage)
            {
                case Define.Scene.Start:
                    bgm.clip = startBgm;
                    break;

                case Define.Scene.BasicStage:
                    bgm.clip = basicBgm;
                    break;
                case Define.Scene.StandardStage:
                    bgm.clip = standardBgm;
                    break;

                case Define.Scene.ChallangeStage:
                    bgm.clip = challangeBgm;
                    break;

                case Define.Scene.BasicBossStage:
                    bgm.clip = basicBossBgm;

                    break;

                case Define.Scene.StandardBossStage:
                    bgm.clip = standardBossBgm;

                    break;

                case Define.Scene.ChallangeBossStage:
                    bgm.clip = challangeBossBgm;

                    break;
                case Define.Scene.ClearStage:
                    bgm.clip = clearBgm;
                    break;

                case Define.Scene.GameoverStage:
                    bgm.clip = gameoverBgm;
                    break;
            }
            bgm.volume = 0.5f;
            bgm.Play();
        }

    }

    public void StopBGM()
    {
        bgm.Stop();
    }
}
