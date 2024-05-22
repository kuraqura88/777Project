using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FisrtCutScene : MonoBehaviour
{
    public void GoScene(string scene)
    {
        StopAllCoroutines();
        StartCoroutine(LoadAysncScene(scene));
    }

    public IEnumerator LoadAysncScene(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        operation.allowSceneActivation = false;
        while (!operation.isDone && operation.progress < 0.9f)
        {
            yield return null;
        }

        GameManager.Instance.SoundManager.StopBGM();


        operation.allowSceneActivation = true;
        if (scene.Equals("StartScenes"))
        {
            GameManager.Instance.scene = Define.Scene.Start;
            GameManager.Instance.stage = Define.Scene.BasicStage;
        }
        else if(scene.Equals("FirstCutScene"))
        {
            GameManager.Instance.scene = Define.Scene.FirstCutScene;
            GameManager.Instance.stage = Define.Scene.BasicStage;
        }
        else if(scene.Equals("MainScene") && GameManager.Instance.scene == Define.Scene.FirstCutScene)
        {
            GameManager.Instance.scene = Define.Scene.BasicStage;

        }

        else if (scene.Equals("MainScene") && GameManager.Instance.scene == Define.Scene.Start)
        {
            GameManager.Instance.scene = Define.Scene.BasicStage;
        }

        else if (scene.Equals("MainScene") && GameManager.Instance.stage == Define.Scene.BasicStage)
        {
            GameManager.Instance.scene = Define.Scene.StandardStage;

            GameManager.Instance.stage = Define.Scene.StandardStage;
        }
        else if (scene.Equals("MainScene") && GameManager.Instance.stage == Define.Scene.StandardStage)
        {
            GameManager.Instance.scene = Define.Scene.ChallangeStage;

            GameManager.Instance.stage = Define.Scene.ChallangeStage;
        }
        else if (scene.Equals("AllClear"))
        {
            // TODO Ŭ����
            GameManager.Instance.scene = Define.Scene.AllClear;
            GameManager.Instance.stage = Define.Scene.AllClear;

        }

        if(GameManager.Instance.scene != Define.Scene.FirstCutScene)
        {
            if (GameManager.Instance.Player != null)
                GameManager.Instance.Player.Enter();
        }



        yield break;
    }
}
