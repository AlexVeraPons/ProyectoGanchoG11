using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class OptionButtonSelected : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _TextName;
    [SerializeField]
    private TMP_Text _TextInfo;
    // Start is called before the first frame update
    void Start()
    {
        _TextName = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Compare selected gameObject with referenced Button gameObject
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            _TextName.color = Color.black;
            _TextInfo.color = Color.black;
        }
        else
        {
            _TextName.color = Color.white;
            _TextInfo.color = Color.white;

        }
    }
}
