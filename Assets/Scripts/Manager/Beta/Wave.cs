using UnityEngine;

public class Wave : MonoBehaviour
{
    public int ID => _waveID;
    private int _waveID;

    public Vector2 SpawnPosition => _spawnPosition;
    private Vector2 _spawnPosition;

    void Awake()
    {
        if (GetComponentInChildren<SpawnPosition>() != null)
        {
            _spawnPosition = GetComponentInChildren<SpawnPosition>().gameObject.transform.position;
        }
    }

    public void SetID(int id)
    {
        _waveID = id;
    }
}