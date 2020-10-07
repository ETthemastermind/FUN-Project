using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedBalloonSpawner : MonoBehaviour
{
    public GameObject BalloonVal1;
    public GameObject BalloonVal2;
    public GameObject BalloonVal3;

    public enum Positions {Top, Middle, Bottom, Left, Centre, Right}

    public Positions[] positions = new Positions[5];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
     ideas:
     Place the balloons manually in the scene
     have them 
     */
}
