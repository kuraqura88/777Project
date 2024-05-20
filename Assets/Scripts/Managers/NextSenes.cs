using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextSenes : MonoBehaviour
{
    public void SensCange()
    {
        SceneManager.LoadScene("GameOverScenes");
    }
    
}
