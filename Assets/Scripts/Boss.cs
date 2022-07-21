using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Boss : MonoBehaviour
{
    private int _monsterLife = 3;
    public int _pointToAddBoss;
    [SerializeField]
    string _victoryScene;
    [SerializeField]
    string _gameOverScene;
    
    [SerializeField]
    GameObject _helmet;
    [SerializeField]
    private Sprite _helmetSprite1;
    [SerializeField]
    private Sprite _helmetSprite2;
    [SerializeField]
    private Sprite _helmetSprite3;
    [SerializeField]
    private Sprite _helmetSprite4;

    [SerializeField]
    private Sprite _deadSprite;
    [SerializeField]
    AudioSource _bossDeadSFX;

    IEnumerator WaitToVictory()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(_victoryScene);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var asteroidClass = col.gameObject.GetComponent<Asteroid>();
        var bird = col.gameObject.GetComponent<Bird>();
        
        if (asteroidClass)
        {
            _monsterLife--;
            ChangeSprite();
        }

        if (bird)
        {
            
            //SceneManager.LoadScene(_gameOverScene);
        }
    }

    private void ChangeSprite()
    {
        if (_monsterLife == 3)
        {
            _helmet.GetComponent<SpriteRenderer>().sprite = _helmetSprite1;
        }
        if (_monsterLife == 2)
        {
            _helmet.GetComponent<SpriteRenderer>().sprite = _helmetSprite2;
        }
        if (_monsterLife == 1)
        {
            _helmet.GetComponent<SpriteRenderer>().sprite = _helmetSprite3;
            
        }

        if (_monsterLife <= 0)
        {
            PointControl.points = PointControl.points = _pointToAddBoss;
            
            _bossDeadSFX.Play();
            _helmet.GetComponent<SpriteRenderer>().sprite = _helmetSprite4;
            GetComponent<SpriteRenderer>().sprite = _deadSprite;
            
            StartCoroutine(WaitToVictory());
        }
    }

}
