using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    Hook _hook;

    private LineRenderer _lineRenderer;
    [SerializeField]
    private Transform _playerPosition;
    [SerializeField]
    private Transform _hookPosition;
    // Start is called before the first frame update

    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _hook = GetComponentInParent<Hook>();
    }

    private void OnEnable()
    {
        _hook.OnFinish += HideRenderer;
        _hook.OnLaunch += ShowRenderer;
    }

    private void OnDisable()
    {
        _hook.OnFinish -= HideRenderer;
        _hook.OnLaunch -= ShowRenderer;
    }

    private void LateUpdate()
    {
        if (_hook.State != HookState.Waiting)
        {
            _lineRenderer.SetPosition(0, _playerPosition.position);
            _lineRenderer.SetPosition(1, _hookPosition.position);
        }
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
