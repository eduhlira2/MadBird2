using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class SceneController : MonoBehaviour
{
    public Image blackfade;
    public Animator animFade;
    private string sceneToLoad;

    private void Start()
    {
        animFade = animFade.GetComponent<Animator>();
    }

    public void Restart (string scene)
    {
        sceneToLoad = scene;
        StartCoroutine(GoToNextLevel());
    }
    IEnumerator GoToNextLevel()
    {
        animFade.SetBool("fade", true);
        yield return new WaitUntil(() => blackfade.color.a == 1);
        SceneManager.LoadScene(sceneToLoad);
    }
}
