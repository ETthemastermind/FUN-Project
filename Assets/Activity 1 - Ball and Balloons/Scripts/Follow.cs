using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform Player;
    public Vector3 P_Offset;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update() //<- could be optimised more?
    {
        
        transform.position = (Player.position + P_Offset);
        
    }
}
