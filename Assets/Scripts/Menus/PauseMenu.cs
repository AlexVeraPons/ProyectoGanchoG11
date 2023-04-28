using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _optionsMenu;
    private ScenePicker _scenePicker;
    private string _mainMenuScene;
    private bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        _scenePicker = GetComponent<ScenePicker>();
        _mainMenuScene = _scenePicker.scenePath.ToString();

        _pauseMenu.SetActive(false);
        _optionsMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //pasar a nou input
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else if (isPaused == false)
            {
                PauseGame();
            }
        }
    }
    void PauseGame()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Debug.Log("pause");
    }
    void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Debug.Log("resume");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(_mainMenuScene);
    }
    public void OptionsMenu()
    {
        _optionsMenu.SetActive(true);
    }
}
