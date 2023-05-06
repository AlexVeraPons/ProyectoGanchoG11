using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _optionsMenu;
    private bool _isPaused;
    private ScenePicker _scenePicker;
    private string _mainMenuScene;
    void Start()
    {
        _scenePicker = GetComponent<ScenePicker>();
        _mainMenuScene = _scenePicker.scenePath.ToString();

        _pauseMenu.SetActive(false);
        _optionsMenu.SetActive(false);
        _isPaused = false;
    }
    void Update()
    {
        if (_isPaused)
        {
            Time.timeScale = 0f;
        }
        else if (_isPaused == false)
        {
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) //pasar a nou input
        {
            if (_isPaused && _optionsMenu.activeSelf == false)
            {
                ResumeGame();
            }
            else if (_isPaused && _optionsMenu.activeSelf)
            {
                ReturnPausePanel();
            }
            else if (_isPaused == false)
            {
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        _isPaused = true;
        _pauseMenu.SetActive(true);
    }
    public void ResumeGame()
    {
        _isPaused = false;
        _pauseMenu.SetActive(false);
    }
    public void GoToOptions()
    {
        _pauseMenu.SetActive(false);
        _optionsMenu.SetActive(true);
    }
    public void ReturnPausePanel()
    {
        _optionsMenu.SetActive(false);
        _pauseMenu.SetActive(true);
    }
    public void GoToMainMenu()
    {
        _pauseMenu.SetActive(false);
        _isPaused = false;
        SceneManager.LoadScene(_mainMenuScene);
    }
}
