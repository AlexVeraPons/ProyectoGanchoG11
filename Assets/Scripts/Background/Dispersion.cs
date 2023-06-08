using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispersion : MonoBehaviour
{
    [SerializeField]
    private int _dispersion;
    void Start()
    {

    }

    public int GetDispersion()
    {
        return _dispersion;
    }
}
