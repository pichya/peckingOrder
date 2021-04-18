using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMemoryGameStart : MonoBehaviour
{
    public MemoryGameLogic gameLogicObj;
    
    // Update is called once per frame
    void OnMouseDown()
    {
        if (gameLogicObj.GameState == "#StartGameWait"){
            if (Input.GetMouseButton(0))
            {
                gameLogicObj.GameState = "#StartGame";
            }
        }
    }
}
