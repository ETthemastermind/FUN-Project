using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxmityToBalloon : MonoBehaviour
{
    public bool BalloonFound = false;
    Vector3[] Directions = new Vector3[4] {(Vector3.forward * 1), (Vector3.back * 1), (Vector3.left * 1), (Vector3.right * 1) };
    public GameObject PrepareToBang;
    public AudioClip PrepareToBang_Audio;
    private AudioSource AS;
    public float Distance;
    public LayerMask layerMask;
    public BallControllerV2 BC;
    // Start is called before the first frame update
    void Start()
    {
        AS = Camera.main.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {

        //BalloonProxCheck();
        
        
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.localPosition + (Vector3.forward * Distance), 0.1f);
        Gizmos.DrawWireSphere(transform.localPosition + (Vector3.back * Distance), 0.1f);
        Gizmos.DrawWireSphere(transform.localPosition + (Vector3.left * Distance), 0.1f);
        Gizmos.DrawWireSphere(transform.localPosition + (Vector3.right * Distance), 0.1f);
    }

    public void BalloonProxCheck()
    {
        
        Debug.Log("running the balloon prox check");
        BalloonFound = false;
        PrepareToBang.SetActive(false);
        for (int i = 0; i < Directions.Length; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Directions[i], out hit, Distance, layerMask))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.tag == "Balloon")
                {
                    Debug.Log("Balloon in Proximity");
                    BalloonFound = true;
                    PrepareToBang.SetActive(true); //activate the prepare to bang graphic
                    if (AS.isPlaying == false) //if the audiosource is not playing
                    {
                        AS.PlayOneShot(PrepareToBang_Audio); //play the prepare to bang audio
                    }
                    
                    
                    //Destroy(hit.transform.gameObject);
                }
                else
                {
                    Debug.Log("No balloon in proximity");
                    
                }
            }

        }
        






    }




}