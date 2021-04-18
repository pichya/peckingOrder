using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameLogic : MonoBehaviour
{
    public string GameState;
    //public GameObject PlayerObj;
    
    public GameObject RedLightObj;
    public GameObject BlueLightObj;
    public GameObject YellowLightObj;
    public GameObject GreenLightObj;
    
    public TouchColorButton redButton;
    public TouchColorButton blueButton;
    public TouchColorButton yellowButton;
    public TouchColorButton greenButton;
    
    float gameTimer;
    public Color dimRedColor;
    public Color highlightRedColor;
    public Color dimBlueColor;
    public Color highlightBlueColor;
    public Color dimYellowColor;
    public Color highlightYellowColor;
    public Color dimGreenColor;
    public Color highlightGreenColor;
    //
    public Color greyColor;
    
    int ColorCounter;
    int PlayerSelectColor;
    int BlinkCounter = 0;
    
    List<int> ColorSequence;
    
    // Start is called before the first frame update
    void Start()
    {
        //highlightColor = new Color(1f,1f,1f,1f);
        //dimColor = new Color(0.65f,0.65f,0.65f,1f);
        //
        //
        ColorSequence = new List<int>();
        DimAll();
        ColorCounter = 0;
        gameTimer = 0;
        GameState = "#StartGame";
    }
    
    public AudioClip beepSound;
    public AudioClip cheerSound;
    public AudioClip endSound;
    private AudioSource audio;
    
    public void BeepSound(){
        audio = GetComponent<AudioSource>();
        audio.clip = beepSound;
        audio.Play();
    }
    
    public void CheerSound(){
        audio = GetComponent<AudioSource>();
        audio.clip = cheerSound;
        audio.Play();
    }
    
    public void EndSound(){
           audio = GetComponent<AudioSource>();
           audio.clip = endSound;
           audio.Play();
       }
    
    public void TouchColor(int colorNum){
        if (GameState == "#PlayerInputWait"){
            switch(colorNum){
                case 0:
                BeepSound();
                RedLightGlow();
                break;
                case 1:
                BeepSound();
                BlueLightGlow();
                break;
                case 2:
                BeepSound();
                YellowLightGlow();
                break;
                case 3:
                BeepSound();
                GreenLightGlow();
                break;
            }
            if (colorNum == ColorSequence[ColorCounter]){
                gameTimer = 0;
                GameState = "#PlayerSelectCorrectWait";
            }else{
                gameTimer = 0;
                EndSound();
                GameState = "#PlayerSelectWrongWait";
            }
        }
    }
    
    void RedLightGlow(){
        RedLightObj.GetComponent<Renderer>().material.color = highlightRedColor;
    }
    
    void BlueLightGlow(){
        BlueLightObj.GetComponent<Renderer>().material.color = highlightBlueColor;
    }
    
    void YellowLightGlow(){
        YellowLightObj.GetComponent<Renderer>().material.color = highlightYellowColor;
    }
    
    void GreenLightGlow(){
        GreenLightObj.GetComponent<Renderer>().material.color = highlightGreenColor;
    }
    
    void RedLightDim(){
        RedLightObj.GetComponent<Renderer>().material.color = dimRedColor;
    }
    
    void BlueLightDim(){
        BlueLightObj.GetComponent<Renderer>().material.color = dimBlueColor;
    }
    
    void YellowLightDim(){
        YellowLightObj.GetComponent<Renderer>().material.color = dimYellowColor;
    }
    
    void GreenLightDim(){
        GreenLightObj.GetComponent<Renderer>().material.color = dimGreenColor;
    }
    
    void DimAll(){
        RedLightDim();
        BlueLightDim();
        YellowLightDim();
        GreenLightDim();
    }
    
    void GlowAll(){
        RedLightGlow();
        BlueLightGlow();
        YellowLightGlow();
        GreenLightGlow();
    }
    
    void GreyAll(){
        RedLightObj.GetComponent<Renderer>().material.color = greyColor;
        BlueLightObj.GetComponent<Renderer>().material.color = greyColor;
        YellowLightObj.GetComponent<Renderer>().material.color = greyColor;
        GreenLightObj.GetComponent<Renderer>().material.color = greyColor;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        switch(GameState){
            case "#StartGameWait":
            break;
            case "#StartGame":
            ColorSequence = new List<int>();
            DimAll();
            ColorCounter = 0;
            gameTimer = 0;
            GameState = "#LevelColorCorrect";
            break;
            //
            case "#LevelColorCorrect":
            StartLevel();
            CheerSound();
            LevelColorCorrect();
            //Debug.Log("#LevelColorCorrect");
            break;
            case "#ChooseRandomColor":
            //Debug.Log("#ChooseRandomColor");
            ChooseRandomColor();
            break;
            case "#JumpToColor":
            //Debug.Log("#ChooseRandomColor");
            JumpToColor();
            break;
            case "#PlaySequence":
            //Debug.Log("#PlaySequence");
            PlayColorSequence();
            break;
            case "#ShowColorWait":
            //Debug.Log("#ShowColorWait");
            ShowColorWait();
            break;
            case "#DimColorWait":
            //Debug.Log("#DimColorWait");
            DimColorWait();
            break;
            case "#EndSequence":
            EndColorSequence();
            break;
            //
            case "#PlayerInputWait":
            break;
            case "#PlayerSelection":
            break;
            case "#PlayerSelectCorrectWait":
            gameTimer = gameTimer + 0.1f * Time.deltaTime;
            if (gameTimer>0.12){
                DimAll();
                ColorCounter++;
                if (ColorSequence.Count > 3){
                    StartMoveFlag();
                }
                if (ColorCounter >= ColorSequence.Count){
                    gameTimer = 0;
                    ColorCounter = 0;
                    GameState = "#LevelColorCorrect";
                    //GameState = "#ChooseRandomColor";
                }else{
                    GameState = "#PlayerInputWait";
                }
            }
            break;
            case "#PlayerSelectWrongWait":
            gameTimer = gameTimer + 0.1f * Time.deltaTime;
            if (gameTimer>0.05){
                DimAll();
                ColorCounter = 0;
                gameTimer = 0;
                GlowAll();
                GameState = "#BlinkSequenceOn";
            }
            break;
            case "#BlinkSequenceOn":
                gameTimer = gameTimer + 0.1f * Time.deltaTime;
                if (gameTimer>0.01){
                    DimAll();
                    BlinkCounter++;
                    if (BlinkCounter>5){
                        gameTimer = 0;
                        BlinkCounter = 0;
                        GameState = "#GameOver";
                    }else{
                        gameTimer = 0;
                        GameState = "#BlinkSequenceOff";
                    }
                }
            break;
            case "#BlinkSequenceOff":
                gameTimer = gameTimer + 0.1f * Time.deltaTime;
                if (gameTimer>0.01){
                    GlowAll();
                    gameTimer = 0;
                    GameState = "#BlinkSequenceOn";
                }
            break;
            case "#EndPlayerSequenceWait":
            break;
            case "#GameOver":
            EndMove();
            GreyAll();
            GameState = "#GameOverWait";
            break;
            case "#GameOverWait":
            break;
        }
    }
    
    void EndColorSequence(){
        gameTimer = gameTimer + 0.1f * Time.deltaTime;
        if (gameTimer>0.02){
            gameTimer = 0;
            //GameState = "#ChooseRandomColor";
            ColorCounter = 0;
            GameState = "#PlayerInputWait";
        }
    }
    
    void ShowColorWait(){
        gameTimer = gameTimer + 0.1f * Time.deltaTime;
        if (gameTimer>0.17){
            DimAll();
            gameTimer = 0;
            GameState = "#DimColorWait";
        }
    }
    
    void DimColorWait(){
        gameTimer = gameTimer + 0.1f * Time.deltaTime;
        if (gameTimer>0.12){
            if (ColorCounter>=ColorSequence.Count){
            // End of Color Sequence
                gameTimer = 0;
                //PlayerObj.GetComponent<MemoryPlayerMoveObject>().JumpToCenter();
                GameState = "#EndSequence";
            }else{
                gameTimer = 0;
                GameState = "#PlaySequence";
                //GameState = "#JumpToColor";
            }
        }
    }
    
    void StartMoveFlag(){
        redButton.moveFlag = true;
        blueButton.moveFlag = true;
        yellowButton.moveFlag = true;
        greenButton.moveFlag = true;
    }
    
    void EndMoveFlag(){
        redButton.moveFlag = false;
        blueButton.moveFlag = false;
        yellowButton.moveFlag = false;
        greenButton.moveFlag = false;
    }

    void StartLevel(){
        redButton.MoveState = "#TurnHead";
        blueButton.MoveState = "#TurnHead";
        yellowButton.MoveState = "#TurnHead";
        greenButton.MoveState = "#TurnHead";
    }
    
    void EndMove(){
        redButton.MoveState = "#End";
        blueButton.MoveState = "#End";
        yellowButton.MoveState = "#End";
        greenButton.MoveState = "#End";
    }
    
    void LevelColorCorrect(){
        gameTimer = gameTimer + 0.1f * Time.deltaTime;
        if (gameTimer>0.6){
            gameTimer = 0;
            GameState = "#ChooseRandomColor";
        }
    }
               
    void ChooseRandomColor(){
        float RandomNum = Random.Range(0f, 4f);
            //
            if (RandomNum>=0f && RandomNum<1f){
                //Red
                ColorSequence.Add(0);
            }
            if (RandomNum>=1f && RandomNum<2f){
                //Blue
                ColorSequence.Add(1);
            }
            if (RandomNum>=2f && RandomNum<3f){
                //Yellow
                ColorSequence.Add(2);
             }
            if (RandomNum>=3f){
                //Green
                ColorSequence.Add(3);
             }
            ColorCounter = 0;
            //
            GameState = "#PlaySequence";
            //GameState = "#JumpToColor";
    }
    
    void JumpToColor(){/*
        int ColorNum = ColorSequence[ColorCounter];
        switch (ColorNum){
            case 0:
            PlayerObj.GetComponent<MemoryPlayerMoveObject>().JumpToRed();
            break;
            case 1:
            PlayerObj.GetComponent<MemoryPlayerMoveObject>().JumpToBlue();
            break;
            case 2:
            PlayerObj.GetComponent<MemoryPlayerMoveObject>().JumpToYellow();
            break;
            case 3:
            PlayerObj.GetComponent<MemoryPlayerMoveObject>().JumpToGreen();
            break;
        }
        GameState = "#JumpToColorWait";
        */
    }
    
    void PlayColorSequence(){
        int ColorNum = ColorSequence[ColorCounter];
        switch (ColorNum){
            case 0:
            BeepSound();
            RedLightGlow();
            break;
            case 1:
            BeepSound();
            BlueLightGlow();
            break;
            case 2:
            BeepSound();
            YellowLightGlow();
            break;
            case 3:
            BeepSound();
            GreenLightGlow();
            break;
        }
        ColorCounter++;
        GameState = "#ShowColorWait";
    }
    
}

