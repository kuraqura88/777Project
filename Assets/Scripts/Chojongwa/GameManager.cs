using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private PlayerDataManager playerDataManager; // PlayerDataManager �߰�

    //�÷��̾�, ����, ���� ����, ó�� �귿���

    public GameObject characterPrefab; // ĳ���� ������
    private GameObject activeCharacter; // Ȱ��ȭ�� ĳ����

    public Text scoretext;                                         //���� �κ�
    private int score = 0;

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
        // Ÿ�̸� ������Ʈ
        UpdateTimer();
    }
    private void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining += Time.deltaTime * speedoftime;
            UpdateTimerUI();
            if(timeRemaining >= 100)                    //�������� Ŭ���� �κ�
            {
                SceneManager.LoadScene("GameOverScenes");
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
    // �������� ĳ���� Ÿ�� �����ϴ� ���� ����
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

        // �������� ĳ���� Ÿ�� ����
        CharacterType randomType = SetCharacterType();
        Debug.Log(randomType);
        CharacterStats baseStats = playerDataManager.GetCharacterStats(randomType);

        // Ȱ��ȭ�� ĳ���Ϳ� ���� ����
        CharacterStats activeCharacterStats = activeCharacter.GetComponent<CharacterStats>();
        activeCharacterStats.Life = baseStats.Life;
        activeCharacterStats.Damage = baseStats.Damage;
        activeCharacterStats.Speed = baseStats.Speed;
        activeCharacterStats.SetTypeStats(randomType); // Ÿ�� ����

        Debug.Log(activeCharacterStats.Life);
        Debug.Log(activeCharacterStats.Damage);
        Debug.Log( activeCharacterStats.Speed);

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

