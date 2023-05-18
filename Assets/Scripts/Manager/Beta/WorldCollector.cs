using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldCollector : MonoBehaviour
{
    public List<World> Worlds => _worlds;
    [SerializeField] private List<World> _worlds;

    private void Awake()
    {
        World[] worlds = GetComponentsInChildren<World>();
        _worlds = worlds.ToList<World>();
    }
}
