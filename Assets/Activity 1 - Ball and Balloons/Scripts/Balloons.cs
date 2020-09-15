using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloons : MonoBehaviour
{
    
    public float BalloonRotSpeed = 10f; //value for the rotation speed of the balloon
    public int BalloonValue;
    public Texture[] BalloonColors; //array of albedo maps for the balloons
    public GameObject Camera; //reference to the camera because it has an audio source
    public AudioSource AS; //reference to the audio source on the camera
    public AudioClip Pop_SFX; //pop sound effect for the balloon

    public ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Balloon Spawned"); //debug to confirm that the balloon has spawned
        int RandomColor = Random.Range(0, 5); //pick a number at random
        //Debug.Log(RandomColor);
        Material BalloonMat = gameObject.GetComponent<Renderer>().material; //gets the material on the balloon
        BalloonMat.SetTexture("_MainTex", BalloonColors[RandomColor]); //change the albedo map of the balloon to the randomly chosen one

        gameObject.transform.Rotate(-90f, 0f,0f); //damn balloon wont spawn in the same orientation that dragging it into the scene shows

        Camera = GameObject.FindGameObjectWithTag("MainCamera"); //finds the main camera
        AS = Camera.GetComponent<AudioSource>(); //gets the audiosource on the camera object

        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0,0,BalloonRotSpeed); //rotate the balloon around the Z axis, doesnt look as good because the texture is uniform across the whole model but will look better with texture variation i.e numbers
        //gameObject.transform.position += Vector3.left * BalloonSpeed * Time.deltaTime; //move the balloon left at it's speed * time.delta time (left happens to be down towards the player here)
    }

    public void OnDestroy()
    {
        Instantiate(ps, transform.position, Quaternion.identity);
        AS.PlayOneShot(Pop_SFX); //when the ballon is destroyed, play the pop sound effect

    }





}
