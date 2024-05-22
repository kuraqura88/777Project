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

    // SelectRandomCharacter �޼����� ��ȯ���� CharacterStats�� ����
    public CharacterStats SelectRandomCharacter()
    {
        // �������� ĳ���� Ÿ�� ����
        characterType = GetRandomCharacterType();
        Debug.Log("Selected Character Type: " + characterType);

        // ���õ� ĳ���� Ÿ���� ������ PlayerDataManager���� ������
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

    // ���� �������� ĳ���͸� �����ϴ� �޼���
    public void GenerateCharacterWithRandomStats(PlayerController controller)
    {
        // ���� ����
        controller.statusHandler.SetCharacterStat(SelectRandomCharacter());
       
        controller.ChangeCharacter(GameManager.Instance.Data.GetPlayerData(characterType));

        StartCoroutine(SpawnPlayer(controller));
    }

    public IEnumerator SpawnPlayer(PlayerController player)
    {
        Debug.Log(characterType + "�� ĳ���Ͱ� ������ �ɷ�");
        while (!isFinished)
        {
            // �귿 �ִϸ��̼� ����
            animator.SetInteger(hashSelector, (int)characterType);

            yield return new WaitUntil(() => isFinished);

            GameManager.Instance.GameStart(Define.Scene.BasicStage);

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
