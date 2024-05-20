using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    private Dictionary<CharacterType, CharacterStats> characterStatsDictionary;

    private void Awake()
    {
        characterStatsDictionary = new Dictionary<CharacterType, CharacterStats>();
    }

    // 캐릭터 타입에 따른 스탯을 추가하는 메소드
    public void AddCharacterStats(CharacterType characterType, CharacterStats stats)
    {
        characterStatsDictionary[characterType] = stats;
    }

    // 특정 캐릭터 타입의 스탯을 반환하는 메소드
    public CharacterStats GetCharacterStats(CharacterType characterType)
    {
        if (characterStatsDictionary.TryGetValue(characterType, out var stats))
        {
            // 캐릭터 타입에 대한 스탯 복사하여 반환
            return new CharacterStats(stats.characterType, stats.Life, stats.Damage, stats.Speed);
        }
        else
        {
            Debug.LogError("Character type not found");
            return null;
        }
    }
}
