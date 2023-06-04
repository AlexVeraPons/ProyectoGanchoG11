using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundEvent : MonoBehaviour
{
    void Play(SingleSound sound)
    {
        AudioManager._instance.PlaySingleSound(sound);
    }
}
