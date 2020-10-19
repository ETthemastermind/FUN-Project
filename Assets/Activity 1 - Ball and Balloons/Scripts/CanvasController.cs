using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject[] CanvasList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ActivateCanvas()
    {
        for (int i = 0; i < CanvasList.Length; i++)
        {
            CanvasList[i].SetActive(true);
        }
    }

    public void DeactivateCanvas()
    {
        for (int i = 0; i < CanvasList.Length; i++)
        {
            CanvasList[i].SetActive(false);
        }
    }
}
