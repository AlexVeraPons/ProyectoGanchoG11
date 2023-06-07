using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInstanciator : MonoBehaviour
{
    [SerializeField] ParticleSystem _particle;

    public void EnableParticle()
    {
        Instantiate(_particle, this.transform.position, this.transform.rotation);
    }
}
