using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    [SerializeField]
    private Transform _playerPosition;
    [SerializeField]
    private Transform _hookPosition;
    // Start is called before the first frame update
    void Start()
    {
       _lineRenderer = GetComponent<LineRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        _lineRenderer.SetPosition(0, _playerPosition.position);
        _lineRenderer.SetPosition(1, _hookPosition.position);
    }
}
