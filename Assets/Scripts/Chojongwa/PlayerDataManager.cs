using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    private Dictionary<Define.CharacterType, CharacterStats> characterStatsDictionary;

    private void Awake()
    {
        // Awake 메서드에서 딕셔너리를 초기화합니다.
        characterStatsDictionary = new Dictionary<Define.CharacterType, CharacterStats>();

        // 캐릭터 타입별로 스탯 초기화
        characterStatsDictionary[Define.CharacterType.Normal] = new CharacterStats(Define.CharacterType.Normal, 1, 1, 1);
        characterStatsDictionary[Define.CharacterType.Rare] = new CharacterStats(Define.CharacterType.Rare, 2, 2, 2);
        characterStatsDictionary[Define.CharacterType.Unique] = new CharacterStats(Define.CharacterType.Unique, 3, 3, 3);
        characterStatsDictionary[Define.CharacterType.Epic] = new CharacterStats(Define.CharacterType.Epic, 4, 4, 4);
    }

    public CharacterStats GetCharacterStats(Define.CharacterType characterType)
    {
        if (characterStatsDictionary.TryGetValue(characterType, out var characterStats))
        {
            // 캐릭터 타입에 대한 스탯 복사하여 반환
            return new CharacterStats(characterStats.characterType, characterStats.Life, characterStats.Power, characterStats.Speed);
        }
        else
        {
            Debug.LogError("Character type not found");
            return null;
        }
    }
}
