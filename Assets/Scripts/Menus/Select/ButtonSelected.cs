using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonSelected : MonoBehaviour
{
    private TMP_Text _TextComponent;
    void Start()
    {
        _TextComponent = GetComponentInChildren<TMP_Text>();
    }

    void Update()
    {
         if(EventSystem.current.currentSelectedGameObject == this.gameObject)
         {
             _TextComponent.color = Color.black;
         } else{
             _TextComponent.color = Color.white;

         }
    }
}
