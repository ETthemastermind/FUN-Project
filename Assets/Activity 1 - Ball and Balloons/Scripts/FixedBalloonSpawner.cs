using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedBalloonSpawner : MonoBehaviour
{

    public GameObject PlacedBalloons;
    public List<GameObject> Balloons;
    /*
    public enum Positions {Top, Middle, Bottom, Left, Centre, Right}
    public Positions[] positions = new Positions[5];
    */
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PlacedBalloons.transform.childCount; i++)
        {
            Balloons.Add(PlacedBalloons.transform.GetChild(i).gameObject);
            Balloons[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
     ideas:
     Place the balloons manually in the scene
     have the balloons find the nearest grid cube and go to that?
     */
}
