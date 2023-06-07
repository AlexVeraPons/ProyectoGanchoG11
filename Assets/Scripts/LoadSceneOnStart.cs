using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnStart : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    private void Start()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
