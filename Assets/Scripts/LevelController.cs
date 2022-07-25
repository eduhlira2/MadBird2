using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public Image blackfade;
    public Animator animFade;
    
    [SerializeField]
    private string _nextLevelName;
    
    private Monster[] _monsters;

    private void Start()
    {
        animFade = animFade.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _monsters = FindObjectsOfType<Monster>();
    }

    private void Update()
    {
        if (MonstersAreAllDead())
        {
            Bird._canLaunch = false;
            StartCoroutine(GoToNextLevel());
        }
    }

    IEnumerator GoToNextLevel()
    {
        
        //yield return new WaitUntil(() => blackfade.color.a == 1);
        yield return new WaitForSeconds(3);
        animFade.SetBool("fade", true);
        SceneManager.LoadScene(_nextLevelName);
    }

    private bool MonstersAreAllDead()
    {
        foreach (var monster in _monsters)
        {
            if (monster.gameObject.activeSelf)
                return false;
        }

        return true;
    }
}
