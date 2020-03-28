using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TouchInteractionState : MonoBehaviour
{
    private VRTK_InteractableObject _objectToMonitor;

    private MeshRenderer _interactableMeshRenderer;
    public AnimationCurve _fadeAnimationCurve;
    private Material _interactableMaterial;
    private float _colorIntensity;
    private bool _isbeingUsedOrUnused;
    [Header("This scripts IsTouched() is bound to", order = 0), Header("InfoIconCanvas buttons Event trigger --> Pointer Enter", order = 1)]
    [Space(30f, order = 2)]
    [Header("This scripts IsUnTouched() is bound to", order = 3), Header("InfoIconCanvas buttons Event trigger --> Pointer Exit", order = 4)]
    [Space(30f, order = 5)]
    [Header("Default max emission for interactable material = 0.65f", order = 6)]
    [Range(0f, 1f)]
    [SerializeField] private float _maxEmission = 0.65f;


    private void Awake()
    {
        _interactableMeshRenderer = GetComponentInChildren<MeshRenderer>();
        _colorIntensity = 0;
        _interactableMaterial = _interactableMeshRenderer.material;
        _maxEmission = _interactableMaterial.GetColor("_EmissionColor").r;
        _interactableMaterial.SetColor("_EmissionColor", new Color(_colorIntensity, _colorIntensity, _colorIntensity));
        _objectToMonitor = (_objectToMonitor != null ? _objectToMonitor : GetComponentInChildren<VRTK_InteractableObject>());
    }

    private void OnEnable()
    {
        _objectToMonitor.InteractableObjectTouched += InteractableObjectTouched;
        _objectToMonitor.InteractableObjectUntouched += InteractableObjectUntouched;
        InteractableUsingToggle._interactableUseTrigger += IsUsed;
        InteractableUsingToggle._interactableUnUseTrigger += IsUnUsed;
        NavigationButtonEvents._exitButtonClick += IsUnUsed;
    }

    private void OnDisable()
    {
        _objectToMonitor.InteractableObjectTouched -= InteractableObjectTouched;
        _objectToMonitor.InteractableObjectUntouched -= InteractableObjectUntouched;
        InteractableUsingToggle._interactableUseTrigger -= IsUsed;
        InteractableUsingToggle._interactableUnUseTrigger -= IsUnUsed;
        NavigationButtonEvents._exitButtonClick -= IsUnUsed;
    }

    private void IsUsed()
    {
        _isbeingUsedOrUnused = true;
        StopAllCoroutines();
        if (_interactableMaterial.GetColor("_EmissionColor").r != _maxEmission)
        {
            StartCoroutine(FadeInOutAlpha(_interactableMaterial.GetColor("_EmissionColor").r, _maxEmission, 3f, false));
        }
    }

    private void IsUnUsed()
    {
        _isbeingUsedOrUnused = true;
        StopAllCoroutines();
        if (_interactableMaterial.GetColor("_EmissionColor").r != 0)
        {
            StartCoroutine(FadeInOutAlpha(_interactableMaterial.GetColor("_EmissionColor").r, 0f, 1f, true));
        }
        else
        {
            _isbeingUsedOrUnused = false;
        }
    }

    protected virtual void InteractableObjectTouched(object sender, InteractableObjectEventArgs e)
    {
        IsTouched();
    }

    protected virtual void InteractableObjectUntouched(object sender, InteractableObjectEventArgs e)
    {
        IsUnTouched();
    }

    public void IsTouched()
    {
        if (!_isbeingUsedOrUnused)
        {
            StopAllCoroutines();
            if (_interactableMaterial.GetColor("_EmissionColor").r != _maxEmission)
            {
                StartCoroutine(FadeInOutAlpha(_interactableMaterial.GetColor("_EmissionColor").r, _maxEmission, 3f, false));
            }
        }
    }

    public void IsUnTouched()
    {
        if (!_isbeingUsedOrUnused)
        {
            StopAllCoroutines();
            if (_interactableMaterial.GetColor("_EmissionColor").r != 0)
            {
                StartCoroutine(FadeInOutAlpha(_interactableMaterial.GetColor("_EmissionColor").r, 0f, 1f, false));
            }
        }
    }

    private IEnumerator FadeInOutAlpha(float from, float to, float speed, bool unUse)
    {
        float count = 0;
        while (count <= 1)
        {
            _colorIntensity = Mathf.Lerp(from, to, _fadeAnimationCurve.Evaluate(count));

            _interactableMaterial.SetColor("_EmissionColor", new Color(_colorIntensity, _colorIntensity, _colorIntensity));

            count += Time.deltaTime * speed;
            yield return null;
        }

        if (unUse)
        {
            _isbeingUsedOrUnused = false;
        }

        yield return null;
    }

}
