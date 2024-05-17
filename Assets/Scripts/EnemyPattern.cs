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

    private void MovePattern2()     // gpt 작품
    {
        float amplitude = 20.0f; // 지그재그의 높이
        float frequency = 5.0f; // 지그재그의 빈도
        float sinWave = Mathf.Sin(Time.time * frequency) * amplitude;   // 시간에 따라 변화하는 값        

        transform.position += (Vector3.left * movespeed + Vector3.up * sinWave) * Time.deltaTime;  // 포물선 운동을 추가한 좌측 이동
        if (transform.position.x < -10.0f)
        {
            Destroy(gameObject);
        }
    }
    private Vector3 currentDirection = Vector3.down; // 초기 이동 방향 설정
    private void MovePattern3()
    {
        // 위로 이동 중이고, y 위치가 6보다 크면 방향 전환
        if (currentDirection == Vector3.up && transform.position.y > 6f)
        {
            currentDirection = Vector3.down;
        }
        // 아래로 이동 중이고, y 위치가 -6보다 작으면 방향 전환
        else if (currentDirection == Vector3.down && transform.position.y < -6f)
        {
            currentDirection = Vector3.up;
        }

        // 현재 방향으로 이동
        transform.position += currentDirection * movespeed * Time.deltaTime;
    }

    private void MovePattern4()
    {
        //transform.position += Vector3.right * movespeed + Vector3.

    }
}

