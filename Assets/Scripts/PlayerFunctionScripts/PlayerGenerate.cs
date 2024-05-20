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
        SpawnPlayer(); // ���� ���� �� ĳ���� ����
    }

    void Update()
    {
        if (playerInstance != null)
        {
            CheckPlayerLife(); // ĳ���� ���� ���� Ȯ��
        }
    }

    void CheckPlayerLife()
    {
        CharacterStats stats = playerInstance.GetComponentInChildren<CharacterStats>();
        if (stats.Life <= 0)
        {
            RespawnPlayer(); // ĳ���� ������
        }
    }
    void SpawnPlayer()
    {
        playerInstance = Instantiate(Player, respawnPosition, Quaternion.identity);
    }

    void RespawnPlayer()
    {
        playerInstance.transform.position = respawnPosition; // ���ο� ��ġ�� �̵�
    }
}
