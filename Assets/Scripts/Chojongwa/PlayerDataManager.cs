using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    private Dictionary<CharacterType, CharacterStats> characterStatsDictionary;

    private void Awake()
    {
        characterStatsDictionary = new Dictionary<CharacterType, CharacterStats>();
    }

    // ĳ���� Ÿ�Կ� ���� ������ �߰��ϴ� �޼ҵ�
    public void AddCharacterStats(CharacterType characterType, CharacterStats stats)
    {
        characterStatsDictionary[characterType] = stats;
    }

    // Ư�� ĳ���� Ÿ���� ������ ��ȯ�ϴ� �޼ҵ�
    public CharacterStats GetCharacterStats(CharacterType characterType)
    {
        if (characterStatsDictionary.TryGetValue(characterType, out var stats))
        {
            // ĳ���� Ÿ�Կ� ���� ���� �����Ͽ� ��ȯ
            return new CharacterStats(stats.characterType, stats.Life, stats.Damage, stats.Speed);
        }
        else
        {
            Debug.LogError("Character type not found");
            return null;
        }
    }
}
