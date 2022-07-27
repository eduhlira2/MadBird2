using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    public Text highScoreText;
    public Text actualScore;
    
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("highScore",0);
        Debug.Log("O valor de highScore salvo eh: "+ PlayerPrefs.GetInt("highScore"));
        highScoreText.text = PlayerPrefs.GetInt("highScore").ToString();
        actualScore.text = PlayerPrefs.GetInt("actualScore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
