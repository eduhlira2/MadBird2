using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointControl : MonoBehaviour
{
    public static int points;
    public Text pointText;
    public Text highScoreText;

    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("O highScore Atual eh: "+ PlayerPrefs.GetInt("highScore"));
        highScoreText.text = PlayerPrefs.GetInt("highScore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("actualScore", points);
        pointText.text = points.ToString();
        
        if (points > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", points);
        }
    }
}
