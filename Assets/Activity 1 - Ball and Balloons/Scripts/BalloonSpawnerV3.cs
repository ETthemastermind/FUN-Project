using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BalloonSpawnerV3 : MonoBehaviour
{
    public GameObject Ball_Player; //reference to the player
    public GameObject[] Balloon_Prefab; //reference to the balloon prefab
    public int NumberOfBalloonsToSpawn; // Reference to number of balloons to spawn per round
    public GridV3 grid;
    public Activity1Settings GameController;
    public GameObject[] SpawnedBalloons; //array for all balloons in the scene
    public GameObject HUDController; //reference to the HUD controller
    public new List<Vector3> SpawnLocations = new List<Vector3>(); //list of the spawn locations for the balloons, randomly generated


    // Start is called before the first frame update
    void Start()
    {
        Ball_Player = GameObject.FindGameObjectWithTag("Player"); //find the player
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<Activity1Settings>();
        NumberOfBalloonsToSpawn = GameController.NumberOfBalloonsToSpawn;
        grid = GameObject.FindGameObjectWithTag("GridObject").GetComponent<GridV3>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ResetBalloons();
        }


        SpawnedBalloons = GameObject.FindGameObjectsWithTag("Balloon"); //finds all the ballons active in the scene per frame
        if (SpawnedBalloons.Length == 0) //if there are no balloons in the scene
        {
            if (HUDController.GetComponent<HudController>().GameComplete == false) //checks the HUDController to make sure that the player hasnt reached the max score (had an issue of balloons spawning and then the time scale turning to 0)
            {
                SpawnBalloons(); //run the spawn balloon function
            }


        }
    }

    public void SpawnBalloons()
    {
        grid = GameObject.FindGameObjectWithTag("GridObject").GetComponent<GridV3>();
        SpawnLocations.Clear();
        for (int i = 0; i < NumberOfBalloonsToSpawn; i++)
        {
            int RandomNumber = Random.Range(0, grid.GridGameObjects.Count);
            Vector3 Location = new Vector3((grid.GridGameObjects[RandomNumber].transform.position.x), (transform.position.y), (grid.GridGameObjects[RandomNumber].transform.position.z));
            SpawnLocations.Add(Location);

        }
        CheckForDuplicates();

        for (int j = 0; j != SpawnLocations.Count; j++)
        {
            int BalloonToSpawn = Random.Range(1, 11); // 50% for 1 pointer, 40% for a 2 pointer, 10% for a 3 pointer
            Debug.Log("Balloon to spawn" + BalloonToSpawn);
            //Instantiate(Balloon_Prefab[2], SpawnLocations[j], Quaternion.identity); //spawn a balloon at the location held in element J of the list

            if (BalloonToSpawn >= 1 && BalloonToSpawn <= 5)
            {
                Instantiate(Balloon_Prefab[0], SpawnLocations[j], Quaternion.identity); //spawn a balloon at the location held in element J of the list
            }

            else if (BalloonToSpawn >= 6 && BalloonToSpawn <= 9)
            {
                Instantiate(Balloon_Prefab[1], SpawnLocations[j], Quaternion.identity); //spawn a balloon at the location held in element J of the list
            }

            else if (BalloonToSpawn == 10)
            {
                Instantiate(Balloon_Prefab[2], SpawnLocations[j], Quaternion.identity); //spawn a balloon at the location held in element J of the list
            }
        }
    }

    public void CheckForDuplicates()
    {
        SpawnLocations = SpawnLocations.Distinct().ToList(); //removes all duplicates in the list
        if (SpawnLocations.Count <= 3) //if the length of the list is less than 3
        {
            
            Debug.Log("Duplicate Spawn Location Found"); //print that a duplicate spawn location was generated to the console
            int RandomNumber = Random.Range(0, grid.GridGameObjects.Count);
            Vector3 Location = new Vector3((grid.GridGameObjects[RandomNumber].transform.position.x), (transform.position.y), (grid.GridGameObjects[RandomNumber].transform.position.z));
            SpawnLocations.Add(Location);
            /*
            int RandomNumber = Random.Range(0, grid.GridGameObjects.Count);
            Vector3 Location = new Vector3((grid.GridGameObjects[RandomNumber].transform.position.x), (transform.position.y), (grid.GridGameObjects[RandomNumber].transform.position.z));
            SpawnLocations.Add(Location);
            */
            CheckForDuplicates(); //run the check duplicates function again, essentially creating a loop until 4 unique locations are generated
        }



    }

    public void ResetBalloons()
    {
        for (int i = 0; i < SpawnedBalloons.Length; i++)
        {
            Destroy(SpawnedBalloons[i]);
        }
    }
    
}
