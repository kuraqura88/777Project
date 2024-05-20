using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
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

    public event Action onGameOver; // 게임 오버 이벤트

    public GameObject characterPrefab; // 캐릭터 프리팹
    public GameObject activeCharacter; // 활성화된 캐릭터

    private EffectManager effectManager;
    private SoundManager soundManager;
    private PlayerDataManager playerDataManager;

    public Text scoreText;
    private int score = 0;

    public Slider slTimer;
    public float speedOfTime = 50f;
    private float timerDuration = 60f;
    private float timeRemaining;

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
        playerDataManager = GetComponent<PlayerDataManager>() ?? gameObject.AddComponent<PlayerDataManager>();
    }

    public EffectManager EffectManager
    {
        get { return effectManager; }
    }

    public SoundManager SoundManager
    {
        get { return soundManager; }
    }

    private void Start()
    {
        timeRemaining = timerDuration;
        UpdateTimerUI();
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining += Time.deltaTime * speedOfTime;
            UpdateTimerUI();
            if (timeRemaining ==100)
            {
                GameOver(); // 타이머가 0에 도달하면 게임 오버 처리
            }
        }
    }

    private void UpdateTimerUI()
    {
        slTimer.value = 1 - (timeRemaining / timerDuration); // 슬라이더 값을 역으로 설정하여 남은 시간 표시
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

    public void GameOver()
    {
        onGameOver?.Invoke(); // 게임 오버 이벤트 호출
        SceneManager.LoadScene("GameOverScenes");
    }

    // PlayerDataManager에서 캐릭터 스탯을 가져오는 메서드
    public CharacterStats GetCharacterStats(CharacterType characterType)
    {
        if (playerDataManager != null)
        {
            Debug.Log(GetCharacterStats(characterType));
            return playerDataManager.GetCharacterStats(characterType);
        }
        else
        {
            Debug.LogError("PlayerDataManager reference is null.");
            return null;
        }
    }
}
