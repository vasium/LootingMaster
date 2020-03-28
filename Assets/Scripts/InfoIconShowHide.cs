using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoIconShowHide : MonoBehaviour
{
    private Button _infoIcon;
    private Animator _infoIconAnimator;
    private Collider _infoIconCollider;
    [SerializeField] private bool _onFromStart = true;


    private void Start()
    {
        _infoIconCollider = GetComponentInChildren<Collider>();
        _infoIconAnimator = GetComponent<Animator>();
        _infoIcon = GetComponentInChildren<Button>();
        if (_onFromStart)
        {
            _infoIconAnimator.SetBool("Show", true);
        }
    }

    private void OnEnable()
    {
        InteractableUsingToggle._interactableUseTrigger += IconClickUse;
        InteractableUsingToggle._interactableUnUseTrigger += IconClickUnUse;
    }

    private void OnDisable()
    {
        InteractableUsingToggle._interactableUseTrigger -= IconClickUse;
        InteractableUsingToggle._interactableUnUseTrigger -= IconClickUnUse;
    }

    public void IconClickUse()
    {
        _infoIconAnimator.SetBool("Show", false);
        _infoIcon.interactable = false;
        _infoIconCollider.enabled = false;
    }

    public void IconClickUnUse()
    {
        _infoIconAnimator.SetBool("Show", true);
        _infoIconCollider.enabled = true;
        _infoIcon.interactable = true;
    }
}
