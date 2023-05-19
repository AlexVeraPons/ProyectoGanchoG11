using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private Button _ResumeButton;
    [SerializeField] private Slider _BrilloSlider;
    private bool _isPaused;
    private ScenePicker _scenePicker;
    private string _mainMenuScene;
    private MenusMap _myInput;

    //private CustomInput _input = null;

    void Start()
    {
        _myInput = new MenusMap();
        _myInput.Interactuar.Enable();

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
        
        
        if (_isPaused == false && _myInput.Interactuar.Pause.WasPressedThisFrame())
        {
            PauseGame();
        }
        else if((_isPaused && _optionsMenu.activeSelf == false) && (_myInput.Interactuar.GoBack.WasPressedThisFrame() || _myInput.Interactuar.Pause.WasPressedThisFrame())){
            ResumeGame();
        }
        else if((_isPaused && _optionsMenu.activeSelf == true) && _myInput.Interactuar.GoBack.WasPressedThisFrame()){
            ReturnPausePanel();
        }


    }
    public void PauseGame()
    {
        _isPaused = true;
        _pauseMenu.SetActive(true);
        _ResumeButton.Select();
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
        _BrilloSlider.Select();
    }
    public void ReturnPausePanel()
    {
        _optionsMenu.SetActive(false);
        _pauseMenu.SetActive(true);
        _ResumeButton.Select();
    }
    public void GoToMainMenu()
    {
        _pauseMenu.SetActive(false);
        _isPaused = false;
        SceneManager.LoadScene(_mainMenuScene);
    }
}
