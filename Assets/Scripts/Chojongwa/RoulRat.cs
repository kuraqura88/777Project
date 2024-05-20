using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoulRat : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.onRoulRat += GenerateCharacterWithRandomStats;
    }

    private CharacterType GetRandomCharacterType()
    {
        return (CharacterType)Random.Range(0, (int)CharacterType.Max);
    }

    // SelectRandomCharacter 메서드의 반환형을 CharacterStats로 변경
    public CharacterStats SelectRandomCharacter()
    {
        // 랜덤으로 캐릭터 타입 선택
        CharacterType randomType = GetRandomCharacterType();
        Debug.Log("Selected Character Type: " + randomType);

        // 선택된 캐릭터 타입의 스탯을 PlayerDataManager에서 가져옴
        CharacterStats baseStats = new CharacterStats();
        baseStats.SetTypeStats(randomType);
        if (baseStats != null)
        {
            return baseStats; // baseStats 반환
        }
        else
        {
            Debug.LogError("Character stats not found for type: " + randomType);
            return null;
        }
    }

    // 랜덤 스탯으로 캐릭터를 생성하는 메서드
    public void GenerateCharacterWithRandomStats()
    {
        PlayerController player = Instantiate<PlayerController>(Resources.Load<PlayerController>("Prefabs/Player/player"));
        player.SetCharacterStats(SelectRandomCharacter());
        player.name = "Player";
        GameManager.Instance.Player = player;
    }
}
