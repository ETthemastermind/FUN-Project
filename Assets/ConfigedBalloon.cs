using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigedBalloon : MonoBehaviour
{
    //REMEMBER TO CODE IN THE BALLOON LEFTOVERS PARTICLE SYSTEM
    public float BalloonRotSpeed = 10f; //value for the rotation speed of the balloon
    public int BalloonValue;
    public AudioSource AS; //reference to the audio source on the camera
    public AudioClip Pop_SFX; //pop sound effect for the balloon
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Rotate(-90f, 0f, 0f); //damn balloon wont spawn in the same orientation that dragging it into the scene shows
        AS = GameObject.Find("BalloonPop").GetComponent<AudioSource>(); //gets the audiosource on the camera object
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 0, BalloonRotSpeed); //rotate the balloon around the Z axis, doesnt look as good because the texture is uniform across the whole model but will look better with texture variation i.e numbers
    }

    public void DestroyBalloon()
    {
        //ParticleSystem BalloonLeftovers = Instantiate(ps, transform.position, Quaternion.identity); //spawn the particle effects of the destroyed balloon
        //Material mat = BalloonLeftovers.GetComponent<ParticleSystemRenderer>().material;
        //mat.SetTexture("_MainTex", PoppedBalloonColors[RandomColor]);
        AS.PlayOneShot(Pop_SFX); //when the ballon is destroyed, play the pop sound effect
        Destroy(gameObject);
    }
}
