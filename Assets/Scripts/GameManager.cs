using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //other scenes
    private string Level1 = "Level1";
    private string Level2 = "Level2";
    private string Victory = "Victory";


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Level1")
        {
            SceneManager.LoadScene(Level1);
        }

        if (scene.name == "Level2")
        {
            SceneManager.LoadScene(Level2);
        }
    }

    public void Teleport()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Level1")
        {
            SceneManager.LoadScene(Level2);
        }

        if (scene.name == "Level2")
        {
            SceneManager.LoadScene(Victory);
        }
    }
}
