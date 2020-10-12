using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public bool MovingCell;
    public bool HiddenCell;
    public GameObject FunctionButtons;
    public GameObject UIButtons;
    public List<GameObject> ComponentsUnderCell;

    public void Start()
    {
        for (int i = 0; i < UIButtons.transform.childCount; i++)
        {
            ComponentsUnderCell.Add(UIButtons.transform.GetChild(i).gameObject);
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
            ColorBlock Colours = gameObject.GetComponent<Button>().colors;
            Colours.normalColor = new Color(255f, Colours.normalColor.g, Colours.normalColor.b, Colours.normalColor.a);
            gameObject.GetComponent<Button>().colors = Colours;

        }

        else if (HiddenCell == false)
        {
            HiddenCell = true;
            ColorBlock Colours = gameObject.GetComponent<Button>().colors;
            Colours.normalColor = new Color(0f, Colours.normalColor.g, Colours.normalColor.b, Colours.normalColor.a);
            gameObject.GetComponent<Button>().colors = Colours;
        }
    }
    /*
    public void HideCell() //https://answers.unity.com/questions/1401626/how-to-change-button-color-highlited-color-etc.html
    {
        if (ChosenCell.GetComponent<Cell>().HiddenCell == true) //if the cell is hidden
        {
            Debug.Log("Cell Hidden");
            ChosenCell.GetComponent<Cell>().HiddenCell = false; //turn the hidden cell bool off
            ColorBlock Colours = ChosenCell.GetComponent<Button>().colors;
            Colours.normalColor = new Color(255f, Colours.normalColor.g, Colours.normalColor.b, Colours.normalColor.a);
            ChosenCell.GetComponent<Button>().colors = Colours;


        }
        else //therefore, if the cell is not hidden
        {
            ChosenCell.GetComponent<Cell>().HiddenCell = true; //turn the hidden cell bool on
            Debug.Log("Cell Unhidden");
            ColorBlock Colours = ChosenCell.GetComponent<Button>().colors;
            Colours.normalColor = new Color(0f, Colours.normalColor.g, Colours.normalColor.b, Colours.normalColor.a);
            ChosenCell.GetComponent<Button>().colors = Colours;
        }
    }
    */

}
