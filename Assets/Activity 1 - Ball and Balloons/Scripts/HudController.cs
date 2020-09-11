﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    public void PrepareForBang(bool inBallonProx)
    {
        if (inBallonProx == true)
        {
            Debug.Log("Prepare for Bang");
            PrepareForBang_GO.SetActive(true);
            PrepareForBang_SFX = true;
            
            

        }

        else
        {
            PrepareForBang_GO.SetActive(false);
            PrepareForBang_SFX = false;
        }
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
}
