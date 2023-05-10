using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonSelected : MonoBehaviour
{
    private TMP_Text _TextComponent;
    // Start is called before the first frame update
    void Start()
    {
        _TextComponent = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Compare selected gameObject with referenced Button gameObject
         if(EventSystem.current.currentSelectedGameObject == this.gameObject)
         {
             Debug.Log(this.gameObject.name + " was selected");
             _TextComponent.color = Color.black;
         } else{
             _TextComponent.color = Color.white;

         }
    }
}
