using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Activity1Settings : MonoBehaviour
{
    public GameObject PlayerBall;
    

    // Rotating Ball Texture
    public AnimatedTexture AnimatedTexture;
    public Toggle AnimTextureToggle;

    // Sound Effects
    public Toggle SoundFXToggle;
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
    public bool AnimatedTexturesActive;
    public bool SoundActive;
    public int NumberOfBalloonsToSpawn;
    //public int NumberOfRounds;
    public int Score_Goal;





    private void Awake()
    {
        LoadPrefs();
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerBall = GameObject.FindGameObjectWithTag("Player");
        AnimatedTexture = PlayerBall.GetComponent<AnimatedTexture>();
        BalloonSpawner = GameObject.FindGameObjectWithTag("BalloonSpawner").GetComponent<BalloonSpawnerV2>();
        NumOfBalloonText.text = NumberOfBalloonsToSpawn.ToString();
        
        //RoundNumber = gameObject.GetComponent<HudController>();
        //RoundNumberText.text = NumberOfRounds.ToString();




    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ToggleAnimatedTexture()
    {
        if (AnimTextureToggle.isOn == true)
        {
            Debug.Log("Turn on animated ball texture");
            AnimatedTexture.Active = true;
            AnimatedTexturesActive = true;
        }

        else
        {
            Debug.Log("Turn off animated ball texture");
            AnimatedTexture.Active = false ;
            AnimatedTexturesActive = false;
        }
    }
    /*
    public void ToggleSoundFX()
    {
        if (SoundFXToggle.isOn == true)
        {
            AudioListener.volume = 1f;
            Debug.Log("Sound FX turned on");
            SoundActive = true;

        }

        else
        {
            AudioListener.volume = 0f;
            Debug.Log("Sound FX turned off");
            SoundActive = false;
        }

    }
    */
    public void ToggleSoundFX(GameObject SoundObject)
    {
        AudioSource AS = SoundObject.GetComponent<AudioSource>();
        if (AS.mute == false)
        {
            AS.mute = true;
            Debug.Log("Turning Sound on component off");
        }
        else
        {
            AS.mute = false;
            Debug.Log("Turning Sound on component on");
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
        //SavePrefs();

    }
    /*
    public void IncrementRoundNumber()
    {
        NumberOfRounds += 1;
        if (NumberOfRounds > MaxRounds)
        {
            NumberOfRounds = MaxRounds;
        }

        Debug.Log("Current Number of Rounds: " + NumberOfRounds);
        RoundNumberText.text = NumberOfRounds.ToString();
        //SavePrefs();
        

    }

    public void DecrementRoundNumber()
    {
        NumberOfRounds -= 1;
        if (NumberOfRounds < MinRounds)
        {
            NumberOfRounds = MinRounds;
        }

        Debug.Log("Current Number of Rounds: " + NumberOfRounds);
       
        RoundNumberText.text = NumberOfRounds.ToString();
        //SavePrefs();

    }
    */

    public void SavePrefs()
    {
        SaveSystem.SavePrefs(this);
    }

    public void LoadPrefs()
    {
        Profile data = SaveSystem.LoadPrefs();
        //NumberOfBalloonsToSpawn = data.NumberOfBalloons_Save;
    }


    

    

    
}
