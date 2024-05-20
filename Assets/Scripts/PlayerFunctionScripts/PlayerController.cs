﻿using UnityEngine;
using static UnityEditor.Progress;

public class PlayerController : MonoBehaviour
{
    CharacterStats characterStats;
    void Start()
    {
        characterStats = GetComponent<CharacterStats>();
        if (characterStats != null)
        {
            characterStats.SetTypeStats(CharacterType.Rare);
        }
        else
        {
            Debug.LogError("캐릭터 타입이 없다.");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            // 아이템 타입을 얻어옴 (예: 아이템의 스크립트에서 itemType 필드를 가져오는 방식)
            itemType itemType = other.GetComponent<Item>().type;

            // 아이템 효과를 적용
            GetComponent<ItemManager>().ApplyItemEffect(itemType);

            // 아이템 제거
            Destroy(other.gameObject);
        }
    }
}