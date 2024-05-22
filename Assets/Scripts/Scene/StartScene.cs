using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.SoundManager.PlayBGM(Define.Scene.Start);
    }
}
