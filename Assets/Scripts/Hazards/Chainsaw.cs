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

    private new void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (other.GetComponent<IKillable>() != null)
        {
            other.GetComponent<IKillable>().Kill();
        }
    }
}
