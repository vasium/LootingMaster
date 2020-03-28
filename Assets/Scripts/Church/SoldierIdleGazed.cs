using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierIdleGazed : MonoBehaviour
{
    private AudioSource _audioSource;
    private Animator _animator;
    private bool _coroutineIsRunning;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponentInChildren<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    public void GazedUpon()
    {
        if (!_coroutineIsRunning)
        {
            StartCoroutine(GazeEvents());
        }
    }

    private IEnumerator GazeEvents()
    {
        _coroutineIsRunning = true;
        yield return new WaitForSeconds(0.2f);
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("NaziSoldier1kLookingAround"))
        {
            _animator.SetTrigger("Gaze");
            //_audioSource.pitch.(Random.Range(0.8f, 1.1f));
            _audioSource.Play();
        }
        yield return new WaitForSeconds(10f);
        _coroutineIsRunning = false;
    }

}
