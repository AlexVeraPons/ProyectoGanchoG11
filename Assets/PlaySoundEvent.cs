using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundEvent : MonoBehaviour
{
    [SerializeField] bool _pitchRandomness = false;
    [SerializeField] bool _volumeRandomness = false;

    void Play(SingleSound sound)
    {
        if (_pitchRandomness == false)
        {
            AudioManager._instance.PlaySingleSound(sound);
        }
        else
        {
            AudioManager._instance.PlaySingleSound(sound, Random.Range(0.8f, 1.2f), Random.Range(0.9f, 1.1f));
        }
    }
}
