using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Define.Scene scene = Define.Scene.BasicStage;


    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                    DontDestroyOnLoad(obj);
                }
                
            }
            return instance;
        }
    }

    #region ========== Option ==========

    private EffectManager effectManager;
    private SoundManager soundManager;
    private DataManager dataManager;

    private PlayerController _player;

    public EffectManager EffectManager
    {
        get { return effectManager; }
    }

    public SoundManager SoundManager
    {
        get { return soundManager; }
    }
    public DataManager Data
    {
        get
        {
            if (dataManager == null)
            {
                dataManager = new DataManager();
            }
            return dataManager;
        }
    }

    public PlayerController Player
    {
        get
        {
            if(scene == Define.Scene.BasicStage || scene == Define.Scene.StandardStage || scene == Define.Scene.ChallangeStage)
            {
                if (_player == null)
                {
                    Init();
                }
                return _player;

            }

            return null;

        }

    }

    private void Init()
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        if (player == null)
        {
            GameObject go = Data.Load<GameObject>("Player", Define.Prefabs.Player);

            GameObject obj = Instantiate(go, new Vector3(-7f, 0f, 0f), Quaternion.identity);

            player = obj.GetComponentInChildren<PlayerController>();

            if(go == null)
            {
                Debug.Log("�÷��̾��� �����Ͱ� �������� �ʽ��ϴ�");
            }
            else
            {
                RoulRat(player);
            }
        }
        _player = player;

    }
    #endregion


    #region ========== Event Action ==========

    public event Action<PlayerController> OnRoulRat;

    public event Action<Define.Scene> OnGameStart;

    public event Action OnAppearBoss;

    public event Action OnGameClear;

    public event Action OnGameOver;

    public void RoulRat(PlayerController controller)
    {
        OnRoulRat?.Invoke(controller);
    }

    public void GameStart(Define.Scene scene)
    {
        isStart = true;
        this.scene = scene;
        mainSceneUI.SetActive(true);

        OnGameStart?.Invoke(scene);
    }

    public void AppearBoss()
    {
        OnAppearBoss?.Invoke();
    }

    public void GameClear()
    {
        OnGameClear?.Invoke();
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    #endregion


    #region ========= Fields ==========
    private bool isStart = false;

    public GameObject mainSceneUI;

    public TMP_Text scoreText;
    private int score = 0;

    public Slider slTimer;
    public float speedOfTime = 50f;
    private float timerDuration = 60f;
    private float timeRemaining;

    #endregion


    #region ========== Life Cycle ==========

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        effectManager = GetComponent<EffectManager>() ?? gameObject.AddComponent<EffectManager>();
        soundManager = GetComponent<SoundManager>() ?? gameObject.AddComponent<SoundManager>();
        if(dataManager == null)
        {
            dataManager = new DataManager();
            dataManager.Init();
        }
    }

    private void Start()
    {
        timeRemaining = timerDuration;
    }

    private void Update()
    {
        if(isStart && (scene == Define.Scene.BasicStage || scene == Define.Scene.StandardStage || scene == Define.Scene.ChallangeStage))
        {
            UpdateTimer();
        }
    }

    #endregion


    #region ========== Method ==========

    private void UpdateTimer()
    {
        timeRemaining -= Time.deltaTime * speedOfTime;
        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        slTimer.value = 1 - (timeRemaining / timerDuration); // �����̴� ���� ������ �����Ͽ� ���� �ð� ǥ��

        if(slTimer.value >= 0.99f)
        {
            isStart = false;
            if (scene == Define.Scene.BasicStage)
            {
                scene = Define.Scene.BasicStage;
            }
            else if(scene == Define.Scene.StandardStage)
            {
                scene = Define.Scene.StandardBossStage;

            }
            else if(scene == Define.Scene.ChallangeStage)
            {
                scene = Define.Scene.ChallangeBossStage;

            }
            AppearBoss();
        }
    }

    public void Score()
    {
        score += 10;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    #endregion

}
