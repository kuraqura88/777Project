using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyPattern : MonoBehaviour
{
    public float movespeed = 1.0f;
    private float stopMove;
    public bool isStop = false;
    public bool canMove; // Ãß°¡
    private Vector2 targetPosition;

    public Define.EnemyType type;

    private void Start()
    {
        canMove = false;    
        stopMove = Random.Range(5.0f, 6.5f);

        float randomX = Random.Range(6f, 7f);
        float randomY = Random.Range(4f, -4f);
        targetPosition = new Vector2(randomX, randomY);
    }

    private void Update()
    {
        if (canMove)
        {
            switch (type)
            {
                case Define.EnemyType.Straight:
                    if (!isStop && transform.position.x > stopMove)
                    {
                        MovePattern1();
                    }
                    else
                    {
                        isStop = true;
                    }
                    break;
                case Define.EnemyType.Wave:
                    MovePattern2();
                    break;
                case Define.EnemyType.UpDown:
                    MovePattern3();
                    break;
                case Define.EnemyType.Diagonal:
                    MovePattern4();
                    break;
            }
        }
    }


    public void CanMove() => canMove = true;
    private void MovePattern1()
    {
        transform.position += Vector3.left * movespeed * Time.deltaTime;
    }
    private void MovePattern2()
    {
        float amplitude = 20.0f;
        float frequency = 5.0f;
        float sinWave = Mathf.Sin(Time.time * frequency) * amplitude;

        transform.position += (Vector3.left * movespeed + Vector3.up * sinWave) * Time.deltaTime;
        if (transform.position.x < -10.0f)
        {
            Destroy(gameObject);
        }
    }
    private Vector3 currentDirection = Vector3.down;
    private void MovePattern3()
    {
        if (currentDirection == Vector3.up && transform.position.y > 6f)
        {
            currentDirection = Vector3.down;
        }
        else if (currentDirection == Vector3.down && transform.position.y < -6f)
        {
            currentDirection = Vector3.up;
        }

        transform.position += currentDirection * movespeed * Time.deltaTime;
    }
    private void MovePattern4()
    {
        float step = movespeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
    }
}

