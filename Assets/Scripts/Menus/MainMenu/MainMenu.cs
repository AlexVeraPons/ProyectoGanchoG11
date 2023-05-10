using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _initialMenu;
    [SerializeField]
    private GameObject _mainMenu;
    private ScenePicker _scenePicker;
    private string _gameScene;
    private string _optionsScene;
    

    private void Start()
    {
        _scenePicker = GetComponent<ScenePicker>();
        _gameScene = _scenePicker.scenePath.ToString();
        _optionsScene = _scenePicker.anotherScenePath.ToString();

        _initialMenu.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void GoToMainMenu()
    {
        _initialMenu.SetActive(false);
        _mainMenu.SetActive(true);
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
