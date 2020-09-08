using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public bool CellsActive = false;

    public GameObject[] CellsInScene;
    public GameObject ChosenCell;
    // Start is called before the first frame update
    void Start()
    {
        CellsInScene = GameObject.FindGameObjectsWithTag("Cell");
        for (int i = 0; i < CellsInScene.Length; i++)
        {
            CellsInScene[i].GetComponent<Button>().interactable = false;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ChosenCell != null)
        {
            ChosenCell.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }


        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            ScaleCellUp();
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            ScaleCellDown();
        }
    }

    public void ActivateCells()
    {
        if (CellsActive == false)
        {
            CellsActive = true;
            Debug.Log("Cell moving mode enabled");
            for (int i = 0; i < CellsInScene.Length; i++)
            {
                CellsInScene[i].GetComponent<Button>().interactable = true;
                for (int j = 0; j < CellsInScene[i].transform.childCount; j++)
                {
                    //Debug.Log(CellsInScene[i].transform.GetChild(j).name);
                    CellsInScene[i].transform.GetChild(j).GetComponent<Button>().enabled = false;
                }

            }

        }

        else
        {
            CellsActive = false;
            Debug.Log("Cell moving mode disabled");
            for (int i = 0; i < CellsInScene.Length; i++)
            {
                CellsInScene[i].GetComponent<Button>().interactable = false;
                for (int j = 0; j < CellsInScene[i].transform.childCount; j++)
                {
                    //Debug.Log(CellsInScene[i].transform.GetChild(j).name);
                    CellsInScene[i].transform.GetChild(j).GetComponent<Button>().enabled = true;
                }
            }

            
        }
    }

    

    public void ActivateCell(GameObject Cell)
    {
        if (ChosenCell == null)
        {
            Debug.Log("Pick up cell");
            ChosenCell = Cell;
        }

        else
        {
            Debug.Log("Put down cell");
            ChosenCell = null;
        }
        /*
        Debug.Log(Cell.name + "Cell Activated");
        ChosenCell = Cell;
        //Button.GetComponent<RuntimeButtonMove>().enabled = true;
        */
    }

    public void ScaleCellUp()
    {
        if (ChosenCell != null)
        {
            float NewY = ChosenCell.transform.localScale.y + 0.1f;
            float NewX = ChosenCell.transform.localScale.x + 0.1f;
            ChosenCell.transform.localScale = new Vector3(NewX, NewY, transform.localScale.z);
        }
        
    }

    public void ScaleCellDown()
    {
        float NewY = ChosenCell.transform.localScale.y - 0.1f;
        float NewX = ChosenCell.transform.localScale.x - 0.1f;
        ChosenCell.transform.localScale = new Vector3(NewX, NewY, transform.localScale.z);

    }
}
