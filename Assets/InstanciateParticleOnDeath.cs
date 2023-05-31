using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciateParticleOnDeath : MonoBehaviour
{
    [SerializeField] ParticleSystem _particle;

    private void OnEnable()
    {
        LifeComponent.OnDeath += InstanciateParticle;
    }

    private void OnDisable()
    {
        LifeComponent.OnDeath -= InstanciateParticle;
    }

    void InstanciateParticle()
    {
        Instantiate(_particle, this.transform.position, this.transform.rotation);
    }
}
