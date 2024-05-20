using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Dictionary<int, SpriteRenderer> backgrounds = new Dictionary<int, SpriteRenderer>();

    public float speed = 0.01f;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            SpriteRenderer bg = transform.GetChild(i).GetComponent<SpriteRenderer>();  
            
            backgrounds.Add(i, bg);
        }
    }

    private void LateUpdate()
    {
        Scroll();
    }
    private void Scroll()
    {
        for(int i = 0; i < backgrounds.Count; i++)
        {
            Vector3 curPos = backgrounds[i].transform.position;
            Vector3 nextPos = Vector3.left * (speed * i * 0.1f) * Time.deltaTime;

            backgrounds[i].transform.position = curPos + nextPos;

            if (backgrounds[i].transform.position.x <= -backgrounds[i].bounds.size.x)
            {
                backgrounds[i].transform.position = Vector3.zero;
            }
        }
    }
}
