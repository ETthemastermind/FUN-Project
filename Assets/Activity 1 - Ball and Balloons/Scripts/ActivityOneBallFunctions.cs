using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityOneBallFunctions : MonoBehaviour
{
    public int PlayerScore; //integer to hold the player score i.e. how many balloons popped
    public GameObject _HUDController;
    public GridV3 Grid;
    public MasterTelemetrySystem TelSystem;
    public GameObject StartGameObject;
    public LayerMask layermask;
    public BallControllerV2 Ball;
    // Start is called before the first frame update
    void Start()
    {
        Ball = this.GetComponent<BallControllerV2>();
        TelSystem = GameObject.FindGameObjectWithTag("TelSystem").GetComponent<MasterTelemetrySystem>();
        Grid = GameObject.FindGameObjectWithTag("GridObject").GetComponent<GridV3>();
        
        
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
            collision.gameObject.GetComponent<Balloons>().DestroyBalloon();
            Ball.HapticFeedback(); //run the haptic feedback function
            

        }
        
    }

    public void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, 1f);
      
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

    /*
    public void BallToStart()
    {
        Debug.Log(Grid.Width);
        int StartPosX = (Grid.Width + 1) / 2;
        int StartPosY = 2;
        Vector2 Start = new Vector2(StartPosX, StartPosY);
        Debug.Log(Start);
        for (int i = 0; i < Grid.GridGameObjects.Count; i++)
        {
            GridAttributes GA = Grid.GridGameObjects[i].GetComponent<GridAttributes>();
            if (GA.GridCoords == Start)
            {
                StartGO = Grid.GridGameObjects[i];
                //transform.position = Grid.GridGameObjects[i].transform.position;

            }
        }
    }
    */
}
