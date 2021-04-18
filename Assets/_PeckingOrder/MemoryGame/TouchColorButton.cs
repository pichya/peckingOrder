using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchColorButton : MonoBehaviour
{
    public MemoryGameLogic gameLogicObj;
    public int ColorNumber;
    public float ChickenRotation;
    public string MoveState;
    public Animator chickenAnimator;
    float moveTimer;
    float moveEnd;
    float rotateIncrement;
    Vector3 movePosition;
    
    public bool moveFlag;
    Rigidbody m_Rigidbody;
    
    void Start()
    {
        ChickenRotation = Random.Range(0f,359f);
        transform.localEulerAngles = new Vector3(0f, ChickenRotation, 0f);
        //
        moveTimer = 0;
        moveEnd = 1f;
        moveFlag = false;
        rotateIncrement = 0f;
        movePosition = new Vector3(0f, 0f, 0f);
        m_Rigidbody = GetComponent<Rigidbody>();
        MoveState = "#Idle";
    }
    
    void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            gameLogicObj.TouchColor(ColorNumber);
        }
    }
    
    void FixedUpdate(){
        switch(MoveState){
            case "#Idle":
            m_Rigidbody.velocity = new Vector3(0f,0f,0f);
            chickenAnimator.SetBool("Walk",false);
            chickenAnimator.SetBool("Eat",false);
            chickenAnimator.SetBool("Turn Head",false);
            MoveState = "#IdleWait";
            break;
            case "#IdleWait":
            float idleNum = Random.Range(0f, 100000f);
            if (idleNum<150){
                MoveState = "#Eat";
            }
            if (idleNum>99800){
                if (idleNum>99850 && moveFlag){
                    MoveState = "#Move";
                }else{
                    MoveState = "#WalkInPlace";
                }
            }
            break;
            case "#Eat":
             m_Rigidbody.velocity = new Vector3(0f,0f,0f);
             moveTimer = 0f;
             moveEnd = Random.Range(0.1f, 0.6f);
             rotateIncrement = Random.Range(-0.1f, 0.1f);
             chickenAnimator.SetBool("Eat",true);
             MoveState = "#EatWait";
            break;
            case "#EatWait":
            moveTimer = moveTimer + 0.1f * Time.deltaTime;
            ChickenRotation += rotateIncrement;
            transform.localEulerAngles = new Vector3(0f, ChickenRotation, 0f);
            if (moveTimer>moveEnd){
                MoveState = "#Idle";
            }
            break;
            case "#WalkInPlace":
            m_Rigidbody.velocity = new Vector3(0f,0f,0f);
            moveTimer = 0f;
            moveEnd = Random.Range(0.1f, 0.3f);
            rotateIncrement = Random.Range(-1.5f, 1.5f);
            chickenAnimator.SetBool("Walk",true);
            MoveState = "#WalkInPlaceWait";
            break;
            case "#WalkInPlaceWait":
            moveTimer = moveTimer + 0.1f * Time.deltaTime;
            ChickenRotation += rotateIncrement;
            transform.localEulerAngles = new Vector3(0f, ChickenRotation, 0f);
            if (moveTimer>moveEnd){
                MoveState = "#Idle";
            }
            break;
            case "#TurnHead":
            m_Rigidbody.velocity = new Vector3(0f,0f,0f);
            moveTimer = 0f;
            chickenAnimator.SetBool("Walk",false);
            chickenAnimator.SetBool("Eat",false);
            chickenAnimator.SetBool("Turn Head",true);
            MoveState = "#TurnHeadWait";
            break;
            case "#TurnHeadWait":
            moveTimer = moveTimer + 0.1f * Time.deltaTime;
            if (moveTimer>0.005){
                MoveState = "#Idle";
            }
            break;
            case "#Move":
            moveTimer = 0f;
            movePosition = transform.TransformPoint(Random.Range(-0.45f,0.45f), 0f, Random.Range(-0.45f,0.45f));
            transform.LookAt(movePosition);
            chickenAnimator.SetBool("Walk",true);
            m_Rigidbody.velocity = transform.forward * Random.Range(0.05f,0.12f);
            MoveState = "#MoveWait";
            break;
            case "#MoveWait":
            moveTimer = moveTimer + 0.1f * Time.deltaTime;
            if (moveTimer>0.4){
                MoveState = "#Idle";
            }
            break;
            case "#End":
            m_Rigidbody.velocity = new Vector3(0f,0f,0f);
            chickenAnimator.SetBool("Walk",false);
            chickenAnimator.SetBool("Eat",false);
            chickenAnimator.SetBool("Turn Head",false);
            MoveState = "#EndWait";
            break;
        }
    }
     
}
