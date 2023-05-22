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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            _TextName.color = Color.black;
            _sliderBackground.color = Color.black;
            _sliderToggle.color = Color.black;
            // _TextInfo.color = Color.black;
            // _leftArrow.sprite = _selectedArrow;
            // _rightArrow.sprite = _selectedArrow;
        }
        else
        {
            _TextName.color = Color.white;
            _sliderBackground.color = Color.white;
            _sliderToggle.color = Color.white;
            // _TextInfo.color = Color.white;
            // _leftArrow.sprite = _defaultArrow;
            // _rightArrow.sprite = _defaultArrow;

        }
    }
}
