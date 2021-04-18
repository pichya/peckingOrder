using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenButtons : MonoBehaviour
{
    public GameObject globalVarsObj;
    
    // Start is called before the first frame update
    void Start()
    {
        globalVarsObj = GameObject.FindWithTag("GameController");
    }

    public void SelectMemoryGame(){
        SceneManager.LoadScene(1);
    }
      
}
