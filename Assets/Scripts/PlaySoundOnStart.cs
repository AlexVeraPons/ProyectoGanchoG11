using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] SingleSound _soundType;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager._instance.PlaySingleSound(_soundType);
    }
}
