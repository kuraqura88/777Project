using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoulRat : MonoBehaviour
{
    private GameManager gameManager; // GameManager ���� �߰�

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>(); // GameManager ���� �ʱ�ȭ
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }

    private CharacterType GetRandomCharacterType()
    {
        return (CharacterType)Random.Range(0, (int)CharacterType.Max);
    }

    // SelectRandomCharacter �޼����� ��ȯ���� CharacterStats�� ����
    public CharacterStats SelectRandomCharacter()
    {
        // �������� ĳ���� Ÿ�� ����
        CharacterType randomType = GetRandomCharacterType();
        Debug.Log("Selected Character Type: " + randomType);

        // ���õ� ĳ���� Ÿ���� ������ PlayerDataManager���� ������
        CharacterStats baseStats = gameManager.GetCharacterStats(randomType);

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
        if (gameManager == null)
        {
            Debug.LogError("GameManager reference is null.");
            return;
        }

        GameObject characterPrefab = gameManager.characterPrefab; // ���� �Ŵ������� ĳ���� ������ ��������

        if (characterPrefab == null)
        {
            Debug.LogError("Character prefab is not assigned in GameManager.");
            return;
        }

        if (gameManager.activeCharacter != null)
        {
            Destroy(gameManager.activeCharacter);
        }

        CharacterStats randomCharacterStats = SelectRandomCharacter();
        if (randomCharacterStats != null)
        {
            gameManager.activeCharacter = Instantiate(characterPrefab);
            CharacterStats activeCharacterStats = gameManager.activeCharacter.GetComponent<CharacterStats>();
            if (activeCharacterStats == null)
            {
                Debug.LogError("Character prefab does not have a CharacterStats component.");
                return;
            }
            activeCharacterStats.Life = randomCharacterStats.Life;
            activeCharacterStats.Damage = randomCharacterStats.Damage;
            activeCharacterStats.Speed = randomCharacterStats.Speed;
            activeCharacterStats.SetTypeStats(randomCharacterStats.characterType);

            Debug.Log("Generated Character Stats - Life: " + activeCharacterStats.Life + ", Damage: " + activeCharacterStats.Damage + ", Speed: " + activeCharacterStats.Speed);
        }
        else
        {
            Debug.LogError("Failed to generate character with random stats.");
        }
    }
}
