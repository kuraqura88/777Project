using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    private Dictionary<CharacterType, CharacterStats> characterStatsDictionary;

    private void Awake()
    {
        characterStatsDictionary = new Dictionary<CharacterType, CharacterStats>
        {
            { CharacterType.Normal, new CharacterStats(CharacterType.Normal, 1, 1, 1) },
            { CharacterType.Rare, new CharacterStats(CharacterType.Rare, 2, 2, 2) },
            { CharacterType.Unique, new CharacterStats(CharacterType.Unique, 3, 3, 3) },
            { CharacterType.Epic, new CharacterStats(CharacterType.Epic, 4, 4, 4) }
        };
    }

    public CharacterStats GetCharacterStats(CharacterType characterType)
    {
        if (characterStatsDictionary.TryGetValue(characterType, out var stats))
        {
            // 스탯 복사하여 반환
            return new CharacterStats(stats.characterType, stats.Life, stats.Damage, stats.Speed);
        }
        else
        {
            Debug.LogError("Character type not found");
            return null;
        }
    }
}
