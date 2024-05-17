using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // �̱��� ������ ���� static �ν��Ͻ� ����
    private static GameManager instance;

    // �̱��� �ν��Ͻ� ������ ���� ������Ƽ
    public static GameManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���ٸ� ���� ����
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
    public float speedoftime= 50f;                               //�ð� ���
    private bool stageclear=false;                                //�������� Ŭ����� �۵��ϴ� bool��
    private float timerDuration = 60f;                           // Ÿ�̸� �ð� ����
    private float timeRemaining;

    private void Awake()
    {
        // �̱��� �ν��Ͻ� �ʱ�ȭ
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
        // Ÿ�̸� ������Ʈ
        UpdateTimer();
    }
    private void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining += Time.deltaTime * speedoftime;
            UpdateTimerUI();
            if(timeRemaining == 100)                    //�������� Ŭ���� �κ�
            {
                stageclear=true;
            }
        }
        else
        {
            timeRemaining = 0;
            UpdateTimerUI();
            // Ÿ�̸� ���� �� ó���� ���� �߰�
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

    // ���� ���� ���� �߰�

}
