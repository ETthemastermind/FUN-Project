using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxmityToBalloon : MonoBehaviour
{
    public bool BalloonFound = false;
    Vector3[] Directions = new Vector3[4] {(Vector3.forward * 1), (Vector3.back * 1), (Vector3.left * 1), (Vector3.right * 1) };
    public GameObject PrepareToBang;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //BalloonProxCheck();
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.localPosition + (Vector3.forward * 1), 0.1f);
        Gizmos.DrawWireSphere(transform.localPosition + (Vector3.back * 1), 0.1f);
        Gizmos.DrawWireSphere(transform.localPosition + (Vector3.left * 1), 0.1f);
        Gizmos.DrawWireSphere(transform.localPosition + (Vector3.right * 1), 0.1f);
    }

    public void BalloonProxCheck()
    {
        Debug.Log("running the balloon prox check");
        BalloonFound = false;
        PrepareToBang.SetActive(false);
        for (int i = 0; i < Directions.Length; i++)
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, 0.1f, Directions[i], out hit, 1f))
            {
                if (hit.transform.tag == "Balloon")
                {
                    Debug.Log("Balloon in Proximity");
                    BalloonFound = true;
                    PrepareToBang.SetActive(true);
                    
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