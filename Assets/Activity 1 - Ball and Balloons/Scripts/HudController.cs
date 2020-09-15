﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    public TextMeshProUGUI ScoreText; //reference to the score text
    public TextMeshProUGUI Timer; //reference to the timer text

    public int MaxScore;
    

    public GameObject ResetButton; //reference for the reset button
    public GameObject BalloonSpawner;
    public GameObject PrepareForBang_GO;
    private bool PrepareForBang_SFX;
    
    public float CurrentGameTime; //float for the maxiumum playtime 
    public bool GameComplete;

    public Activity1Settings GameController;
    public AudioClip inBallonProx_Clip;
    public AudioClip ButtonClick_SFX;

    //Activate Menu
    public GameObject SettingsMenu;
    public GameObject Controls;


    public bool ControlToggle = true; //true = movement, false = camera
    public Sprite CameraSprite;
    public GameObject CameraControlsCanvas;
    public Sprite MovementSprite;
    public GameObject MovementControlsCanvas;

    [Header("Settings Menus")]
    public GameObject MusicMenu_Canvas;
    public GameObject GameMenu_Canvas;
    public GameObject ProfileMenu_Canvas;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1; //makes sure that time scale for this scene is set to 1
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<Activity1Settings>();
        MaxScore = GameController.Score_Goal;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentGameTime += Time.deltaTime;
        Timer.GetComponent<TextMeshProUGUI>().text = "Time: " + (Mathf.RoundToInt(CurrentGameTime));
        
        if (PrepareForBang_SFX == true)
        {
            AudioSource AS = Camera.main.GetComponent<AudioSource>();
            if (AS.isPlaying == false)
            {
                //AS.PlayOneShot(inBallonProx_Clip);

            }
        }
        

    }

    public void IncrementScore(int PlayerScore) //increment the player's score, gets called from the BallController script
    {
        ScoreText.GetComponent<TextMeshProUGUI>().text = PlayerScore.ToString() ; //adjust the text on the score object to reflect the player's current score

        if (PlayerScore >= MaxScore)
        {
            Time.timeScale = 0; //set the time scale to 0 i.e. pause the game
            ResetButton.SetActive(true); //show the reset button
            GameComplete = true;
        }

    }

    public void ResetGame() //fucntion to reset the game
    {
        int CurrentScene = SceneManager.GetActiveScene().buildIndex; //gets the current scene index of the level (in case i end up reusing the hud controller script in multiple scene)
        SceneManager.LoadScene(CurrentScene); //loads the current scene as defined in the line above i.e reset the scene
        
    }
   

    public void ButtonClick()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(ButtonClick_SFX);
    }

    public void OpenCloseSettings()
    {
        if (SettingsMenu.active == true)
        {
            SettingsMenu.SetActive(false);
            Controls.SetActive(true);
            Time.timeScale = 1;


        }
        else
        {
            SettingsMenu.SetActive(true);
            Controls.SetActive(false);
            Time.timeScale = 0;
        }

    }

    public void Camera_Movement_Toggle(Image ButtonImage)
    {
        if (ControlToggle == true) //if the movement controls are active
        {
            ControlToggle = false;
            ButtonImage.sprite = MovementSprite;
            MovementControlsCanvas.SetActive(false);
            CameraControlsCanvas.SetActive(true);

            
        }
        else //if the camera controls are active
        {
            ControlToggle = true;
            ButtonImage.sprite = CameraSprite;
            MovementControlsCanvas.SetActive(true);
            CameraControlsCanvas.SetActive(false);
        }
    }

    #region Settings Menu Functions
    public void MusicMenuOn()
    {
        MusicMenu_Canvas.SetActive(true);
        GameMenu_Canvas.SetActive(false);
        ProfileMenu_Canvas.SetActive(false);
    }
    public void GameMenuOn()
    {
        MusicMenu_Canvas.SetActive(false);
        GameMenu_Canvas.SetActive(true);
        ProfileMenu_Canvas.SetActive(false);
    }
    public void ProfileMenuOn()
    {
        MusicMenu_Canvas.SetActive(false);
        GameMenu_Canvas.SetActive(false);
        ProfileMenu_Canvas.SetActive(true);
    }
    #endregion

    public void ButtonActivateToggle(Toggle Toggle)
    {
        if (Toggle.isOn == true)
        {
            Toggle.isOn = false;
        }
        else
        {
            Toggle.isOn = true;
        }
    }

    

}
