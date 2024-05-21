using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartSenes : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("StartScenes");
    }
}
