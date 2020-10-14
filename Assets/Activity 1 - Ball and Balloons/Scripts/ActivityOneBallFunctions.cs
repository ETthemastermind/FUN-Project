using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActivityOneBallFunctions : MonoBehaviour
{
    public int PlayerScore; //integer to hold the player score i.e. how many balloons popped
    public GameObject _HUDController;
    public GridV3 Grid;
    public MasterTelemetrySystem TelSystem;
    public BallControllerV2 Ball;
    public List<GameObject> Test;
    public GameObject NearestGrid;
    public List<GameObject> GridInProx;
    public LayerMask layermask;
    // Start is called before the first frame update
    void Start()
    {
        Ball = this.GetComponent<BallControllerV2>();
        TelSystem = GameObject.FindGameObjectWithTag("TelSystem").GetComponent<MasterTelemetrySystem>();
        Grid = GameObject.FindGameObjectWithTag("GridObject").GetComponent<GridV3>();
        ReposBall();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReposBall();
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Balloon") // if the other object is a balloon
        {
            GameObject Balloon = collision.gameObject;
            if (Balloon.GetComponent<ConfigedBalloon>().isActiveAndEnabled)
            {
                PlayerScore += Balloon.GetComponent<ConfigedBalloon>().BalloonValue;
                _HUDController.GetComponent<HudController>().IncrementScore(PlayerScore);
                string BalloonValue = (Balloon.GetComponent<Balloons>().BalloonValue).ToString();
                TelSystem.AddLine("Balloon popped value - " + BalloonValue); //run telemetry line
                Balloon.GetComponent<ConfigedBalloon>().DestroyBalloon();
                Ball.HapticFeedback(); //run the haptic feedback function
            }
            else
            {
                PlayerScore += Balloon.GetComponent<Balloons>().BalloonValue; //increment the player's score
                _HUDController.GetComponent<HudController>().IncrementScore(PlayerScore); //update the players score text in the hud controller
                                                                                          //_HUDController.GetComponent<Activity1Settings>().NumberOfBalloonsPopped++;
                string BalloonValue = (Balloon.GetComponent<Balloons>().BalloonValue).ToString();
                TelSystem.AddLine("Balloon popped value - " + BalloonValue); //run telemetry line
                Balloon.GetComponent<Balloons>().DestroyBalloon();
                
            }
        }
        
    }

    public void ReposBall() //https://answers.unity.com/questions/122936/find-nearest-object-using-physicsoverlapsphere.html
    {
        Debug.Log("Repositioning Ball");
        float distance;
        float nearestDistance = float.MaxValue;
        foreach (GameObject grid in Grid.GridGameObjects)
        {
            distance = (transform.position - grid.transform.position).sqrMagnitude;
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                NearestGrid = grid;
            }
        }

        Vector3 Destination = new Vector3(NearestGrid.transform.position.x, transform.position.y, NearestGrid.transform.position.z);
        transform.position = Destination;
    }
}
