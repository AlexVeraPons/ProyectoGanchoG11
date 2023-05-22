using UnityEngine;

public class JamGunOnCollection : MonoBehaviour
{
    GrapplingGun _gun;

    void Awake()
    {
        _gun = GetComponent<GrapplingGun>();
    }
    
    private void OnEnable()
    {
        LifeComponent.OnDeath += JamGun;
        WaveManager.OnUnloadWave += JamGun;
        WaveManager.OnLoadWave += UnJamGun;
    }

    private void OnDisable()
    {
        LifeComponent.OnDeath -= JamGun;
        WaveManager.OnUnloadWave -= JamGun;
        WaveManager.OnLoadWave -= UnJamGun;
    }

    void JamGun()
    {
        _gun.JamGun();
    }

    void UnJamGun()
    {
        _gun.UnJamGun();
    }
}