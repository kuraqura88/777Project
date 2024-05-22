using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void MainSenesGo()
    {
        SceneManager.LoadScene("ProjectileTest");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
