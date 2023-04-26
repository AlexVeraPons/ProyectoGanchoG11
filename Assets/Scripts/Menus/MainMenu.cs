using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private ScenePicker _scenePicker;
    private string _gameScene;

    private void Start()
    {
        _scenePicker = GetComponent<ScenePicker>();
        _gameScene = _scenePicker.scenePath.ToString();
    }

    public void LoadGame() //deshardcodear a quin lvl van
    {
        Debug.Log("Loading Game...");
        SceneManager.LoadScene(_gameScene);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
    public void LoadOptions()
    {
        Debug.Log("Loading Options Menu...");
        SceneManager.LoadScene("Options");
    }
}
