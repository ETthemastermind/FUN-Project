using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HudController : MonoBehaviour
{
    public TextMeshProUGUI ScoreText; //reference to the score text
    public TextMeshProUGUI Timer; //reference to the timer text

    public GameObject ResetButton; //reference for the reset button
    
    public float MaxGameTime; //float for the maxiumum playtime 
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1; //makes sure that time scale for this scene is set to 1
    }

    // Update is called once per frame
    void Update()
    {
        if (MaxGameTime >= 0f) //if the max play time is greater than 0 i.e. the game is still playing
        {
            MaxGameTime -= Time.deltaTime; //adjust the max game time based on the time played
            Timer.GetComponent<TextMeshProUGUI>().text = "Remaining Time: " + (Mathf.RoundToInt(MaxGameTime)); //update the text for the time text to reflect the current amount of playtime left

        }

        else //when the allocated playtime has passed i.e.
        {
            Time.timeScale = 0; //set the time scale to 0 i.e. pause the game
            ResetButton.SetActive(true); //show the reset button
        }
        
    }

    public void IncrementScore(int PlayerScore) //increment the player's score, gets called from the BallController script
    {
        ScoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + PlayerScore; //adjust the text on the score object to reflect the player's current score


    }

    public void ResetGame() //fucntion to reset the game
    {
        int CurrentScene = SceneManager.GetActiveScene().buildIndex; //gets the current scene index of the level (in case i end up reusing the hud controller script in multiple scene)
        SceneManager.LoadScene(CurrentScene); //loads the current scene as defined in the line above i.e reset the scene
        
    }
}
