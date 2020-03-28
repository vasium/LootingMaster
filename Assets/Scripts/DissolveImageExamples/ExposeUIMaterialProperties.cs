using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExposeUIMaterialProperties : MonoBehaviour
{
    public CanvasRenderer _UIMaterialToTarget;
    private Material _actualUIMaterial;

    private void Start()
    {
        _actualUIMaterial = _UIMaterialToTarget.GetMaterial();
    }

    public float myCustomFloat
    {
        get { return _actualUIMaterial.GetFloat("Dissolution level"); }
        set { _actualUIMaterial.SetFloat("Dissolution level", value); }
    }
}
