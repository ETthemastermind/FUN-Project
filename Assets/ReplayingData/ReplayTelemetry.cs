using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReplayTelemetry : MonoBehaviour
{
    public EyetrackingDataSave EDS;

    public GameObject LastPressedButton;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Data", 0.0f, 1f / 60f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Data()
    {
        EDS.MouseX = Input.mousePosition.x;
        EDS.MouseY = Input.mousePosition.y;
        if (LastPressedButton == EventSystem.current.currentSelectedGameObject)
        {
            EDS.ButtonPressed = null;
        }
        else
        {
            EDS.ButtonPressed = EventSystem.current.currentSelectedGameObject;
            LastPressedButton = EventSystem.current.currentSelectedGameObject;


        }
        
    }
}
