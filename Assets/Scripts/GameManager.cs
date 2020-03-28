using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public static GameManager _gameManager;
    //private int _uniqueFoundObjects = 0;

    //public Collider _zone1TriggerCollider;

    ////Last event variables
    //public AudioSource _boxUnderPainting;
    //public AudioSource _boxFurtherAway;
    //public GameObject _showAtLastEvent;
    //public GameObject _hideAtLastEvent;

    //public bool _lastEventUnlocked = false;

    //void Start()
    //{
    //    _showAtLastEvent.SetActive(false);
    //    _hideAtLastEvent.SetActive(true);
    //    _zone1TriggerCollider.enabled = false;
    //    _gameManager = this;
    //}

    //void Update()
    //{
    //    Debug.Log(_uniqueFoundObjects);
    //}

    //public void TriggerLastEvent()
    //{
    //    _uniqueFoundObjects += 1;
    //    if (_uniqueFoundObjects == 3)
    //    {
    //        _zone1TriggerCollider.enabled = true;
    //        _lastEventUnlocked = true;
    //    }
    //}

    //public void LastEvent()
    //{
    //    StartCoroutine(LastEventsCoroutine());
    //}

    //public IEnumerator LastEventsCoroutine()
    //{
    //    yield return new WaitForSeconds(1);
    //    _hideAtLastEvent.SetActive(false);
    //    _showAtLastEvent.SetActive(true);
    //    _boxUnderPainting.Play();
    //    _boxFurtherAway.Play();
    //}

}
