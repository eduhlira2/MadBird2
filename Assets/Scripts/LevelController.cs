using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private string _nextLevelName;
    
    private Asteroid[] _monsters;
    private void OnEnable()
    {
        _monsters = FindObjectsOfType<Asteroid>();
    }

    private void Update()
    {
        if (MonstersAreAllDead())
        {
            GoToNextLevel();
        }
    }

    private void GoToNextLevel()
    {
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
