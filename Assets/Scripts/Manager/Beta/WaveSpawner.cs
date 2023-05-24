using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public void SpawnWave(WorldCollector collector, int worldID, int waveID)
    {
        foreach (Wave wave in collector.Worlds[worldID].WaveList)
        {
            if (wave.ID == waveID)
            {
                wave.gameObject.SetActive(true);
            }
        }
    }

    public void DespawnWave(WorldCollector collector, int worldID, int waveID)
    {
        foreach (Wave wave in collector.Worlds[worldID].WaveList)
        {
            if (wave.ID == waveID)
            {
                wave.gameObject.SetActive(false);
            }
        }
    }

    public void SpawnWorld(WorldCollector collector, int worldID)
    {
        foreach (World world in collector.Worlds)
        {
            if(world.ID == worldID)
            {
                world.gameObject.SetActive(true);
            }
        }
    }

    public void DespawnWorld(WorldCollector collector, int worldID)
    {
        foreach (World world in collector.Worlds)
        {
            if(world.ID == worldID)
            {
                world.gameObject.SetActive(false);
            }
        }
    }

    public void DespawnAll(WorldCollector collector)
    {
        foreach (World world in collector.Worlds)
        {
            foreach (Wave wave in world.WaveList)
            {
                wave.gameObject.SetActive(false);
            }
        }
    }

    public void SpawnAll(WorldCollector collector)
    {
        foreach (World world in collector.Worlds)
        {
            foreach (Wave wave in world.WaveList)
            {
                wave.gameObject.SetActive(true);
            }
        }
    }
}
