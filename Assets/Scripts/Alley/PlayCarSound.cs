using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCarSound : MonoBehaviour
{
    public AudioSource _playCarPassingBy;

    private void Start()
    {
        _playCarPassingBy.transform.parent = transform;
    }

    public void PlayCarPassingBySound()
    {
        _playCarPassingBy.Play();
    }
}
