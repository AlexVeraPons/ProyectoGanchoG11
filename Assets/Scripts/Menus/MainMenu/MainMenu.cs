using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _initialMenu;
    [SerializeField]
    private GameObject _mainMenu;
    private ScenePicker _scenePicker;
    private string _gameScene;
    private string _optionsScene;
    [SerializeField]
    private Button _playButton;
    private bool _isInMainMenu = false;


    private void Start()
    {
        _scenePicker = GetComponent<ScenePicker>();
        _gameScene = _scenePicker.scenePath.ToString();
        _optionsScene = _scenePicker.anotherScenePath.ToString();

        _initialMenu.SetActive(true);
        _mainMenu.SetActive(false);

        if (_isInMainMenu)
        {
            _initialMenu.SetActive(false);
            _mainMenu.SetActive(true);
        }

    }

    public void GoToMainMenu()
    {
        _initialMenu.SetActive(false);
        _mainMenu.SetActive(true);
        _isInMainMenu = true;
        _playButton.Select();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(_gameScene);
    }
    public void LoadOptions()
    {
        SceneManager.LoadScene(_optionsScene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
