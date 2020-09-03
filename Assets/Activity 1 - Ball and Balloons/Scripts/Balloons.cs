using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloons : MonoBehaviour
{
    public float BalloonSpeed = 10f;  //public float for the speed of the balloon
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Balloon Spawned"); //debug to confirm that the balloon has spawned
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += Vector3.left * BalloonSpeed * Time.deltaTime; //move the balloon left at it's speed * time.delta time (left happens to be down towards the player here)
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BalloonDestory") //if the balloon leaves the player's view and enters the trigger of the mesh behind them 
        {
            Debug.Log("Balloon Missed"); //print to console that the player has missed the balloon
            Destroy(gameObject); //destroy the balloon
        }
    }
}
