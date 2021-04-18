using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPlayerMoveObject : MonoBehaviour
{
    public string PlayerState;
    public MemoryGameLogic gameLogic;
    
    public Transform RedPosition;
    public Transform GreenPosition;
    public Transform YellowPosition;
    public Transform BluePosition;
    public Transform CenterPosition;
    
    public SkinnedMeshRenderer skinnedMeshRenderer;
    
    float jumpTimer;
    int jumpCounter;
    float deltaX;
    float deltaZ;
    
    float floorY;
    
    float stretchVal;
    int squashVal;
    float[] lookupTable;
    Vector3 currentPos;
    
    // Start is called before the first frame update
    void Start()
    {
        currentPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        JumpToCenter();
        floorY = currentPos.y;
        lookupTable = new float[]{0f, 0.15f, 0.31f, 0.45f, 0.59f, 0.71f, 0.81f, 0.89f, 0.95f, 0.98f, 1f, 0.98f, 0.95f, 0.89f, 0.81f, 0.71f, 0.59f, 0.45f, 0.31f, 0.15f, 0f};
        
    }
    
    public void JumpToRed(){
        deltaX = RedPosition.position.x - currentPos.x;
        deltaZ = RedPosition.position.z - currentPos.z;
        jumpCounter = 0;
        jumpTimer = 0;
        PlayerState = "#JumpToPosition";
    }
    public void JumpToBlue(){
        deltaX = BluePosition.position.x - currentPos.x;
        deltaZ = BluePosition.position.z - currentPos.z;
        jumpCounter = 0;
        jumpTimer = 0;
        PlayerState = "#JumpToPosition";
    }
    public void JumpToYellow(){
        deltaX = YellowPosition.position.x - currentPos.x;
        deltaZ = YellowPosition.position.z - currentPos.z;
        jumpCounter = 0;
        jumpTimer = 0;
        PlayerState = "#JumpToPosition";
    }
    public void JumpToGreen(){
        deltaX = GreenPosition.position.x - currentPos.x;
        deltaZ = GreenPosition.position.z - currentPos.z;
        jumpCounter = 0;
        jumpTimer = 0;
        PlayerState = "#JumpToPosition";
    }
    public void JumpToCenter(){
        deltaX = CenterPosition.position.x - currentPos.x;
        deltaZ = CenterPosition.position.z - currentPos.z;
        jumpCounter = 0;
        jumpTimer = 0;
        PlayerState = "#JumpToCenter";
    }
    
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerState == "#JumpToPosition"){
            jumpTimer += jumpTimer + 0.01f * Time.deltaTime;
            if (jumpTimer>0.1){
                jumpCounter++;
                currentPos.x += deltaX*0.05f;
                currentPos.z += deltaZ*0.05f;
                currentPos.y = lookupTable[jumpCounter] * 0.1f + floorY;
                stretchVal = currentPos.y * 600f;
                if (stretchVal < 100f) {
                     skinnedMeshRenderer.SetBlendShapeWeight (3, (int)stretchVal);
                }
                this.transform.position = new Vector3(currentPos.x, currentPos.y , currentPos.z);
                if (jumpCounter == 20){
                    jumpTimer = 0;
                    squashVal = 40;
                    skinnedMeshRenderer.SetBlendShapeWeight (3, 0);
                    skinnedMeshRenderer.SetBlendShapeWeight (2, 40);
                    gameLogic.GameState = "#PlaySequence";
                    PlayerState = "#JumpDone";
                }
            }
        }
        //
        if (PlayerState == "#JumpToCenter"){
            jumpTimer += jumpTimer + 0.1f * Time.deltaTime;
            if (jumpTimer>0.1){
                jumpCounter++;
                currentPos.x += deltaX*0.05f;
                currentPos.z += deltaZ*0.05f;
                currentPos.y = lookupTable[jumpCounter] * 0.1f + floorY;
                stretchVal = currentPos.y * 600f;
                if (stretchVal < 100f) {
                     skinnedMeshRenderer.SetBlendShapeWeight (3, (int)stretchVal);
                }
                this.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z);
                if (jumpCounter == 20){
                    jumpTimer = 0;
                    squashVal = 40;
                    skinnedMeshRenderer.SetBlendShapeWeight (3, 0);
                    skinnedMeshRenderer.SetBlendShapeWeight (2, 40);
                    PlayerState = "#JumpDone";
                }
            }
 
        }
        //
        if (PlayerState == "#JumpDone"){
            jumpTimer += jumpTimer + 0.1f * Time.deltaTime;
            if (jumpTimer>0.01){
                squashVal += -10;
                skinnedMeshRenderer.SetBlendShapeWeight (2, squashVal);
                jumpTimer = 0;
                if (squashVal<=0){
                    PlayerState = "#JumpDoneWait";
                }
            }
        }
    }

}
