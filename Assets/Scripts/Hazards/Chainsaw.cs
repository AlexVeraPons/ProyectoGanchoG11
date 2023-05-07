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

    private protected override void PlayRunSound()
    {
        base.PlayRunSound();
        AudioManager._instance.PlayLoopingSound(LoopingSound.Saw);
    }

    private protected override void StopRunSound()
    {
        AudioManager._instance.StopLoopingSound(LoopingSound.Saw);
    }
}