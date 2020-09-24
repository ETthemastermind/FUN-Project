using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandThenView : MonoBehaviour
{
    public GameObject[] Canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Command()
    {
        for (int i = 0; i < Canvas.Length; i++)
        {
            Canvas[i].SetActive(true);
        }
    }

    public void View()
    {
        for (int i = 0; i < Canvas.Length; i++)
        {
            Canvas[i].SetActive(false);
        }
        
    }
}
