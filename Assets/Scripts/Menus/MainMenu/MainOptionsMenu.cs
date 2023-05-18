using UnityEngine.SceneManagement;
using UnityEngine;

public class MainOptionsMenu : MonoBehaviour
{
    private ScenePicker _scenePicker;
    private string _mainMenuScene;
    private MenusMap _myInput;
    void Start()
    {
        _myInput = new MenusMap();
        _myInput.Interactuar.Enable();

        _scenePicker = GetComponent<ScenePicker>();
        _mainMenuScene = _scenePicker.scenePath.ToString();
    }
    void Update()
    {
        if (_myInput.Interactuar.GoBack.WasPressedThisFrame() || _myInput.Interactuar.Pause.WasPressedThisFrame())
        {
            LoadMainMenu();
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(_mainMenuScene);
    }
}
