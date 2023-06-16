using System;
using UnityEngine;

public class Chainsaw : HazardWithPath
{
    [SerializeField]
    private float _angularRotationalSpeed = 0;

    private protected override void HazardUpdate()
    {
        base.HazardUpdate();
        Rotate();
    }

    private protected override void StopRunSound()
    {
        AudioSource thisSource = GetComponent<AudioSource>();
        if(thisSource == null)
        {
            return;
        }
        thisSource.Stop();
    }

    private void Rotate()
    {
        this.transform.Rotate(Vector3.forward * _angularRotationalSpeed * Time.deltaTime);
    }

    private protected override void PlayRunSound()
    {
        base.PlayRunSound();
        
        AudioSource thisSource = GetComponent<AudioSource>();
        if(thisSource == null)
        {
            if (AudioManager._instance == null)
            {
                return;
            }
            AudioManager._instance.CreateLoopSourceComponent(gameObject: this.gameObject, LoopingSound.Saw);
        }

        thisSource = GetComponent<AudioSource>();
        thisSource.Play();
        thisSource.volume = 0.1f;
    }
}