using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    
    public void Restart (string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
