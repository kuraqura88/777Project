using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    // 싱글톤 인스턴스 접근을 위한 프로퍼티
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없다면 새로 생성
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

    private EffectManager effectManager;
    private SoundManager soundManager;
    private PlayerDataManager playerDataManager; // PlayerDataManager 추가

    //플레이어, 점수, 게임 오버, 처음 룰렛요소

    public GameObject characterPrefab; // 캐릭터 프리팹
    private GameObject activeCharacter; // 활성화된 캐릭터

    public Text scoretext;                                         //점수 부분
    private int score = 0;

    public Slider slTimer;
    public float speedoftime= 50f;                               //시간 배속
    private bool stageclear=false;                                //스테이지 클리어시 작동하는 bool값
    private float timerDuration = 60f;                           // 타이머 시간 설정
    private float timeRemaining;

    private void Awake()
    {
        // 싱글톤 인스턴스 초기화
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
        SelectRandomCharacter(); 

    }
    private void Update()
    {
        // 타이머 업데이트
        UpdateTimer();
    }
    private void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining += Time.deltaTime * speedoftime;
            UpdateTimerUI();
            if(timeRemaining >= 100)                    //스테이지 클리어 부분
            {
                SceneManager.LoadScene("GameOverScenes");
            }
        }
        else
        {
            timeRemaining = 0;
            UpdateTimerUI();
            // 타이머 종료 시 처리할 로직 추가
            Debug.Log("Time is Zero.");
        }
    }
    private void UpdateTimerUI()
    {
        slTimer.value = timeRemaining / timerDuration;
    }

    public void Score()
    {
        score += 10; // 점수를 10점씩 증가시킵니다.
        UpdateScoreUI(); // 점수 UI를 업데이트합니다.
    }
    private void UpdateScoreUI()
    {
        scoretext.text = "Score: " + score.ToString(); // 점수 텍스트를 업데이트합니다.
    }
    // 랜덤으로 캐릭터 타입 선택하는 로직 수정
    private CharacterType SetCharacterType()
    {
        return (CharacterType)Random.Range(0, (int)CharacterType.Max);
    }

    public void SelectRandomCharacter()
    {
        if (activeCharacter != null)
        {
            Destroy(activeCharacter);
        }

        activeCharacter = Instantiate(characterPrefab);

        // 랜덤으로 캐릭터 타입 선택
        CharacterType randomType = SetCharacterType();
        Debug.Log(randomType);
        CharacterStats baseStats = playerDataManager.GetCharacterStats(randomType);

        // 활성화된 캐릭터에 스탯 복사
        CharacterStats activeCharacterStats = activeCharacter.GetComponent<CharacterStats>();
        activeCharacterStats.Life = baseStats.Life;
        activeCharacterStats.Damage = baseStats.Damage;
        activeCharacterStats.Speed = baseStats.Speed;
        activeCharacterStats.SetTypeStats(randomType); // 타입 설정

        Debug.Log(activeCharacterStats.Life);
        Debug.Log(activeCharacterStats.Damage);
        Debug.Log( activeCharacterStats.Speed);

    }
}
    //public void GameOver()
    //{
    //    //캐릭터가 죽는 조건
    //    if ()
    //    {
    //        SceneManager.LoadScene("GameOverScenes");
    //    }
    //}

