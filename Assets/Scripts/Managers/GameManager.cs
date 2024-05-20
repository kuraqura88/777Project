using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

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

    private DataManager dataManager;

    //�÷��̾�, ����, ���� ����, ó�� �귿���

    public GameObject[] characters;                                // ���� ������ ĳ���͵��� ���� �迭

    public Text scoretext;                                         //���� �κ�
    private int score = 0;

    public Slider slTimer;
    public float speedoftime= 50f;                               //�ð� ���
    private bool stageclear=false;                                //�������� Ŭ����� �۵��ϴ� bool��
    private float timerDuration = 60f;                           // Ÿ�̸� �ð� ����
    private float timeRemaining;


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
            if(dataManager == null)
            {
                dataManager = new DataManager();
                dataManager.Init();
            }
            return dataManager;
        }
    }


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


    /*
    private void Start()
    {
        timeRemaining = timerDuration;
        UpdateTimerUI();
        SelectRandomCharacter(); // ������ �� ���� ĳ���� ����

    }
    private void Update()
    {
        // Ÿ�̸� ������Ʈ
        UpdateTimer();
    }
    */
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

    public void Score()
    {
        score += 10; // ������ 10���� ������ŵ�ϴ�.
        UpdateScoreUI(); // ���� UI�� ������Ʈ�մϴ�.
    }
    private void UpdateScoreUI()
    {
        scoretext.text = "Score: " + score.ToString(); // ���� �ؽ�Ʈ�� ������Ʈ�մϴ�.
    }
    public void SelectRandomCharacter()
    {
        int randomIndex = Random.Range(0, characters.Length); // �迭���� ������ �ε��� ����
        GameObject selectedCharacter = characters[randomIndex]; // ���õ� ĳ����

        // ���õ� ĳ���͸� Ȱ��ȭ�ϰ�, ������ ĳ���ʹ� ��Ȱ��ȭ
        for (int i = 0; i < characters.Length; i++)
        {
            if (i == randomIndex)
            {
                characters[i].SetActive(true);
            }
            else
            {
                characters[i].SetActive(false);
            }
        }
    }
    //public void GameOver()
    //{
    //    //ĳ���Ͱ� �״� ����
    //    if ()
    //    {
    //        SceneManager.LoadScene("GameOverScenes");
    //    }
    //}
}
