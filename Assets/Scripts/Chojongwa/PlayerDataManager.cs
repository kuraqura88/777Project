using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    private Dictionary<CharacterType, CharacterStats> characterStatsDictionary;

    private void Awake()
    {
        // Awake �޼��忡�� ��ųʸ��� �ʱ�ȭ�մϴ�.
        characterStatsDictionary = new Dictionary<CharacterType, CharacterStats>();

        // ĳ���� Ÿ�Ժ��� ���� �ʱ�ȭ
        characterStatsDictionary[CharacterType.Normal] = new CharacterStats(CharacterType.Normal, 1, 1, 1);
        characterStatsDictionary[CharacterType.Rare] = new CharacterStats(CharacterType.Rare, 2, 2, 2);
        characterStatsDictionary[CharacterType.Unique] = new CharacterStats(CharacterType.Unique, 3, 3, 3);
        characterStatsDictionary[CharacterType.Epic] = new CharacterStats(CharacterType.Epic, 4, 4, 4);
    }

    public CharacterStats GetCharacterStats(CharacterType characterType)
    {
        if (characterStatsDictionary.TryGetValue(characterType, out var characterStats))
        {
            // ĳ���� Ÿ�Կ� ���� ���� �����Ͽ� ��ȯ
            return new CharacterStats(characterStats.characterType, characterStats.Life, characterStats.Damage, characterStats.Speed);
        }
        else
        {
            Debug.LogError("Character type not found");
            return null;
        }
    }
}
