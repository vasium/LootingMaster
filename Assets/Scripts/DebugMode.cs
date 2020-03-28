using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMode : MonoBehaviour
{
    public GameObject[] _buttons;
    public GameObject _IntroGameObject;
    public bool _debugModeOn;

    private void Start()
    {
        if (!_debugModeOn)
        {
            DebugModeOFF();
        }
        else
        DebugModeON();
    }


    private void DebugModeON()
    {
        foreach (GameObject go in _buttons)
        {
            go.SetActive(true);
            _IntroGameObject.SetActive(false);
        }
    }

    private void DebugModeOFF()
    {
        foreach (GameObject go in _buttons)
        {
            go.SetActive(false);
            _IntroGameObject.SetActive(true);
        }
    }
}
