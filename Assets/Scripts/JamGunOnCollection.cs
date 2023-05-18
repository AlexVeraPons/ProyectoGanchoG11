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
        Collectible.OnCollected += JamGun;
        LifeComponent.OnDeath += JamGun;
        WaveManager.OnLoadWave += UnJamGun;
    }

    private void OnDisable()
    {
        Collectible.OnCollected -= JamGun;
        LifeComponent.OnDeath -= JamGun;
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