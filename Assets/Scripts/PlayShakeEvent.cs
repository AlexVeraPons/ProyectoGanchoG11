using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayShakeEvent : MonoBehaviour
{
    public void PlayShake(float magnitude)
    {
        CameraShaker._instance.StartShake(magnitude, 0f);
    }
}
