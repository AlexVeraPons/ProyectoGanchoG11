using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    HookBehaviour _hook;

    private LineRenderer _lineRenderer;
    [SerializeField]
    private Transform _playerPosition;
    [SerializeField]
    private Transform _hookPosition;
    // Start is called before the first frame update

    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _hook = GetComponentInParent<HookBehaviour>();
    }

    private void OnEnable()
    {
        //_hook.OnFinish += HideRenderer;
        //_hook.OnLaunch += ShowRenderer;
    }

    private void OnDisable()
    {
        //_hook.OnFinish -= HideRenderer;
        //_hook.OnLaunch -= ShowRenderer;
    }

    private void LateUpdate()
    {
        _lineRenderer.SetPosition(0, _playerPosition.position + (Vector3)_hook.LaunchOffset);
        _lineRenderer.SetPosition(1, _hookPosition.position);
    }

    void HideRenderer()
    {
        _lineRenderer.enabled = false;
    }

    void ShowRenderer()
    {
        _lineRenderer.enabled = true;
    }
}
