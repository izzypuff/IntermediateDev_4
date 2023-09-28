using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public so that the button can access it
    public void LoadGame()
    {
        //use the scene manager to load the game scene
        SceneManager.LoadScene("Week4");
    }
}
