using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchController : MonoBehaviour
{
    [SerializeField]
    private float _glitchDuration;

    [SerializeField]
    private Material _glitchMaterial;

    [SerializeField]
    private bool _glitchOnWaveStart = false;

    private Material _originalMaterial;

    private void OnEnable()
    {
        if (_glitchOnWaveStart)
        {
            WaveManager.OnLoadWave += Glitch;
        }
    }

    private void OnDisable()
    {
        WaveManager.OnLoadWave -= Glitch;
    }

    private void Awake()
    {
        _originalMaterial = GetComponent<SpriteRenderer>().material;
    }

    public void ModifyGlitchDuration(float newDuration)
    {
        _glitchDuration = newDuration;
    }
    public void Glitch()
    {
        StartCoroutine(GlitchCoroutine());
    }

    private IEnumerator GlitchCoroutine()
    {
        GetComponent<SpriteRenderer>().material = new Material(_glitchMaterial);

        yield return new WaitForSeconds(_glitchDuration);
        GetComponent<SpriteRenderer>().material = _originalMaterial;
    }
}
