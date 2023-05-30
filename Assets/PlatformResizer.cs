using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformResizer : MonoBehaviour
{
    private Vector2 _size;

    public float Percentage => _timer / _maxTimer;
    private float _timer = 0f;
    [SerializeField] private float _maxTimer = 1f;

    private void Start()
    {
        _size = this.transform.localScale;
    }

    private void Update()
    {
        if(_timer < _maxTimer)
        {
            _timer += Time.deltaTime;
        }
        else if(_timer > _maxTimer)
        {
            _timer = _maxTimer;
        }

        float currentSizeX = Mathf.Lerp(0f, _size.x, _timer / _maxTimer);
        float currentSizeY = Mathf.Lerp(0f, _size.y, _timer / _maxTimer);
        Vector2 currentSize = new Vector2(currentSizeX, currentSizeY);

        this.transform.localScale = currentSize;
    }
}
