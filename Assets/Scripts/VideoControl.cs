using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoControl : MonoBehaviour
{
    private VideoPlayer _videoPlayer;
    private bool _startPlaying;
    void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();


        if (_videoPlayer.transform.tag == "Del1")
        {
            SetFrame(600);
        }
        if (_videoPlayer.transform.tag == "Del2")
        {
            SetFrame(4000);
        }
        if (_videoPlayer.transform.tag == "Del3")
        {
            SetFrame(4000);
        }
        if (_videoPlayer.transform.tag == "Del4")
        {
            SetFrame(140);
        }
    }

    public void SetFrame(long nFrame)
    {
        nFrame = (long)Mathf.Clamp(nFrame, 0, _videoPlayer.clip.frameCount - 1);
        _videoPlayer.frame = nFrame;
        _videoPlayer.Play();
         Invoke("PauseVideoPlayer", 3);
    }

    private void PauseVideoPlayer()
    {
        _videoPlayer.Pause();
    }
}
