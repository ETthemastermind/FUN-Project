using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool HiddenCell;
    public List<GameObject> ComponentsUnderCell;

    public void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            ComponentsUnderCell.Add(transform.GetChild(i).gameObject);
        }
    }

}
