using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public GameObject startUI;
    public GameObject loading;
    public void GoMainScene()
    {
        StartCoroutine(LoadAysncScene("MainScene"));
    }

    public IEnumerator LoadAysncScene(string scene)
    {
        loading.SetActive(true);
        AsyncOperation operation =  SceneManager.LoadSceneAsync(scene);

        operation.allowSceneActivation = false;
        while (!operation.isDone && operation.progress < 0.9f)
        {
            Debug.Log("·ÎµùÁß : " + operation.progress);
            yield return null;
        }

        GameManager.Instance.SoundManager.StopBGM();

        operation.allowSceneActivation = true;
        GameManager.Instance.scene = Define.Scene.BasicStage;
        GameManager.Instance.Player.Enter();

        startUI.SetActive(false);


        yield break;
    }
    public void Exit()
    {
        Application.Quit();
    }

}