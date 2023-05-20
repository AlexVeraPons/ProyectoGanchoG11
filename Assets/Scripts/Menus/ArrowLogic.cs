using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ArrowLogic : MonoBehaviour
{
    [SerializeField]
    private Sprite _selectedArrow;
    [SerializeField]
    private ButtonLogic _buttonLogic;
    [SerializeField]
    private Image _image;
    // private int _buttonIndex;

    private void OnEnable()
    {
        _buttonLogic.OnChangeLeft += Modify;
        _buttonLogic.OnChangeRight += Modify;
    }
    private void OnDisable()
    {
        _buttonLogic.OnChangeLeft -= Modify;
        _buttonLogic.OnChangeRight -= Modify;
    }
    // Start is called before the first frame update
    void Start()
    {
        //_image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Modify(int Index)
    {
        Debug.Log("modify");
    }
}
