﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProxmityToBalloon : MonoBehaviour
{
    public bool BalloonFound = false;
    Vector3[] SDirections = new Vector3[4] {(Vector3.forward * 1), (Vector3.back * 1), (Vector3.left * 1), (Vector3.right * 1) };
    Vector3[] DDirections = new Vector3[4] { (Vector3.forward + Vector3.right) * 1, (Vector3.forward + Vector3.left) * 1, (Vector3.back + Vector3.right) * 1, (Vector3.back + Vector3.left) * 1 };
    public GameObject PrepareToBang;
    public AudioClip PrepareToBang_Audio;
    public AudioSource AS;
    public float SDistance = 1.5f;
    public float DDistance = 3f;
    public LayerMask layerMask;
    public BallControllerV2 BC;
    public Activity1Settings ActSet;
    public bool test;
    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {

        //BalloonProxCheck();
        
        
    }

    /*
    public void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.localPosition + SDirections[0], 0.1f);
        Gizmos.DrawWireSphere(transform.localPosition + SDirections[1], 0.1f);
        Gizmos.DrawWireSphere(transform.localPosition + SDirections[2], 0.1f);
        Gizmos.DrawWireSphere(transform.localPosition + SDirections[3], 0.1f);

        Gizmos.DrawWireSphere(transform.localPosition + DDirections[0], 0.1f);
        Gizmos.DrawWireSphere(transform.localPosition + DDirections[1], 0.1f);
        Gizmos.DrawWireSphere(transform.localPosition + DDirections[2], 0.1f);
        Gizmos.DrawWireSphere(transform.localPosition + DDirections[3], 0.1f);

        
    }
    */
    
    

    public void BalloonProxCheck()
    {
        
        Debug.Log("running the balloon prox check");
        BalloonFound = false;
        PrepareToBang.SetActive(false);
        #region old code
        /*
        if (ActSet.DiagonalControlsActive == false) //if the diagonal controls arent active
        {
            Directions = new Vector3[4];
            Directions = SDirections;
            Distance = 1.5f;
        }
        else // if the diagonal controls are active
        {
            Directions = new Vector3[8];
            Directions = SDirections.Concat(DDirections).ToArray();
            Distance = 3f;
        }
        */
#endregion
        for (int i = 0; i < SDirections.Length; i++)
        {
            //Debug.Log(Directions[i]);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, SDirections[i], out hit, SDistance, layerMask))
            {
                Debug.Log("Balloon in Proximity");
                BalloonFound = true;
                PrepareToBang.SetActive(true); //activate the prepare to bang graphic
                if (AS.isPlaying == false) //if the audiosource is not playing
                {
                    AS.PlayOneShot(PrepareToBang_Audio); //play the prepare to bang audio
                }
                #region old code
                /*
                Debug.Log("boop" + hit.transform.name);
                if (hit.transform.tag == "Balloon")
                {
                    Debug.Log("Balloon in Proximity");
                    BalloonFound = true;
                    PrepareToBang.SetActive(true); //activate the prepare to bang graphic
                    if (AS.isPlaying == false) //if the audiosource is not playing
                    {
                        AS.PlayOneShot(PrepareToBang_Audio); //play the prepare to bang audio
                    }
                }
                else
                {
                    Debug.Log("No balloon in proximity - 4 Directional");
                }
                */
                #endregion
            }
            #region disabled diag
            /*
            if (ActSet.DiagonalControlsActive == true)
            {
                if (Physics.Raycast(transform.position, DDirections[i], out hit, DDistance, layerMask))
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
                    }
                    else
                    {
                        Debug.Log("No balloon in proximity - 8 Directional");
                    }
                }
            }
            */
            #endregion

        }

        







    }




}