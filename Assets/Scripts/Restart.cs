using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    //allows access to scene called SampleScene
    private string Level1 = "Level1";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if space is pressed
        if(Input.GetKey(KeyCode.Space))
        {
            //change to main scene
            SceneManager.LoadScene(Level1);
        }
    }
}
