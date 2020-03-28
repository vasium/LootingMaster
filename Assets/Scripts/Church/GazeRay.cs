using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeRay : MonoBehaviour
{
    private RaycastHit _hit;
    private float _time = 0.0f;
    private float _interpolationPeriod = 0.1f;
    private float _rayDistance = 4f;

    private int _rayLayerToHit;

    private void Awake()
    {
        _rayLayerToHit = LayerMask.GetMask("GazeTarget");
    }

    void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _interpolationPeriod)
        {
            _time = _time - _interpolationPeriod;

            if (Physics.Raycast(transform.position, transform.forward, out _hit, _rayDistance, _rayLayerToHit))
            {
                if (_hit.collider.gameObject.GetComponent<SoldierIdleGazed>())
                {
                    _hit.collider.gameObject.GetComponent<SoldierIdleGazed>().GazedUpon();
                }
            }
        }
    }
}
