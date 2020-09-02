using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloons : MonoBehaviour
{
    public float BalloonSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Balloon Spawned");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += Vector3.left * BalloonSpeed * Time.deltaTime;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BalloonDestory")
        {
            Debug.Log("Balloon Missed");
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Debug.Log("Point Scored!");

        }
    }
}
