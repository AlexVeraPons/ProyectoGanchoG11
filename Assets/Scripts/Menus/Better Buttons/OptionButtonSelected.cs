using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class OptionButtonSelected : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _TextName;
    [SerializeField]
    private TMP_Text _TextInfo;
    [SerializeField]
    private Image _leftArrow;
    [SerializeField]
    private Image _rightArrow;
    [SerializeField]
    private Sprite _selectedArrow;
    [SerializeField]
    private Sprite _defaultArrow;
    // Start is called before the first frame update
    void Start()
    {
        //_TextName = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Compare selected gameObject with referenced Button gameObject
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            _TextName.color = Color.black;
            _TextInfo.color = Color.black;
            _leftArrow.sprite = _selectedArrow;
            _rightArrow.sprite = _selectedArrow;
        }
        else
        {
            _TextName.color = Color.white;
            _TextInfo.color = Color.white;
            _leftArrow.sprite = _defaultArrow;
            _rightArrow.sprite = _defaultArrow;

        }
    }
}
