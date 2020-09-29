using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryParticleSystem : MonoBehaviour
{
    private ParticleSystem ps; //https://answers.unity.com/questions/219609/auto-destroying-particle-system.html
    // Start is called before the first frame update
    void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.IsAlive() == false)
        {
            Destroy(gameObject);
        }
    }
}
