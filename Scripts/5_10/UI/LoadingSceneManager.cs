using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    public Slider slider;
    public string ingameSceneName;

    void Start()
    {
    }

    public void OnLoadGame()
    {
        StartCoroutine(LoadAsynSceneCoroutine());
    }

    IEnumerator LoadAsynSceneCoroutine()
    {
        print("Coroutin just start now");

        AsyncOperation operation = SceneManager.LoadSceneAsync(ingameSceneName);

        operation.allowSceneActivation = false;



        while (!operation.isDone)
        {
            float progress = operation.progress * 100;
            slider.value = progress;
            if(slider.value >= 90)
            {
                slider.value = 100;
                operation.allowSceneActivation = true;
                print("Coroutin is ending now");
            }
            yield return null;
        }

    }
}
