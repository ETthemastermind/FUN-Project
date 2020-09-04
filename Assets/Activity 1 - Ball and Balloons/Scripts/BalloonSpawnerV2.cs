using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BalloonSpawnerV2 : MonoBehaviour
{
    public GameObject Ball_Player;
    public GameObject BalloonPrefab;
    public int NumberOfBalloonsToSpawn;
    public new List<Vector3> SpawnLocations = new List<Vector3>();
    public GameObject[] SpawnedBalloons;
    public GameObject HUDController;
    
    // Start is called before the first frame update
    void Start()
    {
        Ball_Player = GameObject.FindGameObjectWithTag("Player");
        

    }

    // Update is called once per frame
    void Update()
    {
        SpawnedBalloons = GameObject.FindGameObjectsWithTag("Balloon");
        if (SpawnedBalloons.Length == 0)
        {
            if (HUDController.GetComponent<HudController>().GameComplete == false)
            {
                SpawnBalloons();
            }
            

        }
    }
    public void SpawnBalloons()
    {

        SpawnLocations.Clear();
        for (int i = 0; i <= NumberOfBalloonsToSpawn - 1; i++)
        {

            Vector3 SpawnLocation = new Vector3(Random.Range(-5, 5), transform.position.y, Random.Range(-5, 5));
            SpawnLocations.Add(SpawnLocation);

        }

        CheckForDuplicates();

        for (int j = 0; j != SpawnLocations.Count; j++)
        {
            Instantiate(BalloonPrefab, SpawnLocations[j], Quaternion.identity);
            

        }

        

        

    }
    public void CheckForDuplicates()
    {
        SpawnLocations = SpawnLocations.Distinct().ToList();
        if (SpawnLocations.Count <= 3)
        {
            Debug.Log("Duplicate Spawn Location Found");
            Vector3 SpawnLocation = new Vector3(Random.Range(-5, 5), transform.position.y, Random.Range(-5, 5));
            SpawnLocations.Add(SpawnLocation);
            CheckForDuplicates();
        }
        


    }
}
