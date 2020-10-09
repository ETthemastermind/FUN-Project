using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool HiddenCell;
    public GameObject[] FunctionButtons;
    public GameObject PARENT_ComponentsUnderCell;
    public List<GameObject> ComponentsUnderCell;

    public void Start()
    {
        for (int i = 0; i < PARENT_ComponentsUnderCell.transform.childCount; i++)
        {
            ComponentsUnderCell.Add(PARENT_ComponentsUnderCell.transform.GetChild(i).gameObject);
        }
    }

    public void ScaleUp()
    {
        GameObject ParentObject = transform.parent.gameObject;
        float NewY = ParentObject.transform.localScale.y + 0.1f; //increase the size of the x and y variables
        float NewX = ParentObject.transform.localScale.x + 0.1f;
        ParentObject.transform.localScale = new Vector3(NewX, NewY, transform.localScale.z); //apply the new x and y variables
    }

    public void ScaleDown()
    {
        GameObject ParentObject = transform.parent.gameObject;
        float NewY = ParentObject.transform.localScale.y - 0.1f; //increase the size of the x and y variables
        float NewX = ParentObject.transform.localScale.x - 0.1f;
        ParentObject.transform.localScale = new Vector3(NewX, NewY, transform.localScale.z); //apply the new x and y variables
    }
    
    public void HideCell()
    {
        if (HiddenCell == true)
        {
            HiddenCell = false;

        }

        else if (HiddenCell == false)
        {
            HiddenCell = true;
        }
    }


     /*
                CellsInScene[i].GetComponent<Button>().interactable = true; //reactivate the interactble option
                CellsInScene[i].SetActive(true);
                for (int j = 0; j < CellsInScene[i].transform.childCount; j++) //for each child of the cell
                {
                    GameObject CurrentChild = CellsInScene[i].transform.GetChild(j).gameObject; //assign the current child to a reference for ease of use
                    if (CurrentChild.GetComponent<Button>() != null) //if the child has a button component
                    {
                        CellsInScene[i].transform.GetChild(j).GetComponent<Button>().enabled = false; //turn the button component off

                    }
                    
                }
                */
}
