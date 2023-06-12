using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSpawnerComponent : MonoBehaviour
{
    [SerializeField]
    private Transform _originSquare;
    void Start()
    {
        
    }
    public Transform GetTransform(){
        return _originSquare;
    }
}
