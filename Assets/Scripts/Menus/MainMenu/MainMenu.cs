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


    [SerializeField]
    private DataManager _dataManager;
    private void Awake()
    {

    }
    private void Start()
    {
        _scenePicker = GetComponent<ScenePicker>();
        _gameScene = _scenePicker.scenePath.ToString();
        _optionsScene = _scenePicker.anotherScenePath.ToString();


        if (DataManager._instance._gameAlredyPlayed)
        {
            GoToMainMenu();
            Debug.Log("true");
        }
        else
        {
            _initialMenu.SetActive(true);
            _mainMenu.SetActive(false);
            Debug.Log(DataManager._instance._gameAlredyPlayed);
        }

    }

    public void GoToMainMenu()
    {
        DataManager._instance._gameAlredyPlayed = true;
        _initialMenu.SetActive(false);
        _mainMenu.SetActive(true);
        _playButton.Select();
        Debug.Log("MainMenu");
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
