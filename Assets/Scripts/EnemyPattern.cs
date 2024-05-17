using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPattern : MonoBehaviour
{
    public float movespeed = 1.0f;
    private float stopMove;
    public bool isStop = false;
    private EnemyRespawn enemyRespawn;

    private void Start()
    {
        Application.targetFrameRate = 60;
        stopMove = Random.Range(5.0f, 6.5f);

        enemyRespawn = GetComponent<EnemyRespawn>();
    }

    private void Update()
    {
        if (gameObject.tag == "enemyif")
        {
            if (!isStop && transform.position.x > stopMove)
            {
                MovePattern1();
            }
            else
            {
                isStop = true;
            }
        }
        else if (gameObject.tag == "enemyfor")
        {
            MovePattern2();
        }
        else if (gameObject.tag == "enemyswitch")
        {
            MovePattern3();
        }
    }

    private void MovePattern1()
    {
        transform.position += Vector3.left * movespeed * Time.deltaTime;
    }

    private void MovePattern2()     // gpt ��ǰ
    {
        float amplitude = 20.0f; // ��������� ����
        float frequency = 5.0f; // ��������� ��
        float sinWave = Mathf.Sin(Time.time * frequency) * amplitude;   // �ð��� ���� ��ȭ�ϴ� ��        

        transform.position += (Vector3.left * movespeed + Vector3.up * sinWave) * Time.deltaTime;  // ������ ��� �߰��� ���� �̵�
        if (transform.position.x < -10.0f)
        {
            Destroy(gameObject);
        }
    }
    private Vector3 currentDirection = Vector3.down; // �ʱ� �̵� ���� ����
    private void MovePattern3()
    {
        // ���� �̵� ���̰�, y ��ġ�� 6���� ũ�� ���� ��ȯ
        if (currentDirection == Vector3.up && transform.position.y > 6f)
        {
            currentDirection = Vector3.down;
        }
        // �Ʒ��� �̵� ���̰�, y ��ġ�� -6���� ������ ���� ��ȯ
        else if (currentDirection == Vector3.down && transform.position.y < -6f)
        {
            currentDirection = Vector3.up;
        }

        // ���� �������� �̵�
        transform.position += currentDirection * movespeed * Time.deltaTime;
    }

    private void MovePattern4()
    {
        //transform.position += Vector3.right * movespeed + Vector3.

    }
}

