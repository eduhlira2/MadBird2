using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChanceController : MonoBehaviour
{
    [SerializeField]
    private string _nextLevelName;

    private Asteroid[] _asteroids;
    private void OnEnable()
    {
        _asteroids = FindObjectsOfType<Asteroid>();
    }

    private void Update()
    {
        if (AsteroidsAreAllDead())
        {
            GoToNextLevel();
        }
    }

    private void GoToNextLevel()
    {
        SceneManager.LoadScene(_nextLevelName);
    }

    private bool AsteroidsAreAllDead()
    {
        foreach (var asteroid in _asteroids)
        {
            if (asteroid.gameObject.activeSelf)
                return false;
        }

        return true;
    }
}
