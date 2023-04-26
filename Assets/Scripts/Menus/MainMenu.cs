using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    //options pa deshardcodear, crear un string y anar al string


    [SerializeField] private string GameScene;
    public void LoadGame() //deshardcodear a quin lvl van
    {
        Debug.Log("Loading Game...");
        SceneManager.LoadScene(GameScene); 
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
    public void LoadOptions()
    {
        Debug.Log("Loading Options Menu...");
        SceneManager.LoadScene("Options"); 
    }
}
