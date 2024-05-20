using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoulRat : MonoBehaviour
{
    private GameManager gameManager; // GameManager 참조 추가

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>(); // GameManager 참조 초기화
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }
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
        CharacterStats baseStats = gameManager.GetCharacterStats(randomType);

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
        if (gameManager == null)
        {
            Debug.LogError("GameManager reference is null.");
            return;
        }

        GameObject characterPrefab = gameManager.characterPrefab; // 게임 매니저에서 캐릭터 프리팹 가져오기

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
