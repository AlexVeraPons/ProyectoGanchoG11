using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager _instance;

    [SerializeField]
    private GameObject _dataManager;
    public bool _gameAlredyPlayed;
    
    void Awake()
    {
        if(_instance == null) { _instance = this; DontDestroyOnLoad(this.gameObject); }
        else { Destroy(this.gameObject); }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
