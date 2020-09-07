using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Activity1Settings : MonoBehaviour
{
    public GameObject PlayerBall;
    //Activate Menu
    public GameObject SettingsMenu;

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

    //Number of Rounds
    public HudController RoundNumber;
    public int MinRounds;
    public int MaxRounds;
    public TMP_Text RoundNumberText;
    //Ball Speed? also need to edit the ball rotation...


    // Start is called before the first frame update
    void Start()
    {
        PlayerBall = GameObject.FindGameObjectWithTag("Player");
        AnimatedTexture = PlayerBall.GetComponent<AnimatedTexture>();
        BalloonSpawner = GameObject.FindGameObjectWithTag("BalloonSpawner").GetComponent<BalloonSpawnerV2>();
        NumOfBalloonText.text = BalloonSpawner.NumberOfBalloonsToSpawn.ToString();
        RoundNumber = gameObject.GetComponent<HudController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCloseSettings()
    {
        if (SettingsMenu.active == true)
        {
            SettingsMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            SettingsMenu.SetActive(true);
            Time.timeScale = 0;
        }

    }

    public void ToggleAnimatedTexture()
    {
        if (AnimTextureToggle.isOn == true)
        {
            Debug.Log("Turn on animated ball texture");
            AnimatedTexture.Active = true;
        }

        else
        {
            Debug.Log("Turn off animated ball texture");
            AnimatedTexture.Active = false ;
        }
    }

    public void ToggleSoundFX()
    {
        if (SoundFXToggle.isOn == true)
        {
            AudioListener.volume = 1f;
            Debug.Log("Sound FX turned off");
        }

        else
        {
            AudioListener.volume = 0f;
            Debug.Log("Sound FX turned on");
        }

    }

    public void IncrementBallonNumber()
    {

        BalloonSpawner.NumberOfBalloonsToSpawn += 1;
        if (BalloonSpawner.NumberOfBalloonsToSpawn > MaxBalloons)
        {
            BalloonSpawner.NumberOfBalloonsToSpawn = MaxBalloons;
        }

        Debug.Log("Current Number of Balloons: " + BalloonSpawner.NumberOfBalloonsToSpawn);
        NumOfBalloonText.text = BalloonSpawner.NumberOfBalloonsToSpawn.ToString();


    }

    public void DecrementBallonNumber()
    {
        BalloonSpawner.NumberOfBalloonsToSpawn -= 1;
        if (BalloonSpawner.NumberOfBalloonsToSpawn < MinBalloons)
        {
            BalloonSpawner.NumberOfBalloonsToSpawn = MinBalloons;
        }

        Debug.Log("Current Number of Balloons: " + BalloonSpawner.NumberOfBalloonsToSpawn);
        NumOfBalloonText.text = BalloonSpawner.NumberOfBalloonsToSpawn.ToString();
    }

    public void IncrementRoundNumber()
    {
        RoundNumber.NumberOfRounds += 1;
        if (RoundNumber.NumberOfRounds > MaxRounds)
        {
            RoundNumber.NumberOfRounds = MaxRounds;
        }

        Debug.Log("Current Number of Rounds: " + RoundNumber.NumberOfRounds);
        RoundNumberText.text = RoundNumber.NumberOfRounds.ToString();
        

    }

    public void DecrementRoundNumber()
    {
        RoundNumber.NumberOfRounds -= 1;
        if (RoundNumber.NumberOfRounds < MinRounds)
        {
            RoundNumber.NumberOfRounds = MinRounds;
        }

        Debug.Log("Current Number of Rounds: " + RoundNumber.NumberOfRounds);
        RoundNumberText.text = RoundNumber.NumberOfRounds.ToString();

    }
}
