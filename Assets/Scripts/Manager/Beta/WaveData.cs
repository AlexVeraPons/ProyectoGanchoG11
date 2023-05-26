public class WaveData
{
    int _worldID;
    int _waveID;


    public WaveData(int worldID, int waveID)
    {
        this._worldID = worldID;
        this._waveID = waveID;
    }

    public int GetWorldID() { return _worldID; }
    public int GetWaveID() { return _waveID; }
}