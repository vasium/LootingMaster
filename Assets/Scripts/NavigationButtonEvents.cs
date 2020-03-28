using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NavigationButtonEvents : MonoBehaviour
{
    public delegate void ExitButtonClick();
    public static event ExitButtonClick _exitButtonClick;
    public delegate void PreviousButtonClick();
    public static event PreviousButtonClick _previousButtonClick;
    public delegate void NextButtonClick();
    public static event NextButtonClick _nextButtonClick;

    [SerializeField] private GameObject _navSwitchObject;
    [SerializeField] private AudioSource _buttonAudioSource;
    [SerializeField] private AudioClip[] _buttonSounds;

    [SerializeField] private Button _previousButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private CanvasGroup _navCanvasGroup;
    public AnimationCurve _navAnimationCurve;

    //how many states are awailable on current object? Set by interactables script.
    //number of states -1
    public static int _currentTotalStates = 0;
    public static int _currentState = 0;

    private void Start()
    {
        //_navAnimator.SetBool("On", false);
        //_previousButton.interactable = false;
        //_nextButton.interactable = true;
        _navSwitchObject.SetActive(false);
    }

    private void OnEnable()
    {
        InteractableUsingToggle._interactableUseTrigger += CanvasFadeIn;
        InteractableUsingToggle._interactableUnUseTrigger += CanvasFadeOut;
        //_navAnimator.SetBool("On", true);
        //_navSwitchObject.SetActive(true);
        //_currentState = 0;
        //_previousButton.interactable = false;
        //_nextButton.interactable = true;
    }

    private void OnDisable()
    {
        InteractableUsingToggle._interactableUseTrigger -= CanvasFadeIn;
        InteractableUsingToggle._interactableUnUseTrigger -= CanvasFadeOut;
        //_currentState = 0;
        ////_navAnimator.SetBool("On", false);
        //_previousButton.interactable = false;
        //_nextButton.interactable = true;
        //_navSwitchObject.SetActive(false);
    }

    public void ExitButtonClicked()
    {
        //_navAnimator.SetBool("On", false);
        
        _buttonAudioSource.clip = _buttonSounds[Random.Range(0, _buttonSounds.Length)];
        _buttonAudioSource.pitch = 0.7f;
        _buttonAudioSource.Play();
        _exitButtonClick();
    }

    public void PreviousButtonClicked()
    {
        if (!_nextButton.interactable)
        {
            _nextButton.interactable = true;
        }

        if (_currentState > 0)
        {
            --_currentState;
            _buttonAudioSource.clip = _buttonSounds[Random.Range(0, _buttonSounds.Length)];
            _buttonAudioSource.pitch = 1f;
            _buttonAudioSource.Play();
            _previousButtonClick();
        }
        if (_currentState <= 0)
        {
            _previousButton.interactable = false;
        }
    }

    public void NextButtonClicked()
    {
        if (!_previousButton.interactable)
        {
            _previousButton.interactable = true;
        }

        if (_currentState < _currentTotalStates)
        {
            ++_currentState;
            _buttonAudioSource.clip = _buttonSounds[Random.Range(0, _buttonSounds.Length)];
            _buttonAudioSource.pitch = 1f;
            _buttonAudioSource.Play();
            _nextButtonClick();
        }
        if (_currentState >= _currentTotalStates)
        {
            _nextButton.interactable = false;
        }
    }

    private void CanvasFadeIn()
    {
        StopAllCoroutines();
        _navCanvasGroup.alpha = 0;
        _currentState = 0;
        _navSwitchObject.SetActive(true);
        _exitButton.interactable = false;
        _previousButton.interactable = false;
        _nextButton.interactable = false;
        StartCoroutine(FadeInOutAlpha(0f, 1f, 0.4f, true));
    }

    private void CanvasFadeOut()
    {
        StopAllCoroutines();
        _exitButton.interactable = false;
        _previousButton.interactable = false;
        _nextButton.interactable = false;
        StartCoroutine(FadeInOutAlpha(1f, 0f, 1.5f, false));
    }

    public IEnumerator FadeInOutAlpha(float from, float to, float speed, bool fadeIn)
    {
        float count = 0;
        if (fadeIn)
        {
            yield return new WaitForSeconds(2f);
        }
        while (count <= 1)
        {
            _navCanvasGroup.alpha = Mathf.Lerp(from, to, _navAnimationCurve.Evaluate(count));

            count += Time.deltaTime * speed;
            yield return null;
        }

        if (fadeIn)
        {
            _exitButton.interactable = true;
            _nextButton.interactable = true;
        }
        else
        {            
            _navSwitchObject.SetActive(false);
        }

        yield return null;
    }
}
