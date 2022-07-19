using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    private int _monsterLife = 3;
    [SerializeField] string victoryScene;
    [SerializeField] string gameOverScene;

    [SerializeField] GameObject _capacete;
    [SerializeField] private Sprite _capacete1;
    [SerializeField] private Sprite _capacete2;
    [SerializeField] private Sprite _capacete3;
    [SerializeField] private Sprite _capacete4;

    [SerializeField] private Sprite _MonstroMorto;
    [SerializeField]

    AudioSource _bossDeadSFX;

    // Start is called before the first frame update
    void Start()
    {
        if (_bossDeadSFX == null)
        {
            print("O áudio não foi iniciado");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_monsterLife == 3)
        {
            _capacete.GetComponent<SpriteRenderer>().sprite = _capacete1;
        }
        if (_monsterLife == 2)
        {
            _capacete.GetComponent<SpriteRenderer>().sprite = _capacete2;
        }
        if (_monsterLife == 1)
        {
            _capacete.GetComponent<SpriteRenderer>().sprite = _capacete3;
            _bossDeadSFX.Play();
        }

        if (_monsterLife <= 0)
        {
            _capacete.GetComponent<SpriteRenderer>().sprite = _capacete4;
            GetComponent<SpriteRenderer>().sprite = _MonstroMorto;
            Debug.Log("Venceu o Boss!!!!"); //
            StartCoroutine(WaitToVictory());
        }
        
    }

    IEnumerator WaitToVictory()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(victoryScene); // Chama a cena da vitória.
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var asteroidClass = col.gameObject.GetComponent<Asteroid>();
        var bird = col.gameObject.GetComponent<Bird>();
        if (asteroidClass)
        {
            Debug.Log("atacou o boss");
            _monsterLife = _monsterLife - 1;
            Debug.Log("A vida do monstro eh: " + _monsterLife);
        }

        if (bird)
        {
            SceneManager.LoadScene(gameOverScene); // Chama a cena da vitória.
        }

        Debug.Log("Não atacou o boss");
    }



}
