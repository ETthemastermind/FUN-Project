using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityOneBallFunctions : MonoBehaviour
{
    public int PlayerScore; //integer to hold the player score i.e. how many balloons popped
    public GameObject _HUDController;
    public MasterTelemetrySystem TelSystem;

    public BallControllerV2 Ball;
    // Start is called before the first frame update
    void Start()
    {
        Ball = this.GetComponent<BallControllerV2>();
        TelSystem = GameObject.FindGameObjectWithTag("TelSystem").GetComponent<MasterTelemetrySystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Balloon") // if the other object is a balloon
        {
            PlayerScore += collision.gameObject.GetComponent<Balloons>().BalloonValue; //increment the player's score
            _HUDController.GetComponent<HudController>().IncrementScore(PlayerScore); //update the players score text in the hud controller
            //_HUDController.GetComponent<Activity1Settings>().NumberOfBalloonsPopped++;
            string BalloonValue = (collision.gameObject.GetComponent<Balloons>().BalloonValue).ToString();
            TelSystem.AddLine("Balloon popped value - " + BalloonValue); //run telemetry line
            Destroy(collision.gameObject); //destroy the balloon
            Ball.HapticFeedback(); //run the haptic feedback function
            

        }
        
    }
    /*

    
    public void OnTriggerStay(Collider other) //when the ball stays in a trigger area
    {
        if (other.gameObject.tag == "Boundary") //if trigger area entered belongs to the boundary wall
        {
            Debug.Log("Boundary Hit");
            Ball.HapticFeedback(); //run the haptic feedback function
        }
    }
    */
    
}
