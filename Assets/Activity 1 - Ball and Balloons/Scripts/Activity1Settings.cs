using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Activity1Settings : MonoBehaviour
{
    public GameObject PlayerBall;
    public MasterTelemetrySystem TelSystem;

    // Rotating Ball Texture
    public AnimatedTexture AnimatedTexture;
    public Toggle AnimTextureToggle;

    // Sound Effects
    //public Toggle SoundFXToggle;
    // reset button

    //Number of Balloons per round
    public BalloonSpawnerV2 BalloonSpawner;
    public int MinBalloons;
    public int MaxBalloons;
    public TMP_Text NumOfBalloonText;

    /*
    //Number of Rounds
    public HudController RoundNumber;
    public int MinRounds;
    public int MaxRounds;
    public TMP_Text RoundNumberText;
    */
    //Ball Speed? also need to edit the ball rotation...

    [Header("Game Attributes")]
    //public bool AnimatedTexturesActive;
    public bool SoundActive;
    public int NumberOfBalloonsToSpawn;
    public int Score_Goal;
    public int CompletedTime;
    public int NumberOfBalloonsPopped;

    private int BallSize = 1;
    private Vector3 DefaultBallSize;
    private Vector3 DefaultBallPos;


    public TMP_Text BallSpeedText;
    public TMP_Text WinText;
    public GameObject Win_PopUpCanvas;
    public GameObject[] OtherCanvases;
    public TMP_Text BallSizeText;

    [Header("Grid Refs")]
    public GridV3 Grid;
    public TextMeshProUGUI GridText;


    [Header("AudioClips")]
    public AudioSource[] AudioSources;
    
    public AudioClip IncreasingBallSpeed_Audio;
    public AudioClip DecreasingBallSpeed_Audio;

    public AudioClip IncreasingBallSize_Audio;
    public AudioClip DecreasingBallSize_Audio;

    public AudioClip IncreasingGridSize_Audio;
    public AudioClip DecreasingGridSize_Audio;

    [Header("Diagonal Controls")]
    public bool DiagonalControlsActive;
    public GameObject[] DiagonalControlsObjects;

    public ActivityOneSave Save;

    [Header("References related to loading data")]
    public Toggle AnimTex_Toggle;

    private void Awake()

    {
        //LoadPrefs();
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerBall = GameObject.FindGameObjectWithTag("Player");
        AnimatedTexture = PlayerBall.GetComponent<AnimatedTexture>();
        BalloonSpawner = GameObject.FindGameObjectWithTag("BalloonSpawner").GetComponent<BalloonSpawnerV2>();
        Application.targetFrameRate = -1;
        Grid = GameObject.FindGameObjectWithTag("GridObject").GetComponent<GridV3>();
        TelSystem = GameObject.FindGameObjectWithTag("TelSystem").GetComponent<MasterTelemetrySystem>();

        DefaultBallSize = PlayerBall.transform.localScale;
        DefaultBallPos = PlayerBall.transform.localPosition;

        


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData(Application.streamingAssetsPath + "/EthanActivity1Save.FUNSAV");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            LoadData(Application.streamingAssetsPath + "/DEFAULT_Activity1Save.FUNSAV");
        }
    }


    public void ToggleAnimatedTexture()
    {
        if (AnimTextureToggle.isOn == true)
        {
            Debug.Log("Turn on animated ball texture");
            AnimatedTexture.Active = true;
            Save.AnimatedTexture = true;
            TelSystem.AddLine("Animated ball texture turned on");
            
        }

        else
        {
            Debug.Log("Turn off animated ball texture");
            AnimatedTexture.Active = false ;
            Save.AnimatedTexture = false;
            TelSystem.AddLine("Animated ball texture turned off");

        }
    }


    public void ToggleSlider(GameObject Slider) //toggle the sliders for the audio components in the settings
    {
        if (Slider.active == false) //if the slider isnt active
        {
            Slider.SetActive(true); //activate slider
        }
        else //if the slider is active
        {
            Slider.SetActive(false); //deactivate the slider
        }
    }

    public void IncrementBallonNumber()
    {

        NumberOfBalloonsToSpawn += 1;
        if (NumberOfBalloonsToSpawn > MaxBalloons)
        {
            NumberOfBalloonsToSpawn = MaxBalloons;
        }

        Debug.Log("Current Number of Balloons: " + NumberOfBalloonsToSpawn);
        
        NumOfBalloonText.text = NumberOfBalloonsToSpawn.ToString();
        
        //SavePrefs();



    }

    public void DecrementBallonNumber()
    {
        NumberOfBalloonsToSpawn -= 1;
        if (NumberOfBalloonsToSpawn < MinBalloons)
        {
            NumberOfBalloonsToSpawn = MinBalloons;
        }

        Debug.Log("Current Number of Balloons: " + NumberOfBalloonsToSpawn);
        NumOfBalloonText.text = NumberOfBalloonsToSpawn.ToString();
        

    }
    public void ToggleSoundFX(GameObject SoundObject) //function to mute the audiosource passed in on the gameobject
    {
        AudioSource AS = SoundObject.GetComponent<AudioSource>(); //get the audiosource
        if (AS.mute == false) //if the audiosource is unmuted
        {
            AS.mute = true; //mute
            Debug.Log("Turning Sound on component off");
            TelSystem.AddLine("Sound of " + EventSystem.current.currentSelectedGameObject.name + "turned off");

            string name = AS.gameObject.name;
            switch (name)
            {
                case "BallPassive":
                    Save.BallSound = false;
                    break;
                case "BalloonPop":
                    Save.BalloonPop = false;
                    break;
                case "GameMusic":
                    Save.GameMusic = false;
                    break;
                default:
                    Debug.Log("Unexpected name in the baggage area");
                    break;
            }
        }
        
        else //therefore if its unmuted
        {
            AS.mute = false; //unmute
            Debug.Log("Turning Sound on component on");
            TelSystem.AddLine("Sound of " + EventSystem.current.currentSelectedGameObject.name + "turned on");

            string name = AS.gameObject.name;
            switch (name)
            {
                case "BallPassive":
                    Save.BallSound = true;
                    break;
                case "BalloonPop":
                    Save.BalloonPop = true;
                    break;
                case "GameMusic":
                    Save.GameMusic = true;
                    break;
                default:
                    Debug.Log("Unexpected name in the baggage area");
                    break;
            }
        }
    }

    public void IncreaseVolume(AudioSource AS) //pass in an audiosource as a parameter
    {
        float CurrentVolume = AS.volume; //get the current volume. Why have i done it this way... it can be done in one line
        float NextVolume = CurrentVolume + 0.1f; //get the next volume
        AS.volume = NextVolume; //set the next volume
        //Debug.Log(EventSystem.current.currentSelectedGameObject.transform.parent.parent);
        TelSystem.AddLine("Volume increased for " + EventSystem.current.currentSelectedGameObject.transform.parent.parent + " to " + AS.volume);
        string name = AS.gameObject.name;
        switch (name)
        {
            case "BallPassive":
                Save.BallSoundVolume = NextVolume;
                break;
            case "BalloonPop":
                Save.BalloonPop_Volume = NextVolume;
                break;
            case "GameMusic":
                Save.GameMusic_Volume = NextVolume;
                break;
            default:
                break;
        }

    }
    public void DecreaseVolume(AudioSource AS) //same as above but - instead of +
    {
        float CurrentVolume = AS.volume;
        float NextVolume = CurrentVolume - 0.1f;
        AS.volume = NextVolume;
        //Debug.Log(EventSystem.current.currentSelectedGameObject.transform.parent.parent);
        TelSystem.AddLine("Volume decreased for " + EventSystem.current.currentSelectedGameObject.transform.parent.parent + " to " + AS.volume);
        string name = AS.gameObject.name;
        switch (name)
        {
            case "BallPassive":
                Save.BallSoundVolume = NextVolume;
                break;
            case "BalloonPop":
                Save.BalloonPop_Volume = NextVolume;
                break;
            case "GameMusic":
                Save.GameMusic_Volume = NextVolume;
                break;
            default:
                break;
        }

    }

    public void IncreaseBallSpeed() //function to increase the ball speed
    {
        if (PlayerBall.GetComponent<BallControllerV2>().LerpSpeed == 5) //if the current speed of the ball is 5
        {
            //do nothing
        }
        else //if its not
        {
            PlayerBall.GetComponent<BallControllerV2>().LerpSpeed++; //increment the ball speed
            BallSpeedText.text = (PlayerBall.GetComponent<BallControllerV2>().LerpSpeed).ToString(); //update the text component
            if (AudioSources[3].isPlaying == true)
            {
                AudioSources[3].Stop();

            }
            AudioSources[3].PlayOneShot(IncreasingBallSpeed_Audio);
            Save.BallSpeed = PlayerBall.GetComponent<BallControllerV2>().LerpSpeed; //set the ball speed
            TelSystem.AddLine("Ball speed increased to " + PlayerBall.GetComponent<BallControllerV2>().LerpSpeed);
            
        }
    }

    public void DecreaseBallSpeed() //same as above but to decrease the speed
    {
        if (PlayerBall.GetComponent<BallControllerV2>().LerpSpeed == 1)
        {
            //do nothing
        }
        else
        {
            PlayerBall.GetComponent<BallControllerV2>().LerpSpeed--;
            BallSpeedText.text = (PlayerBall.GetComponent<BallControllerV2>().LerpSpeed).ToString();
            if (AudioSources[3].isPlaying == true)
            {
                AudioSources[3].Stop();

            }
            AudioSources[3].PlayOneShot(DecreasingBallSpeed_Audio);
            Save.BallSpeed = PlayerBall.GetComponent<BallControllerV2>().LerpSpeed; //set the ball speed
            TelSystem.AddLine("Ball speed decreased to " + PlayerBall.GetComponent<BallControllerV2>().LerpSpeed);
        }

    }
    #region Functions related to ball speed
    public void IncreaseBallSize()
    {
        BallSize++;
        if (BallSize > 3)
        {
            BallSize = 3;
        }
        BallSizeText.text = BallSize.ToString();
        SetBallSize(BallSize);
        if (AudioSources[3].isPlaying == true)
        {
            AudioSources[3].Stop();

        }
        AudioSources[3].PlayOneShot(IncreasingBallSize_Audio);
        Save.BallSize = BallSize;
        TelSystem.AddLine("Ball size increased to" + BallSize);
        #region old version
        /*
        //Debug.Log(PlayerBall.transform.localScale);
        Vector3 MaxSize = new Vector3(0.6f, 0.6f, 0.6f);
        int BallSizeNumber = int.Parse(BallSizeText.text);
        if (PlayerBall.transform.localScale != MaxSize)
        {
            PlayerBall.transform.localScale += new Vector3(0.15f, 0.15f, 0.15f);
            PlayerBall.transform.localPosition = new Vector3(PlayerBall.transform.localPosition.x, PlayerBall.transform.localPosition.y + 0.15f, PlayerBall.transform.localPosition.z);
            BallSizeNumber++;
            BallSizeText.text = BallSizeNumber.ToString();
            if (AudioSources[3].isPlaying == true)
            {
                AudioSources[3].Stop();

            }
            AudioSources[3].PlayOneShot(IncreasingBallSize_Audio);
            Save.BallSize = float.Parse(BallSizeText.text);
            //TelSystem.AddLine("Ball size increased to" + PlayerBall.transform.localScale);
        }
        */
        #endregion
    }

    public void DecreaseBallSize()
    {
        BallSize--;
        if (BallSize < 1)
        {
            BallSize = 1;
        }
        BallSizeText.text = BallSize.ToString();
        SetBallSize(BallSize);
        AudioSources[3].PlayOneShot(IncreasingBallSize_Audio);
        Save.BallSize = BallSize;
        TelSystem.AddLine("Ball size decreased to" + BallSize);
    }

    public void SetBallSize(int SetSize)
    {
        switch (SetSize)
        {
            case 1:
                PlayerBall.transform.localScale = DefaultBallSize;
                PlayerBall.transform.localPosition = new Vector3(PlayerBall.transform.localPosition.x, DefaultBallPos.y, PlayerBall.transform.localPosition.z);
                break;

            case 2:
                PlayerBall.transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
                PlayerBall.transform.localPosition = new Vector3(PlayerBall.transform.localPosition.x, DefaultBallPos.y + 0.15f, PlayerBall.transform.localPosition.z);
                break;

            case 3:
                PlayerBall.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                PlayerBall.transform.localPosition = new Vector3(PlayerBall.transform.localPosition.x, DefaultBallPos.y + 0.3f, PlayerBall.transform.localPosition.z);
                break;

            default:
                Debug.Log("Unexpected Ball Size");
                break;
                
        }
    }
    #endregion

    public void Win_PopUp()
    {
        //< Username >, you completed<Name of the game> and popped < number of balloons> to get the max score of < max score > in just < Completed time > !
             string Username = "<Username>";
        string NameOfGame = "<Name of the game>";

        string StatsText = Username + " ,you completed " + NameOfGame + " and popped " + NumberOfBalloonsPopped + " to get the max score of " + Score_Goal + " in just " + CompletedTime + " seconds!";
        Debug.Log(StatsText);
        for (int i = 0; i < OtherCanvases.Length; i++)
        {
            OtherCanvases[i].SetActive(false);
        }
        Win_PopUpCanvas.SetActive(true);
        WinText.text = StatsText;
        TelSystem.AddLine("Game Completed: Number of Balloons Popped - " +  NumberOfBalloonsPopped + ". Score goal -"+ Score_Goal + ".Completed time- " + CompletedTime + "seconds");
    }

    public void GridUp()
    {
        //Debug.Log("Making grid bigger");
        Grid.NextGrid();
        TelSystem.AddLine("Increase grid size button pressed");
        GridText.text = Grid.Height.ToString() + " x " + Grid.Width.ToString();
        if (AudioSources[3].isPlaying == true)
        {
            AudioSources[3].Stop();

        }
        AudioSources[3].PlayOneShot(IncreasingGridSize_Audio);
        Save.Grid[0] = Grid.Height;
        Save.Grid[1] = Grid.Width;
        //TelSystem.AddLine("Grid increased to" + GridText.text);
    }

    public void GridDown()
    {
        //Debug.Log("Making grid smaller");
        Grid.LastGrid();
        TelSystem.AddLine("Decrease grid size button pressed");
        GridText.text = Grid.Height.ToString() + " x " + Grid.Width.ToString();
        if (AudioSources[3].isPlaying == true)
        {
            AudioSources[3].Stop();

        }
        AudioSources[3].PlayOneShot(DecreasingGridSize_Audio);
        Save.Grid[0] = Grid.Height;
        Save.Grid[1] = Grid.Width;
        //TelSystem.AddLine("Grid decreased to" + GridText.text);
    }

    public void ShowHideGridLines()
    {
        Grid.ShowHideGridLines();
        if (Grid.GridLinesHidden == true)
        {
            Save.ShowGridLines = false;
        }
        else
        {
            Save.ShowGridLines = true;
        }
    }

    public void ShowHideGridBoxes()
    {
        Grid.ShowHideGridBoxes();
        if (Grid.GridBoxesHidden == true)
        {
            Save.ShowGridBoxes = false;
        }
        else
        {
            Save.ShowGridBoxes = true;
        }
    }

    public void ToggleDiagonalControls()
    {
        if (DiagonalControlsActive == true)
        {
            DiagonalControlsActive = false;
            for (int i = 0; i < DiagonalControlsObjects.Length; i++)
            {
                DiagonalControlsObjects[i].SetActive(false);
            }
            Grid.FourDirectionalGrid();
            Save.DiagonalMovement = false;
            TelSystem.AddLine("Diagonal controls deactivated");
        }

        else
        {
            DiagonalControlsActive = true;
            for (int i = 0; i < DiagonalControlsObjects.Length; i++)
            {
                DiagonalControlsObjects[i].SetActive(true);
            }
            Grid.EightDirectionalGrid();
            Save.DiagonalMovement = true;
            TelSystem.AddLine("Diagonal controls activated");
        }
    }

    public void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.streamingAssetsPath + "/EthanActivity1Save.FUNSAV");
        bf.Serialize(fs, Save);
        fs.Close();
        Debug.Log("Data Saved");
    }

    public void LoadData(string path)
    {
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(path, FileMode.Open);
            Save = (ActivityOneSave)bf.Deserialize(fs);
            fs.Close();
            Debug.Log("Data Loaded");
            
            if (Save.AnimatedTexture == true) //set the scene based on the loaded data
            {
                AnimTex_Toggle.isOn = true;
            }
            else
            {
                AnimTex_Toggle.isOn = false;
            }

            SetBallSize(Save.BallSize);


            
            
        }
    }
    
    
    
}
