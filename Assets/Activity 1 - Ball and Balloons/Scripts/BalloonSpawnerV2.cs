using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BalloonSpawnerV2 : MonoBehaviour
{
    public GameObject Ball_Player; //reference to the player
    public GameObject BalloonPrefab; //reference to the balloon prefab 
    private int NumberOfBalloonsToSpawn; // Reference to number of balloons to spawn per round
    public new List<Vector3> SpawnLocations = new List<Vector3>(); //list of the spawn locations for the balloons, randomly generated
    public GameObject[] SpawnedBalloons; //array for all balloons in the scene
    public GameObject HUDController; //reference to the HUD controller

    public Activity1Settings GameController;

    
    
    // Start is called before the first frame update
    void Start()
    {
        Ball_Player = GameObject.FindGameObjectWithTag("Player"); //find the player
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<Activity1Settings>();
        NumberOfBalloonsToSpawn = GameController.NumberOfBalloonsToSpawn;
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnedBalloons = GameObject.FindGameObjectsWithTag("Balloon"); //finds all the ballons active in the scene per frame
        if (SpawnedBalloons.Length == 0) //if there are no balloons in the scene
        {
            if (HUDController.GetComponent<HudController>().GameComplete == false) //checks the HUDController to make sure that the player hasnt reached the max score (had an issue of balloons spawning and then the time scale turning to 0)
            {
                SpawnBalloons(); //run the spawn balloon function
            }
            

        }
    }
    public void SpawnBalloons() //function to spawn balloons
    {

        SpawnLocations.Clear(); //clears the current list of spawn locations for balloons
        for (int i = 0; i <= NumberOfBalloonsToSpawn - 1; i++) //for the number of balloons to spawn (-1 is there so that the chosen number of balloons set in the inspector works with the list)
        {

            Vector3 SpawnLocation = new Vector3(Random.Range(-5, 5), transform.position.y, Random.Range(-5, 5)); //generate a random location for a balloon to spawn
            SpawnLocations.Add(SpawnLocation); //adds the random location to the list of locations

        }

        CheckForDuplicates(); //run the check for duplicates function

        for (int j = 0; j != SpawnLocations.Count; j++) //for the length of the spawn locations list (basically how many balloons are wanted to spawn)
        {
            Instantiate(BalloonPrefab, SpawnLocations[j], Quaternion.identity); //spawn a balloon at the location held in element J of the list
            

        }

        

        

    }
    public void CheckForDuplicates() //function to check for duplicates in the list of locations, I got unlucky and had 3 out of 4 balloons spawn in the same place
    {
        SpawnLocations = SpawnLocations.Distinct().ToList(); //removes all duplicates in the list
        if (SpawnLocations.Count <= 3) //if the length of the list is less than 3
        {
            Debug.Log("Duplicate Spawn Location Found"); //print that a duplicate spawn location was generated to the console
            Vector3 SpawnLocation = new Vector3(Random.Range(-5, 5), transform.position.y, Random.Range(-5, 5)); //create a new location to replace the duplicated
            SpawnLocations.Add(SpawnLocation); //adds the replacement location to the list
            CheckForDuplicates(); //run the check duplicates function again, essentially creating a loop until 4 unique locations are generated
        }
        


    }

    

    
}
