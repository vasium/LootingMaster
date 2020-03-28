using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutSounds : MonoBehaviour
{
    private AudioSource[] _enviromentSounds;
    private List<float>  _audioSourceList = new List<float>();




    private void Start()
    {
        _enviromentSounds = GetComponentsInChildren<AudioSource>();

        foreach (AudioSource audioSource in _enviromentSounds)
        {
            audioSource.volume = 0.5f;

          //   _audioSourceList.Add(audioSource.volume);
        }
    }
}
