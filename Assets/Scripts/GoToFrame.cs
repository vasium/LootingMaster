using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class GoToFrame : MonoBehaviour
{
    private VideoPlayer _videoPlayer;


    void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
            SetFrame();
    }

    public void SetFrame()
    {
        long nFrame = (long)Mathf.Clamp(600, 0, _videoPlayer.clip.frameCount - 1);
        _videoPlayer.frame = nFrame;
        _videoPlayer.Play();
    }
}
