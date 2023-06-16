using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleAfterLifetime : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private float _timer = 0f;

    private void Awake()
    {
        _particleSystem = this.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_timer > _particleSystem.main.startLifetime.constant)
        {
            Destroy(this.gameObject);
        }    
    }
}
