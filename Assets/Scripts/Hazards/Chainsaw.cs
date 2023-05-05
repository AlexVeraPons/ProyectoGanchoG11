using System;
using UnityEngine;

public class Chainsaw : HazardThatMoves
{
    [SerializeField]
    private float _angularRotationalSpeed = 0;

    private protected override void HazardUpdate()
    {
        base.HazardUpdate();
        Rotate();
    }

    private void Rotate()
    {
        this.transform.Rotate(Vector3.forward * _angularRotationalSpeed * Time.deltaTime);
    }
}