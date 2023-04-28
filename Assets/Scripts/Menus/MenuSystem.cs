using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSystem : MonoBehaviour
{
    [SerializeField] private GameObject _initialMenu;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _optionsMenu;
    private GameObject _currentMenu;
    private GameObject _previousMenu;
    private bool _isPaused;
    void Start()
    {
        _initialMenu.SetActive(true);
        _currentMenu = _initialMenu;
        _previousMenu = _initialMenu;
        _isPaused = true;
        _mainMenu.SetActive(false);
        _pauseMenu.SetActive(false);
        _optionsMenu.SetActive(false);
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
            if (_isPaused)
            {
                GoToPlayGame();
            }
            else if (_isPaused == false)
            {
                GoToPauseMenu();
            }
        }
    }

    public void GoToMainMenu()
    {
        DeActivatePreviousMenu(_currentMenu);
        _previousMenu = _currentMenu;
        _currentMenu = _mainMenu;
        ActivateMenu(_currentMenu);
        _isPaused = true;
    }
    public void GoToPauseMenu()
    {
        DeActivatePreviousMenu(_currentMenu);
        _previousMenu = _currentMenu;
        _currentMenu = _pauseMenu;
        ActivateMenu(_currentMenu);
        _isPaused = true;
    }
    public void GoToOptionsMenu()
    {
        DeActivatePreviousMenu(_currentMenu);
        _previousMenu = _currentMenu;
        _currentMenu = _optionsMenu;
        ActivateMenu(_currentMenu);
        _isPaused = true;
    }
    public void GoToPreviousMenu()
    {
        DeActivatePreviousMenu(_currentMenu);
        _currentMenu = _previousMenu;
        ActivateMenu(_currentMenu);
    }
    public void GoToPlayGame()
    {
        DeActivatePreviousMenu(_currentMenu);
        _isPaused = false;
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
    private void DeActivatePreviousMenu(GameObject currentMenu)
    {
        currentMenu.SetActive(false);
    }
    private void ActivateMenu(GameObject currentMenu)
    {
        currentMenu.SetActive(true);
    }
}
