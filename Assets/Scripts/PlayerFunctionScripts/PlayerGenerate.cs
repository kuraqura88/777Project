using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerate : MonoBehaviour
{
    public GameObject Player;
    private GameObject playerInstance;
    private Vector2 respawnPosition = new Vector2(-5, 0);

    void Start()
    {
        SpawnPlayer(); // 게임 시작 시 캐릭터 생성
    }

    void Update()
    {
        if (playerInstance != null)
        {
            CheckPlayerLife(); // 캐릭터 생존 여부 확인
        }
    }

    void CheckPlayerLife()
    {
        CharacterStats stats = playerInstance.GetComponentInChildren<CharacterStats>();
        if (stats.Life <= 0)
        {
            RespawnPlayer(); // 캐릭터 리스폰
        }
    }
    void SpawnPlayer()
    {
        playerInstance = Instantiate(Player, respawnPosition, Quaternion.identity);
    }

    void RespawnPlayer()
    {
        playerInstance.transform.position = respawnPosition; // 새로운 위치로 이동
    }
}
