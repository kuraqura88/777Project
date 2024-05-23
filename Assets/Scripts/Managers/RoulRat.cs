using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class RoulRat : MonoBehaviour
{
    public Animator animator;
    public AudioSource aSource;

    private readonly int hashSpeed = Animator.StringToHash("Speed");
    private readonly int hashSelector = Animator.StringToHash("Selector");

    public Define.CharacterType characterType;

    private bool isFinished = false;

    private float maxSpeed = 0;

    private void OnEnable()
    {
        GameManager.Instance.OnRoulRat += GenerateCharacterWithRandomStats;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnRoulRat -= GenerateCharacterWithRandomStats;
    }

    private void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Shuffle"))
        {
            if(!isFinished)
            {
                maxSpeed = Mathf.MoveTowards(maxSpeed, 1f, Time.deltaTime);
                animator.SetFloat(hashSpeed, maxSpeed);
            }
        }
    }

    private Define.CharacterType GetRandomCharacterType()
    {
        int count = Random.Range(0, 100);
        // 30 30 40
        if(count < 30)
        {
            return Define.CharacterType.Normal;
        }
        else if(count >= 30 && count < 60)
        {
            return Define.CharacterType.Rare;

        }
        else if(count >= 60 && count < 100)
        {
            return Define.CharacterType.Unique;

        }
        return Define.CharacterType.Normal;
    }

    // SelectRandomCharacter 메서드의 반환형을 CharacterStats로 변경
    public CharacterStats SelectRandomCharacter()
    {
        // 랜덤으로 캐릭터 타입 선택
        characterType = GetRandomCharacterType();
        Debug.Log("Selected Character Type: " + characterType);

        // 선택된 캐릭터 타입의 스탯을 PlayerDataManager에서 가져옴
        CharacterStats baseStats = new CharacterStats();
        baseStats.SetTypeStats(characterType);

        if (baseStats != null)
        {
            return baseStats;
        }
        else
        {
            Debug.LogError("Character stats not found for type: " + characterType);
            return null;
        }
    }

    // 랜덤 스탯으로 캐릭터를 생성하는 메서드
    public void GenerateCharacterWithRandomStats(PlayerController controller)
    {
        // 스텟 설정
        controller.statusHandler.SetCharacterStat(SelectRandomCharacter());
       
        controller.ChangeCharacter(GameManager.Instance.Data.GetPlayerData(characterType));

        StartCoroutine(SpawnPlayer(controller));
    }

    public IEnumerator SpawnPlayer(PlayerController player)
    {
        Debug.Log(characterType + "이 캐릭터가 정해진 능력");
        while (!isFinished)
        {
            // 룰렛 애니메이션 설정
            animator.SetInteger(hashSelector, (int)characterType);

            yield return new WaitUntil(() => isFinished);

            if(GameManager.Instance.stage == Define.Scene.BasicStage)
            {
                GameManager.Instance.GameStart(Define.Scene.BasicStage);

            }
            else if(GameManager.Instance.stage == Define.Scene.StandardStage)
            {
                GameManager.Instance.GameStart(Define.Scene.StandardStage);

            }
            else if (GameManager.Instance.stage == Define.Scene.ChallangeStage)
            {
                GameManager.Instance.GameStart(Define.Scene.ChallangeStage);
            }

            gameObject.SetActive(false);
        }

        yield break;
    }

    public void Finish() => isFinished = true;

    public void PlayRoulRatSound(AudioClip clip)
    {
        if(aSource.isPlaying)
        {
            aSource.Stop();
        }
        aSource.PlayOneShot(clip, 0.5f);
    }
}
