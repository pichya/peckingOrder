using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public int GameSelectionNumber;
    
    void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void SetGameSelection(int gameNumber){
        Debug.Log("gameNumber: "+gameNumber);
        GameSelectionNumber = gameNumber;
    }
    
    public int GetGameSelection(){
        return GameSelectionNumber;
    }
}
