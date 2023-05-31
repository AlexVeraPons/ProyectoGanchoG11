using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerParticle : MonoBehaviour
{
    [SerializeField] private Transform _transform;

    void FixedUpdate()
    {
        this.transform.position = _transform.position;
    }
}
