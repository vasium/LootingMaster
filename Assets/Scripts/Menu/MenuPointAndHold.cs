using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPointAndHold : MonoBehaviour
{
    public Image _imageLoadCircle;
    private Button _button;
    private float _counter;
    private bool _loadingButtonOn;
    private float _loadingSpeed = 4f;


    private void Start()
    {
        _button = GetComponent<Button>();
    }


    public void ActivateButtonLoadSequence()
    {
        Debug.Log("StartLoading");
        _loadingButtonOn = true;
    }

    public void StopButtonLoadSequence()
    {
        Debug.Log("StopLoading");
        _loadingButtonOn = false;
        _counter = 0f;
        _imageLoadCircle.GetComponent<Image>().fillAmount = 0;
    }

    private void Update()
    {
        if (_loadingButtonOn)
        {
            _counter += Time.deltaTime;
            _imageLoadCircle.fillAmount = _counter / _loadingSpeed;


            if (_imageLoadCircle.fillAmount >= 1)
            {
                _loadingButtonOn = false;
                _counter = 0f;
            }

        }
    }
}