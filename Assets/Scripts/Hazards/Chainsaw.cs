using System;
using UnityEngine;

public class Chainsaw : HazardThatMoves
{
    [Space(10)]
    [Header("Chainsaw Settings")]
    [SerializeField]
    private float _angularRotationalSpeed = 0;

    private protected override void HazardUpdate()
    {
        base.HazardUpdate();
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.forward * _angularRotationalSpeed * Time.deltaTime);
    }
}