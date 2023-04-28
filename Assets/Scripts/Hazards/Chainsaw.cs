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
        transform.Rotate(Vector3.forward * _angularRotationalSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_running == false)
        {
            return;
        }

        if (other.GetComponent<IKillable>() != null)
        {
            other.GetComponent<IKillable>().Kill();
        }
    }
}
