using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneOnDeath : MonoBehaviour, IKillable
{
    public void Kill()
    {
        print("eeeeeeeeeeeeeeeeeeeeee");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
