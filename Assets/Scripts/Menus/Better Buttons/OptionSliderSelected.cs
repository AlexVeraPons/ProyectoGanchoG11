using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class OptionSliderSelected : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _TextName;
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private Image _sliderBackground;
    [SerializeField]
    private Image _sliderToggle;
    [SerializeField]
    private Image _leftArrow;
    [SerializeField]
    private Image _rightArrow;
    [SerializeField]
    private Sprite _selectedArrow;
    [SerializeField]
    private Sprite _defaultArrow;
    void Start()
    {

    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            _TextName.color = Color.black;
            _sliderBackground.color = Color.black;
            _sliderToggle.color = Color.black;
            _leftArrow.sprite = _selectedArrow;
            _rightArrow.sprite = _selectedArrow;
        }
        else
        {
            _TextName.color = Color.white;
            _sliderBackground.color = Color.white;
            _sliderToggle.color = Color.white;
            _leftArrow.sprite = _defaultArrow;
            _rightArrow.sprite = _defaultArrow;

        }
    }
}
