using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMusicOnStart : MonoBehaviour
{
    //This script wont work unless SoundManager is in the scene

    public Music _music;

    private void Start()
    {
        AudioManager._instance.PlayMusic(_music);
    }
}
