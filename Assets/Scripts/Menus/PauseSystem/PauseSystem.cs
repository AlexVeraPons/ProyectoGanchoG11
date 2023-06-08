using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class PauseSystem : MonoBehaviour
{
    [Header("PAUSE PANELS")]
    [Space(5)]
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _optionsMenu;

    [Space(10)]
    [Header("BUTTONS IN PAUSE PANEL")]
    [Space(5)]
    [Header("Resume button in Pause")]
    [SerializeField] private Button _ResumeButton;
    [SerializeField] private Image _resumeButtonSelectedImage;
    [SerializeField] private Image _resumeButtonDefaultImage;

    [Space(5)]
    [Header("Options button in Pause")]
    [SerializeField] private Button _optionButton;
    [SerializeField] private Image _optionButtonSelectedImage;
    [SerializeField] private Image _optionButtonDefaultImage;

    [Space(5)]
    [Header("Exit button in Pause")]
    [SerializeField] private Button _exitButton;
    [SerializeField] private Image _exitButtonSelectedImage;
    [SerializeField] private Image _exitButtonDefaultImage;

    [Space(10)]
    [Header("BUTTONS IN OPTIONS PAUSE PANEL")]
    [Space(5)]
    [Header("Display button")]
    [SerializeField] private Button _displayButton;
    [SerializeField] private Image _displayButtonSelectedImage;
    [SerializeField] private Image _displayButtonDefaultImage;

    [Space(5)]
    [Header("Resolution button")]
    [SerializeField] private Button _resolutionButton;
    [SerializeField] private Image _resolucionButtonSelectedImage;
    [SerializeField] private Image _resolucionButtonDefaultImage;

    [Space(5)]
    [Header("Brillo button")]
    [SerializeField] private Button _BrilloButton;
    [SerializeField] private Image _brilloButtonSelectedImage;
    [SerializeField] private Image _brilloButtonDefaultImage;
    
    [Space(5)]
    [Header("Volume button")]
    [SerializeField] private Button _volumeButton;
    [SerializeField] private Image _volumeButtonSelectedImage;
    [SerializeField] private Image _volumeButtonDefaultImage;

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
        else if ((_isPaused && _optionsMenu.activeSelf == false) && (_myInput.Interactuar.GoBack.WasPressedThisFrame() || _myInput.Interactuar.Pause.WasPressedThisFrame()))
        {
            ResumeGame();
        }
        else if ((_isPaused && _optionsMenu.activeSelf == true) && _myInput.Interactuar.GoBack.WasPressedThisFrame())
        {
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
        ResetButonColors();
    }
    public void GoToOptions()
    {
        _pauseMenu.SetActive(false);
        _optionsMenu.SetActive(true);
        ResetButonColors();
        _displayButton.Select();
    }
    public void ReturnPausePanel()
    {
        _optionsMenu.SetActive(false);
        _pauseMenu.SetActive(true);
        ResetButonColors();
        _ResumeButton.Select();
    }
    public void GoToMainMenu()
    {
        _pauseMenu.SetActive(false);
        _isPaused = false;
        SceneManager.LoadScene(_mainMenuScene);
    }
    private void ResetButonColors()
    {
        ResetScale(_ResumeButton);
        ResetScale(_optionButton);
        ResetScale(_exitButton);

        ResetScale(_displayButton);
        ResetScale(_resolutionButton);
        ResetScale(_BrilloButton);
        ResetScale(_volumeButton);


        DeleteAlphas(_resumeButtonSelectedImage);
        DeleteAlphas(_optionButtonSelectedImage);
        DeleteAlphas(_exitButtonSelectedImage);


        DeleteAlphas(_displayButtonSelectedImage);
        DeleteAlphas(_resolucionButtonSelectedImage);
        DeleteAlphas(_brilloButtonSelectedImage);
        DeleteAlphas(_volumeButtonSelectedImage);


        ResetAlphas(_resumeButtonDefaultImage);
        ResetAlphas(_optionButtonDefaultImage);
        ResetAlphas(_exitButtonDefaultImage);

        ResetAlphas(_displayButtonDefaultImage);
        ResetAlphas(_resolucionButtonDefaultImage);
        ResetAlphas(_brilloButtonDefaultImage);
        ResetAlphas(_volumeButtonDefaultImage);
    }
    void DeleteAlphas(Image image)
    {
        image.color = new Color(_exitButtonDefaultImage.color.r, _exitButtonDefaultImage.color.g, _exitButtonSelectedImage.color.b, 0);
    }
    void ResetAlphas(Image image)
    {
        image.color = new Color(_exitButtonDefaultImage.color.r, _exitButtonDefaultImage.color.g, _exitButtonSelectedImage.color.b, 255);
    }
    void ResetScale(Button button)
    {
        button.transform.localScale = new Vector3(1, 1, 1);
    }
}
