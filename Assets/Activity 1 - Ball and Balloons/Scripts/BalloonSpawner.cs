using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject Balloon; //reference for the Balloon Prefab
    public int NumberOfBalloonsSpawned; //int value for how many balloons each spawner is allowed to spawn
    public int BalloonLimit; //keeps track how many ballooons this spawner has spawned
    public float SpawnCooldown; //float value for the current balloon cooldown before another is allowed to spawn
    public float CurrentCooldown = 0f; //float value for how long the cooldown has been active for

    public bool ReadyToSpawn = false; //check to see if a ballon is ready to spawn

    public int BalloonLowerLimit; // for the cooldown, higher number
    public int BalloonUpperLimit;  //for the cooldown, lower number
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnCooldown = Random.Range(1, 5); //determines the first spawn of balloons
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(RandomNumber);
        if (ReadyToSpawn == true & NumberOfBalloonsSpawned != BalloonLimit) //if a ballon is allowed to spawn
        {
            Instantiate(Balloon, gameObject.transform.position, Quaternion.identity); //instantiate the ballon
            NumberOfBalloonsSpawned += 1; //increment the number of balloons spawned
            ReadyToSpawn = false; //spawner is no longer ready to spawn a balloon
            SpawnCooldown = Random.Range(BalloonLowerLimit, BalloonUpperLimit); //determine the spawn cooldown time for another balloon can spawn

        }


        else if (ReadyToSpawn == false) //if the spawner isnt ready to spawn a balloon
        {
            if (CurrentCooldown <= SpawnCooldown) //if the current cooldown is less than the designated spawn cooldown
            {
                CurrentCooldown += Time.deltaTime; //increment the current cooldown

            }

            else
            {
                ReadyToSpawn = true; //balloon is now ready to spawn
                CurrentCooldown = 0f; //reset the current cooldown

            }
        }
        
        
    }
}
