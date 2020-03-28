using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractablesEventStates : MonoBehaviour
{
    //common items (one instance in scene)
    [Header("Components to be draged from NavigationAndTextPanelParent", order = 0)]
    [SerializeField] private AudioSource _narratorAudioSource;
    [SerializeField] private TypeWriter _typewriter;

    //objects used by specific states
    [Header("Animations for slide show (animator on each slide image)", order = 0)]
    [SerializeField] private Animator[] _animationStates;
    [Space(30f, order = 0)]
    [Header("Higher value = slower", order = 1)]
    [Range(0.02f, 0.15f)]
    [SerializeField] private float _thisTypewriteDelay = 0.02f;
    [Header("> for new page", order = 1)]
    [Header("| for pause", order = 1)]
    [Space(20f, order = 2)]
    [TextArea(3, 25, order = 2)]
    [SerializeField] private string[] _textPerAnimation;
    //voice clips for this interactable, when we get them
    [SerializeField] private AudioClip[] _narratorClipPerAnimation;

    //number of states -1 for this interactable = number of animators
    private int _totalStates = -1;
    private float _fadeCount;
    private float _narratorVolume = 1;


    private void Awake()
    {
        foreach (Animator animator in _animationStates)
        {
            ++_totalStates;
        }
    }

    protected void OnEnable()
    {
        _typewriter.ClearText();

        foreach (Animator animator in _animationStates)
        {
            animator.SetBool("Show", false);
        }

        NavigationButtonEvents._previousButtonClick += PreparePlayState;
        NavigationButtonEvents._nextButtonClick += PreparePlayState;

        if (_typewriter != null)
        {
            _typewriter.enabled = true;
        }

        NavigationButtonEvents._currentTotalStates = _totalStates;
        _narratorAudioSource.Stop();
        StopAllCoroutines();
        PreparePlayState();
    }

    protected void OnDisable()
    {
        NavigationButtonEvents._previousButtonClick -= PreparePlayState;
        NavigationButtonEvents._nextButtonClick -= PreparePlayState;
        if (_typewriter != null)
        {
            _typewriter.enabled = false;
        }
        StopAllCoroutines();
        foreach (Animator animator in _animationStates)
        {
            animator.SetBool("Show", false);
        }
        if (_narratorAudioSource != null)
        {
            _narratorAudioSource.Stop();
        }
    }

    private void PreparePlayState()
    {
        StopAllCoroutines();
        StartCoroutine(PlayState(NavigationButtonEvents._currentState, _thisTypewriteDelay));
    }

    private IEnumerator PlayState(int state, float typewriteDelay)
    {
        _typewriter.ClearText();
        _narratorAudioSource.volume = _narratorVolume;
        if (!_animationStates[state].GetBool("Show"))
        {
            _animationStates[state].SetBool("Show", true);
        }

        _fadeCount = 0f;
        while (_fadeCount <= 1f)
        {
            _narratorAudioSource.volume = Mathf.Lerp(_narratorVolume, 0f, _fadeCount);
            _fadeCount += Time.deltaTime * 2f;

            yield return null;
        }

        _narratorAudioSource.Stop();
        //Fade out audio fast;

        yield return new WaitForSeconds(1f);
        //play audioclip
        if (_typewriter != null)
        {
            _narratorAudioSource.clip = _narratorClipPerAnimation[state];
            _narratorAudioSource.volume = _narratorVolume;
            _narratorAudioSource.Play();
            _typewriter.PassTextToTypeWrite(_textPerAnimation[state], typewriteDelay);
        }
        yield return null;
    }
}
