using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandThenView : MonoBehaviour
{
    public BallController bc;
    public bool CommandThenView_Active = false;
    public GameObject ControlCanvas;
    // Start is called before the first frame update
    void Start()
    {
        bc = gameObject.GetComponent<BallController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bc._BallIsMoving == true && CommandThenView_Active == true)
        {
            ControlCanvas.SetActive(false);
        }
        else
        {
            ControlCanvas.SetActive(true);
        }
    }
}
