using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Must have in order to function
    public static AudioManager _instance;

    //Single sound related values
    private List<AudioSource> _singleSoundSources = new List<AudioSource>();
    Dictionary<SingleSound, AudioClip> _singleSoundDictionary = new Dictionary<SingleSound, AudioClip>();

    [Tooltip("The list of single use sounds and their associated SoundType")]
    public List<SoundAudioClipPair> _singleSoundList = new List<SoundAudioClipPair>();

    //Looping sound related values
    private List<AudioSource> _loopSoundSources = new List<AudioSource>();
    Dictionary<LoopingSound, AudioClip> _loopSoundDictionary = new Dictionary<LoopingSound, AudioClip>();

    [Tooltip("The list of looping sounds and their associated SoundType")]
    public List<SoundLoopAudioClipPair> _loopSoundList = new List<SoundLoopAudioClipPair>();

    //Music related values
    private List<AudioClip> _musicSources = new List<AudioClip>();
    Dictionary<Music, AudioClip> _musicDictionary = new Dictionary<Music, AudioClip>();

    [Tooltip("The list of music tracks and their associated MusicType")]
    public List<MusicAudioClipPair> _musicList = new List<MusicAudioClipPair>();
    private AudioSource _musicSource;

    void Awake()
    {
        AssignSoundValues();
        AssignMusicValues();

        if (IsInstanceEmpty() == true)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        GenerateMusicSource();
        PlayMusic(Music.MainMenu);
    }

    private bool IsInstanceEmpty()
    {
        return _instance == null;
    }

    private void OnDestroy()
    {
        _instance.ClearAll();
    }

    public void ClearAll()
    {
        _singleSoundSources.Clear();
        _loopSoundSources.Clear();
        _musicSources.Clear();

        for(int i = 0; i < transform.childCount - 1; i++)
        {
            for(int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                Destroy(transform.GetChild(i).transform.GetChild(j).gameObject);
            }
        }
    }

    public void PlaySingleSound(SingleSound singleSound)
    {
        AudioClip clip;
        _singleSoundDictionary.TryGetValue(singleSound, out clip);
        if (clip != null)
        {
            AudioSource source;
            if (SoundSourceAvailable(_singleSoundSources, out source) == true)
            {
                source.PlayOneShot(clip);
            }
            else
            {
                GenerateSoundSource(doesLoop: false);
                PlaySingleSound(singleSound);
            }
        }
    }

    //Deprecated
    public void PlayLoopingSound(LoopingSound loopingSound)
    {
        /*
        AudioClip clip;
        _loopSoundDictionary.TryGetValue(loopingSound, out clip);
        if (clip != null)
        {
            AudioSource source;
            if (SoundSourceAvailable(_loopSoundSources, out source) == true)
            {
                source.clip = clip;
                source.Play();
            }
            else
            {
                GenerateSoundSource(doesLoop: true);
                PlayLoopingSound(loopingSound);
            }
        }*/
    }

    //Deprecated
    public void StopLoopingSound(LoopingSound loopingSound)
    {
        /*
        AudioClip clip;
        _loopSoundDictionary.TryGetValue(loopingSound, out clip);
        if(clip != null)
        {
            for(int i = 0; i < _loopSoundSources.Count; i++)
            {
                SoundLoopAudioClipPair loopClip;
                _loopSoundDictionary.TryGetValue(loopingSound, out clip);
                if(_loopSoundSources[i].clip == clip)
                {
                    print("stopppppp");
                    _loopSoundSources[i].Stop();
                    Destroy(_loopSoundSources[i].gameObject);
                }
            }
        }*/
    }

    public void PlayMusic(Music music)
    {
        AudioClip musicClip;
        _musicDictionary.TryGetValue(music, out musicClip);
        if (musicClip != null && IsMusicAlreadyPlaying(music) == false)
        {
            _musicSource.Stop();
            _musicSource.clip = musicClip;
            _musicSource.Play();
        }
    }

    public void CreateLoopSourceComponent(GameObject gameObject, LoopingSound loopSound)
    {
        AudioClip clip;
        _loopSoundDictionary.TryGetValue(loopSound, out clip);
        if(clip != null)
        {
            AudioSource tempSource = gameObject.AddComponent<AudioSource>();
            tempSource.loop = true;
            tempSource.clip = clip;
            tempSource.Play();
        }
    }

    /// <summary>
    /// Checks to see if there is an AudioSource that is not being used in that frame.
    /// Returns an AudioSource if there is one.
    /// </summary>
    private bool SoundSourceAvailable(List<AudioSource> audioSources, out AudioSource newSource)
    {
        if (audioSources.Count > 0)
        {
            foreach (var source in audioSources)
            {
                if (source.isPlaying == false)
                {
                    newSource = source;
                    return true;
                }
            }
        }

        newSource = null;
        return false;
    }

    void AssignSoundValues()
    {
        foreach (var key in _singleSoundList)
        {
            _singleSoundDictionary[key.key] = key.value;
        }

        foreach (var key in _loopSoundList)
        {
            _loopSoundDictionary[key.key] = key.value;
        }
    }
    void AssignMusicValues()
    {
        foreach (var key in _musicList)
        {
            _musicDictionary[key.key] = key.value;
        }
    }
    void GenerateMusicSource()
    {
        GameObject musicObject = new GameObject("MusicSource");
        _musicSource = musicObject.AddComponent<AudioSource>();
        _musicSource.loop = true;
        musicObject.transform.parent = this.transform.GetChild(2);
    }
    public void GenerateSoundSource(bool doesLoop = false)
    {
        GameObject sourceObject = new GameObject("TempSource");
        AudioSource source = sourceObject.AddComponent<AudioSource>();

        if(doesLoop == false)
        {
            sourceObject.transform.SetParent(this.transform.GetChild(1));
            _singleSoundSources.Add(source);
        }
        else
        {
            sourceObject.transform.SetParent(this.transform.GetChild(0));
            source.loop = true;
            _loopSoundSources.Add(source);
        }
    }
    public bool IsMusicAlreadyPlaying(Music music)
    {
        AudioClip clip;
        _musicDictionary.TryGetValue(music, out clip);
        if(_musicSource.clip == clip)
        {
            return true;
        }

        return false;
    }
}

[Serializable]
public class SoundAudioClipPair
{
    public SingleSound key;
    public AudioClip value;
}

[Serializable]
public class SoundLoopAudioClipPair
{
    public LoopingSound key;
    public AudioClip value;
}

[Serializable]
public class MusicAudioClipPair
{
    public Music key;
    public AudioClip value;
}

public enum SingleSound
{
    EnemyAppear,
    EnemyDisappear,
    PlatformActivates,
    PlatformDeactivates,
    LaserWarning,
    Laser,
    HookLaunch,
    HookStuck,
    PlatformBounce,
    ButtonOver,
    ButtonClick,
    MissileCrash,
    PlayerGround,
    PlayerDeath,
    HazardAreaHorizontalScale,
    HazardAreaVerticalScale
}

public enum LoopingSound
{
    Saw,
    MissileFollowing,
    HookRetrieving,
    PlatformMoves
}

public enum Music
{
    MainMenu,
    Level
}