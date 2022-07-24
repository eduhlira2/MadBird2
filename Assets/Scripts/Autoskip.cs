using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class Autoskip : MonoBehaviour
{
    private VideoPlayer _videoPlayer;
    [SerializeField]
    private string _scene;

    private bool _flag;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _flag = false;
    }

    void Update()
    {
        if(_flag)
        {
            if (!_videoPlayer.isPlaying)
            {
                SceneManager.LoadScene(_scene);
            }
        }

        _flag = _videoPlayer.isPlaying;
    }
}
