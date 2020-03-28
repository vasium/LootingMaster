using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTextureOffset : MonoBehaviour
{
    public float _animationSpeed = 0.015f;
    private Material _godRayMaterial;
    private bool _animating;

    private int _offsetID;

    void Awake()
    {
        _godRayMaterial = GetComponent<MeshRenderer>().material;
        _offsetID = Shader.PropertyToID("_MainTex");
        
    }

    private void OnEnable()
    {
        _animating = true;
        StartCoroutine(AnimateGodRays());
    }

    private void OnDisable()
    {
        _animating = false;
        StopAllCoroutines();
    }

    private IEnumerator AnimateGodRays()
    {
        float offset = 0f; 
        while(_animating)
        {
            if (offset >= 1f)
            {
                offset = 0f;
            }

            offset = Time.time * _animationSpeed;


            _godRayMaterial.SetTextureOffset(_offsetID, new Vector2(offset, 0));

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
}
