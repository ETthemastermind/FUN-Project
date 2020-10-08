using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HudController : MonoBehaviour
{
    public MasterTelemetrySystem TelSystem;
    public AudioSource Canvas_AudioSource;
    public GameObject Player;
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

    [Header("Settings Audio Clips")]
    public AudioClip OpenSettings_Audio;
    public AudioClip CloseSettings_Audio;

    [Header ("Toggle Camera and Movement")]
    public bool ControlToggle = true; //true = movement, false = camera
    public Sprite CameraSprite;
    public GameObject CameraControlsCanvas;
    public Sprite MovementSprite;
    public GameObject MovementControlsCanvas;

    [Header("Settings Menus")]
    
    public GameObject MusicMenu_Canvas;
    public AudioClip OpeningMusicMenu_Audio;

    public GameObject GameMenu_Canvas;
    public AudioClip OpeningGameMenu_Audio;
    


    public GameObject ProfileMenu_Canvas;
    public AudioClip OpeningProfileMenu_Audio;

    [Header("Minimap and Wing Mirror Refs")]
    public GameObject Minimap;
    public GameObject Mirrors;

    [Header("Comms Menu")]
    public GameObject CommsMenu;

    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1; //makes sure that time scale for this scene is set to 1
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<Activity1Settings>();
        MaxScore = GameController.Score_Goal;
        Player = GameObject.FindGameObjectWithTag("Player");
        TelSystem = GameObject.FindGameObjectWithTag("TelSystem").GetComponent<MasterTelemetrySystem>();
        //Canvas_AudioSource = GameObject.FindGameObjectWithTag("CanvasAudioSource").GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        CurrentGameTime += Time.deltaTime;
        Timer.GetComponent<TextMeshProUGUI>().text = "Time: " + (Mathf.RoundToInt(CurrentGameTime));
        
        /*
        if (PrepareForBang_SFX == true)
        {
            AudioSource AS = Camera.main.GetComponent<AudioSource>();
            if (AS.isPlaying == false)
            {
                //AS.PlayOneShot(inBallonProx_Clip);

            }
        }
        */
        

    }

    public void IncrementScore(int PlayerScore) //increment the player's score, gets called from the BallController script
    {
        ScoreText.GetComponent<TextMeshProUGUI>().text = PlayerScore.ToString() ; //adjust the text on the score object to reflect the player's current score
        GameController.GetComponent<Activity1Settings>().NumberOfBalloonsPopped++;
        if (PlayerScore >= MaxScore)
        {
            
            //ResetButton.SetActive(true); //show the reset button
            GameController.CompletedTime = Mathf.RoundToInt(CurrentGameTime);
            
            GameComplete = true;
            GameController.Win_PopUp();
            Time.timeScale = 0; //set the time scale to 0 i.e. pause the game
        }

    }

    public void ResetGame() //fucntion to reset the game
    {
        int CurrentScene = SceneManager.GetActiveScene().buildIndex; //gets the current scene index of the level (in case i end up reusing the hud controller script in multiple scene)
        SceneManager.LoadScene(CurrentScene); //loads the current scene as defined in the line above i.e reset the scene
        
    }
   

    public void ButtonClick(AudioClip SpeechSynth) //play a button click sound effect
    {
        //Camera.main.GetComponent<AudioSource>().PlayOneShot(ButtonClick_SFX);
        if (Canvas_AudioSource.isPlaying == true )
        {
            Canvas_AudioSource.Stop();
            
        }
        Canvas_AudioSource.PlayOneShot(ButtonClick_SFX);
        Canvas_AudioSource.PlayOneShot(SpeechSynth);
        TelSystem.AddLine(EventSystem.current.currentSelectedGameObject.name + "button clicked");
        TelSystem.gameObject.GetComponent<ReplayTelemetry>().DatawithButtonPress(EventSystem.current.currentSelectedGameObject.gameObject);
        Debug.Log(EventSystem.current.currentSelectedGameObject.gameObject);
    }

    public void OpenCloseSettings() //function to open and close the settings menu
    {
        if (SettingsMenu.active == true) //if the settings menu is open
        {
            SettingsMenu.SetActive(false); //close the menu
            //Controls.SetActive(true);
            CommsMenu.SetActive(false);
            Time.timeScale = 1; //set the time scale back to 1
            Canvas_AudioSource.PlayOneShot(CloseSettings_Audio);
            TelSystem.AddLine("Settings menu closed");
            GameController.SaveData();
            Player.GetComponent<ActivityOneBallFunctions>().ReposBall();



        }
        else //do the opposite
        {
            SettingsMenu.SetActive(true);
            //Controls.SetActive(false);
            CommsMenu.SetActive(false);
            Time.timeScale = 0;
            Canvas_AudioSource.PlayOneShot(OpenSettings_Audio);
            TelSystem.AddLine("Settings menu opened");
        }

    }

    public void OpenCloseComms()
    {
        if (CommsMenu.active == true)
        {
            Time.timeScale = 1;
            CommsMenu.SetActive(false);
            TelSystem.AddLine("Communication paged closed");
        }
        else
        {
            Time.timeScale = 0;
            CommsMenu.SetActive(true);
            TelSystem.AddLine("Communication paged opened");
        }
    }

    
    
    public void Camera_Movement_Toggle(Image ButtonImage) //switch between player movement and camera movement
    {
        if (ControlToggle == true) //if the movement controls are active
        {
            ControlToggle = false;
            ButtonImage.sprite = MovementSprite;
            MovementControlsCanvas.SetActive(false);
            CameraControlsCanvas.SetActive(true);
            TelSystem.AddLine("Camera Controls activated and Movement Controls deactivated");


        }
        else //if the camera controls are active
        {
            ControlToggle = true;
            ButtonImage.sprite = CameraSprite;
            MovementControlsCanvas.SetActive(true);
            CameraControlsCanvas.SetActive(false);
            TelSystem.AddLine("Movement Controls activated and Camera Controls deactivated");
        }
    }
    

    #region Settings Menu Functions
    public void MusicMenuOn()
    {
        MusicMenu_Canvas.SetActive(true);
        GameMenu_Canvas.SetActive(false);
        ProfileMenu_Canvas.SetActive(false);
        Canvas_AudioSource.PlayOneShot(OpeningMusicMenu_Audio);
        TelSystem.AddLine("Music tab opened");
    }
    public void GameMenuOn()
    {
        MusicMenu_Canvas.SetActive(false);
        GameMenu_Canvas.SetActive(true);
        ProfileMenu_Canvas.SetActive(false);
        Canvas_AudioSource.PlayOneShot(OpeningGameMenu_Audio);
        TelSystem.AddLine("Game tab opened");
    }
    public void ProfileMenuOn()
    {
        MusicMenu_Canvas.SetActive(false);
        GameMenu_Canvas.SetActive(false);
        ProfileMenu_Canvas.SetActive(true);
        Canvas_AudioSource.PlayOneShot(OpeningProfileMenu_Audio);
        TelSystem.AddLine("Profile tab opened");
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

    public void ToggleBetweenMMandWM()
    {
        if (Minimap.active == true) //if the minimap is active
        {
            Minimap.SetActive(false); //deactive the minimap gameobject
            Mirrors.SetActive(true); //activate the mirrors gameobject
            GameController.Save.Mirrors = true;
            GameController.Save.MiniMap = false;

            TelSystem.AddLine("Mirror UI activated");
        }
        else
        {
            Minimap.SetActive(true); //active the minimap gameobject
            Mirrors.SetActive(false); //deactivate the mirrors gameobject
            GameController.Save.Mirrors = false;
            GameController.Save.MiniMap = true;
            TelSystem.AddLine("Minimap UI activated");
        }
    }



    

    

}
