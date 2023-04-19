using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUIUpdater : MonoBehaviour
{
    [SerializeField] ScoreOnStoreObject _scoreStorer;
    [SerializeField] TextMeshProUGUI _scoreText;

    private void Update()
    {
        _scoreText.text = "Score: " + (Mathf.Round(_scoreStorer.Score)).ToString();
    }
}
