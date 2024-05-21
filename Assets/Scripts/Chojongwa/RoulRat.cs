using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoulRat : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.onRoulRat += GenerateCharacterWithRandomStats;
    }

    private Define.CharacterType GetRandomCharacterType()
    {
        return (Define.CharacterType)Random.Range(0, (int)Define.CharacterType.MAX);
    }

    // SelectRandomCharacter �޼����� ��ȯ���� CharacterStats�� ����
    public CharacterStats SelectRandomCharacter()
    {
        // �������� ĳ���� Ÿ�� ����
        Define. CharacterType randomType = GetRandomCharacterType();
        Debug.Log("Selected Character Type: " + randomType);

        // ���õ� ĳ���� Ÿ���� ������ PlayerDataManager���� ������
        CharacterStats baseStats = new CharacterStats();
        baseStats.SetTypeStats(randomType);
        if (baseStats != null)
        {
            return baseStats; // baseStats ��ȯ
        }
        else
        {
            Debug.LogError("Character stats not found for type: " + randomType);
            return null;
        }
    }

    // ���� �������� ĳ���͸� �����ϴ� �޼���
    public void GenerateCharacterWithRandomStats()
    {
        PlayerController player = Instantiate<PlayerController>(Resources.Load<PlayerController>("Prefabs/Player/player"));
        player.SetCharacterStats(SelectRandomCharacter());
        player.name = "Player";
        GameManager.Instance.Player = player;
    }
}
