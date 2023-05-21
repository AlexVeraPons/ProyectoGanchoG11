using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ArrowLogic : MonoBehaviour
{
    [SerializeField]
    private ButtonLogic _buttonLogic;
    [SerializeField]
    private Sprite _leftArrow;
    [SerializeField]
    private Sprite _rightArrow;
    // private int _buttonIndex;
    [SerializeField]
    private Sprite _clickedArrow;
    

    private void OnEnable()
    {
        _buttonLogic.OnChangeLeft += ModifyLeftArrow;
        _buttonLogic.OnChangeRight += ModifyRightArrow;
    }
    private void OnDisable()
    {
        _buttonLogic.OnChangeLeft -= ModifyLeftArrow;
        _buttonLogic.OnChangeRight -= ModifyRightArrow;
    }
    void Start()
    {
        //_image = GetComponent<Image>();
    }

    void Update()
    {
        
    }
    void ModifyLeftArrow(int Index)
    {
        _leftArrow = _clickedArrow;
        Debug.Log("arrow esquerra");
    }
    void ModifyRightArrow(int Index)
    {
        _rightArrow = _clickedArrow;
        Debug.Log("arrow dreta");
    }
}
