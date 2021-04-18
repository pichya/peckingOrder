using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMenuButtons : MonoBehaviour
{
    public GameObject buttonTiles;
    
    float scrollTimer;
    float minX;
    float maxX;
    float posX;
    float scrollVel;
    
    // Start is called before the first frame update
    void Start()
    {
        scrollTimer = 0;
        minX = -1450f;
        maxX = 167f;
        posX = 167f;
        scrollVel = -100f;
    }
    
    
    // Update is called once per frame
    void FixedUpdate()
    {
        posX += scrollVel*Time.deltaTime;
        Debug.Log(posX);
        if (posX>=maxX){
            posX = maxX;
            scrollVel*=-1;
        }
        if (posX<=minX){
            posX = minX;
            scrollVel*=-1;
        }
        GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, 168f);
    }
}
