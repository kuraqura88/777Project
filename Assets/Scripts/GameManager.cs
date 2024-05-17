using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 싱글톤 패턴을 위한 static 인스턴스 변수
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

    public GameObject player;
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
    }
    private void Start()
    {
        timeRemaining = timerDuration;
        UpdateTimerUI();
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
            if(timeRemaining == 100)                    //스테이지 클리어 부분
            {
                stageclear=true;
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

    public EffectManager EffectManager
    {
        get { return effectManager; }
    }

    public SoundManager SoundManager
    {
        get { return soundManager; }
    }

    // 게임 관련 로직 추가

}
