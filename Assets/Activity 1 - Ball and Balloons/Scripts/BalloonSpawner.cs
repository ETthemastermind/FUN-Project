using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject Balloon;
    public int NumberOfBalloonsSpawned;
    public int BalloonLimit;
    public float SpawnCooldown;
    public float CurrentCooldown = 0f;
    public bool BalloonRecentlySpawned;

    public bool ReadyToSpawn = false;

    public int BalloonLowerLimit;
    public int BalloonUpperLimit;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnCooldown = Random.Range(BalloonLowerLimit, BalloonUpperLimit);
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(RandomNumber);
        if (ReadyToSpawn == true & NumberOfBalloonsSpawned != BalloonLimit)
        {
            Instantiate(Balloon, gameObject.transform.position, Quaternion.identity);
            NumberOfBalloonsSpawned += 1;
            ReadyToSpawn = false;
            SpawnCooldown = Random.Range(BalloonLowerLimit, BalloonUpperLimit);

        }


        if (ReadyToSpawn == false)
        {
            if (CurrentCooldown <= SpawnCooldown)
            {
                CurrentCooldown += Time.deltaTime;

            }

            else
            {
                ReadyToSpawn = true;
                CurrentCooldown = 0f;

            }
        }
        
        
    }
}
