using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class InteractableUsingToggle : MonoBehaviour
{
    public delegate void InteractableUseTrigger();
    public static event InteractableUseTrigger _interactableUseTrigger;
    public delegate void InteractableUnUseTrigger();
    public static event InteractableUnUseTrigger _interactableUnUseTrigger;

    private VRTK_InteractableObject _objectToMonitor;
    private bool _use = false;
    private bool _hasBeenUsed = false;
    private float _currentAnimationLength;

    [Header("Button under local InfoIconCanvas is bound", order = 0), Space(10), Header("to this scripts UseInteractableToggle()", order = 1), Space(0)]

    [Space(25, order = 2)]
    [Header("Local Components on interactable", order = 3)]
    [SerializeField] private InteractablesEventStates _interactablesEventStates;
    [SerializeField] private GameObject _interactablesCanvas;
    [SerializeField] private CanvasGroup _canvasGroupInteractable;
    [SerializeField] private Animator _onUseAnimator;
    [SerializeField] private AudioSource _onUseAudioSource;
    public AnimationCurve _alphaFadeCurve;
    [SerializeField] private GameObject _navigationSpawnpoint;
    [Space(15, order = 4)]
    [Header("Components to be draged from NavigationAndTextPanelParent", order = 5)]
    [SerializeField] private GameObject _navigationPanel;

    private Collider[] _interactableColliders;

    private void Awake()
    {        
        _interactableColliders = transform.GetChild(0).GetComponents<Collider>();
        _interactablesEventStates.enabled = false;
        _onUseAnimator.SetBool("Use", false);
        _interactablesCanvas.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        _objectToMonitor = (_objectToMonitor == null ? GetComponentInChildren<VRTK_InteractableObject>() : _objectToMonitor);

        if (_objectToMonitor != null)
        {
            _objectToMonitor.InteractableObjectUsed += InteractableObjectUsed;
            NavigationButtonEvents._exitButtonClick += UseInteractableToggle;
        }
    }

    protected virtual void OnDisable()
    {
        if (_objectToMonitor != null)
        {
            _objectToMonitor.InteractableObjectUsed -= InteractableObjectUsed;
            NavigationButtonEvents._exitButtonClick -= UseInteractableToggle;
        }
    }

    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        UseInteractableToggle();
    }

    public void UseInteractableToggle()
    {
        if (!_use)
        {
            _use = true;
            _onUseAudioSource.pitch = 1f;
            _onUseAudioSource.volume = 1f;
            _onUseAudioSource.Play();
            foreach (Collider collider in _interactableColliders)
            {
                collider.enabled = false;
            }
            _interactableUseTrigger();

            _onUseAnimator.SetBool("Use", true);
            _currentAnimationLength = _onUseAnimator.GetCurrentAnimatorStateInfo(0).length;
            Invoke("Use", _currentAnimationLength + 1.5f);
        }

        else
        {
            _use = false;
            _onUseAudioSource.pitch = 0.7f;
            _onUseAudioSource.volume = 0.65f;
            _onUseAudioSource.Play();
            _interactableUnUseTrigger();

            _onUseAnimator.SetBool("Use", false);
            UnUse();
        }
    }

    public void Use()
    {
        if (_use)
        {
            foreach (Collider collider in _interactableColliders)
            {
                collider.enabled = true;
            }

            _interactablesCanvas.SetActive(true);
            _interactablesEventStates.enabled = true;
            _navigationPanel.transform.position = _navigationSpawnpoint.transform.position;
            _navigationPanel.transform.rotation = _navigationSpawnpoint.transform.rotation;
            _canvasGroupInteractable.alpha = 1f;
            if (!_hasBeenUsed)
            {
                //increment int
                //GameManager._gameManager.TriggerLastEvent(int);
            }
            _hasBeenUsed = true;
        }
    }

    public void UnUse()
    {
        if (!_use)
        {
            StartCoroutine(FadeOutAlpha());
        }
    }

    public IEnumerator FadeOutAlpha()
    {
        float count = 0f;

        while (count <= 1f)
        {
            _canvasGroupInteractable.alpha = Mathf.Lerp(1f, 0f, _alphaFadeCurve.Evaluate(count));

            count += Time.deltaTime * 1.5f;
            yield return null;
        }

        yield return null;

        _interactablesCanvas.SetActive(false);
        _interactablesEventStates.enabled = false;
    }
}
