using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigedBalloon : MonoBehaviour
{
    public float BalloonRotSpeed = 10f; //value for the rotation speed of the balloon
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Rotate(-90f, 0f, 0f); //damn balloon wont spawn in the same orientation that dragging it into the scene shows
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 0, BalloonRotSpeed); //rotate the balloon around the Z axis, doesnt look as good because the texture is uniform across the whole model but will look better with texture variation i.e numbers
    }
}
