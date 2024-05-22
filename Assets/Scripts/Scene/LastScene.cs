using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LastScene : MonoBehaviour
{
    public TMP_Text text;

    private void OnEnable()
    {
        text.text = GameManager.Instance.GetScore().ToString();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
