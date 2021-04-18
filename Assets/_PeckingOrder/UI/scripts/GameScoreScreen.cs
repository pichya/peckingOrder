using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScoreScreen : MonoBehaviour
{
    public GameObject InfoScreen;
    
    // Start is called before the first frame update
    void Start()
    {
         HideAllScreens();
    }

    public void ShowInfo(){
        HideAllScreens();
        InfoScreen.SetActive(true);
    }
    
    public void HideAllScreens(){
        InfoScreen.SetActive(false);
    }
    
    public void RestartGame(){
        SceneManager.LoadScene(0);
    }
}
