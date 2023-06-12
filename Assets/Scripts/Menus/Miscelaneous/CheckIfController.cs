using UnityEngine;
using UnityEngine.UI;
public class CheckIfController : MonoBehaviour
{
    private bool _controllerConnected;
    [SerializeField]
    private Image _backImage;
    [SerializeField]
    private Sprite _pcImage;
    [SerializeField]
    private Sprite _XBOXImage;
    void Update()
    {
        string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            if (names[x].Length == 19 || names[x].Length == 33)
            {
                _controllerConnected = true;
            }
            else
            {
                _controllerConnected = false;
            }
        }


        if (_controllerConnected)
        {
            _backImage.sprite = _XBOXImage;
        }
        else
        {
            _backImage.sprite = _pcImage;
        }
    }
}
