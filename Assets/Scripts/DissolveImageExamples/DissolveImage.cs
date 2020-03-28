using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DissolveImage : MonoBehaviour
{

    public AnimationCurve _animationCurve;       
    public CanvasRenderer _UIMaterialToTarget;
    public Image _eRRMAP;
    private Material _actualUIMaterial;

    private void Start()
    {
        _actualUIMaterial = Instantiate(_eRRMAP.material);
        _actualUIMaterial.SetFloat("_DissolveAmount", 1f);
        _actualUIMaterial.SetFloat("_DissolveWidth", 0f);
        _eRRMAP.material = _actualUIMaterial;
    }

    private IEnumerator DissolveIn()
    {
        float count = 1f;

        while (count >= 0)
        {

            _actualUIMaterial.SetFloat("_DissolveAmount", _animationCurve.Evaluate(count));

            _actualUIMaterial.SetFloat("_DissolveWidth", _animationCurve.Evaluate(count * 0.3f));
            _eRRMAP.material = _actualUIMaterial;

            count -= Time.deltaTime * 0.12f;
            yield return null;
        }
        yield return null;
    }
}