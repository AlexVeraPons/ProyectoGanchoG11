using UnityEngine;

public class LoadMusicOnEnable : MonoBehaviour
{
    //This script wont work unless SoundManager is in the scene

    public Music _music;

    private void OnEnable()
    {
        AudioManager._instance.PlayMusic(_music);
    }
}
