using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BalloonSpawnerV4 : MonoBehaviour
{
    public int NumberOfBalloonsToSpawn; //number of balloons to spawn in the scene
    public List<Vector3> SpawnLocations; //list of SpawnLocations
    public List<GameObject> BalloonsSpawned;

    public Texture[] OnePointTextures; //array of possible textures
    public Texture[] TwoPointTextures;
    public Texture[] ThreePointTextures;

    public GridV4 grid; //reference to the grid
    int BalloonValue; //reference to the current balloon value
    int RandomNumber; //reference when a random number is need
    GameObject Balloon; //reference to a spawned balloon
    ConfigedBalloon CB; //reference to the configedballoon script on the above balloon
    Material BM; //reference to the material on the above balloon
    public Vector3 rot;

    ObjectPooler objectPooler; //reference to the object pooler

    bool ready = false; //bool to check if the grid is ready for the balloons
    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    public void Update()
    {
       
        
        if (ready == false) //if the ready bool false
        {
            //Debug.Log("Checking if Ready");
            CheckIfReady(); //check if the grid is ready
        }
        
    }
    // Update is called once per frame
    public void SpawnBalloons()
    {
        SpawnLocations.Clear(); //clear the list of spawn balloon locations
        for (int i = 0; i < NumberOfBalloonsToSpawn; i++) //for the number of balloons to spawn
        {
            RandomNumber = Random.Range(0, grid.ActiveGrids.Count); //get a random number between 0 and the number of active grids
            Vector3 t = grid.ActiveGrids[RandomNumber].transform.position; //get the location of the grid chosen for the balloon to spawn on
            SpawnLocations.Add(new Vector3(t.x, 5f, t.z)); //create a spawn location
        }

        CheckForDuplicates(); //check for duplicates

        for (int i = 0; i != SpawnLocations.Count; i++) //for each of the spawn locations
        {
            BalloonValue = Random.Range(1, 11); // 50% for 1 pointer, 40% for a 2 pointer, 10% for a 3 pointer
            Quaternion QRot = Quaternion.Euler(rot.x, rot.y, rot.z);
            if (BalloonValue >= 1 && BalloonValue <= 5) //1 pointer
            {
                Balloon = objectPooler.SpawnFromPool("Balloon", SpawnLocations[i], QRot); //spawn a balloon from the pool
                CB = Balloon.GetComponent<ConfigedBalloon>(); //get the config balloon script
                CB.BalloonValue = 1; //set the value of the balloon
                CB.BalloonSpawner = this;
                RandomNumber = Random.Range(0, 5); //get a random number between 0 and 5 for the balloon colour
                BM = Balloon.GetComponent<Renderer>().material; //get the balloon material
                BM.SetTexture("_MainTex", OnePointTextures[RandomNumber]); //set the texture
                BalloonsSpawned.Add(Balloon); //add the balloon to the balloon spawned list
            }

            else if (BalloonValue >= 6 && BalloonValue <= 9) //2 pointer
            {
                Balloon = objectPooler.SpawnFromPool("Balloon", SpawnLocations[i], QRot);
                CB = Balloon.GetComponent<ConfigedBalloon>();
                CB.BalloonValue = 2;
                CB.BalloonSpawner = this;
                RandomNumber = Random.Range(0, 3);
                BM = Balloon.GetComponent<Renderer>().material;
                BM.SetTexture("_MainTex", TwoPointTextures[RandomNumber]);
                BalloonsSpawned.Add(Balloon);

            }
            else if (BalloonValue == 10) //3 pointer
            {
                Balloon = objectPooler.SpawnFromPool("Balloon", SpawnLocations[i], QRot);
                CB = Balloon.GetComponent<ConfigedBalloon>();
                CB.BalloonValue = 3;
                CB.BalloonSpawner = this;
                RandomNumber = Random.Range(0, 3);
                BM = Balloon.GetComponent<Renderer>().material;
                BM.SetTexture("_MainTex", ThreePointTextures[RandomNumber]);
                BalloonsSpawned.Add(Balloon);

            }


        }


    }

    public void CheckForDuplicates()
    {
        SpawnLocations = SpawnLocations.Distinct().ToList();
        if (SpawnLocations.Count <= 3) //if the length of the list is less than 3
        {
            RandomNumber = Random.Range(0, grid.ActiveGrids.Count);
            SpawnLocations.Add(grid.ActiveGrids[RandomNumber].transform.position);
            CheckForDuplicates();
        }
    }

    public void CheckIfReady()
    {
        if (grid.ActiveGrids.Count != 0)
        {
            ready = true;
            SpawnBalloons();
        }
    }

    public void RemoveBalloon(GameObject balloon)
    {
        BalloonsSpawned.Remove(balloon);
        if (BalloonsSpawned.Count == 0)
        {
            SpawnBalloons();
        }
    }

}
